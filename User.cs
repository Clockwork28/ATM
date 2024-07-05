using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTS
{
   public class Account
    {
        public string accLogin;
        public int accPin;
        public string accName;
        public int accNum;
        public string accType;
        public double accBalance;
        public string accStatus;
        public static int accCount = 0;

        public Account(string aAccLogin, int aAccPin, string aAccName,string aAccType, double aAccBalance, string aAccStatus)
        {
            accCount++;
            accLogin = aAccLogin;
            accPin = aAccPin;
            accName = aAccName;
            accNum = accCount;
            accType = aAccType;
            accBalance = aAccBalance;
            accStatus = aAccStatus;
         

            //Console.WriteLine($"Account Successfully Created – the account number assigned is: {accNum}");
        }

    }

}
