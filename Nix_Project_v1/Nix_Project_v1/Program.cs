using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Project_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            int exit;
            Administration admin = new Administration();
            ConsoleInterface console = new ConsoleInterface(admin);
            do
            {
                try
                {
                    exit = console.MainMenu();
                }
                catch (Exception e)
                {
                    exit = 1;
                    Console.WriteLine("Something went wrong! Try again. Press any key to continue.");
                    Console.ReadKey();
                }
            } while (exit != 0);
        }
    }
}
