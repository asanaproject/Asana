using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (result!=DialogResult.Cancel)
            {
                // Open document 

                return img.FileName;

            }
            return String.Empty;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
