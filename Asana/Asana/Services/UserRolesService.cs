using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (user != null)
            {
                try
                {
                    using (var asana = new AsanaDbContext())
                    {
                        var u = asana.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
                        if (u == null)
                        {
                            sendEmail.SendInvitation(user.Email);
                            throw new Exception($"This employee has not registered yet. Invitation message is sent to {user.Email}.");
                        }
                        var userRole = asana.UserRoles.FirstOrDefault(x => x.Email.Equals(user.Email) 
                                                                        && x.ProjectId == user.ProjectId
                                                                        && x.FullName.Equals(x.FullName));
                        if (userRole != null)
                        {
                            throw new Exception("User with this username already exists.");
                        }
                        asana.UserRoles.Add(user);
                        await asana.SaveChangesAsync();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        public void LoadRoles(Guid projectId)
        {
            if (!String.IsNullOrEmpty(projectId.ToString()))
            {
                using (var context = new AsanaDbContext())
                {
                    var c = context.UserRoles.Include("Project").Where(x => x.ProjectId == projectId).ToList();
                    CurrentUserRoles.Instance.Employees = new ObservableCollection<UserRoles>(c);
                }
            }
        }
    }
}