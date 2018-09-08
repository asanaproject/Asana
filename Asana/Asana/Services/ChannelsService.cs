using Asana.Model;
using Asana.Objects;
using Asana.Tools;
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

        public bool InsertRoom(string name, ChatRoomType chatRoomType = ChatRoomType.Public)
        {
            try
            {
                using (var dbContext = new AsanaDbContext())
                {
                    ChatRoom chat = new ChatRoom() { Name = name, Type = chatRoomType, Desc = "Don't have description." };
                    dbContext.ChatRooms.Add(chat);
                    dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { UserId = CurrentUser.Instance.User.Id, ChatRoomId = chat.ID });
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }

        }

        public bool InsertDirectMessage(string email, ChatRoomType chatRoomType = ChatRoomType.Direct)
        {
            try
            {
                using (var dbContext = new AsanaDbContext())
                {
                    var friendUser = dbContext.Users.Single(x => x.Email == email);
                    if (friendUser == null || dbContext.ChatRooms.Any(x=>x.Name == email + " - " + CurrentUser.Instance.User.Email))
                        throw new Exception();
                    ChatRoom chat = new ChatRoom() { Name = email + " - " + CurrentUser.Instance.User.Email, Type = chatRoomType, Desc = "Don't have description." };
                    dbContext.ChatRooms.Add(chat);
                    dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { UserId = CurrentUser.Instance.User.Id, ChatRoomId = chat.ID });
                    dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { UserId = friendUser.Id, ChatRoomId = chat.ID });
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception err)
            {
                Errors.SeacrhFriendErrorMsg();
                return false;
            }

        }

        public bool JoinRoom(int ChatId)
        {
            try
            {
                using (var dbContext = new AsanaDbContext())
                {
                    //int ChatId = db.ChatRooms.Single(x => x.Name == name).ID;
                    dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { ChatRoomId = ChatId, UserId = CurrentUser.Instance.User.Id });
                    dbContext.SaveChanges();
                }
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
                using (var dbContext = new AsanaDbContext())
                {
                    int ChatId = dbContext.ChatRooms.Single(x => x.Name == name).ID;
                    dbContext.ChatRoomUsers.Add(new ChatRoomUsers() { ChatRoomId = ChatId, UserId = CurrentUser.Instance.User.Id });
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }

        public ObservableCollection<ChatRoom> GetListPublicChannelsId()
        {
            using (var dbContext = new AsanaDbContext())
            {
                ObservableCollection<ChatRoom> listId = new ObservableCollection<ChatRoom>();
                foreach (var cru in dbContext.ChatRoomUsers.ToList())
                {
                    if (dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId ).Type == ChatRoomType.Public && cru.UserId == CurrentUser.Instance.User.Id)
                        listId.Add(dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId));
                }
                return listId;
            }
        }

        public ObservableCollection<ChatRoom> GetListPrivateChannelsId()
        {
            using (var dbContext = new AsanaDbContext())
            {
                ObservableCollection<ChatRoom> listId = new ObservableCollection<ChatRoom>();
                foreach (var cru in dbContext.ChatRoomUsers.ToList())
                {
                    if (dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId).Type == ChatRoomType.Private && cru.UserId == CurrentUser.Instance.User.Id)
                        listId.Add(dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId));
                }
                return listId;
            }
        }

        public ObservableCollection<ChatRoom> GetListDirectChannelsId()
        {
            using (var dbContext = new AsanaDbContext())
            {
                ObservableCollection<ChatRoom> listId = new ObservableCollection<ChatRoom>();
                foreach (var cru in dbContext.ChatRoomUsers.ToList())
                {
                    if (dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId).Type == ChatRoomType.Direct && cru.UserId == CurrentUser.Instance.User.Id)
                    {
                        var xy = dbContext.ChatRooms.Single(x => x.ID == cru.ChatRoomId);
                        int id = dbContext.ChatRoomUsers.Single(x => x.ChatRoomId == xy.ID && x.UserId != CurrentUser.Id).UserId;
                        string name = dbContext.Users.Single(y => y.Id == id).FullName;
                        listId.Add(new ChatRoom() { ID = xy.ID , Desc = xy.Desc,Name = name,Type = xy.Type});
                    }
                }
                return listId;
            }
        }
    }
}
