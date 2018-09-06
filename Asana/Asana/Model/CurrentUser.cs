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
        public static string Username { get; set; } = "{{Username}}";
        public static int Id { get; set; } = 0;

        private User user;

        public User User
        {
            get { return user; }
            set { user = value; Username = user.Username; Id = user.Id; }
        }

        private CurrentUser() { }
        private static CurrentUser instance;
        public static CurrentUser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUser();
                }
                return instance;

            }
        }

        //public void SetProps(User info)
        //{
        //    currenUser = info;
        //}
    }
}
