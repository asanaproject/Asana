using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        public ListChannelsViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            chatRooms = new ObservableCollection<ChatRoom>();
            channelService = new ChannelsService();
        }

        private ObservableCollection<ChatRoom> chatRooms;

        public ObservableCollection<ChatRoom> ChatRooms
        {
            get { return chatRooms; }
            set { Set(ref chatRooms, value); }
        }

        private ChatRoom selectedItem;

        public ChatRoom SelectedItem
        {
            get { return selectedItem; }
            set { Set(ref selectedItem, value); }
        }

        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new RelayCommand((() => UpdateSource())));
        }


        private RelayCommand<ChatRoom> _joinCommand;

        public RelayCommand<ChatRoom> JoinCommand => _joinCommand ?? (_joinCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ChatRoom>(
        x =>
        {
            channelService.JoinRoom(x.ID);
            ChatRooms.Remove(x);
            MessageBox.Show("Your joined " + x.Name + "!", "Channel", MessageBoxButton.OK, MessageBoxImage.Information);
        }));

        public async void UpdateSource()
        {
            var result = await channelService.GetListAllPublicChannelsNotJoined();
            int changedChannels1 = result.Count - ChatRooms.Count;
            result.Skip(ChatRooms.Count).Take(changedChannels1).ToList().ForEach(x => ChatRooms.Add(x));

        }

    }
}
