﻿using Asana.Model;
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
                    ChatRoom chat = new ChatRoom() { Name = name, ProjectId = CurrentProject.Instance.Project.Id, ChatRoomType = chatRoomType, Desc = "Don't have description." };
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
                    var friendUser = dbContext.Users.SingleOrDefault(x => x.Email == email);
                    if (friendUser == null || dbContext.ChatRooms.Any(x => x.Name == email + " - " + CurrentUser.Instance.User.Email) || !dbContext.Projects.Any(x => x.UserId == friendUser.Id && x.Id == CurrentProject.Instance.Project.Id))
                        throw new Exception();
                    ChatRoom chat = new ChatRoom() { Name = email + " - " + CurrentUser.Instance.User.Email, ProjectId = CurrentProject.Instance.Project.Id, ChatRoomType = chatRoomType, Desc = "Don't have description." };
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

        public bool JoinRoom(Guid ChatId)
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
                    Guid ChatId = dbContext.ChatRooms.SingleOrDefault(x => x.Name == name && x.ProjectId == CurrentProject.Instance.Project.Id).ID;

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

        public Task<List<ChatRoom>> GetListPublicChannelsId()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                using (var dbContext = new AsanaDbContext())
                {
                    List<ChatRoom> listId = new List<ChatRoom>();
                    foreach (var cru in dbContext.ChatRoomUsers.ToList())
                    {
                        var ch = dbContext.ChatRooms.SingleOrDefault(x => x.ID == cru.ChatRoomId && x.ProjectId == CurrentProject.Instance.Project.Id);
                        if (ch != null && ch.ChatRoomType == ChatRoomType.Public && cru.UserId == CurrentUser.Instance.User.Id)
                            listId.Add(ch);
                    }
                    return listId;

                }
            });
        }

        //public Task<ChatRoom> GetLastAdded(string name)
        //{
        //    return System.Threading.Tasks.Task.Run(() =>
        //    {
        //        using (var dbContext = new AsanaDbContext())
        //           return dbContext.ChatRooms.Last(x => x.Name == name);
        //    });
        //}

        public Task<List<ChatRoom>> GetListPrivateChannelsId()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                using (var dbContext = new AsanaDbContext())
                {
                    List<ChatRoom> listId = new List<ChatRoom>();
                    foreach (var cru in dbContext.ChatRoomUsers.ToList())
                    {
                        var ch = dbContext.ChatRooms.SingleOrDefault(x => x.ID == cru.ChatRoomId && CurrentProject.Instance.Project.Id == x.ProjectId);
                        if (ch != null && ch.ChatRoomType == ChatRoomType.Private && cru.UserId == CurrentUser.Instance.User.Id)
                            listId.Add(ch);
                    }
                    return listId;
                }
            });
        }

        public Task<List<ChatRoom>> GetListDirectChannelsId()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                using (var dbContext = new AsanaDbContext())
                {
                    List<ChatRoom> listId = new List<ChatRoom>();
                    foreach (var cru in dbContext.ChatRoomUsers.ToList())
                    {
                        var ch = dbContext.ChatRooms.SingleOrDefault(x => x.ID == cru.ChatRoomId && x.ProjectId == CurrentProject.Instance.Project.Id);
                        if (ch != null && ch.ChatRoomType == ChatRoomType.Direct && cru.UserId == CurrentUser.Instance.User.Id)
                        {
                            var id = dbContext.ChatRoomUsers.SingleOrDefault(x => x.ChatRoomId == ch.ID && x.UserId != CurrentUser.Id).UserId;
                            string name = dbContext.Users.SingleOrDefault(y => y.Id == id).FullName;
                            listId.Add(new ChatRoom() { ID = ch.ID, ProjectId = CurrentProject.Instance.Project.Id, Desc = ch.Desc, Name = name, ChatRoomType = ch.ChatRoomType });
                        }
                    }
                    return listId;
                }
            });
        }

        public Task<List<ChatRoom>> GetListAllPublicChannelsNotJoined()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                using (var dbContext = new AsanaDbContext())
                {
                    List<ChatRoom> listId = new List<ChatRoom>();
                    foreach (var cr in dbContext.ChatRooms.ToList())
                    {
                        if (cr.ProjectId == CurrentProject.Instance.Project.Id && cr.ChatRoomType == ChatRoomType.Public && !dbContext.ChatRoomUsers.ToList().Any(x => x.ChatRoomId == cr.ID && x.UserId == CurrentUser.Id))
                            listId.Add(cr);
                    }
                    return listId;
                }
            });
        }
    }
}