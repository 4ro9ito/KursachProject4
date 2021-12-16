using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var Menu = new Menu();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Добро пожаловать в курсовую работу! (11 вариант) Доступные опции:");
            Menu.MainMenu();
        }
    }
}
