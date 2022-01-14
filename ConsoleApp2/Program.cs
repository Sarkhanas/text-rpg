using ConsoleApp2.Modules;
using System;

namespace ConsoleApp2
{
    class Program
    {
        Config config = new Config();
        
        static void Main(string[] args)
        {
            Gameplay gp = new Gameplay();
            Character player = new Character("player");
            gp.fight(player);
        }

        //functions for gameplay... yup it is idiotic
        
    }
}
