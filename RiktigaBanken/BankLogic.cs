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
        private int AccountNumber { get; set; }  = 1001;  //Christian
        public static List<Customer> customerList = new List<Customer>();


        public static void WriteText()
        {
            int count = 0;
            string[] rader = new string[customerList.Count];
            foreach (var item in customerList)
            {
                rader[count] = item.WriteToString();
                count++;
            }
            File.WriteAllLines("Kundlista.txt", rader);
        }

        public static void ReadText()
        {
            if (File.Exists("Kundlista.txt"))
            {
                string[] lines = File.ReadAllLines("Kundlista.txt");

                foreach (var item in lines)
                {
                    string[] vektor = item.Split(new string[] {"###"}, StringSplitOptions.None);
                    //List<SavingsAccount> newAccount = new List<SavingsAccount>();
                    //                       {CreateAccount(double.Parse(vektor[3]))};
                    //                  Customer newCustomer = new Customer(vektor[0], vektor[1], long.Parse(vektor[2]), newAccount);
                    //                customerList.Add(newCustomer);
                }
            }

        }

        public void PrintCustomers() //Christian
        {
            foreach (Customer customer in customerList)
            {
                {
                    Console.WriteLine(customer.ToString());
                }

            }
        }

        public List<string> GetCustomer(long pNr) //Christian
        {
            var cust = customerList.FirstOrDefault(c => c.customerPNR == pNr);

            var custlist = new List<string>();

            if (cust != null)
            {
                custlist.Add(cust.customerSureName);
                custlist.Add(cust.customerLastName);
            }

            return custlist;

        }

        public bool AddCustomer(string fname, string lname, long pNr) //Christian
        {
            var custexists = customerList.Any(c => c.customerPNR == pNr); //hämta aktuell kund från kundlistan

            if (custexists)  //finns kunden redan går det inte att skapa nytt
            {
                return false;
            }
            Customer newCust = new Customer(fname, lname, pNr);  //skapa ny kund i kundlistan
            customerList.Add(newCust);
            
            return true;
        }

        public int AddSavingsAccount(long pNr) //Christian
        {
            int accountNr = AccountNumber;  //hämta kontonr från globala property

            foreach (var cst in customerList)  //kolla att kontonr inte finns redan
            {
                var acc = cst.accounts.FirstOrDefault(a => a.accountNumber == accountNr);

                if (acc != null)  //konto finns redan hos en kund
                {
                    return -1;
                }
            }

            
                var cust = customerList.FirstOrDefault(k => k.customerPNR == pNr); //hämta kund som vill skapa konto

                if (cust != null)  //kund finns, skapa konto
                {
                    var acc = new SavingsAccount(1.15, 0, accountNr);

                    cust.accounts.Add(acc);  //lägg till konto i kundens konto-lista
                    this.AccountNumber++; //sätt kontonr på BankLogic-objektet till nästa nummer.
                }
                else
                {
                    return -2;
                }

            
            return accountNr;
        }

        public static List<int> usedNumbers = new List<int>();
        public SavingsAccount CreateAccount()
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



        public bool ChangeCustomerName(string fname, string lname, long pNr)  //Christian
        {
            var cust = customerList.FirstOrDefault(c => c.customerPNR == pNr);  //hämta aktuell kund från kundlistan

            if (cust == null) //hittar inte kunden
                return false;

            cust.customerSureName = fname;  //ändra namn på kunden
            cust.customerLastName = lname;

            return true;
        }



        public bool Deposit(long pNr, int accountId, double amount)  //Christian
        {
            var cust = customerList.FirstOrDefault(c => c.customerPNR == pNr);  //hämta aktuell kund från kundlistan

            if (cust == null) //hittar inte kunden
                return false;

            var acc = cust.accounts.FirstOrDefault(a => a.accountNumber == accountId); //hämta aktuell konto från kundens kontolista

            if (acc == null) //hittar inte konto
                return false;

            acc.setBalance(acc.getBalance() + amount); //öka saldot med angivet belopp 

            return true;
        }

        public bool Withdraw(long pNr, int accountId, double amount)  //Christian
        {
            var cust = customerList.FirstOrDefault(c => c.customerPNR == pNr);  //hämta aktuell kund

            if (cust == null)  //kund finns inte
                return false;

            var acc = cust.accounts.FirstOrDefault(a => a.accountNumber == accountId); //hämta konto från kundens kontolista

            if (acc == null) //konto finns inte
                return false;

            if (acc.getBalance() - amount < 0) // kolla att beloppet att ta ut inte överskrider saldot
                return false;

            acc.setBalance(acc.getBalance() - amount);  //sätt nytt saldo

            return true;
        }

        public List<string> RemoveCustomer(long pNr) //Christian
        {
            var total = 0.0;

            var cust = customerList.FirstOrDefault(c => c.customerPNR == pNr);  //hämta aktuell kund att ta bort

            var custlist = new List<string>(); //lista att skicka tillbaka med kunduppgifter

            if (cust != null)
            {
                custlist.Add(cust.customerSureName + " " + cust.customerLastName);  //första raden i listan är för- och efternamn

                foreach (var acc in cust.accounts)  //loopa igenom alla konton för kunden
                {
                    total = total + acc.getBalance() + acc.getBalance() * (acc.accountInterest / 100);
                    custlist.Add(acc.accountNumber.ToString());  //lägg till rad med kontonr samt saldo inkl ränta
                }

                custlist.Add(total.ToString()); //lägg till totala saldot av alla konto inkl ränta sist i listan
                customerList.Remove(cust);  //ta bort kund inkl konton från kundlistan
            }


            return custlist;
        }

        public static void RemoveAccount(int customerIndex, int accountIndex) //Christoffer
        {

            customerList[customerIndex].accounts.RemoveAt(accountIndex);
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
            //await WriteText();
            WriteText();
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

        public static void WithdrawMoney(Account acc)//Zacharias
        {
            Console.WriteLine("\n Hur mycket vill du ta ut?");
            Console.WriteLine("\n Välj mellan 500, 1000, 5000, 10000");
            int withdrawalAmount = int.Parse(Console.ReadLine());
            if (withdrawalAmount != 500 && withdrawalAmount != 1000 && withdrawalAmount != 5000 && withdrawalAmount != 10000)
            {
                Console.WriteLine("Felaktig inmatning, var god försök igen");
            }
            if (withdrawalAmount < 0)
            {
                Console.WriteLine("Du kan ej ta ut ut ett värde under 0");
            }
            if (acc.getBalance() < withdrawalAmount)
            {
                Console.WriteLine("Du har ej tillräckligt saldo för att ta ut");
            }
            else
            {
                Console.WriteLine("Var god ta pengarna");
                acc.setBalance(acc.getBalance() - withdrawalAmount);
                Console.WriteLine("Ditt nya saldo är " + acc.getBalance());

            }


        }
        public static async Task ChangeName(string newFirstName, string newLastName, int customerIndex) //static? void? //Christoffer
        {

            customerList[customerIndex].customerSureName = newFirstName;
            customerList[customerIndex].customerLastName = newLastName;

            //await WriteText();
            WriteText();
        }

    }


}
        
    

