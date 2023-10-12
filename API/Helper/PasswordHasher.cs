using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;

namespace APP.Helper
{


    public class PasswordHasher
    {

        public string HashPassword(string password)
        {
            // Hash the password using a work factor (e.g., 12) to determine the hashing time
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
            return hashedPassword;
        }

        public bool VerifyPassword(string password, string passwordToCompare)
        {
            // Verify a hashed password
            return BCrypt.Net.BCrypt.Verify(password, passwordToCompare);
        }
    }

}
