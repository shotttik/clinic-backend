using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;

namespace clinic
{
    public class Utils
    {
        private readonly Random _random = new Random();

        public static void GenerateThumbnail(string thumbPath, string thumbNewPath, int thumbWidth = 165, int thumbHeight = 165)
        {

            String imageName = Path.GetFileName(thumbPath);
            int imageHeight = thumbHeight;
            int imageWidth = thumbWidth;

            Image fullSizeImg = Image.FromFile(thumbPath);
            Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Image thumbNailImage = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);
            thumbNailImage.Save(thumbNewPath, ImageFormat.Jpeg);
            thumbNailImage.Dispose();
            fullSizeImg.Dispose();


        }
        public static bool ThumbnailCallback()
        {
            return false;
        }
        
        public static string GenerateRandomString(int length=7)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // generate a random string of the given length
            string randomString = new string(Enumerable.Repeat(chars, length)
              .Select(s => s [random.Next(s.Length)]).ToArray());


            return randomString;
        }


    }
    public static class ConfigurationHelper
    {
        public static IConfiguration? config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }
    }
}
