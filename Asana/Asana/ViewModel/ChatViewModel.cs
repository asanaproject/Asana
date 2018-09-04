using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private readonly AsanaDbContext asanaDbContext;
        private readonly ChannelsService ChannelsService;
        private ObservableCollection<ChatRoom> privateChannels;

        public ObservableCollection<ChatRoom> PrivateChannels
        {
            get { return privateChannels; }
            set { privateChannels = value; Set(ref privateChannels, value); }
        }

        private ObservableCollection<ChatRoom> directMessages;

        public ObservableCollection<ChatRoom> DirectMessages
        {
            get { return directMessages; }
            set { directMessages = value; Set(ref directMessages, value); }
        }

        private ObservableCollection<ChatRoom> publicChannels;

        public ObservableCollection<ChatRoom> PublicChannels
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
        () =>
        {
            WindowBluringCustom.Bluring();
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Name");
            chatRoomAdd.ShowDialog();
            WindowBluringCustom.Normal();
            string channelname = chatRoomAdd.GetName();
            ChannelsService.InsertRoom(channelname);
        }));

        private RelayCommand _addChatRoomPrivate;

        public RelayCommand AddChatRoomPrivate => _addChatRoomPrivate ?? (_addChatRoomPrivate = new RelayCommand(
        () =>
        {
            WindowBluringCustom.Bluring();
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Name");
            chatRoomAdd.ShowDialog();
            WindowBluringCustom.Normal();
            string channelname = chatRoomAdd.GetName();
            ChannelsService.InsertRoom(channelname);
        }));

        private RelayCommand _channelListCommand;
        public RelayCommand ChannelListCommand
        {
            get => _channelListCommand ?? (_channelListCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.ListChannels)
                )));
        }


        private Timer timer;

        private void OnCallBack()
        {
            //ObservableCollection<ChatRoom> chatroomNames = new ObservableCollection<ChatRoom>();
            //using (var db = new AsanaDbContext())
            //{
            //    foreach (var dd in db.ChatRoomUsers)
            //    {
            //        if (db.ChatRooms.Single(x => x.ID == dd.ChatRoomId && dd.UserId == CurrentUser.Instance.User.Id) != null)
            //            chatroomNames.Add(db.ChatRooms.Single(x => x.ID == dd.ChatRoomId && dd.UserId == CurrentUser.Instance.User.Id));
            //    }
            //}
            //PublicChannels = chatroomNames;
        }


        private readonly NavigationService navigationService;

        public ChatViewModel(NavigationService navigationService,AsanaDbContext asanaDbContext)
        {
            this.navigationService = navigationService;
            PublicChannels = new ObservableCollection<ChatRoom>();
            PrivateChannels = new ObservableCollection<ChatRoom>();
            DirectMessages = new ObservableCollection<ChatRoom>();
            ChatItems = new ObservableCollection<MessageItem>();
           
            ChannelsService = new ChannelsService(asanaDbContext);
            //PublicChannels = ChannelsService.GetListPublicChannelsId();
            //PrivateChannels = ChannelsService.GetListPublicChannelsId();
            //PublicChannels = ChannelsService.GetListPrivateChannelsId();
        }
    }
}
