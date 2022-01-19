using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace ConsoleApp2.Modules
{
    class Gameplay
    {
        public Gameplay() {}

        public static Character register()
        {
            try
            {
                Console.Write("Write your nickname: ");
                Character player = new Character(Console.ReadLine());

                save(player);

                return player;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return null;
            }
        }
        
        public static Character login()
        {
            try
            {
                string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
                var filePathAndName = Path.Combine(appRootDir, "..\\Profile\\profile.txt");

                if (!File.Exists(filePathAndName))
                {
                    return register();
                }

                StreamReader sr = new StreamReader(filePathAndName);
                string lin = sr.ReadLine();
                string[] inf = new string[32];
                inf[0] = lin;
                int p = 1;
                do
                {
                    lin = sr.ReadLine();
                    inf[p] += lin;
                    p++;
                }
                while (lin != null);
                List<Item> inventory = new List<Item>();
                int itemIndexEnd = 0;
                for (int l = 22; l < inf.Length; l++)
                {
                    if (inf[l] == "---")
                    {
                        itemIndexEnd = l;
                        break;
                    }
                }
                for (int i = 22; i < itemIndexEnd; i += 3)
                {
                    inventory.Add(new Item(inf[i], inf[i + 1], inf[i + 2]));
                }
                List<Sphere> spheres = new List<Sphere>();
                for (int i = itemIndexEnd + 1; i < inf.Length - 3; i += 3)
                {
                    spheres.Add(new Sphere(inf[i], inf[i + 1], inf[i + 2]));
                }
                Character player = new Character(
                    inf[0], int.Parse(inf[1]), int.Parse(inf[2]), int.Parse(inf[3]),
                    new Armor(inf[5], inf[6], inf[7]), new Armor(inf[8], inf[9], inf[10]),
                    new Armor(inf[11], inf[12], inf[13]), new Armor(inf[14], inf[15], inf[16]),
                    new Weapon(inf[18], inf[19], int.Parse(inf[20])),
                    inventory, spheres);
                sr.Close();
                return player;
            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return null;
            }
            
        }

        public static void save(Character player)
        {
            string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
            var filePathAndName = Path.Combine(appRootDir, "..\\Profile\\profile.txt");
            string inv = "";
            string spher = "";
            for (int i = 0; i < player.inventory.Count; i++)
            {
                inv += player.inventory[i].writer();
            }
            for (int i = 0; i < player.spheres.Count; i++)
            {
                spher += player.spheres[i].writer();
            }
            File.WriteAllText(filePathAndName,
                $"{player.name}\n" +
                $"{player.health}\n" +
                $"{player.damage}\n" +
                $"{player.resist}\n" +
                $"---\n" +
                $"{(player.hat == null ? new Armor(null, null, null).writer() : player.hat.writer())}" +
                $"{(player.arms == null ? new Armor(null, null, null).writer() : player.arms.writer())}" +
                $"{(player.body == null ? new Armor(null, null, null).writer() : player.body.writer())}" +
                $"{(player.legs == null ? new Armor(null, null, null).writer() : player.legs.writer())}" +
                $"---\n" +
                $"{player.weapon.writer()}" +
                $"---\n" +
                $"{inv}" +
                $"---\n" +
                $"{spher}");


            StreamWriter sw = new StreamWriter(filePathAndName, true, Encoding.UTF8);
            sw.Close();
        }

        public static void open(Character player, Sphere sphere)
        {
            Config config = new Config();
            switch (sphere.lootType)
            {
                case "item":
                    Item[] lootItems = new Item[sphere.lootnum];
                    for (int i = 0; i < lootItems.Length; i++)
                    {
                        lootItems[i] = new Item();
                    }
                    for (int i = 0; i < lootItems.Length; i++)
                    {
                        lootItems[i].Info();
                        Console.Write("Wanna take this Item?(yes = Y, no = N)");
                        string answer = Console.ReadLine();
                        if (answer == "Y")
                        {
                            player.inventory.Add(lootItems[i]);
                        }
                    }
                    break;

                case "armor":
                    Armor[] lootArmor = new Armor[sphere.lootnum];
                    for (int i = 0; i < lootArmor.Length; i++)
                    {
                        lootArmor[i] = new Armor();
                    }
                    for (int i = 0; i < lootArmor.Length; i++)
                    {
                        lootArmor[i].Info();
                        Console.Write("Wanna take this Item?(yes = Y, no = N)");
                        string answer = Console.ReadLine();
                        if (answer == "Y")
                        {
                            player.Equip(lootArmor[i]);
                        }
                    }
                    break;

                case "weapon":
                    Weapon[] lootWeapon = new Weapon[sphere.lootnum];
                    for (int i = 0; i < lootWeapon.Length; i++)
                    {
                        lootWeapon[i] = new Weapon();
                    }
                    for (int i = 0; i < lootWeapon.Length; i++)
                    {
                        lootWeapon[i].Info();
                        Console.Write("Wanna take this Item?(yes = Y, no = N)");
                        string answer = Console.ReadLine();
                        if (answer.ToUpper() == "Y")
                        {
                            player.Equip(lootWeapon[i]);
                        }
                    }
                    break;
            }
        }

        public void fight(Character player)
        {
            Random rnd = new Random();
            ConsoleKeyInfo key;
            string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
            var filePathAndName = Path.Combine(appRootDir, "..\\Texts\\fight.txt");
            StreamReader sr = new StreamReader(filePathAndName);
            string dialogs = "";
            string fightDialogs = "";
            do
            {
                dialogs = sr.ReadLine();
                fightDialogs += dialogs;
            }
            while (dialogs != null);
            string[] startFightDialogs = fightDialogs.Split(new char[] { '&' });
            Console.WriteLine(startFightDialogs[rnd.Next(0, startFightDialogs.Length)]);
            Enemy enemy = new Enemy();
            enemy.Info();
            Thread.Sleep(5000);
            do
            {
                Console.Clear();
                Console.WriteLine("What you gonna do?\n(1) atack (2) item (0) escape");
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        enemy.health -= player.Atack(enemy);
                        Console.WriteLine();
                        player.health -= enemy.Atack(player);
                        Thread.Sleep(10000);
                        Console.Clear();
                        break;

                    case ConsoleKey.D2:
                        var chs = 0;
                        Console.WriteLine($"inventory:");
                        for (int i = 0; i < player.inventory.Count; i++)
                        {
                            Console.WriteLine($"{i+1}: ");
                            player.inventory[i].Info();
                        }
                        Console.WriteLine("Choose item(item number, 0 - turn back to action list): ");
                        chs = int.Parse(Console.ReadLine());
                        if (chs > 0 && chs < player.inventory.Count)
                        {
                            player.inventory[chs-1].Use(player);
                        }
                        goto default;

                    case ConsoleKey.Escape:
                        int luck = rnd.Next(1, 2);
                        if (luck == 2) 
                        {
                            Console.WriteLine("You tried escape, but something goes wrong");
                            enemy.Atack(player);
                            goto default;
                        } 
                        break;

                    default:
                        break;
                }
                player.Info();
                enemy.Info();
                Thread.Sleep(10000);
            } while(player.health > 0 && enemy.health > 0 && key.Key != ConsoleKey.Escape);
        }

        public static void Game(Character player)
        {
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                Console.Write(
                "(1) Look for fight\n" +
                "(2) Stats\n" +
                "(3) Inventory\n" +
                "(4) Spheres\n" +
                "(5) Save\n" +
                "(ESC) Exit");

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        int[] chance = new int[] { 1, 2, 2, 2, 1, 1, 1, 2 };
                        int success = chance[new Random().Next(0, chance.Length)];
                        if (success == 2)
                        {
                            Console.Write("You searched everywhere, but did not find enemies");
                            Thread.Sleep(6000);
                            break;
                        }
                        new Gameplay().fight(player);
                        break;

                    case ConsoleKey.D2:
                        player.Info();
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine($"Inventory:");
                        for (int i = 0; i < player.inventory.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: ");
                            player.inventory[i].Info();
                        }
                        Thread.Sleep(3000);
                        break;

                    case ConsoleKey.D4:
                        var chs = 0;
                        Console.WriteLine($"Spheres:");
                        for (int i = 0; i < player.spheres.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: ");
                            player.spheres[i].Info();
                        }
                        Console.WriteLine("Choose sphere(sphere number, 0 - turn back to action list): ");
                        chs = int.Parse(Console.ReadLine());
                        if (chs > 0 && chs < player.spheres.Count)
                        {
                            Sphere sphere = player.spheres[chs - 1];
                            Gameplay.open(player, sphere);
                            player.spheres.RemoveAt(chs - 1);
                        }
                        Console.Clear();
                        Console.WriteLine($"Spheres:");
                        for (int i = 0; i < player.spheres.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: ");
                            player.spheres[i].Info();
                        }
                        Thread.Sleep(3000);
                        break;

                    case ConsoleKey.D5:
                        save(player);
                        break;

                    default:
                        goto case ConsoleKey.D5;
                }
            } while (key.Key != ConsoleKey.Escape);
            
        }
    }
}
