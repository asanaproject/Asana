using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Utilities
{
   public class PasswordHasher
    {
        public static string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            try
            {
                using (var algorithm = new System.Security.Cryptography.SHA512Managed())
                {
                    hashBytes = algorithm.ComputeHash(bytes);
                }
                return Convert.ToBase64String(hashBytes);
            }
            finally
            {
                Log.Error("Problem With Encrypt");

            }


        }
    }
}
