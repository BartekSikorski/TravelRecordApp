using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class User
    {
        public static bool Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
