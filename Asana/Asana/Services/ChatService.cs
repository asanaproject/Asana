using Asana.Objects;
using Asana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services
{
    public class ChatService
    {
        public bool InsertRoom(string name)
        {
            using (var db = new AsanaDbContext())
            {
                name = "#" + name;
                db.ChatRooms.Add(new ChatRoom() { Name = name });
                //db.UserChatRooms.Add(new UserChatRoom() { Users = 1, ChatRooms = });
                db.Messages.Add(new Message() { ChatUserID = CurrentUser.Instance.User.Id, Timestap = DateTime.Now, Body = "created " + name });

                }
            return true;
        }   
    }
}
