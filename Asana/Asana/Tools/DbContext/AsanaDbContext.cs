using Asana.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asana.Objects
{
    public class AsanaDbContext : DbContext
    {
        public AsanaDbContext() : base("AsanaDbContext")
        {
            Database.SetInitializer<AsanaDbContext>(null);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ExtraInfo> ExtraInfos { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoomUsers> ChatRoomUsers { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<KanbanState> KanbanState { get; set; }
        public DbSet<TaskKanbanState> TaskKanbanState { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }



    }
}
