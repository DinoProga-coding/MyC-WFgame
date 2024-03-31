
namespace Rpg.Files
{
    class Recipe
    {
        public int Item1 { get; private set; }
        public int Item2 { get; private set; } 
        public string CraftedItem { get; private set; }
        public Recipe(int item1, int item2, string craftedItem)
        {
            Item1 = item1;
            Item2 = item2;

            CraftedItem = craftedItem;
        }
    }
}
