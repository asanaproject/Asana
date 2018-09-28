using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Asana.Model;
using Asana.Objects;
using Asana.Services.Interfaces;
using Asana.Tools;

namespace Asana.Services
{
    public class UserRoleService : IUserRoleService
    {
        
        public async System.Threading.Tasks.Task CreateAsync(UserRoles user)
        {
            EmailHelper sendEmail = new EmailHelper();
            if (user!=null)
            {
                try
                {
                    using (var asana=new AsanaDbContext())
                    {
                        //if (asana.Projects.First(x=>x.Id==user.ProjectId).Users.First(x=>x.Email==user.Email)==null)
                        //{
                        //    sendEmail.SendInvitation(user.Email);
                        //    throw new Exception($"This employee has not registered yet. Invitation message is sent to {user.Email}.");
                        //}
                        var role = asana.Roles.First(x => x.Type.Equals("employee"));
                        user.Role =role;
                        asana.Projects.First(x => x.Id == user.ProjectId).Users.Add(user);
                        await asana.SaveChangesAsync();
                        CurrentProject.Instance.Project = asana.Projects.Include("Users").First(x => x.Id == user.ProjectId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Information",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
           
        }
    }
}

