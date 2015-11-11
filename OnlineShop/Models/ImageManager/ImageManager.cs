using System;
using System.Collections.Generic;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Requests;
using System.Net;
using System.IO;
using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;

namespace OnlineShop.Models.ImageManager
{
    public static class ImageManager
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static DriveService Service;

        static ImageManager()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            Service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Res.ApplicationName,
            });
        }
        /// <summary>
        /// Insert new file.
        /// </summary>
        /// <param name="service">Drive API service instance.</param>
        /// <param name="title">Title of the file to insert, including the extension.</param>
        /// <param name="description">Description of the file to insert.</param>
        /// <param name="parentId">Parent folder's ID.</param>
        /// <param name="mimeType">MIME type of the file to insert.</param>
        /// <param name="filename">Filename of the file to insert.</param><br>  
        /// <returns>Inserted file meta-data, null is returned if an API error occurred.</returns>
        private static Google.Apis.Drive.v2.Data.File insertFile(String title, String description, String parentId, String mimeType, String filename)
        {
            // File's meta-data.
            Google.Apis.Drive.v2.Data.File body = new Google.Apis.Drive.v2.Data.File();
            body.Title = title;
            body.Description = description;
            body.MimeType = mimeType;

            // Set the parent folder.
            if (!String.IsNullOrEmpty(parentId))
            {
                body.Parents = new List<ParentReference>()
                {
                    new ParentReference()
                    {
                        Id = parentId
                    }
                };
            }

            // File's content.
            byte[] byteArray = System.IO.File.ReadAllBytes(filename);
            MemoryStream stream = new MemoryStream(byteArray);
            try
            {
                FilesResource.InsertMediaUpload request = Service.Files.Insert(body, stream, mimeType);
                request.Upload();

                Google.Apis.Drive.v2.Data.File file = request.ResponseBody;

                // Uncomment the following line to print the File ID.
                 Debug.WriteLine("File ID: " + file.Id);

                return file;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Download a file and return a string with its content.
        /// </summary>
        /// <param name="authenticator">
        /// Authenticator responsible for creating authorized web requests.
        /// </param>
        /// <param name="service">Drive API service instance.</param>
        /// <param name="fileId">File Id of the file to Download.</param>
        /// <returns>File's content if successful, null otherwise.</returns>
        public static byte[] DownloadFile(string fileId)
        {
            var file = Service.Files.Get(fileId).Execute();
            return Service.HttpClient.GetByteArrayAsync(file.DownloadUrl).Result;
        }
    }
}