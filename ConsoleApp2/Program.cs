using ConsoleApp2.Modules;
using System;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        
        Config config = new Config();
        Gameplay gp = new Gameplay();
        
        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Character player;

            ConsoleKeyInfo key;
            do
            {
                string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
                var filePathAndName = Path.Combine(appRootDir, "..\\Profile\\profile.txt");
                var exists = File.Exists(filePathAndName);

                if (exists == false)
                {
                    Console.Write(
                    "!!! TO SELECT, USE THE BUTTONS SPECIFIED TO THE LEFT OF THE SECTION !!!\n" +
                    "\n(1) Start\n" +
                    "\n(ESC) Exit\n"
                    );
                }
                else
                {
                    Console.Write(
                    "!!! TO SELECT, USE THE BUTTONS SPECIFIED TO THE LEFT OF THE SECTION !!!\n" +
                    "\n(1) Continue\n" +
                    "(2) New Game\n" +
                    "(ESC) Exit\n"
                    );
                }

                key = Console.ReadKey();

                switch(key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        player = Gameplay.login();
                        player.Info();
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        if (exists == false)
                        {
                            goto default;
                        }
                        player = Gameplay.register();
                        Gameplay.save(player);
                        goto case ConsoleKey.D1;

                    default:
                        break;
                }

            }
            while (key.Key != ConsoleKey.Escape);
        }
        
    }
}
