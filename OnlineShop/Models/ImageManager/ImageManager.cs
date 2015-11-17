using System.Configuration;
using DropNet;

namespace OnlineShop.Models.ImageManager
{
	public static class ImageManager
	{
		//todo this is not security!!!
		private static readonly string App_key = ConfigurationManager.AppSettings["DropBoxApiKey"];
		private static readonly string App_secret = ConfigurationManager.AppSettings["DropBoxAppSecret"];
		private static readonly string Token = ConfigurationManager.AppSettings["DropBoxToken"];
		private static readonly string TokenSecret = ConfigurationManager.AppSettings["DropBoxTokenSecret"];  

		private static DropNetClient _client = null; 

		static ImageManager()
		{                       
			_client = new DropNetClient(App_key, App_secret,Token,TokenSecret);
			_client.UseSandbox = true;
		}

		public static byte[] DownloadFile(string path)
		{
			return _client.GetFile(path);
		}

		public static bool UploadFile(byte[] content, string filename, string path)
		{            
			var uploaded = _client.UploadFile(path, filename, content);
			if (uploaded != null && uploaded.Bytes != 0)
				return true;
			return false;             
		}
	}
}