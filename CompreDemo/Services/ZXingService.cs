using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace Services
{
    public class ZXingService
    {
        public static void GetCode(string text, string filePath, string fileName, BarcodeFormat format, EncodingOptions options)
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            //BarcodeWriter writer = new BarcodeWriter
            //{
            //    Format = format,
            //    Options = options
            //};
            //Bitmap map = writer.Write(text);
            //string path = Path.Combine(filePath, fileName);
            //map.Save(path, ImageFormat.Png);
            //map.Dispose();
        }

        public static void GetQrCode(string text, string filePath, string fileName, int width = 500, int heigth = 500, int margin = 1)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = heigth,
                Margin = margin
            };
            GetCode(text, filePath, fileName, BarcodeFormat.QR_CODE, options);
        }

        public static void GetBarCode(string text, string filePath, string fileName, int width = 150, int heigth = 50, int margin = 2)
        {
            EncodingOptions options = new EncodingOptions
            {
                Width = width,
                Height = heigth,
                Margin = margin
            };
            GetCode(text, filePath, fileName, BarcodeFormat.CODE_128, options);
        }

        //public static string Read(Bitmap map)
        //{
        //    BarcodeReader reader = new BarcodeReader();
        //    reader.Options.CharacterSet = "UTF-8";
        //    Result result = reader.Decode(map);
        //    return result == null ? "" : result.Text;
        //}
    }


}
