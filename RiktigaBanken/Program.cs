using System;

namespace RiktigaBanken
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { BankLogic.ReadText(); }
            catch (Exception e)
            {
                Console.WriteLine($"Kan inte öppna Kundlista.txt för att {e}");
                Console.ReadKey();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            BankMenu.Menu();



        }
    }
}
