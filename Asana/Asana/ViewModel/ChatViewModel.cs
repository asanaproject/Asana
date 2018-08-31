using Asana.Model;
using Asana.Navigation;
using Asana.Objects;
using Asana.Tools;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Asana.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {


        private ObservableCollection<string> privateMessages;

        public ObservableCollection<string> PrivateMessages
        {
            get { return privateMessages; }
            set { privateMessages = value; Set(ref privateMessages, value); }
        }

        private ObservableCollection<string> directMessages;

        public ObservableCollection<string> DirectMessages
        {
            get { return directMessages; }
            set { directMessages = value; Set(ref directMessages, value); }
        }

        private ObservableCollection<string> channels;

        public ObservableCollection<string> Channels
        {
            get { return channels; }
            set { channels = value; Set(ref channels, value); }
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
            ChatRoomAdd chatRoomAdd = new ChatRoomAdd("Name");
            chatRoomAdd.ShowDialog();
            string channelname = chatRoomAdd.GetName();
            using (var db = new AsanaDbContext())
            {
                db.ChatRooms.Add(new ChatRoom() { Name = channelname });
            }
        }));




        private readonly NavigationService navigationService;

        public ChatViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            PrivateMessages = new ObservableCollection<string>() { "Ali", "Ali1", "Ali2" };
            Channels = new ObservableCollection<string>() { "Ali", "Ali1", "Ali2" };
            DirectMessages = new ObservableCollection<string>() { "Ali", "Ali1", "Ali2" };
            ChatItems = new ObservableCollection<MessageItem>() { new MessageItem() { ProfName = "Ali", Body = "TestBodyssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" }, new MessageItem() { ProfName = "Ali1", Body = "TestBody1" }, new MessageItem() { ProfName = "Ali2", Body = "TestBody2" }, new MessageItem() { ProfName = "Ali", Body = "TestBodyssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" }, new MessageItem() { ProfName = "Ali1", Body = "TestBody1" }, new MessageItem() { ProfName = "Ali2", Body = "TestBody2" } };
        }
    }
}
