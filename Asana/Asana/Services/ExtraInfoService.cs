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
    public class ExtraInfoService : IExtraInfoService
    {

        public async System.Threading.Tasks.Task Add(ExtraInfo extraInfo)
        {
            if (extraInfo != null)
            {
                try
                {
                    if (FindByEmail(extraInfo.Email))
                    {
                        return;
                    }
                    using (var context = new AsanaDbContext())
                    {
                        context.ExtraInfos.Add(extraInfo);
                        await context.SaveChangesAsync();                        
                    }
                }
                catch (Exception ex) {                 

                    MessageBox.Show(ex.Message);
                }                         }

        }

        public bool FindByEmail(string email)
        {
            using (var context = new AsanaDbContext())
            {
                return context.ExtraInfos.ToList().Exists(x => x.Email.Equals(email));

            }
        }
    }
}
