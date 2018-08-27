using Asana.Model;
using Asana.Navigation;
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


        private readonly NavigationService navigationService;

        public ChatViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            PrivateMessages = new ObservableCollection<string>() { "Ali", "Ali1", "Ali2" };
            Channels = new ObservableCollection<string>() { "Ali", "Ali1", "Ali2" };
            DirectMessages = new ObservableCollection<string>() { "Ali", "Ali1", "Ali2" };
            ChatItems = new ObservableCollection<MessageItem>() { new MessageItem() { ProfName = "AB", Body = "TestBodyssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" }, new MessageItem() { ProfName = "NX", Body = "TestBody1" }, new MessageItem() { ProfName = "TM", Body = "TestBody2" }, new MessageItem() { ProfName = "TM", Body = "TestBodyssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" }, new MessageItem() { ProfName = "TM", Body = "TestBody1" }, new MessageItem() { ProfName = "AU", Body = "TestBody2" } };

        }
    }
}
