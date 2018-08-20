﻿using Serilog;
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

            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"); //Butun emaillar
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }

        public static bool CheckUsername(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_]{5,20}$");
            Match match = regex.Match(username);
            if (match.Success)
                return true;
            return false;
        }


        public static bool CheckMobileTelephone(string phonenumber)
        {

            Regex regex = new Regex(@"^ \+\d( ?\d){8,24}$"); //8-24 araliginda reqemler olmali +994772209966
            Match match = regex.Match(phonenumber);
            if (match.Success)
                return true;
            return false;
        }

        public static bool CheckPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,18}$"); //8-18 araliginda en az bir boyuk bir kicik ve reqem olmalidi :)
            Match match = regex.Match(password);
            if (match.Success)
                return true;
            return false;
        }
    }
}