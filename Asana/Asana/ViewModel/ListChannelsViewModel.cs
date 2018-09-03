using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class ListChannelsViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;

        public ListChannelsViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            chatRooms = new ObservableCollection<ChatRoom>();
        }

        private ObservableCollection<ChatRoom> chatRooms;

        public ObservableCollection<ChatRoom> ChatRooms
        {
            get { return chatRooms; }
            set { chatRooms = value; Set(ref chatRooms, value); }
        }
        
        public void UpdateSource()
        {
            using (var db = new AsanaDbContext())
            {
                var tmp = db.ChatRooms.ToList().Where(x => x.Users.ToList().Exists(y => y == CurrentUser.Instance.User)).ToList();
                tmp.ForEach(x => chatRooms.Add(x));
            }

        }
    }
}
