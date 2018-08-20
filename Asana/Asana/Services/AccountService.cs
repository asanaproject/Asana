﻿using Asana.Objects;
using Asana.Services.Interfaces;
using Asana.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Model
{
    public class AccountService : IAccountService
    {
        public  bool LoginControl(string Email, string Password)
        {
            using (var db = new AsanaDbContext())
            {
                Password = PasswordHasher.Hash(Password);
                if (db.Users.Any(user => user.Email == Email && user.Password == Password))
                    return true;
                else
                    return false;
            }
        }
    }
}