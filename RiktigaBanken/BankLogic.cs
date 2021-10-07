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
                rader[count] = item.WriteToString();
                count++;
            }
            await File.WriteAllLinesAsync("Kundista.txt", rader);
        }

        public static void ReadText()
        {
            string[] lines = File.ReadAllLines("Kundlista.txt");

            foreach (var item in lines)
            {
                string[] vektor = item.Split(new string[] { "###" }, StringSplitOptions.None);
                List<SavingsAccount> newAccount = new List<SavingsAccount>() { CreateAccount(double.Parse(vektor[3])) };
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

        public static void RemoveMoney()
        {

        }

        public static void CustomerMenuMethod()
        {
            Console.WriteLine("Välj en befintlig kund:");
            int number = 1;
            foreach (var customer in BankLogic.customerList)
            {

                Console.WriteLine($"({number}) {customer.ToString()}");
                number++;
            }
            int customerChoice = 0;
            Int32.TryParse(Console.ReadLine(), out customerChoice);
            if (customerChoice > 0 && customerChoice < BankLogic.customerList.Count +1)
            {
                Console.Clear();
                Console.WriteLine($"\n\tDu har valt \n\t{BankLogic.customerList[customerChoice - 1].ToString()}\n\tVad vill du göra? \n\t\t(1)Se konton och sätta in/ta ut pengar \n\t\t(2) Ändra namn på kunden");
                int customerEdit;
                Int32.TryParse(Console.ReadLine(), out customerEdit);
                switch (customerEdit)
                {
                    case 1:

                        for (int i = 0; i < BankLogic.customerList[customerChoice - 1].accounts.Count; i++)
                        {
                            Console.WriteLine($"({i + 1})Konto {BankLogic.customerList[customerChoice - 1].accounts[i].accountNumber.ToString()} innehåller följande mängd pengar {BankLogic.customerList[customerChoice - 1].accounts[i].getBalance()}");
                        }

                        Console.WriteLine("\n\tVilket göra en insättning till eller uttag från?");
                        int addRemove;
                        Int32.TryParse(Console.ReadLine(), out addRemove);
                        if (addRemove > 0 && addRemove < BankLogic.customerList[customerChoice - 1].accounts.Count)
                        {
                            int addRemove2;
                            Int32.TryParse(Console.ReadLine(), out addRemove2);
                            if (addRemove2 == 1)
                            {

                            }
                            else if (addRemove2 == 2)
                            {

                            }
                            else
                                Console.WriteLine("FELFELFELFEL");

                        }
                        break;

                    case 2:
                        Console.WriteLine("\n\tSkriv förnamn:");
                        string newSurname = Console.ReadLine();
                        Console.WriteLine("\n\tSkriv efternamn:");
                        string newLastname = Console.ReadLine();
                        ChangeName(newSurname, newLastname, customerChoice - 1);
                        break;


                    default:
                        Console.WriteLine("\n\tFelaktigt val");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
