using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using NDesk.Options;
using OnlineShop.Models;
namespace OnlineShop.App_Start
{
	public static class BackUpDb
	{
		private static string _defaultDownloadsPath;

		private static string _apiKey = "80ac675758564dd7b31677c6a83b64a7";
		private static string _downloadsPath;
		private static string _dbName = "onlineShop";
		private static bool _showHelp = false;
		private static DatabaseDTO _db = new DatabaseDTO();

		private static long ToUnixTimestamp(this DateTime date)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			TimeSpan diff = date - origin;
			return (long)Math.Floor(diff.TotalSeconds);
		}

		private static void Init()
		{
			_defaultDownloadsPath = "C:\\Users\\John\\BackUp";
			//_defaultDownloadsPath =  Directory.GetCurrentDirectory()+"\\"+ Res.BackUpPath;
			if (!Directory.Exists(_defaultDownloadsPath))
				Directory.CreateDirectory(_defaultDownloadsPath);
			_downloadsPath = _defaultDownloadsPath;
		}

		private static bool ParseInput()
		{
			var p = new OptionSet()
			{
				
			};
			p.Add("apiKey", _apiKey, v => _apiKey = v);
			p.Add("db=", _dbName, v => _dbName = v);
			p.Add("path=", _downloadsPath, v => _downloadsPath = v);
			p.Add("help", _showHelp.ToString(), v => _showHelp = v != null);
				//{"apiKey=", v => _apiKey},
				//{"db=",  v => _dbName = v},
				//{"path=",  v => _downloadsPath = v},
				//{"help", v => _showHelp = v != null},
			try
			{
				//p.Parse();

				_downloadsPath = _downloadsPath ?? _defaultDownloadsPath;

				if (!_showHelp)
				{
					if (string.IsNullOrEmpty(_apiKey)) throw new Exception("-apiKey option is missing");
					if (string.IsNullOrEmpty(_dbName)) throw new Exception("-db option is missing");
				}
			}
			catch (Exception e)
			{
				Console.Write("ghbackup: ");
				Console.WriteLine(e.Message);
				Console.WriteLine("Try `ghbackup --help' for more information.");
				return false;
			}
			return true;
		}

		private static bool ExecBackup()
		{
			using (var webClient = new WebClient())
			{
				webClient.Headers.Add("Authorization", string.Format("bearer {0}", _apiKey));
				DatabasesDTO dto = null;
				try
				{
					var databasesJson = webClient.DownloadString("https://api.gearhost.com/v1/databases");
					dto = new JavaScriptSerializer().Deserialize<DatabasesDTO>(databasesJson);
				}
				catch (Exception)
				{
					Console.WriteLine("Error downloading API data. Check your API key");
					return false;
				}

				_db = dto.databases.FirstOrDefault(d =>
					string.Compare(d.name, _dbName, StringComparison.OrdinalIgnoreCase) == 0);

				if (_db == null)
				{
					Console.WriteLine("Database not found. Check your database name.");
					return false;
				}
				Console.WriteLine("Database found. Executing backup...");
				string localFileName = _dbName + "_" + DateTime.Now.ToUnixTimestamp() + ".zip";
				string localPath = Path.Combine(_downloadsPath, localFileName);

				var data = webClient.DownloadString(string.Format("https://api.gearhost.com/v1/databases/{0}/users", _db.id));
				webClient.DownloadFile(string.Format("https://api.gearhost.com/v1/databases/{0}/backup", _db.id), localPath);
			}

			return true;
		}

		public static void BackUp()
		{
			Init();

			if (!ParseInput())
				return;

			//if (showHelp)
			//{
			//	ShowHelp();
			//	return;
			//}

			if (!ExecBackup())
			{
				Console.WriteLine("Aborting...");
			}
			//else
			//{
			//	Console.WriteLine("Backup successfully downloaded.");
			//	Task delData = DeleteDb();
			//	Task.WaitAll(delData);

			//	Task createData = CreateDb();
			//	Task.WaitAll(createData);
			//}
		}

		//private static void ShowHelp()
		//{
		//	Console.WriteLine("usage example:");
		//	Console.WriteLine(@"ghbackup.exe -apiKey=123abc -dbName=products -path=C:\Backups");
		//	Console.WriteLine(
		//		"apiKey and dbName are required, path is optional. Default location is Downloads directory under the app folder.");
		//}

		//private async static Task<string> DeleteDb()
		//{
		//	if (_db.id == null)
		//	{
		//		return null;
		//	}
		//	var baseAddress = new Uri("https://api.gearhost.com/v1/");
		//	using (var httpClient = new HttpClient { BaseAddress = baseAddress })
		//	{
		//		httpClient.DefaultRequestHeaders.
		//			TryAddWithoutValidation("Authorization", string.Format("bearer {0}", apiKey));
		//		Console.WriteLine("Begin delete");

		//		using (var response = await httpClient.DeleteAsync(String.Format("databases/{0}", _db.id)))
		//		{
		//			Console.WriteLine("End delete");
		//			return await response.Content.ReadAsStringAsync();
		//		}
		//	}
		//}

		//private async static Task<string> CreateDb()
		//{
		//	var baseAddress = new Uri("https://api.gearhost.com/v1/");
		//	using (var httpClient = new HttpClient { BaseAddress = baseAddress })
		//	{
		//		httpClient.DefaultRequestHeaders.
		//			TryAddWithoutValidation("Authorization", string.Format("bearer {0}", apiKey));

		//		using (var content = new StringContent(
		//			"{  \"name\": \"hushtest2\",  \"plan\": \"free\",  \"type\": \"mssql\"}",
		//			System.Text.Encoding.Default, "application/json"))
		//		{
		//			using (var response = await httpClient.PostAsync("databases", content))
		//			{
		//				string responseData = await response.Content.ReadAsStringAsync();
		//				return responseData;
		//			}
		//		}
		//	}
		//}
	}
}