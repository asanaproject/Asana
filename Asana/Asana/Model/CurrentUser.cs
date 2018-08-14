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
        public User currenUser { get; set; }

        private static CurrentUser instance;

        public static CurrentUser GetInstance()
        {
            if (instance == null) instance = new CurrentUser();
            return instance;
        }

        public CurrentUser()
        {

        }

        public void SetProps(User info)
        {
            currenUser = info;
        }
    }
}
