using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Asana.Tools
{
    public class ProfilePhoto
    {
        public static string LoadImage()
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image jpeg(*.jpg)|*.jpg|Image png(*.png)|*.png";
            img.DefaultExt = ".jpeg";
            var result = img.ShowDialog();


            // Process open file dialog box results 
            if (result != DialogResult.Cancel)
            {
                // Open document 

                return img.FileName;

            }
            return String.Empty;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            if (ImageFormat.Jpeg.Equals(imageIn.RawFormat))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (ImageFormat.Png.Equals(imageIn.RawFormat))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            }
            else if (ImageFormat.Bmp.Equals(imageIn.RawFormat))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            }
            return ms.ToArray();
        }

        public static BitmapImage ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new System.IO.MemoryStream(byteArrayIn))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
