using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.RegisterUser
{
    public static class PasswordEncryptor
    {
       

        public static string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var byteHashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(byteHashed).Replace("-", "").ToLower();

                return hash;
            }
        }

        public static bool VerifyPassword(string enteredPassword ,string hashedPassword)
        {
            if(EncryptPassword(enteredPassword) == hashedPassword)
            {
                return true;
            }
            return false;
           
        }
    }
}
