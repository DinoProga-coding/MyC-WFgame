using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpg.Files
{
    interface ICraft
    {
        void Craft(Recipe recipe, Item item1, Item item2, Weapon weapon);
        void CraftMob(Recipe recipe, Item item1, Item item2, Mob mob);
        void CraftArmor(Recipe recipe, Item item1, Item item2, Armor armor);
    }
}
