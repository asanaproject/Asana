using Asana.Model;
using Asana.Objects;
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
        public bool InsertRoom(string name,string type = "Public")
        {
            using (var db = new AsanaDbContext())
            {
                name = "#" + name;
                ChatRoom chat = new ChatRoom() { Name = name, Type = type };
                db.ChatRooms.Add(chat);
            }
            return true;
        }

        public bool JoinRoom(string name)
        {
            using (var db = new AsanaDbContext())
            {
                if (db.ChatRooms.Single(x => x.Name == name).Users.Single(x => x == CurrentUser.Instance.User) == null)
                {
                    db.ChatRooms.Single(x => x.Name == name).Users.Add(CurrentUser.Instance.User);
                    return true;
                }
                return false;
            }
        }

        public bool RemoveRoom(string name)
        {
            using (var db = new AsanaDbContext())
            {
                if(db.ChatRooms.Any(x=>x.Name == name) && db.ChatRooms.Single(x=>x.Name == name).Users.ToList()[0] == CurrentUser.Instance.User)
                {
                    db.ChatRooms.ToList().RemoveAll(x=>x.Name == name);
                    return true;
                }
                return false;
            }
        }

        public ObservableCollection<string> GetListPublicChannelsId()
        {
            ObservableCollection<string> listId = new ObservableCollection<string>();
            using (var db = new AsanaDbContext())
            {
                foreach (var item in db.ChatRooms)
                {
                    if(item.Users.Any(x=> x == CurrentUser.Instance.User) && item.Type == "Public")
                    {
                        listId.Add(item.Name);
                    }
                }
            }
            return listId;
        }

        public ObservableCollection<string> GetListPrivateChannelsId()
        {
            ObservableCollection<string> listId = new ObservableCollection<string>();
            using (var db = new AsanaDbContext())
            {
                foreach (var item in db.ChatRooms)
                {
                    if (item.Users.Any(x => x == CurrentUser.Instance.User) && item.Type == "Private")
                    {
                        listId.Add(item.Name);
                    }
                }
            }
            return listId;
        }
    }
}
