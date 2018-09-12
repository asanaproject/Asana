using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Asana.Tools
{
    public class Errors
    {
        public static void LoginErrorMsg ()
        {
            MessageBox.Show("Your Username or Password incorrect,please check you account information and try again !", 
                "Warning!", MessageBoxButton.OK,MessageBoxImage.Warning);
        }

        public static void ConfirmCodeErrorMsg()
        {
            MessageBox.Show("Confirmation Code is not correct, enter it correctly!",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void PasswordForgotErrorMsg()
        {
            MessageBox.Show("Passwords are not same , enter it correctly!",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void SendCodeErrorMsg()
        {
            MessageBox.Show("Your Email Wrong,Please check your email!", "Email", MessageBoxButton.OK);
        }

        public static void SeacrhFriendErrorMsg()
        {
            MessageBox.Show("Email incorrect!", "Email", MessageBoxButton.OK,MessageBoxImage.Warning);
        }


        public static void SomethingErrorMsg()
        {
            MessageBox.Show("Something error,please check your information!",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
