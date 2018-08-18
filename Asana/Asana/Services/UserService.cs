using Asana.Objects;
using Asana.Services.Interfaces;
using Asana.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public  void Insert(User user)
        {
            try
            {
                if (user != null)
                {
                    user.Password = PasswordHasher.Hash(user.Password);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
