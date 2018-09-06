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
using System.Windows;
using System.Timers;
namespace Asana.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {

        #region Props


        private readonly ChannelsService ChannelsService;
        private readonly ChatService ChatService;
        private readonly Timer inboxtimer;
        private readonly Timer chattimer;
        private int selectedColumn;

        public int SelectedColumn
        {
            get { return selectedColumn; }
            set { Set(ref selectedColumn, value); }
        }


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

        private ObservableCollection<dynamic> inbox;

        public ObservableCollection<dynamic> Inbox
        {
            get { return inbox; }
            set { inbox = value; Set(ref inbox, value); }
        }


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
            set
            {
                Set(ref selectedItem, value);
                SelectedColumn = 3;
                chattimer.Start();
            }
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
            if (SelectedItem != null)
                ChatService.SendMessagesChannel(SelectedItem.ID, Message_Text);

        }));


        private RelayCommand _inboxCommand;

        public RelayCommand InboxCommand => _inboxCommand ?? (_inboxCommand = new RelayCommand(
        () =>
        {
            SelectedItem = null;
            if (ChatService.GetAllMails() != null)
                SelectedColumn = 1;
            else
                SelectedColumn = 0;
            inboxtimer.Start();
        }));

        private RelayCommand _closedCommand;

        public RelayCommand ClosedCommand
        {
            get => _closedCommand ?? (_closedCommand = new RelayCommand((() => { inboxtimer.Stop(); chattimer.Stop(); })));
        }



        private RelayCommand _starredCommand;

        public RelayCommand StarredCommand => _starredCommand ?? (_starredCommand = new RelayCommand(
        () =>
        {
            SelectedItem = null;
            SelectedColumn = 2;
            inboxtimer.Stop(); chattimer.Stop();
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
            if (ChatService.GetAllMails() != null)
                SelectedColumn = 1;
            else
                SelectedColumn = 0;
            inboxtimer.Start();
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


        #endregion

        private object header;

        public object Header
        {
            get { return header; }
            set { header = value; Set(ref header, value); }
        }
        private void ChatItemsRefresh(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ChatItems.Clear();
                if(SelectedItem != null)
                ChatService.GetSelectedChannelMessages(SelectedItem.ID).ToList().ForEach((x) =>
                {
                    ChatItems.Add(new
                    {
                        x.ID,
                        x.UserId,
                        x.ChatRoomId,
                        x.Body,
                        x.SendTime
                    });
                });
            });
        }

        private void InboxItemsRefresh(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Inbox.Clear();
                ChatService.GetAllMails().ToList().ForEach((x) =>
                {
                    Inbox.Add(new
                    {
                        x.ID,
                        x.UserId,
                        x.SenderEmail,
                        Body = x.Title + " - " + x.Body,
                        SendTime = x.SendTime.ToShortDateString()
                    });
                });
            });
        }


        private readonly NavigationService navigationService;

        public ChatViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            PublicChannels = new ObservableCollection<ChatRoom>();
            PrivateChannels = new ObservableCollection<ChatRoom>();
            DirectMessages = new ObservableCollection<ChatRoom>();
            Inbox = new ObservableCollection<dynamic>();
            ChatItems = new ObservableCollection<dynamic>();
            ChannelsService = new ChannelsService();
            ChatService = new ChatService();
            Header = new HeaderViewModel(navigationService);
            inboxtimer = new Timer(500);
            chattimer = new Timer(500);
            inboxtimer.Elapsed += InboxItemsRefresh;
            chattimer.Elapsed += ChatItemsRefresh;
            
        }


    }
}
