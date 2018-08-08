using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Asana.Tools
{
    public class RegexChecker
    {
        public static bool CheckEmail(string email)
        {
            try
            {
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

        public static bool CheckMobileTelephone(string phonenumber)
        {
            try
            {
                Regex regex = new Regex(@"^ \+\d( ?\d){8,24}$"); //8-24 araliginda reqemler olmali +994772209966
                Match match = regex.Match(phonenumber);
                if (match.Success)
                    return true;
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
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,18}$"); //6-18 araliginda en az bir boyuk bir kicik ve reqem olmalidi :)
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
