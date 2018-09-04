using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ListChannelsViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        ChannelsService channelService;
        private readonly AsanaDbContext asanaDbContext;
        public ListChannelsViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            chatRooms = new ObservableCollection<ChatRoom>();
            asanaDbContext = new AsanaDbContext();
            channelService = new ChannelsService(asanaDbContext);
        }

        private ObservableCollection<ChatRoom> chatRooms;

        public ObservableCollection<ChatRoom> ChatRooms
        {
            get { return chatRooms; }
            set { chatRooms = value; Set(ref chatRooms, value); }
        }

        private ChatRoom selectedItem;

        public ChatRoom SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; Set(ref selectedItem, value); }
        }


        private GalaSoft.MvvmLight.CommandWpf.RelayCommand<ChatRoom> _joinCommand;

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand<ChatRoom> JoinCommand => _joinCommand ?? (_joinCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ChatRoom>(
        x =>
        {
            //using (var db = new AsanaDbContext())
            //{
            //    db.ChatRooms.Single(y => y.ID == x.ID).Users.Add(CurrentUser.Instance.User);
            //}
            channelService.JoinRoom(x.ID);
            ChatRooms.Remove(x);
            MessageBox.Show("Your joined " + x.Name + "!", "Channel", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        public void UpdateSource()
        {
            //using (var db = new AsanaDbContext())
            //{
            //    var tmp = db.ChatRooms.ToList().Where(x => x.Users.ToList().Exists(y => y == CurrentUser.Instance.User)).ToList();
            //    tmp.ForEach(x => chatRooms.Add(x));
            //}
        }
    }
}
