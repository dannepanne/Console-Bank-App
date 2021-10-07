using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RiktigaBanken
{
    class BankLogic
    {
        public static List<Customer> customerList = new List<Customer>();


        public static async Task WriteText()
        {
            int count = 0;
            string[] rader = new string[customerList.Count];
            foreach (var item in customerList)
            {
                rader[count] = item.ToString();
                count++;
            }
            await File.WriteAllLinesAsync("NyKundLista.txt", rader);
        }

        public static void ReadText()
        {
            string[] lines = File.ReadAllLines("Kundlista.txt");

            foreach (var item in lines)
            {
                List<SavingsAccount> newAccount = new List<SavingsAccount>();
                string[] vektor = item.Split(new string[] { "###" }, StringSplitOptions.None);
                Customer newCustomer = new Customer(vektor[0], vektor[1], long.Parse(vektor[2]), newAccount);
                customerList.Add(newCustomer);
            }

        }

        

        public bool AddCustomer(string fname, string lname, long pNr)
        {
            var custexists = customerList.Any(c => c.customerPNR == pNr);

            if (custexists)
            {
                return false;
            }
            customerList.Add(new Customer(fname, lname, pNr));

            return true;
        }


        public static List<int> usedNumbers = new List<int>();
        public  SavingsAccount CreateAccount()
        {
            int accountnumber = 1000;
            foreach (Customer customer in BankLogic.customerList)
            {
                foreach (var item in customer.accounts)
                {
                    if (item.accountNumber == accountnumber)
                    {
                        accountnumber++;
                    }
                }

            }
            SavingsAccount newAcc = new SavingsAccount(1, 0, accountnumber);
            usedNumbers.Add(accountnumber);
            return newAcc;

        }
        public  SavingsAccount CreateAccount(double money, long pnr)
        {

            int accountnumber = 1000 + usedNumbers.Count + 1;
            SavingsAccount newAcc = new SavingsAccount(1/*interest*/, money, accountnumber);
            usedNumbers.Add(accountnumber);
            return newAcc;
        }




        public static void RemoveAccount() //Christoffer
        {

        }


        public static void ChangeName() //static? void? //Christoffer
        {

        }


        public static void Interest() //static? void? //Zacharias
        {

        }

        public static void RemoveCustomer() //sttic.... //Christoffer
        {

        }


        public static void AddMoney()//Zacharias
        {

        }

        public static void RemoveMoney()//Zacharias
        {

        }

    }
}
