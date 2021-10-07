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
                newAccount.Add(CreateAccount(200.0));
                newAccount.Add(CreateAccount(200.0));
                newAccount.Add(CreateAccount(200.0));
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
        public static SavingsAccount CreateAccount()
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
        public static SavingsAccount CreateAccount(double money)
        {

            int accountnumber = 1000 + usedNumbers.Count + 1;
            SavingsAccount newAcc = new SavingsAccount(1/*interest*/, money, accountnumber);
            usedNumbers.Add(accountnumber);
            return newAcc;
        }




        public static void RemoveAccount(int customerIndex, int accountIndex) //Christoffer
        {
            customerList[customerIndex].accounts.RemoveAt(accountIndex);
        }


        public static async Task ChangeName(string newFirstName,string newLastName, int customerIndex) //static? void? //Christoffer
        {
            customerList[customerIndex].customerSureName = newFirstName;
            customerList[customerIndex].customerSureName = newLastName;

            await WriteText();
        }


        public static void Interest() //static? void? //Zacharias
        {

        }

        public static async Task RemoveCustomerAsync(int index) //sttic.... //Christoffer
        {
            customerList.RemoveAt(index);
            await WriteText();
        }


        public static void AddMoney()//Zacharias
        {

        }

        public static void RemoveMoney()//Zacharias
        {

        }

    }
}
