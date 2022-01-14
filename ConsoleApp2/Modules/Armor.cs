using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Modules
{
    class Armor
    {
        public string name;
        public string type;
        public string effect;
        Config config = new Config();

        public Armor()
        {
            this.name = config.armorName[new Random().Next(0, config.armorName.Length)];
            this.type = config.armorTypes[new Random().Next(0, config.armorTypes.Length)];
            if (type == "obsidian")
            {
                this.effect = config.itemEffects[4];
            }
            else
            {
                this.effect = config.itemEffects[new Random().Next(0, config.itemEffects.Length)];
            }
        }

        public Armor(string name, string type, string effect)
        {
            this.name = name;
            this.type = type;
            this.effect = effect;
        }

        public void Info()
        {
            Console.WriteLine(
                $"name: {this.name}\n" +
                $"type: {this.type}\n" +
                $"effect: {this.effect}\n");
        }
        public string writer()
        {
            return $"{this.name}\n" +
                $"{this.type}\n" +
                $"{this.effect}\n";
        }
    }
}
