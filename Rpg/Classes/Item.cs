using System.Windows.Forms;

namespace Rpg.Files
{
    class Item
    {
        public string Name { get; private set; }
        public int Counter { get; private set; }
        public int Stack {  get; private set; }
        public Item(string name, int counter, int stack)
        {
            Name = name;
            Counter = counter;
            Stack = stack;
        }
        public void Put(int number, PictureBox obj, Label countText, Item item)
        {
            Counter += number;
            obj.Dispose();
            obj.Visible = false;
            countText.Text = item.Counter.ToString();
        }
        public void ChangeCount(int updatedCount)
        {
            Counter -= updatedCount;
        }
        public void AddCount(int updatedCount)
        {
            Counter += updatedCount;
        }
        public void SetCount(int num)
        {
            Counter = num;
        }
    }
}
