using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class ExtraWindowViewModel : ViewModelBase
    {
        private ViewModelBase contentWindown;

        public ViewModelBase ContentWindow
        {
            get { return contentWindown; }
            set { Set(ref contentWindown, value); }
        }

        private double width;

        public double Width
        {
            get { return width; }
            set { Set(ref width, value); }
        }

        private double height;

        public double Height
        {
            get { return height; }
            set { Set(ref height, value); }
        }


        public ExtraWindowViewModel(ViewModelBase view)
        {
            ContentWindow = view;
        }

    }
}
