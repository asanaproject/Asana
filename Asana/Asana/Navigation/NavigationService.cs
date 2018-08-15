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
   public class NavigationService : INavigationService
    {
        private Dictionary<ViewType, ViewModelBase> pages = new Dictionary<ViewType, ViewModelBase>();
        private Stack<ViewType> history = new Stack<ViewType>();

        public ViewModelBase Current { get; set; }

        public void NavigateTo(ViewType name)
        {
            try
            {
                Current = pages[name];
                history.Push(name);
                Messenger.Default.Send(Current);
            }
            catch (Exception)
            {
                throw new Exception("Page not found!");
            }
        }

        public void GoBack()
        {
            if (history.Count > 0)
                Current = pages[history.Pop()];
        }

        public void ClearHistory()
        {
            history.Clear();
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
