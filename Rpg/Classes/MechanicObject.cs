using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpg.Classes
{
    class MechanicObject : Block
    {
        public bool IsActive { get; private set; }
        public bool IsCrafted { get; private set; }


        public MechanicObject( string name, int x, int y, bool isActive) : base(name, x, y )
        {
            IsActive = isActive;
        }

        public void SetIsCrafted(bool value)
        {
            IsCrafted = value;
        }     
        
        public void SetIsActive(bool value)
        {
            IsActive = value;
        }
    }
}
