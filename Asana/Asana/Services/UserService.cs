using Asana.Model;
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
using Task = System.Threading.Tasks.Task;

namespace Asana.Services
{
    public class UserService : IUserService
    {

        public async Task CreateAsync(User user)
        {
            try
            {
                if (user != null)
                {
                    using (var dbContext = new AsanaDbContext())
                    {

                        if (dbContext.Users.ToList().Exists(x => x.Username == user.Username))
                        {
                            throw new Exception("User with this username already exists.");
                        }
                        user.Password = PasswordHasher.Hash(user.Password);
                        dbContext.Users.Add(user);
                       await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.InnerException.ToString());
            }
        }

        public User Select(string email)
        {
            try
            {
                using (var dbContext = new AsanaDbContext())
                {
                    if ((email != "" || email != null) && dbContext.Users.Any(x => x.Email == email))
                        return dbContext.Users.Single(x => x.Email == email);
                    throw new Exception("User with this email not founded");
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
