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
        public User User { get; set; } = new User ();
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
