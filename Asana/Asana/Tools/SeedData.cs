using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class SeedData
    {
        public static async void EnsurePopulated()
        {
            using (var context = new AsanaDbContext())
            {
                if (context.Roles.Count() == 0)
                {
                    context.Roles.Add(new Roles { Type = "customer" });
                    context.Roles.Add(new Roles { Type = "employee" });
                    context.Roles.Add(new Roles { Type = "project manager" });
                   
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
