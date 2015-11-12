using DropNet;
using HigLabo.Net;
using HigLabo.Net.Dropbox;
using System.Web;

namespace OnlineShop.Models.ImageManager
{
    public static class ImageManager
    {
        //todo this is not security!!!
        private const string App_key = "b3ihb5o2yp61qeo";
        private const string App_secret = "jg38tvwfxi2n9x4";      

        private static string Token = null;
        private static string TokenSecret = null;
        private static DropNetClient _client = null;

        static ImageManager()
        {                       
            _client = new DropNetClient(App_key, App_secret);
            // Sync
            var userLogin = _client.GetToken();

            Token = userLogin.Token;
            TokenSecret = userLogin.Secret;
        }

        public static void DownloadFile(string path)
        {
           
        }

        public static void UploadFile(byte[] content, string filename, string path)
        {
            // Sync
            var uploaded = _client.UploadFile(path, filename, content);                      
        }
    }
}