using Rpg.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rpg.Files
{
    interface ICraft
    {
        void Craft(Recipe recipe, Item item1, Item item2, Weapon weapon);
        void CraftMob(Recipe recipe, Item item1, Item item2, Mob mob);
        void CraftArmor(Recipe recipe, Item item1, Item item2, Armor armor);
        void CraftBlock(Recipe recipe, Item item1, Item item2, Block block, PictureBox blockObj);
        void CraftMechanism(Recipe recipe, Item item1, Item item2, MechanicObject block, PictureBox blockObj);
    }
}
