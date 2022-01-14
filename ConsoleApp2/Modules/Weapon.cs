using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Modules
{
    class Weapon
    {
        public string name;
        public string type;
        public int damage;
        Config config = new Config();

        public Weapon()
        {
            this.name = config.weaponsNames[new Random().Next(0, config.weaponsNames.Length)];
            this.type = config.weaponsTypes[new Random().Next(0, config.weaponsTypes.Length)];
            if (this.type == "chain weapon")
            {
                this.damage = new Random().Next(1, 100) * 2;
            }
            else
            {
                this.damage = new Random().Next(1, 100);
            }
        }

        public Weapon(string name, string type, int damage)
        {
            this.name = name;
            this.type = type;
            this.damage = damage;
        }

        public void Info()
        {
            Console.WriteLine(
                $"Name: {this.name}\n" +
                $"Type: {this.type}\n" +
                $"Damage: {this.damage}\n");
        }

        public string writer()
        {
            return $"{this.name}\n" +
                $"{this.type}\n" +
                $"{this.damage}\n";
        }
    }
}
