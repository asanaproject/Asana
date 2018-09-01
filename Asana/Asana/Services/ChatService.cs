using Asana.Objects;
using Asana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Asana.Services
{
    public class ChatService
    {
        public bool SendMessagesChannel(int channel_id,string body)
        {
            try
            {
                using (var db = new AsanaDbContext())
                {
                    db.Messages.Add(new Message() { ChatRoomId = channel_id, Body = body, Timestap = DateTime.Now, ChatUserID = CurrentUser.Instance.User.Id });
                    return true;
                }
            }
            catch(Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }

    }
}
