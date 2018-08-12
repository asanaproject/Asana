using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime EndSession { get; set; }

        private static CurrentUser instance;

        public static CurrentUser GetInstance()
        {
            if (instance == null) instance = new CurrentUser();
            return instance;
        }

        public CurrentUser()
        {

        }

        public void SetProps(ExtraInfo info)
        {
            instance.Id = info.Id;
            instance.Username = info.Username;
            instance.Email = info.Email;
            instance.EndSession = DateTime.Now.AddDays(1);
        }

    }
}
