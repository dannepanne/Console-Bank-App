using System;
using System.IO;

namespace RiktigaBanken
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { BankLogic.ReadText(); }
            catch (Exception e) //om ingen fil finns får vi ett felmeddelande men programmet kan köras
            {
                Console.WriteLine($"Kan inte öppna Kundlista.txt för att {e}");
                File.WriteAllText("Kundlista.txt", "Christoffer###korell###3321035365###1337###1000");
                BankLogic.ReadText();
                Console.ReadKey();
            }
            Console.ForegroundColor = ConsoleColor.White; //vit text på blå bakgrund för en riktig bankomatfeeling
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            BankMenu.Menu();



        }
    }
}
