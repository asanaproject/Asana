using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Services;
using Asana.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Timers;
using Asana.View;

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

        private void StopAllTimers()
        {
            inboxtimer.Stop();
            chattimer.Stop();
            starredTimer.Stop();
        }

        private int selectedColumn;

        public int SelectedColumn
        {
            get { return selectedColumn; }
            set
            {
                Set(ref selectedColumn, value);
                if (value == 0 || value == 1)
                {
                    ColumnTitle = "#Inbox";
                }
                else if (value == 2 || value == 5)
                {
                    ColumnTitle = "#Starred";
                }
                StopAllTimers();
            }
        }


        #region Channels
        private readonly Timer chattimer;
        private object chatSelectedItem;

        public object ChatSelectedItem
        {
            get { return chatSelectedItem; }
            set { Set(ref chatSelectedItem, value); }
        }

        private ObservableCollection<ChatRoom> privateChannels;

        public ObservableCollection<ChatRoom> PrivateChannels
        {
            get { return privateChannels; }
            set { Set(ref privateChannels, value); }
        }

        private ObservableCollection<ChatRoom> directMessages;

        public ObservableCollection<ChatRoom> DirectMessages
        {
            get { return directMessages; }
            set { Set(ref directMessages, value); }
        }

        private ObservableCollection<ChatRoom> publicChannels;

        public ObservableCollection<ChatRoom> PublicChannels
        {
            get { return publicChannels; }
            set { Set(ref publicChannels, value); }
        }


        private ObservableCollection<dynamic> chatItems;

        public ObservableCollection<dynamic> ChatItems
        {
            get { return chatItems; }
            set { Set(ref chatItems, value); }
        }

        private ChatRoom selectedItem;
        public ChatRoom SelectedItem
        {
            get { return selectedItem; }
            set
            {
                Set(ref selectedItem, value);
                StopAllTimers();
                SelectedColumn = 3;
                if (value != null)
                    ColumnTitle = "#" + value.Name;
                ChatItems.Clear();
                chattimer.Start();
            }
        }


        private RelayCommand _addChatRoomChannels;

        public RelayCommand AddChatRoomChannels => _addChatRoomChannels ?? (_addChatRoomChannels = new RelayCommand(
        () =>
        {
            try
            {
                WindowBluringCustom.Bluring();
                ExtraWindow extraWindow = new ExtraWindow(new ChatRoomAddViewModel(ChatRoomType.Public), 500, 160);
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
                ChatRoomDatas();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }));


        private string message_Text;

        public string Message_Text
        {
            get { return message_Text; }
            set { Set(ref message_Text, value); }
        }

        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(
        () =>
        {
            if (SelectedItem != null)
                ChatService.SendMessagesChannel(SelectedItem.ID, Message_Text);
            Message_Text = "";

        }));

        private RelayCommand _starredCommand;


        private RelayCommand _addChatRoomPrivate;

        public RelayCommand AddChatRoomPrivate => _addChatRoomPrivate ?? (_addChatRoomPrivate = new RelayCommand(
        () =>
        {
            try
            {
                WindowBluringCustom.Bluring();
                ExtraWindow extraWindow = new ExtraWindow(new ChatRoomAddViewModel(ChatRoomType.Private), 500, 160);
                extraWindow.ShowDialog();
                WindowBluringCustom.Normal();
                ChatRoomDatas();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }));

        private RelayCommand _addDirectMessage;
        public RelayCommand AddDirectMessage => _addDirectMessage ?? (_addDirectMessage = new RelayCommand(
      () =>
      {
          try
          {
              WindowBluringCustom.Bluring();
              ExtraWindow extraWindow = new ExtraWindow(new ChatRoomAddViewModel(ChatRoomType.Direct), 500, 160);
              extraWindow.ShowDialog();
              WindowBluringCustom.Normal();
              ChatRoomDatas();
          }
          catch (Exception err)
          {
              MessageBox.Show(err.Message);
          }
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

            System.Threading.Tasks.Task.Run(async () =>
            {
                if (SelectedItem == null)
                    return;
                var listed = await ChatService.GetSelectedChannelMessages(SelectedItem.ID);
                App.Current.Dispatcher.Invoke(() =>
                {
                     
                    if (SelectedItem != null && listed.Count != ChatItems.Count)
                    {
                        ChatItems.Clear();
                        listed.ToList().ForEach(x => ChatItems.Add(x));
                    }
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
            set { Set(ref inbox, value); }
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
            if (obj == null)
            {
                return;
            }
            if (obj.BodyHtml != null)
                FileHelper.WriteBytesToFileWithStrin(obj.BodyHtml);
            else
                FileHelper.WriteTextToFile(obj.Body);
            ChatService.SetMarked(obj.ID);
            ColumnTitle = ColumnTitle + " / " + obj.Title;
            UrlForMail = FileHelper.GetTextFromFile(FileHelper.GetPath("//Resources//mail.html"));
            SelectedColumn = 4;
        }

        public object SelectedItemInbox
        {
            get { return selectedItemInbox; }
            set { Set(ref selectedItemInbox, value); SelectedItemInbox_Change(value); }
        }

        private void InboxItemsRefresh(object sender, ElapsedEventArgs e)
        {
            System.Threading.Tasks.Task.Run(async() =>
            {
                var listed = await ChatService.GetAllUnFavoritesMails();
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (listed.Count != Inbox.Count)
                    {
                        Inbox.Clear();
                        listed.ForEach((x) => Inbox.Add(x));
                        if (Inbox.Count == 0 && (SelectedColumn == 1 || SelectedColumn == 0))
                            SelectedColumn = 0;
                        else if (SelectedColumn != 1 && Inbox.Count != 0 && (SelectedColumn == 1 || SelectedColumn == 0))
                            SelectedColumn = 1;
                    }
                });
            });
        }

        private RelayCommand _markAllReadCommand;

        public RelayCommand MarkAllReadCommand => _markAllReadCommand ?? (_markAllReadCommand = new RelayCommand(
        async () =>
        {
            ChatService.MarkedAllMail();
            Inbox.Clear();
            var result = await ChatService.GetAllUnFavoritesMails();
            result.ToList().ForEach(x => Inbox.Add(x));
        }));


        private RelayCommand _inboxCommand;

        public RelayCommand InboxCommand => _inboxCommand ?? (_inboxCommand = new RelayCommand(
        async() =>
        {
            NullAllProperties();
            SelectedColumn = 0;
            var result = await ChatService.GetAllUnFavoritesMails();
            if (result.Count != 0)
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

        public void NullAllProperties()
        {
            SelectedItem = null;
            SelectedItemInbox = null;
        }

        public RelayCommand StarredCommand => _starredCommand ?? (_starredCommand = new RelayCommand(
        async() =>
        {
            NullAllProperties();
            SelectedColumn = 2;
            var result = await ChatService.GetAllFavoritesMails();
            if (result.Count != 0)
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
            System.Threading.Tasks.Task.Run(async() =>
            {
                var listed = await ChatService.GetAllFavoritesMails();
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (listed.Count != StarredList.Count)
                    {
                        StarredList.Clear();
                        listed.ForEach((x) => StarredList.Add(x));
                        if (StarredList.Count == 0 && (SelectedColumn == 2 || SelectedColumn == 5))
                            SelectedColumn = 2;
                        else if (SelectedColumn != 5 && StarredList.Count != 0 && (SelectedColumn == 2 || SelectedColumn == 5))
                            SelectedColumn = 5;
                    }
                });
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

        public async void CheckInbox()
        {
            var result = await ChatService.GetAllUnFavoritesMails();
            if (result.Count != 0)
                SelectedColumn = 1;
            else
                SelectedColumn = 0;
        }

        public async void CheckStarred()
        {
            var result = await ChatService.GetAllFavoritesMails();
            if (result.Count != 0)
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
            PublicChannelRoomRefresh();
            PrivateChannelRoomRefresh();
            DirectChannelRoomRefresh();
        }

        private async void PublicChannelRoomRefresh()
        {
            PublicChannels.Clear();
            var publiclist = await ChannelsService.GetListPublicChannelsId();
            publiclist.ForEach(x => PublicChannels.Add(x));
        }

        private async void PrivateChannelRoomRefresh()
        {
            PrivateChannels.Clear();
            var privatelist = await ChannelsService.GetListPrivateChannelsId();
            privatelist.ForEach(x => PrivateChannels.Add(x));
        }

        private async void DirectChannelRoomRefresh()
        {
            DirectMessages.Clear();
            var directlist = await ChannelsService.GetListDirectChannelsId();
            directlist.ForEach(x => DirectMessages.Add(x));
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
            inboxtimer = new Timer(1);
            chattimer = new Timer(1);
            starredTimer = new Timer(1);
            inboxtimer.Elapsed += InboxItemsRefresh;
            starredTimer.Elapsed += StarredItemsRefresh;
            chattimer.Elapsed += ChatItemsRefresh;

        }
        #endregion

    }
}
