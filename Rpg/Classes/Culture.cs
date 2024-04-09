using Rpg.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Rpg.Classes
{
    class Culture
    {
        public int TimerValue { get; private set; }
        public bool Stade2 { get; private set; }
        public string Name { get; private set; } 
        public Culture( string name)
        {
            Name = name;
        }

        public void SetValue(bool stade, bool value)
        {
            stade = value;
        }
        public void UpdateTime() 
        {
            TimerValue++;
        }
        public void SetTime() 
        {
            TimerValue = 0;
        }

        public void Put(PictureBox cultureObject, Culture culture, Timer cultureTimer, Item item)
        {
            item.AddCount(1);
            cultureTimer.Enabled = false;
            cultureObject.Visible = false;
            culture.SetTime();
        }

    }
}
