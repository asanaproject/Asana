using Asana.Objects;
using Asana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Collections.ObjectModel;
using System.Windows;

namespace Asana.Services
{
    public class ChatService
    {
        public bool SendMessagesChannel(Guid ChatRoomId, string body)
        {
            try
            {
                using (var db = new AsanaDbContext())
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

        public Task<List<dynamic>> GetSelectedChannelMessages(Guid ChatRoomId)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    List<dynamic> messages = new List<dynamic>();
                    using (var db = new AsanaDbContext())
                        db.Messages.Where(x => x.ChatRoomId == ChatRoomId).ToList().ForEach(x => messages.Add(new
                        {
                            x.ID,
                            x.UserId,
                            x.ChatRoomId,
                            x.Body,
                            x.SendTime
                        }));
                    messages = messages.OrderBy(x => x.SendTime).ToList();
                    return messages;
                }
                catch (Exception err)
                {
                    Log.Error(err.Message);
                    return new List<dynamic>();
                }
            });
        }

        public System.Threading.Tasks.Task<List<dynamic>> GetAllUnFavoritesMails()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    List<dynamic> messages = new List<dynamic>();
                    using (var db = new AsanaDbContext())
                        db.Mails.Where(x => x.UserId == CurrentUser.Instance.User.Id && x.Favorite == false).ToList().ForEach(x => messages.Add(new
                        {
                            x.ID,
                            x.Title,
                            x.UserId,
                            x.SenderEmail,
                            x.Marked,
                            x.BodyHtml,
                            x.Favorite,
                            Body = x.Title + " - " + x.Body,
                            SendTime = x.SendTime.ToShortDateString()
                        }));
                    return messages;
                }
                catch (Exception err)
                {
                    Log.Error(err.Message);
                    return new List<dynamic>();
                }
            });
        }

        public Task<List<dynamic>> GetAllFavoritesMails()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    List<dynamic> messages = new List<dynamic>();
                    using (var db = new AsanaDbContext())
                        db.Mails.Where(x => x.UserId == CurrentUser.Instance.User.Id && x.Favorite == true).ToList().ForEach(x => messages.Add(new
                        {
                            x.ID,
                            x.Title,
                            x.UserId,
                            x.SenderEmail,
                            x.Marked,
                            x.BodyHtml,
                            x.Favorite,
                            Body = x.Title + " - " + x.Body,
                            SendTime = x.SendTime.ToShortDateString()
                        }));
                    return messages;
                }
                catch (Exception err)
                {
                    Log.Error(err.Message);
                    return new List<dynamic>();
                }
            });
        }

        public async void RemoveAsync(Guid projectId)
        {
            if (projectId != null)
            {
                try
                {
                    using (var context = new AsanaDbContext())
                    {
                        var chat = context.ChatRooms.Where(x => x.ProjectId == projectId);
                        if (chat != null)
                        {
                            foreach (var item in chat)
                            {
                                context.ChatRooms.Remove(item);
                            }
                        }
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public void SetMarked(Guid id)
        {
            try
            {
                using (var db = new AsanaDbContext())
                {
                    db.Mails.Single(x => x.ID == id).Marked = true;
                    db.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }


        public void AddStarred(Guid id)
        {
            try
            {
                using (var db = new AsanaDbContext())
                {
                    db.Mails.Single(x => x.ID == id).Favorite = true;
                    db.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }

        public void RemoveStarred(Guid id)
        {
            try
            {
                using (var db = new AsanaDbContext())
                {
                    db.Mails.Single(x => x.ID == id).Favorite = false;
                    db.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }
        public void MarkedAllMail()
        {
            try
            {
                using (var db = new AsanaDbContext())
                {
                    db.Mails.Where(x => x.UserId == CurrentUser.Instance.User.Id).ToList().ForEach(x => x.Marked = true);
                    db.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
        }
    }
}
