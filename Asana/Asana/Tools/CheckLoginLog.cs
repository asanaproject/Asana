using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Asana.Tools
{
    public class CheckLoginLog
    {
        private static string path = Path.GetTempPath() + "User.info";
        public static string Load()
        {
            if (File.Exists(path))
            {
                string hashed = FileHelper.GetTextFromFile(path);
                string body = PasswordHasher.Decrypt(hashed);
                string[] parts = body.Split(new string[] { "::ExpireDate::" },
                             StringSplitOptions.RemoveEmptyEntries);
                if (DateTime.Parse(parts[1]) > DateTime.Now)
                    return parts[0];
            }
            return "";
        }

        public static void Save(string email)
        {
            string body = email + "::ExpireDate::" + DateTime.Now.AddDays(1).ToString();
            using (var writer = new StreamWriter(path))
            {
                writer.Write(PasswordHasher.Encrypt(body));
            }

        }

        public static void Remove()
        {
            File.Delete(path);
        }
    }
}
