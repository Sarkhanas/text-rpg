using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp2.Modules
{
    class Gameplay
    {
        public Gameplay() {}

        public static Character register()
        {
            try
            {
                string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
                var filePathAndName = Path.Combine(appRootDir, "..\\Profile\\profile.txt");
                Console.Write("Write your nickname: ");
                Character player = new Character(Console.ReadLine());
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
                    $"{(player.hat == null? new Armor(null, null, null).writer(): player.hat.writer())}" +
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
                /*for (int i = 0; i < player.inventory.Count; i++)
                {
                    sw.WriteLine(player.inventory[i].writer());
                }*/
                /*for (int i = 0; i < player.spheres.Count; i++)
                {
                    sw.WriteLine(player.spheres[i].writer());
                }*/
                return player;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return null;
            }
        }
        //need to correct
        public static Character login()
        {
            string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
            var filePathAndName = Path.Combine(appRootDir, "..\\Profile");

            if (Directory.EnumerateFiles(filePathAndName, "profile.txt", SearchOption.AllDirectories) == null)
            {
                return register();
            }

            appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
            filePathAndName = Path.Combine(appRootDir, "..\\Profile\\profile.txt");
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
            for (int i = itemIndexEnd + 1; i < inf.Length-3; i += 3)
            {
                spheres.Add(new Sphere(inf[i], inf[i + 1], inf[i + 2]));
            }
            Character player = new Character(
                inf[0], int.Parse(inf[1]), int.Parse(inf[2]), int.Parse(inf[3]),
                new Armor(inf[5], inf[6], inf[7]), new Armor(inf[8], inf[9], inf[10]),
                new Armor(inf[11], inf[12], inf[13]), new Armor(inf[14], inf[15], inf[16]),
                new Weapon(inf[18], inf[19], int.Parse(inf[20])),
                inventory, spheres);
            /*foreach(var elem in inf)
            {
                Console.WriteLine(elem);
            }*/
            return player;
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
            int choose = 0;
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
            do
            {
                choose = -1;
                Console.WriteLine("What you gonna do?\n1-atack 2-item 0-escape");
                switch (choose)
                {
                    case 1:
                        player.Atack(enemy);
                        enemy.Atack(player);
                        Console.Clear();
                        break;

                    case 2:
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

                    case 0:
                        int luck = rnd.Next(1, 2);
                        if (luck == 2) 
                        {
                            Console.WriteLine("You tried escape, but something goes wrong");
                            enemy.Atack(player);
                            goto default;
                        } 
                        break;

                    default:
                        choose = -1;
                        break;
                }
                player.Info();
                enemy.Info();
            } while(player.health <= 0 || enemy.health <= 0 || choose == 0);
        }
    }
}
