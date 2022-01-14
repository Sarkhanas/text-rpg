using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Enemy
    {
        public string name;
        public int health;
        public int damage;
        public int resist;
        Config config = new Config();

        public Enemy()
        {
            this.name = config.enemyNames[new Random().Next(0, config.enemyNames.Length)];
            this.health = new Random().Next(5, 100);
            this.damage = new Random().Next(1, 50);
            this.resist = new Random().Next(1, 50);
        }

        public int Atack(Character player)
        {
            return player.health - (this.damage * player.resist / 100);
        }

        public void Info()
        {
            Console.WriteLine(
                $"name: {this.name}\n" +
                $"health: {this.health}\n" +
                $"damage: {this.damage}\n" +
                $"resist: {this.resist}%\n");
        }
    }
}
