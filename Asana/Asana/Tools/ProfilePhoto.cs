using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Asana.Tools
{
    public class ProfilePhoto
    {
        public static string LoadImage()
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image png(*.png)|*.png";
            img.DefaultExt = ".png";
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
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }


        }


        public static Bitmap BitmapImageToBitmap(BitmapImage image)
        {
            if (image == null)
            {
                return null;
            }
            Bitmap bitmap;
            using (var ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(ms);
                bitmap = new Bitmap(ms);
            }
            return bitmap;
        }

        public static BitmapImage ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn.Length == 0 || byteArrayIn == null)
            {
                return null;
            }
            var btm = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {

                btm.BeginInit();
                btm.StreamSource = ms;
                // Below code for caching is crucial.
                btm.CacheOption = BitmapCacheOption.OnLoad;
                btm.EndInit();
                btm.Freeze();
            }
            return btm;
        }


    }
}
