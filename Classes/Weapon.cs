using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpg.Files
{
    class Weapon
    {
        public string Name { get; private set; }
        public int Damage { get; private set; }
        public bool IsCrafted { get; private set; }
        public Weapon(string name, int damage, bool isCrafted)
        {
            Name = name;
            Damage = damage;
            IsCrafted = isCrafted;
        }
        public void SetIsCrafted(bool value)
        {
            IsCrafted = value;
        }
    }
}
