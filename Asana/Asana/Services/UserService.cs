using Asana.Objects;
using Asana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Services
{
    public class UserService : IUserService
    {
        private readonly AsanaDbContext dbContext;

        public UserService(AsanaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task Insert(User user)
        {
            try
            {
                if (user != null)
                {
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
