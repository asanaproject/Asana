using Asana.Model;
using Asana.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.ViewModel
{
    public class ChatRoomAddViewModel : ViewModelBase
    {
        private ChannelsService channelService = new ChannelsService();

        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        private ChatRoomType chatRoomType;
        private RelayCommand _addChatRoomChannels;

        public void Closewindow()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title == "ExtraWindow")
                        window.Close();
                }
            });
        }

        public RelayCommand AddChatRoomChannels => _addChatRoomChannels ?? (_addChatRoomChannels = new RelayCommand(
        () =>
        {
            Task.Run(() =>
            {
                try
                {
                    if (chatRoomType == ChatRoomType.Direct)
                        channelService.InsertDirectMessage(Name);
                    else
                        channelService.InsertRoom(Name, chatRoomType);
                    Closewindow();
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }));
        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(
       () =>
       {
           Task.Run(() =>
           {
               Closewindow();
           });
       }));

        public ChatRoomAddViewModel(ChatRoomType chatRoomType)
        {
            this.chatRoomType = chatRoomType;
        }
    }
}
