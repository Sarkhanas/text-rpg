using ConsoleApp2.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Character
    {
        public string name;
        public int health;
        public int damage;
        public int resist;
        public Armor hat;
        public Armor arms;
        public Armor body;
        public Armor legs;
        public Weapon weapon;
        public List<Item> inventory = new List<Item>() { new Item() };
        public List<Sphere> spheres = new List<Sphere>() { new Sphere() };

        public Character(string name)
        {
            this.name = name;
            this.health = 100;
            this.weapon = new Weapon();
            this.damage = 1 + this.weapon.damage;
            this.resist = new Random().Next(1, 30);
        }

        public Character(string name, int health, int damage, int resist, Armor hat, Armor arms,
            Armor body, Armor legs, Weapon weapon, List<Item> inventory, List<Sphere> spheres)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.resist = resist;
            this.hat = hat;
            this.arms = arms;
            this.body = body;
            this.legs = legs;
            this.weapon = weapon;
            this.inventory = inventory;
            this.spheres = spheres;
        }

        public int Atack(Enemy enemy)
        {
            return enemy.health - (this.damage * enemy.resist / 100);
        }

        public void Info()
        {
            Console.WriteLine(
                $"name: {this.name}\n" +
                $"health: {this.health}\n" +
                $"damage: {this.damage}\n" +
                $"resist: {this.resist}%\n" +
                $"armor:\n");
            if (this.hat != null)
            {
                Console.WriteLine("hat: ");
                this.hat.Info();
            }
            if (this.arms != null)
            {
                Console.WriteLine("arms: ");
                this.arms.Info();
            }
            if (this.body != null)
            {
                Console.WriteLine("body: ");
                this.body.Info();
            }
            if (this.legs != null)
            {
                Console.WriteLine("legs: ");
                this.legs.Info();
            }
            if (this.weapon != null)
            {
                Console.WriteLine("weapon: ");
                this.weapon.Info();
            }
            Console.WriteLine("inventory: ");
            for (int i = 0; i < this.inventory.Count; i++)
            {
                inventory[i].Info();
            }

        }
    }
}
