using ConsoleApp2.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Character
    {
        public int winner = 0; 
        public string name;
        public int maxHP = 100;
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
        public List<string> effects = new List<string>();
        Config config = new Config();

        public Character(string name)
        {
            this.name = name;
            this.health = 100;
            this.weapon = new Weapon();
            this.damage = 1 + this.weapon.damage;
            this.resist = new Random().Next(1, 30);
        }

        public Character(string name, int maxHP, int health, int damage, int resist, Armor hat, Armor arms,
            Armor body, Armor legs, Weapon weapon, List<Item> inventory, List<Sphere> spheres)
        {
            this.name = name;
            this.maxHP = maxHP;
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
            Console.Write($"You dealt {this.damage - this.damage * enemy.resist / 100} damage to the enemy");
            return this.damage - this.damage * enemy.resist / 100;
        }

        public void Equip(Weapon weapon)
        {
            this.damage -= this.weapon.damage;

            this.weapon = weapon;

            this.damage += this.weapon.damage;
        }

        public void Equip(Armor armor)
        {
            switch (armor.effect)
            {
                case "heal":
                    this.health += 45;
                    if (this.health > this.maxHP)
                    {
                        this.health = this.maxHP;
                    }
                    break;

                case "resist":
                    this.resist += this.resist > 55 ? (15 + this.resist) * 5 / 100 : 15;
                    if (this.resist > 95)
                    {
                        this.resist = 95;
                    }
                    break;

                case "power":
                    this.damage += 10;
                    break;

                case "Max HP":
                    this.maxHP += 15;
                    break;

                case "glazing":
                    this.health = 1;

                    this.damage += this.health > 1 ?
                        30 + (this.health * 99) / 100
                        :
                        30 + (this.maxHP * 99) / 100;

                    break;
            }
            if (armor.name == config.armorName[0])
            {
                this.hat = armor;
            }
            if (armor.name == config.armorName[1])
            {
                this.body = armor;
            }
            if (armor.name == config.armorName[2])
            {
                this.legs = armor;
            }
            if (armor.name == config.armorName[3])
            {
                this.arms = armor;
            }
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
                if(this.hat.name != "")
                {
                    Console.WriteLine("hat: ");
                    this.hat.Info();
                }
            }
            if (this.arms != null)
            {
                if (this.arms.name != "")
                {
                    Console.WriteLine("arms: ");
                    this.arms.Info();
                }
                
            }
            if (this.body != null)
            {
                if (this.body.name != "")
                {
                    Console.WriteLine("body: ");
                    this.body.Info();
                }
                
            }
            if (this.legs != null)
            {
                if (this.legs.name != "")
                {
                    Console.WriteLine("legs: ");
                    this.legs.Info();
                }
            }
            if (this.weapon != null)
            {
                if (this.weapon.name != "")
                {
                    Console.WriteLine("weapon: ");
                    this.weapon.Info();
                }
            }
            Console.WriteLine("inventory: ");
            for (int i = 0; i < this.inventory.Count; i++)
            {
                this.inventory[i].Info();
                Console.WriteLine();
            }
            Console.WriteLine("spheres: ");
            for (int i = 0; i < this.spheres.Count; i++)
            {
                this.spheres[i].Info();
                Console.WriteLine();
            }
        }
    }
}
