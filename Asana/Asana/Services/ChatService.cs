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
                ChatRoom room = new ChatRoom();
                room.Name = name;
                room.Users.Add(CurrentUser.Instance.User);
                db.ChatRooms.Add(room);
            }
            return true;
        }


        public bool RemoveRoom(string name)
        {
            return true;
        }
        //-- Ayri Service olacaq

        public bool SendMessagesChannel(int channel_id,string body)
        {
            return true;
        }

        public bool SendMessagePrivateChannel(int channel_id,string body)
        {
            return true;
        }


    }
}
