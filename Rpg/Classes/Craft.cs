using System.Windows.Forms;
using Rpg.Classes;

namespace Rpg.Files
{
    class Crafting : ICraft
    {
        public void Craft(Recipe recipe, Item item1, Item item2, Weapon weapon)
        {
            if (item1.Counter >= recipe.Item1 && item2.Counter >= recipe.Item2)
            {
                item1.ChangeCount(recipe.Item1);
                item2.ChangeCount(recipe.Item2);

                /*MessageBox.Show($"Вы потратили {recipe.Item1} {item1.Name} \n" +
                    $"Вы потратили {recipe.Item2} {item2.Name}");*/

                MessageBox.Show($"Вы скрафтили: {recipe.CraftedItem}");
                weapon.SetIsCrafted(true);
            }
            else
            {
                MessageBox.Show("Вам не хватает материалов");
            }
        }       
        public void CraftMob(Recipe recipe, Item item1, Item item2, Mob mob)
        {
            if (item1.Counter >= recipe.Item1 && item2.Counter >= recipe.Item2)
            {
                item1.ChangeCount(recipe.Item1);
                item2.ChangeCount(recipe.Item2);

                /*MessageBox.Show($"Вы потратили {recipe.Item1} {item1.Name} \n" +
                    $"Вы потратили {recipe.Item2} {item2.Name}");*/

                MessageBox.Show($"Вы получили: {recipe.CraftedItem}");
                mob.SetIsBuyed(true);
            }
            else
            {
                MessageBox.Show("Вам не хватает материалов");
            }
        }      
        public void CraftArmor(Recipe recipe, Item item1, Item item2, Armor armor)
        {
            if (item1.Counter >= recipe.Item1 && item2.Counter >= recipe.Item2)
            {
                item1.ChangeCount(recipe.Item1);
                item2.ChangeCount(recipe.Item2);

                MessageBox.Show($"Вы получили: {recipe.CraftedItem}");
                armor.SetIsCrafted(true);
            }
            else
            {
                MessageBox.Show("Вам не хватает материалов");
            }
        }
        public void CraftBlock(Recipe recipe, Item item1, Item item2, Block block, PictureBox blockObj)
        {
            if (item1.Counter >= recipe.Item1 && item2.Counter >= recipe.Item2)
            {
                item1.ChangeCount(recipe.Item1);
                item2.ChangeCount(recipe.Item2);

                MessageBox.Show($"Вы получили: {recipe.CraftedItem}");
                block.SetIsCrafted(true);
                blockObj.Visible = true;
            }
            else
            {
                MessageBox.Show("Вам не хватает материалов");
            }
        }
        public void CraftMechanism(Recipe recipe, Item item1, Item item2, MechanicObject block, PictureBox blockObj)
        {
            if (item1.Counter >= recipe.Item1 && item2.Counter >= recipe.Item2)
            {
                item1.ChangeCount(recipe.Item1);
                item2.ChangeCount(recipe.Item2);

                MessageBox.Show($"Вы получили: {recipe.CraftedItem}");
                block.SetIsCrafted(true);
                blockObj.Visible = true;
            }
            else
            {
                MessageBox.Show("Вам не хватает материалов");
            }
        }
    }
}
