using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpg.Files
{
    class Armor
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public bool IsCrafted { get; private set; }
        public Armor(string name, int health, bool isCrafted)
        {
            Name = name;
            Health = health;
            IsCrafted = isCrafted;
        }
        public void SetIsCrafted(bool value)
        {
            IsCrafted = value;
        }
    }
}
