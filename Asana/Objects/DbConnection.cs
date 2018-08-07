using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class DbConnection : DbContext
    {
        public DbConnection() : base("name=DbConnection")
        {

        }
        public DbSet<User> Users{ get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        public DbSet<TaskState> TaskStates { get; set; }
        public DbSet<Task> Tasks{ get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Language> Languages{ get; set; }
        public DbSet<ExtraInfo> ExtraInfos{ get; set; }
        public DbSet<Dashboard> Dashboards{ get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Column> Columns{ get; set; }
    }
}
