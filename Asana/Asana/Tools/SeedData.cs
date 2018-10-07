using Asana.Model;
using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                if (context.KanbanState.Count() == 0)
                {
                    context.KanbanState.Add(new KanbanState { Name = "Ready for Next Stage" });
                    context.KanbanState.Add(new KanbanState { Name = "In Progress" });
                    context.KanbanState.Add(new KanbanState { Name = "Blocked" });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
