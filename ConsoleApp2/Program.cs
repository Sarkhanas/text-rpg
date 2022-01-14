using ConsoleApp2.Modules;
using System;

namespace ConsoleApp2
{
    class Program
    {
        Config config = new Config();
        
        static void Main(string[] args)
        {
            Character Sarkhanas = new Character("Sarkhanas");
            Sarkhanas.Info();
            Sphere[] spheres = new Sphere[3];
            for (int i = 0; i < spheres.Length; i++)
            {
                spheres[i] = new Sphere();
            }
            for (int i = 0; i < spheres.Length; i++)
            {
                Gameplay.open(Sarkhanas, spheres[i]);
            }
            Sarkhanas.Info();
        }

        //functions for gameplay... yup it is idiotic
        
    }
}
