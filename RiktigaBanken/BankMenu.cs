using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RiktigaBanken
{
    class BankMenu
    {
        public static void Menu()
        {
            Console.Clear();
            bool bankmenu = true;
            var blogic = new BankLogic();
            //DANIEL
            while (bankmenu) //bankmeny för val och navigering i banken
            {
                Console.Clear();
                Console.WriteLine("\n\tVälkommen till Banken, vad vill du göra idag? \n\t(1) Visa kunder och sätt sätt in/ ta ut pengar \n\t(2) Ny kund  \n\t(3) Avsluta och skriv ut saldo \n\t(4) Räntekalkylator \n\t(5) Avsluta och spara");
                int choice = 0;
                Int32.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:

                        CustomerMenuMethod();

                        break;

                    case 2:
                        bool check = true;

                        // Skapa nytt kund+konto - createAccount(double) för konto double = pengar - Christian
                        while (check)
                        {
                            Console.Write("Förnamn: ");
                            var fname = Console.ReadLine();
                            Console.Write("Efternamn: ");
                            var lname = Console.ReadLine();
                            Console.Write("Personnummer (YYMMDDNNNN): ");
                            long pnr;
                            long.TryParse(Console.ReadLine(), out pnr);
                            Customer cCheck = BankLogic.customerList.Find(c => c.customerPNR == pnr);
                            long PNRcheck;
                            if (cCheck != null)
                            {
                                PNRcheck = cCheck.customerPNR;
                            } else
                            {
                                PNRcheck = 99999999999;
                            }
                            string pnrCheck = pnr.ToString();
                            if (fname != null && lname != null && pnrCheck.Length == 10)
                            {
                                if (PNRcheck != pnr)
                                {
                                    blogic.AddCustomerAsync(fname, lname, pnr);
                                    Console.WriteLine("Kund tillagd: " + fname + lname);
                                    Thread.Sleep(3000);
                                    check = false;

                                } else
                                    Console.WriteLine("Kund finns redan.");
                                    check = false;
                            }
                            else
                            {
                                Console.WriteLine("Felaktigt inmatade kunduppgifter, försök igen");
                            }
                            

                        }


                        break;
               
                        
                    case 3:
                        int number = 1;
                        Console.WriteLine("Vilken kund med tillhörande konto/n ska avslutas?");
                        foreach (var customer in BankLogic.customerList)
                        {

                            Console.WriteLine($"({number}) {customer.ToString()}");
                            number++;
                        }
                        int customerChoice = 0;
                        Int32.TryParse(Console.ReadLine(), out customerChoice);
                        if (customerChoice > 0 && customerChoice < BankLogic.customerList.Count + 1)
                        {
                            BankLogic.RemoveCustomerAsync(customerChoice  - 1);                           
                            Console.ReadKey();
                        }
                        else
                        { Console.WriteLine("FELFELFELFELFEL"); }

                            break;

                    case 4:
                        BankLogic.Interest();
                        break;

                    case 5:
                        Console.WriteLine("Avslutar och sparar till Kundlista.txt");
                        BankLogic.WriteText();
                        bankmenu = false;
                        break;


                    default:
                        Console.WriteLine("FEL");
                        break;


                }

            }


            static void CustomerMenuMethod() //meny för att navigera mellan kunder och ta ut /sätta in pengar.
            {
                Console.Clear();
                Console.WriteLine("Välj en befintlig kund:");
                int number = 1;
                foreach (var customer in BankLogic.customerList)
                {

                    Console.WriteLine($"({number}) {customer.ToString()}");
                    number++;
                }
                int customerChoice = 0;
                Int32.TryParse(Console.ReadLine(), out customerChoice);
                if (customerChoice > 0 && customerChoice < BankLogic.customerList.Count + 1)
                {
                    Console.Clear();
                    Console.WriteLine($"\n\tDu har valt \n\t{BankLogic.customerList[customerChoice - 1].ToString()}\n\tVad vill du göra? \n\t\t(1) Se konton och sätta in/ta ut pengar \n\t\t(2) Ändra namn på kund \n\t\t(3) Öppna ett nytt konto");
                    int customerEdit;
                    Int32.TryParse(Console.ReadLine(), out customerEdit);
                    switch (customerEdit)
                    {
                        case 1:

                            Console.WriteLine("\n\tVilket konto vill du göra en insättning till eller uttag från?");
                            for (int i = 0; i < BankLogic.customerList[customerChoice - 1].accounts.Count; i++)
                            {
                                Console.WriteLine($"\n\t({i + 1})Konto {BankLogic.customerList[customerChoice - 1].accounts[i].accountNumber.ToString()} innehåller följande mängd pengar {BankLogic.customerList[customerChoice - 1].accounts[i].getBalance()}");
                            }
                            
                            int accPicker;
                            Int32.TryParse(Console.ReadLine(), out accPicker);
                            int addRemove;
                            Console.WriteLine("\n\tVill du göra: \n\t(1) Insättning \n\t(2) Uttag");
                            Int32.TryParse(Console.ReadLine(), out addRemove);
                            
                            if (accPicker > 0 && accPicker < BankLogic.customerList[customerChoice - 1].accounts.Count +1)
                            {
                                if (addRemove == 1)
                                {
                                    BankLogic.DepositMoney(BankLogic.customerList[customerChoice - 1].accounts[accPicker - 1]);
                                }
                                else if (addRemove == 2)
                                {
                                    BankLogic.WithdrawMoney(BankLogic.customerList[customerChoice - 1].accounts[accPicker - 1]);
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
                            BankLogic.ChangeName(newSurname, newLastname, customerChoice - 1);
                            break;

                        case 3:
                            Console.WriteLine("Hur mycket pengar vill du sätta in på ditt sprillans nya konto?");
                            string inMoney = Console.ReadLine();
                            double newMoney = BankLogic.GetComma(inMoney);
                            SavingsAccount newAccount = BankLogic.CreateAccount(newMoney);
                            BankLogic.customerList[customerChoice - 1].accounts.Add(newAccount);
                            Console.WriteLine("Nytt konto skapat");
                            Console.ReadKey();


                            break;

                        default:
                            Console.WriteLine("\n\tFelaktigt val");
                            Console.ReadKey();
                            break;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Felaktigt val, återgår till huvudmenyn");
                    Console.ReadKey();
                }
            }
        }
    }
}