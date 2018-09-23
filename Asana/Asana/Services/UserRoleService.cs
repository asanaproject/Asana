using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Asana.Model;
using Asana.Objects;
using Asana.Services.Interfaces;

namespace Asana.Services
{
    public class UserRoleService : IUserRoleService
    {
        
        public async System.Threading.Tasks.Task Add(UserRoles user)
        {
            if (user!=null)
            {
                try
                {
                    using (var asana=new AsanaDbContext())
                    {
                        if (asana.Projects.First(x=>x.Id==user.ProjectId).UserRole.First(x=>x.Email==user.Email)!=null)
                        {
                            throw new Exception("User with this email already exists.");
                        }
                        var role = asana.Roles.First(x => x.Type.Equals("employee"));
                        user.Role =role;
                        user.RoleId = role.Id;
                        asana.UserRoles.Add(user);
                        await asana.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
           
        }
    }
}

