﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RiktigaBanken
{
    
        abstract class Account
        {
            public int accountNumber { get; set; }
            public string accountType { get; set; }
            protected double accountBalance { get; set; }
            public double accountInterest { get; set; }


        public double getBalance()
        {
            return accountBalance;
        }

        }
        class SavingsAccount : Account
        {
            public SavingsAccount(double _accountInterest, double _accountBalance, int _accountNumber) /*: base(_accountInterest,_accountBalance, _accountNumber)*/
            {
                this.accountBalance = _accountBalance;
                this.accountInterest = 1;
                this.accountNumber = _accountNumber;
                this.accountType = "SavingsAccount";
            }

            


        }
        class SalaryAccount : Account
        {
            public SalaryAccount(double _accountInterest, double _accountBalance, int _accountNumber) /*: base(_accountInterest, _accountBalance, _accountNumber)*/
            {
                this.accountBalance = _accountBalance;
                this.accountInterest = 0;
                this.accountNumber = _accountNumber;
                this.accountType = "SalaryAccount";
            }



        }
    
}
//Customer Daniel = new Customer("Daniel", "Lenberg", 12345678 );
//Daniel.accounts.Add(BankLogic.CreateAccount(200.0));


//Console.WriteLine(Daniel.ToString());
//foreach (var item in Daniel.accounts)
//{
//    Console.WriteLine($"{item.accountNumber} {item.accountInterest} {item.accountType} {item.getBalance()}"  ); 
//}