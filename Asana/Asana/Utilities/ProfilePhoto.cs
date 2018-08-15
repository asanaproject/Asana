using System;
using System.Collections.Generic;
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
    }
}
