using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Navigation
{
  public  class NavigationService
    {
        private Dictionary<ViewType, ViewModelBase> pages = new Dictionary<ViewType, ViewModelBase>();
        public ViewModelBase Current { get; set; }

        public void NavigateTo(ViewType name)
        {
            try
            {
                Current = pages[name];
                Messenger.Default.Send(Current);
            }
            catch (Exception)
            {
                throw new Exception("Page not found!");
            }
        }

        public void NavigateTo<T>(ViewType name, T data)
        {
            try
            {
                Current = pages[name];
                if (data != null)
                    Messenger.Default.Send(data);
                Messenger.Default.Send(Current);
            }
            catch (Exception)
            {
                throw new Exception("Page not found!");
            }
        }

        public void AddPage(ViewModelBase page, ViewType name)
        {
            if (pages.ContainsKey(name))
                pages[name] = page;
            else
                pages.Add(name, page);
        }

        public void RemovePage(ViewType name)
        {
            try
            {
                pages.Remove(name);
            }
            catch (Exception)
            {
                throw new Exception("Page not found!");
            }
        }
    }
}
