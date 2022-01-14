using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Modules
{
    class Item
    {
        public string name;
        public string type;
        public string effect;
        Config config = new Config();

        public Item()
        {
            this.type = config.itemTypes[new Random().Next(0, config.itemTypes.Length)];

            if (this.type == "scroll")
            {
                this.name = this.type;
                this.effect = config.itemEffects[3];
            }

            if (this.type == "food")
            {
                this.name = config.foodNames[new Random().Next(0, config.foodNames.Length)];
                if (this.name == "pizza")
                {
                    this.effect = config.itemEffects[3];
                }
                else if (this.name == "mushroom;)")
                {
                    this.effect = config.itemEffects[4];
                }
                else
                {
                    this.effect = config.itemEffects[new Random().Next(0, config.itemEffects.Length)];
                }
            }

            if (this.type == "potion")
            {
                int rnd = new Random().Next(0, config.potionNames.Length);
                this.name = config.potionNames[rnd];
                this.effect = config.itemEffects[rnd];
            }
        }

        public Item(string name, string type, string effect)
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
