using DropNet;
using DropNet.Models;
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
        private const string Token = "uz3ar5zpt2vnnx8x";
        private const string TokenSecret = "n1rdu5bv3fsz8i6";
        private const string DefPath = "/OnlineShopGearHost/";

        private static DropNetClient _client = null; 

        static ImageManager()
        {                       
            _client = new DropNetClient(App_key, App_secret,Token,TokenSecret);
            _client.UseSandbox = true;
        }

        public static byte[] DownloadFile(string path)
        {
            return _client.GetFile(DefPath+path);
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