using System;

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
                Console.ReadKey();
            }
            Console.ForegroundColor = ConsoleColor.White; //vit text på blå bakgrund för en riktig bankomatfeeling
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            BankMenu.Menu();



        }
    }
}
