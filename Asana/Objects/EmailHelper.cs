using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace Asana.Objects
{
    public class EmailHelper
    {
        SmtpClient client;

        string email = "asanateam.az@gmail.com";
        string pass = "asana12345";


        public EmailHelper()
        {
            client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(email, pass);
        }

        public void Send(string receiver_email, string subject, string message,string sender_email = "asanateam.az@gmail.com")
        {
            MailMessage mailMessage = new MailMessage(sender_email, receiver_email, subject, message);
            client.Send(mailMessage);
        }


        public void SendRegisterActivationCode(string receiver_email, string sender_email = "asanateam.az@gmail.com")
        {
            string newhtml = HtmlParser.InsertInto('^', FileHelper.FindFile("//Resources//verify.html"));
            MailMessage mailMessage = new MailMessage(sender_email, receiver_email, "Register Activation Code!", newhtml);
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        }
    }
}
