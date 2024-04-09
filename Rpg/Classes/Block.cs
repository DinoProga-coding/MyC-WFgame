using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpg.Classes
{
    class Block
    {
        public string Name {  get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsCrafted { get; private set; }

        public Block(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }   
        public void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void SetIsCrafted(bool value)
        {
            IsCrafted = value;
        }
    }
}
