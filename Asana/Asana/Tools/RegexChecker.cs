using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Tools
{
    public class RegexChecker
    {
        public static bool CheckEmail(string email)
        {
            try
            {
                if (email == null)
                    return false;
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"); //Butun emaillar
                Match match = regex.Match(email);
                if (match.Success)
                    return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
            return false;
        }

        public static bool CheckUsername(string username)
        {
            try
            {
                if (username == null)
                    return false;
                Regex regex = new Regex(@"^[a-zA-Z0-9_]{5,20}$");
                Match match = regex.Match(username);
                if (match.Success)
                    return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
            return false;

        }


        public static bool CheckMobileTelephone(string phoneNumber)
        {
            try
            {
                if (phoneNumber == null)
                    return false;
                return Regex.IsMatch(phoneNumber, @"^[0-9]{10}$");
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
            return false;
        }

        public static bool CheckPassword(string password)
        {
            try
            {
                if (password == null)
                    return false;
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,18}$"); //8-18 araliginda en az bir boyuk bir kicik ve reqem olmalidi :)
                Match match = regex.Match(password);
                if (match.Success)
                    return true;
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
            }
            return false;
        }
    }
}
