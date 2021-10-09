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


        public static async Task WriteText() //skriver till textfil, både skriv och läsmetoderna använder sig av samma formatering för informationen så vi kan spara och läsa från samma fil.
        {
            int count = 0;
            string[] rader = new string[customerList.Count];
            foreach (var item in customerList)
            {
                    rader[count] = item.WriteToString();
                    count++;
            }
            await File.WriteAllLinesAsync("Kundlista.txt", rader);
        }

        public static void ReadText()
        {
            string[] lines = File.ReadAllLines("Kundlista.txt"); //läser in från textfil, varje rad har formatet FÖRNAMN###EFTERNAMN###PERSONNUMMER###SALDO###KONTONUMMER

            foreach (var item in lines)
            {
                string[] vektor = item.Split(new string[] { "###" }, StringSplitOptions.None);
                List<SavingsAccount> newAccount = new List<SavingsAccount>() { CreateAccount(double.Parse(vektor[3]), Int32.Parse(vektor[4]))};
                Customer newCustomer = new Customer(vektor[0], vektor[1], long.Parse(vektor[2]), newAccount);
                customerList.Add(newCustomer);
            }

        }



        public async Task<bool> AddCustomerAsync(string fname, string lname, long pNr)
        {
            var custexists = customerList.Any(c => c.customerPNR == pNr);

            if (custexists)
            {
                return false;
            }
            Customer newCust = new Customer(fname, lname, pNr);
            newCust.accounts.Add(BankLogic.CreateAccount(200));
            customerList.Add(newCust);
            await WriteText();
            return true;
        }


        public static List<int> usedNumbers = new List<int>(); //lista med alla använda kontonummer så att vi inte återanvänder samma 2 gånger (bör sparas till fil om man tar bort kunder)
        public SavingsAccount CreateAccount() //skapar ett kontonummer baserat på antalet existera
        {
            int accountnumber = 1000;
            foreach (Customer customer in BankLogic.customerList)
            {
                foreach (var item in customer.accounts)
                {
                    if (item.accountNumber == accountnumber && accountnumber != usedNumbers.IndexOf(accountnumber))
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

        public static SavingsAccount CreateAccount(double money, int accNumber) //constructors för att både göra ett nytt konto med en summa med ett kontonummer som tilldelas 
        {
            SavingsAccount newAcc = new SavingsAccount(1/*interest*/, money, accNumber);
            usedNumbers.Add(accNumber);
            return newAcc;
        }




        public static void RemoveAccount(int customerIndex, int accountIndex) //Christoffer
        {
            
            customerList[customerIndex].accounts.RemoveAt(accountIndex);
        }


        public static async Task ChangeName(string newFirstName, string newLastName, int customerIndex) //static? void? //Christoffer
        {
            
            customerList[customerIndex].customerSureName = newFirstName;
            customerList[customerIndex].customerLastName = newLastName;

            await WriteText();
        }


        public static void Interest() //static? void? //Zacharias
        {
            Console.WriteLine("Mata in ditt saldo");
            double saldo = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Mata in din räntesats");
            double ränta = Convert.ToDouble(Console.ReadLine());
            double räntaBetald = saldo * (ränta / 100);
            double totalPI = ränta + räntaBetald;
            Console.WriteLine("Saldo = " + saldo);
            Console.WriteLine("Ränta = " + ränta + "%");
            Console.WriteLine("Ränta betald = " + räntaBetald);
            Console.WriteLine("Totala summan = " + saldo + ränta);
        }

        public static async Task RemoveCustomerAsync(int index) //sttic.... //Christoffer
        {
            double sum = 0;
            foreach (var account in customerList[index].accounts)
            {
                sum = +account.getBalance();
            }
            Console.WriteLine($"Konton avslutade, totalt uttag i samband med detta är {sum}");
            customerList.RemoveAt(index);
            await WriteText();
        }


        public static void DepositMoney(Account acc)//Zacharias
        {

            Console.WriteLine("Hur mycket vill du sätta in?");
            int depositAmount = int.Parse(Console.ReadLine());
            if (depositAmount < 0)
            {
                Console.WriteLine("Du kan ej sätta in ett värde under 0");
            }
            acc.setBalance(acc.getBalance() + depositAmount);
            Console.WriteLine("Ditt nya saldo är " + acc.getBalance());


        }

        public static async Task WithdrawMoney(Account acc)//Zacharias
        {
            bool withDraw = true;
            while (withDraw)
            {
                Console.WriteLine("\n Hur mycket vill du ta ut?");
                Console.WriteLine("\n Välj mellan 500, 1000, 5000, 10000");
                int withdrawalAmount = int.Parse(Console.ReadLine());
                if (withdrawalAmount != 500 && withdrawalAmount != 1000 && withdrawalAmount != 5000 && withdrawalAmount != 10000)
                {
                    Console.WriteLine("Felaktig inmatning, var god försök igen");
                }
                else if (withdrawalAmount < 0)
                {
                    Console.WriteLine("Du kan ej ta ut ut ett värde under 0");
                }
                else if (acc.getBalance() < withdrawalAmount)
                {
                    Console.WriteLine("Du har ej tillräckligt saldo för att ta ut");
                }
                else
                {
                    Console.WriteLine("Var god ta pengarna");
                    acc.setBalance(acc.getBalance() - withdrawalAmount);
                    Console.WriteLine("Ditt nya saldo är " + acc.getBalance());
                    await WriteText();
                    Console.ReadKey();
                    withDraw = false;
                }
            }
            


        }
    }


}
        
    

