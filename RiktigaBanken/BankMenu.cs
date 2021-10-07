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
                Console.WriteLine("\n\tVälkommen till Banken, vad vill du göra idag? \n\t(1) Visa kunder \n\t(2) Öppna ett konto \n\t(3) Avsluta och skriv ut saldo \n\t(4) Avsluta");
                int choice = 0;
                Int32.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:

                        BankLogic.CustomerMenuMethod();
                        
                        break;

                    case 2:

                        // Skapa nytt kund+konto - createAccount(double) för konto double = pengar - Christian
                        break;

                    case 3:



                        break;

                    case 4:


                        break;

                    case 5:
                        Console.WriteLine("Avslutar");
                        bankmenu = false;
                        break;


                    default:
                        Console.WriteLine("FEL");
                        break;

                
                }

            }
        }
    }
}