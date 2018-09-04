using Asana.Model;
using Asana.Objects;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Services
{
    public class ChannelsService
    {
        private readonly AsanaDbContext dbContext;

        public ChannelsService(AsanaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public bool InsertRoom(string name, string type = "Public")
        {
            try
            {
                name = "#" + name;
                ChatRoom chat = new ChatRoom() { Name = name, Type = type, Desc = "Desccc" };
                dbContext.ChatRooms.Add(chat);
                dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { UserId = CurrentUser.Instance.User.Id, ChatRoomId = chat.ID });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }

        }

        public bool JoinRoom(int ChatId)
        {
            try
            {

                //int ChatId = db.ChatRooms.Single(x => x.Name == name).ID;
                dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { ChatRoomId = ChatId, UserId = CurrentUser.Instance.User.Id });
                return true;

            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }

        public bool RemoveRoom(string name)
        {
            try
            {

                int ChatId = dbContext.ChatRooms.Single(x => x.Name == name).ID;
                dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { ChatRoomId = ChatId, UserId = CurrentUser.Instance.User.Id });
                return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }

        public ObservableCollection<ChatRoom> GetListPublicChannelsId()
        {
            ObservableCollection<ChatRoom> listId = new ObservableCollection<ChatRoom>();
            foreach (var cru in dbContext.ChatRoomUsers)
            {
                if (dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId) != null && cru.UserId == CurrentUser.Instance.User.Id)
                    listId.Add(dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId));
            }
            return listId;
        }

        public ObservableCollection<string> GetListPrivateChannelsId()
        {
            ObservableCollection<string> listId = new ObservableCollection<string>();
        
            return listId;
        }
    }
}
