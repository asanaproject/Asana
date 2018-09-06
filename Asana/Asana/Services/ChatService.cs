using Asana.Objects;
using Asana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Collections.ObjectModel;

namespace Asana.Services
{
    public class ChatService
    {
        public bool SendMessagesChannel(int ChatRoomId, string body)
        {
            try
            {
                using (var db= new AsanaDbContext())
                {
                    db.Messages.Add(new Message() { Body = body, ChatRoomId = ChatRoomId, SendTime = DateTime.Now, UserId = CurrentUser.Instance.User.Id });
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }

        public ObservableCollection<Message> GetSelectedChannelMessages(int ChatRoomId)
        {
            try
            {
                ObservableCollection<Message> messages = new ObservableCollection<Message>();
                using (var db = new AsanaDbContext())
                    db.Messages.Where(x => x.ChatRoomId == ChatRoomId).ToList().ForEach(x => messages.Add(x));    
                return messages;
            }
            catch(Exception err)
            {
                Log.Error(err.Message);
                return new ObservableCollection<Message>();
            }
        }

        public ObservableCollection<Mail> GetAllMails()
        {
            try
            {
                ObservableCollection<Mail> messages = new ObservableCollection<Mail>();
                using (var db = new AsanaDbContext())
                    db.Mails.Where(x => x.UserId == CurrentUser.Instance.User.Id).ToList().ForEach(x => messages.Add(x));
                return messages;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return new ObservableCollection<Mail>();
            }
        }


    }
}
