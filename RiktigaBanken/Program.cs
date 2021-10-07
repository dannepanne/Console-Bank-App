using System;

namespace RiktigaBanken
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BankLogic.ReadText();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            BankMenu.Menu();
            BankLogic.WriteText();


        }
    }
}
