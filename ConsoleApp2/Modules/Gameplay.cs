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
                Console.Write("Write your nickname");
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
                    $"\n" +
                    $"{player.hat.writer()}\n" +
                    $"{player.arms.writer()}\n" +
                    $"{player.body.writer()}\n" +
                    $"{player.legs.writer()}\n" +
                    $"{player.weapon.writer()}\n" +
                    $"\n" +
                    $"{inv}\n" +
                    $"{spher}\n");

                
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

        public static Character login()
        {
            try
            {
                if (Directory.EnumerateFiles("../Profile", "profile.txt", SearchOption.AllDirectories) == null)
                {
                    return register();
                }

                string appRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
                var filePathAndName = Path.Combine(appRootDir, "..\\Profile\\profile.txt");
                StreamReader sr = new StreamReader(filePathAndName);
                string lin = sr.ReadLine();
                string inf = "";
                do
                {
                    lin = sr.ReadLine();
                    inf += lin;
                }
                while (lin != null);
                string[] info = inf.Split(new char[] { ' ', '\n' });
                List<Item> inventory = new List<Item>();
                int itemIndexEnd = 0;
                int checker = 0;
                int i = 0;
                while (i != info.Length-3)
                {
                    if (i > 18 && int.TryParse(info[i + 3], out checker))
                    {
                        inventory.Add(new Item(info[i], info[i + 1], info[i + 2]));
                        i += 3;
                    }
                    else
                    {
                        itemIndexEnd = i;
                        break;
                    }
                    i++;
                }
                i = 0;
                List<Sphere> spheres = new List<Sphere>();
                while (i != info.Length -3)
                {
                    if (i > itemIndexEnd && int.TryParse(info[i + 3], out checker))
                    {
                        spheres.Add(new Sphere(info[i], info[i + 1], info[i + 2], int.Parse(info[i + 3])));
                        i += 3;
                    }
                    else
                    {
                        break;
                    }
                    i++;
                }
                Character player = new Character(info[0], int.Parse(info[1]), int.Parse(info[2]), int.Parse(info[3]),
                    new Armor(info[4], info[5], info[6]), new Armor(info[7], info[8], info[9]),
                    new Armor(info[10], info[11], info[12]), new Armor(info[13], info[14], info[15]),
                    new Weapon(info[16], info[17], int.Parse(info[18])),
                    inventory, spheres);
                return player;
            } catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return null;
            }
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
                            if (lootArmor[i].name == config.armorName[0])
                            {
                                player.hat = lootArmor[i];
                            }
                            if (lootArmor[i].name == config.armorName[1])
                            {
                                player.body = lootArmor[i];
                            }
                            if (lootArmor[i].name == config.armorName[2])
                            {
                                player.legs = lootArmor[i];
                            }
                            if (lootArmor[i].name == config.armorName[3])
                            {
                                player.arms = lootArmor[i];
                            }
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
                            player.weapon = lootWeapon[i];
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
                        Console.WriteLine("Выберите предмет для использования(номер предмета): ");
                        chs = int.Parse(Console.ReadLine());
                        if (chs > 0 && chs < player.inventory.Count)
                        {
                            player.inventory[chs].Use(player);
                        }
                        break;

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
