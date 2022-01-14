using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Config
    {
        public string[] weaponsTypes = new string[] 
        {
            "chain weapon", "long sword", "greate sword",
            "axe", "warhammer", "reaper"
        };

        public string[] weaponsNames = new string[]
        {
            "Ferallity", "Blazefury", "Dreamwhisper",
            "Nemesis", "Liar's Ornament", "Improved Baton",
            "Tormented Goblet", "Windweaver", "Destruction of Dilkigence",
            "Cryptic", "Glaive of Broken Bones", "Harmony",
            "Seal of the Earth"
        };

        public string[] itemTypes = new string[] 
        {
            "potion", "food", "scroll"
        };

        public string[] potionNames = new string[]
        {
            "Healing potion", "Resist potion", "Power potion", "Max HP potion", "Glass cannon"
        };

        public string[] itemEffects = new string[]
        {
            "heal", "resist", "power", "Max HP", "glazing"
        };

        public string[] foodNames = new string[]
        {
            "PIECE of meat", "mushroom;)", "apple",
            "pizza", "berries", "ice-cream"
        };

        public string[] armorTypes = new string[]
        {
            "textile", "leather", "steel",
            "lamelar", "obsidian"
        };

        public string[] armorName = new string[]
        {
            "helmet", "breastplate", "greaves",
            "armbands"
        };

        public string[] sphereType = new string[]
        {
            "common", "uncommon", "rare",
            "epic", "legend"
        };

        public string[] enemyNames = new string[]
        {
            "skeleton", "ghost", "lizard",
            "dragon", "thief", "dark knight",
            "slime"
        };

        public Config()
        {

        }
    }
}
