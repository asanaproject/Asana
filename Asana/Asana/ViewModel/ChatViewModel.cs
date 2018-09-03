using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Asana.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        private int selectedId;

        private readonly ChannelsService ChannelsService;
        private ObservableCollection<string> privateChannels;

        public ObservableCollection<string> PrivateChannels
        {
            get { return privateChannels; }
            set { privateChannels = value; Set(ref privateChannels, value); }
        }

        private ObservableCollection<string> directMessages;

        public ObservableCollection<string> DirectMessages
        {
            get { return directMessages; }
            set { directMessages = value; Set(ref directMessages, value); }
        }

        private ObservableCollection<string> publicChannels;

        public ObservableCollection<string> PublicChannels
        {
            get { return publicChannels; }
            set { publicChannels = value; Set(ref publicChannels, value); }
        }

        private ObservableCollection<MessageItem> chatItems;

        public ObservableCollection<MessageItem> ChatItems
        {
            get { return chatItems; }
            set { chatItems = value; Set(ref chatItems, value); }
        }

        private RelayCommand _addChatRoomChannels;

        public RelayCommand AddChatRoomChannels => _addChatRoomChannels ?? (_addChatRoomChannels = new RelayCommand(
        x =>
        {
            WindowBluringCustom.Bluring();
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Name");
            chatRoomAdd.ShowDialog();
            WindowBluringCustom.Normal();
            string channelname = chatRoomAdd.GetName();
            ChannelsService.InsertRoom(channelname);
            publicChannels.Add(channelname);
        }));

        private RelayCommand _addChatRoomPrivate;

        public RelayCommand AddChatRoomPrivate => _addChatRoomPrivate ?? (_addChatRoomPrivate = new RelayCommand(
        x =>
        {
            WindowBluringCustom.Bluring();
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Name");
            chatRoomAdd.ShowDialog();
            WindowBluringCustom.Normal();
            string channelname = chatRoomAdd.GetName();
            ChannelsService.InsertRoom(channelname);
            PrivateChannels.Add(channelname);
        }));

        //private Timer timer;

        //private void OnCallBack()
        //{
        //    using (var db = new AsanaDbContext()) 
        //    {
        //        db.Messages.Select(x => x.ChatRoomId == selectedId);
        //    }
        //}

        private RelayCommand _channelListCommand;
        public RelayCommand ChannelListCommand
        {
            get => _channelListCommand ?? (_channelListCommand = new RelayCommand(
                (x => navigationService.NavigateTo(ViewType.ListChannels)
                )));
        }



        private readonly NavigationService navigationService;

        public ChatViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            PublicChannels = new ObservableCollection<string>();
            PrivateChannels = new ObservableCollection<string>();
            DirectMessages = new ObservableCollection<string>();
            ChatItems = new ObservableCollection<MessageItem>();
            ChannelsService = new ChannelsService();
            //PrivateChannels = ChannelsService.GetListPublicChannelsId();
            //PublicChannels = ChannelsService.GetListPrivateChannelsId();
            //System.Threading.Tasks.Task.Run(() =>
            //{
            //    timer = new Timer(_ => OnCallBack(), null, 1000 * 10, Timeout.Infinite);
            //});
        }
    }
}
