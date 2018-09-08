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
        //SelectedColumns
        //Inbox - 0
        //InboxList - 1
        //Starred - 2
        //Chat - 3
        //InboxView - 4
        //StarredList - 5

        private readonly ChannelsService ChannelsService;
        private readonly ChatService ChatService;

        private string columnTitle;
        public string ColumnTitle
        {
            get { return columnTitle; }
            set { Set(ref columnTitle, value); }
        }


        private int selectedColumn;

        public int SelectedColumn
        {
            get { return selectedColumn; }
            set {
                Set(ref selectedColumn, value);
                if(value == 0 || value == 1)
                {
                    ColumnTitle = "#Inbox";
                }
                else if(value == 2 || value == 5)
                {
                    ColumnTitle = "#Starred";
                }

                inboxtimer.Stop();
                starredTimer.Stop();
                chattimer.Stop();
            }
        }



        #region Channels
        private readonly Timer chattimer;
        private object chatSelectedItem;

        public object ChatSelectedItem
        {
            get { return chatSelectedItem; }
            set { Set(ref chatSelectedItem, value);  }
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
                if(value != null)
                    ColumnTitle = "#" + value.Name;
                ChatItems.Clear();
                chattimer.Start();
            }
        }


        private RelayCommand _addChatRoomChannels;

        public RelayCommand AddChatRoomChannels => _addChatRoomChannels ?? (_addChatRoomChannels = new RelayCommand(
        () =>
        {
            WindowBluringCustom.Bluring();
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Public Channel Name");
            chatRoomAdd.ShowDialog();
            WindowBluringCustom.Normal();
            string channelname = chatRoomAdd.GetName();
            ChannelsService.InsertRoom(channelname);

            ChatRoomDatas();
        }));


        private string message_Text;

        public string Message_Text
        {
            get { return message_Text; }
            set { message_Text = value; Set(ref message_Text, value); }
        }

        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(
        () =>
        {
            if (SelectedItem != null)
                ChatService.SendMessagesChannel(SelectedItem.ID, Message_Text);

        }));

        private RelayCommand _starredCommand;


        private RelayCommand _addChatRoomPrivate;

        public RelayCommand AddChatRoomPrivate => _addChatRoomPrivate ?? (_addChatRoomPrivate = new RelayCommand(
        () =>
        {
            WindowBluringCustom.Bluring();
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Private Channel Name");
            chatRoomAdd.ShowDialog();
            WindowBluringCustom.Normal();
            string channelname = chatRoomAdd.GetName();
            ChannelsService.InsertRoom(channelname,ChatRoomType.Private);
            ChatRoomDatas();
        }));

        private RelayCommand _channelListCommand;
        public RelayCommand ChannelListCommand
        {
            get => _channelListCommand ?? (_channelListCommand = new RelayCommand(
                (() => navigationService.NavigateTo(ViewType.ListChannels)
                )));
        }


        private void ChatItemsRefresh(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ChatItems.Clear();
                if (SelectedItem != null)
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


        #endregion

        #region Inbox
        private readonly Timer inboxtimer;

        private ObservableCollection<dynamic> inbox;

        public ObservableCollection<dynamic> Inbox
        {
            get { return inbox; }
            set { inbox = value; Set(ref inbox, value); }
        }


        private string urlForMail;

        public string UrlForMail
        {
            get { return urlForMail; }
            set { Set(ref urlForMail, value); }
        }


        private object selectedItemInbox;

        public void SelectedItemInbox_Change(dynamic obj)
        {
            if(obj.BodyHtml != null)
                FileHelper.WriteBytesToFileWithStrin(obj.BodyHtml);
            else
                FileHelper.WriteTextToFile(obj.Body);
            UrlForMail = FileHelper.GetTextFromFile(FileHelper.GetPath("//Resources//mail.html"));
            ChatService.SetMarked(obj.ID);
            SelectedColumn = 4;
            ColumnTitle = ColumnTitle + " / " + obj.Title;
        }

        public object SelectedItemInbox
        {
            get { return selectedItemInbox; }
            set { Set(ref selectedItemInbox, value); SelectedItemInbox_Change(value); }
        }

        private void InboxItemsRefresh(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Inbox.Clear();
                ChatService.GetAllUnFavoritesMails().ToList().ForEach((x) =>
                {
                        Inbox.Add(new
                        {
                            x.ID,
                            x.Title,
                            x.UserId,
                            x.SenderEmail,
                            x.Marked,
                            x.BodyHtml,
                            x.Favorite,
                            Body = x.Title + " - " + x.Body,
                            SendTime = x.SendTime.ToShortDateString()
                        });
                });
                if (Inbox.Count == 0)
                    SelectedColumn = 0;
                else if (SelectedColumn != 1 && Inbox.Count != 0)
                    SelectedColumn = 1;
            });
        }



        private RelayCommand _inboxCommand;

        public RelayCommand InboxCommand => _inboxCommand ?? (_inboxCommand = new RelayCommand(
        () =>
        {
            SelectedItem = null;
            if (ChatService.GetAllUnFavoritesMails().Count != 0)
                SelectedColumn = 1;
            else
                SelectedColumn = 0;
            inboxtimer.Start();
        }));



        private RelayCommand<dynamic> _addFavorite;

        public RelayCommand<dynamic> AddFavorite => _addFavorite ?? (_addFavorite = new RelayCommand<dynamic>(
        x =>
        {
            ChatService.AddStarred(x.ID);
        }));

        #endregion

        #region Starred
        private readonly Timer starredTimer;

        public RelayCommand StarredCommand => _starredCommand ?? (_starredCommand = new RelayCommand(
        () =>
        {
            SelectedItem = null;
            if (ChatService.GetAllFavoritesMails().Count != 0)
                SelectedColumn = 5;
            else
                SelectedColumn = 2;
            starredTimer.Start();
        }));

        private ObservableCollection<dynamic> starredList;

        public ObservableCollection<dynamic> StarredList
        {
            get { return starredList; }
            set { Set(ref starredList, value); }
        }

        private RelayCommand<dynamic> _removeFavorite;

        public RelayCommand<dynamic> RemoveFavorite => _removeFavorite ?? (_removeFavorite = new RelayCommand<dynamic>(
        x =>
        {
            ChatService.RemoveStarred(x.ID);
        }));

        private void StarredItemsRefresh(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                StarredList.Clear();
                ChatService.GetAllFavoritesMails().ToList().ForEach((x) =>
                {
                    StarredList.Add(new
                    {
                        x.ID,
                        x.Title,
                        x.UserId,
                        x.SenderEmail,
                        x.Marked,
                        x.BodyHtml,
                        x.Favorite,
                        Body = x.Title + " - " + x.Body,
                        SendTime = x.SendTime.ToShortDateString()
                    });
                });
                if (StarredList.Count == 0)
                    SelectedColumn = 2;
                else if (SelectedColumn != 5 && StarredList.Count != 0)
                    SelectedColumn = 5;
            });
        }


        #endregion

        #region UserControl

        private RelayCommand _closedCommand;

        public RelayCommand ClosedCommand
        {
            get => _closedCommand ?? (_closedCommand = new RelayCommand((() => { inboxtimer.Stop(); chattimer.Stop(); })));
        }



        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new RelayCommand((() => LoadedDatas())));
        }

        public void CheckInbox()
        {
            if (ChatService.GetAllUnFavoritesMails().Count != 0)
                SelectedColumn = 1;
            else
                SelectedColumn = 0;
        }

        public void CheckStarred()
        {
            if (ChatService.GetAllFavoritesMails().Count != 0)
                SelectedColumn = 5;
            else
                SelectedColumn = 2;
        }

        private void LoadedDatas()
        {
            ChatRoomDatas();
            CheckInbox();
            inboxtimer.Start();
        }

        private void ChatRoomDatas()
        {
            PublicChannels.Clear();
            PrivateChannels.Clear();
            DirectMessages.Clear();
            ChannelsService.GetListPublicChannelsId().ToList().ForEach(x => PublicChannels.Add(x));
            ChannelsService.GetListPrivateChannelsId().ToList().ForEach(x => PrivateChannels.Add(x));
            ChannelsService.GetListDirectChannelsId().ToList().ForEach(x => DirectMessages.Add(x));
        }

        private object header;

        public object Header
        {
            get { return header; }
            set { header = value; Set(ref header, value); }
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
            StarredList = new ObservableCollection<dynamic>();
            ChannelsService = new ChannelsService();
            ChatService = new ChatService();
            Header = new HeaderViewModel(navigationService);
            inboxtimer = new Timer(1000);
            chattimer = new Timer(500);
            starredTimer = new Timer(1000);
            inboxtimer.Elapsed += InboxItemsRefresh;
            starredTimer.Elapsed += StarredItemsRefresh;
            chattimer.Elapsed += ChatItemsRefresh;
        }
        #endregion

    }
}
