using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiktigaBanken
{
    class BankMenu
    {
        public static void Menu()
        {
            bool bankmenu = true;
            var blogic = new BankLogic();
            //DANIEL
            while (bankmenu)
            {
                Console.WriteLine("\n\tVälkommen till Banken, vad vill du göra idag? " +
                                  "\n\t(0) Visa kunder och sätt sätt in/ ta ut pengar " +
                                  "\n\t(1) Visa kundlista " + 
                                  "\n\t(2) Skapa kund " +
                                  "\n\t(3) Skapa konto " +
                                  "\n\t(4) Ändra kundnamn " +
                                  "\n\t(5) Avsluta kund och skriv ut saldo " +
                                  "\n\t(6) Avsluta och spara");
                int choice = 0;
                Int32.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 0:

                        CustomerMenuMethod();

                        break;
                    case 1:

                        blogic.PrintCustomers();

                        break;

                    case 2:

                        // Skapa nytt kund+konto - createAccount(double) för konto double = pengar - Christian
                        Console.Write("Förnamn: ");
                        var fname = Console.ReadLine();
                        Console.Write("Efternamn: ");
                        var lname = Console.ReadLine();
                        Console.Write("Personnummer (YYMMDDNNNN): ");
                        long pnr;
                        long.TryParse(Console.ReadLine(), out pnr);
                        var retbool = blogic.AddCustomer(fname, lname, pnr);
                        
                        if (retbool)
                        {
                            Console.WriteLine("Kund tillagd: " + fname + " " + lname);
                            Console.Write("Vill du öppna ett konto? [J/N]: ");
                            var answer = Console.ReadLine();

                            if (answer.ToLower() == "j")
                            {
                                Console.Write("Hur mycket vill du sätta in: ");
                                var amount = 0;
                                int.TryParse(Console.ReadLine(), out amount);

                                var accNr = blogic.AddSavingsAccount(pnr);

                                if (accNr > 0)
                                {
                                    Console.WriteLine("Konto " + accNr + " skapat.");
                                    blogic.Deposit(pnr, accNr, amount);
                                }
                            }
                            
                        }
                        else
                            Console.WriteLine("Kund finns redan.");

                        break;

                    case 3:
                        Console.Write ("Ange personnummer på kunden: ");
                        long.TryParse(Console.ReadLine(), out pnr);

                        var acc = blogic.AddSavingsAccount(pnr);

                        if (acc > 0)
                        {
                            Console.WriteLine("Konto " + acc + " skapat.");
                            Console.Write("Hur mycket vill du sätta in: ");
                            var amount = 0;
                            int.TryParse(Console.ReadLine(), out amount);
                            blogic.Deposit(pnr, acc, amount);

                        }
                        else
                        {
                            switch (acc)
                            {
                                case -1:
                                    Console.WriteLine("Konto kunde ej skapas.");
                                    break;
                                case -2:
                                    Console.WriteLine("Kund saknas.");
                                    break;
                                default:
                                    Console.WriteLine("Något gick fel.");
                                    break;
                            }
                        }

                        break;
                    case 4:
                        Console.Write("Ange personnummer på kunden: ");
                        long.TryParse(Console.ReadLine(), out pnr);
                        var cust = blogic.GetCustomer(pnr);
                        if (cust.Count > 0)
                        {
                            var oldfname = cust[0];
                            var oldlname = cust[1];

                            Console.Write("Skriv in nytt namn, tryck bara Enter för att lämna namnet oförändrad.");
                            Console.Write("Förnamn (" + oldfname + "): ");
                            fname = Console.ReadLine();
                            Console.Write("Efternamn(" + oldlname + "): ");
                            lname = Console.ReadLine();

                            if (fname == string.Empty)
                                fname = oldfname;
                            if (lname == string.Empty)
                                lname = oldlname;

                            retbool = blogic.ChangeCustomerName(fname, lname, pnr);

                            if (retbool)
                            {
                                Console.WriteLine("Kund uppdaterad.");
                            }
                            else
                            {
                                Console.WriteLine("Uppdatering misslyckades.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Kund med personnummer " + pnr.ToString() + " saknas.");
                        }
                        break;


                    case 5:

                        //Console.Write("Ange personnummer på kunden som ska tas bort: ");
                        //long.TryParse(Console.ReadLine(), out pnr);

                        //var retlist = blogic.RemoveCustomer(pnr);
                        //if (retlist.Count > 0)
                        //{
                        //    var l = retlist.Count;

                        //    Console.WriteLine("Kund " + retlist[0] + " borttagen");
                        //    Console.WriteLine("Följande konton togs bort:");
                        //    for (int i = 1; i < l - 1; i++)
                        //    {
                        //        Console.WriteLine(retlist[i]);
                        //    }

                        //    Console.WriteLine("Total saldo inkl ränta: " + retlist[l - 1]);
                        //}

                        //break;
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
                            BankLogic.RemoveCustomerAsync(customerChoice - 1);
                        }
                        else
                        { Console.WriteLine("FELFELFELFELFEL"); }

                        break;

                    case 6:
                        Console.WriteLine("Avslutar och sparar till Kundlista.txt");
                        BankLogic.WriteText();
                        bankmenu = false;
                        break;


                    default:
                        Console.WriteLine("FEL");
                        break;


                }

            }

            static void CustomerMenuMethod()
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
                if (customerChoice > 0 && customerChoice < BankLogic.customerList.Count + 1)
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
                            Console.WriteLine("\n\tVilket konto vill du göra en insättning till eller uttag från?");
                            int accPicker;
                            Int32.TryParse(Console.ReadLine(), out accPicker);
                            int addRemove;
                            Console.WriteLine("\n\t Vill du göra: \n\t(1)Insättning\n\t(2)Uttag");
                            Int32.TryParse(Console.ReadLine(), out addRemove);

                            if (accPicker > 0 && accPicker < BankLogic.customerList[customerChoice - 1].accounts.Count + 1)
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


                        default:
                            Console.WriteLine("\n\tFelaktigt val");
                            Console.ReadKey();
                            break;
                    }
                }
            }


        }
    }
}