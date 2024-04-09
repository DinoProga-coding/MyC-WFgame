using System.Windows.Forms;

namespace Rpg.Files
{
    class Factory
    {
        public string Name { get; private set; }
        public bool CanBuilded { get; private set; }
        public Factory(string name, bool canBuilded)
        {
            Name = name;
            CanBuilded = canBuilded;
        }

        public void SetValue(bool value)
        {
            CanBuilded = value;
        }
        public void Build(Item item1, Item item2, int num1, int num2, Panel CraftPanel)
        {
            if (item1.Counter >= num1 && item2.Counter >= num2)
            {
                MessageBox.Show("Вы восстановили фабрику");
                CanBuilded = true;
                item1.ChangeCount(num1);
                item2.ChangeCount(num2);
                CraftPanel.Visible = false;
            }
            else
            {
                MessageBox.Show("Вам не хватает материалов");
            }
        }
    }
}
