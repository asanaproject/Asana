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
using System.Windows;

namespace Asana.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {

        #region Props
     

        private readonly ChannelsService ChannelsService;
        private readonly ChatService ChatService;
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

        //private ObservableCollection<dynamic> inbox;

        //public ObservableCollection<dynamic> Inbox
        //{
        //    get { return inbox; }
        //    set { inbox = value; Set(ref inbox, value); }
        //}


        private ObservableCollection<dynamic> chatItems;

        public ObservableCollection<dynamic> ChatItems
        {
            get { return chatItems; }
            set { chatItems = value; Set(ref chatItems, value); }
        }


        private ChatRoom selectedItem;

        public ChatRoom SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; Set(ref selectedItem, value); }
        }

        private string message_Text;

        public string Message_Text
        {
            get { return message_Text; }
            set { message_Text = value; Set(ref message_Text, value); }
        }

        #endregion

        #region Commands
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
            ChatRoomDatas();
        }));


        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(
        () =>
        {
            if(SelectedItem !=  null)
                ChatService.SendMessagesChannel(SelectedItem.ID, Message_Text);
            GetMessages();
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
            ChatRoomDatas();
        }));

        private RelayCommand _channelListCommand;
        public RelayCommand ChannelListCommand
        {
            get => _channelListCommand ?? (_channelListCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.ListChannels)
                )));
        }


        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new RelayCommand((() => LoadedDatas())));
        }

        private void LoadedDatas()
        {
            ChatRoomDatas();
         
        }

        private void ChatRoomDatas()
        {
            PublicChannels.Clear();
            PrivateChannels.Clear();
            DirectMessages.Clear();
            ChannelsService.GetListPublicChannelsId().ToList().ForEach(x => PublicChannels.Add(x));
            ChannelsService.GetListPrivateChannelsId().ToList().ForEach(x => PrivateChannels.Add(x));
            //ChannelsService.GetListPrivateChannelsId().ToList().ForEach(x => PrivateChannels.Add(x));
        }

        private void GetMessages()
        {
            ChatItems.Clear();
            ChatService.GetSelectedChannelMessages(SelectedItem.ID).ToList().ForEach(x=>ChatItems.Add(x));
        }
        #endregion

   

        private readonly NavigationService navigationService;

        public ChatViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            PublicChannels = new ObservableCollection<ChatRoom>();
            PrivateChannels = new ObservableCollection<ChatRoom>();
            DirectMessages = new ObservableCollection<ChatRoom>();
            ChatItems = new ObservableCollection<dynamic>();
            ChannelsService = new ChannelsService();
            ChatService = new ChatService();
        }
    }
}
