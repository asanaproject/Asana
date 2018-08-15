using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    
        [Serializable]
        public class NotifyClass : INotifyPropertyChanged
        {
            [NonSerialized]
            private PropertyChangedEventHandler _PropertyChanged;

            public virtual event PropertyChangedEventHandler PropertyChanged
            {
                add { _PropertyChanged += value; }
                remove { _PropertyChanged -= value; }
            }

        protected void OnPropertyChanged([CallerMemberName]string prop = "") => _PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(prop));
    }
}
