using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Modules
{
    class Sphere
    {
        public string name;
        public string type;
        public string lootType;
        public int lootnum;
        Config config = new Config();

        public Sphere()
        {
            string[] loot = new string[] { "item", "armor", "weapon"};
            this.type = config.sphereType[new Random().Next(0, config.sphereType.Length)];
            this.name = this.type + " sphere";
            this.lootType = loot[new Random().Next(1, loot.Length)];
            switch (this.type)
            {
                case "common":
                    this.lootnum = 1;
                    break;

                case "uncommon":
                    this.lootnum = 2;
                    break;

                case "rare":
                    this.lootnum = 3;
                    break;

                case "epic":
                    this.lootnum = 4;
                    break;

                case "legend":
                    this.lootnum = 5;
                    break;
            }
        }

        public Sphere(string name, string type, string lootType)
        {
            this.name = name;
            this.type = type;
            this.lootType = lootType;
            switch (this.type)
            {
                case "common":
                    this.lootnum = 1;
                    break;

                case "uncommon":
                    this.lootnum = 2;
                    break;

                case "rare":
                    this.lootnum = 3;
                    break;

                case "epic":
                    this.lootnum = 4;
                    break;

                case "legend":
                    this.lootnum = 5;
                    break;
            }
        }

        public void Info()
        {
            Console.WriteLine(
                $"name: {this.name}\n" +
                $"type: {this.type}\n" +
                $"loot type: {this.lootType}");
        }

        public string writer()
        {
            return $"{this.name}\n" +
                $"{this.type}\n" +
                $"{this.lootType}\n";
        }
    }
}
