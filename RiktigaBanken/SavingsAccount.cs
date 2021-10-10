using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banken
{
    class SavingsAccount
    {
        public int accountNumber { get; set; }
        public string accountType { get; set; }
        public double accountBalance { get; set; }
        public double accountInterest { get; set; }



        public SavingsAccount(double _accountInterest, double _accountBalance, string _accountType, int _accountNumber)
        {
            this.accountBalance = _accountBalance;
            this.accountInterest = _accountInterest;
            this.accountNumber = _accountNumber;
            this.accountType = _accountType;
        }

    }

}

