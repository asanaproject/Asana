﻿using Asana.Objects;
using Asana.Services.Interfaces;
using Asana.Tools;
using Serilog;
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
        public bool LoginControl(string Email, string Password)
        {
            using (var db = new AsanaDbContext())
            {
                Password = PasswordHasher.Hash(Password);
                if (RegexChecker.CheckEmail(Email))
                {
                    if (db.Users.Any(user => user.Email == Email && user.Password == Password))
                    {
                        CurrentUser.Instance.User = db.Users.Single(user => user.Email == Email && user.Password == Password);
                        return true;
                    }

                    else
                        return false;
                }
                else if(RegexChecker.CheckUsername(Email))
                {
                    if (db.Users.Any(user => user.Username == Email && user.Password == Password))
                    {
                        CurrentUser.Instance.User = db.Users.Single(user => user.Username == Email && user.Password == Password);
                        return true;
                    }

                    else
                        return false;
                }
                return false;
            }
        }
        
        public bool ForgotControl(string NewPassword)
        {
            try
            {
                using (var db = new AsanaDbContext())
                {
                    string email = CurrentUser.Instance.User.Email;
                    db.Users.Single(users => users.Email == email).Password = PasswordHasher.Hash(NewPassword);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception error)
            {
                Log.Error(error.Message);
                return false;
            }
        }

        public bool Logout()
        {
            try
            {
                CheckLoginLog.Remove();
                CurrentUser.Instance.User = new User();
                return true;
            }
            catch(Exception err)
            {
                Log.Error(err.Message);
                return false;
            }
        }
    }
}
