using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Util
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GetRandomCoupon()
        {
            int strlen = RandomNumberGenerator.GetInt32(1, 13);
            char[] randomChars = new char[strlen];

            for (int i = 0; i < strlen; i++)
            {
                randomChars[i] = chars[RandomNumberGenerator.GetInt32(0, chars.Length)];
            }

            return new string(randomChars);
        }


    }

}
