using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Backend
{
    class Bear : Character
    {
        int health = 3;

        public Bear(int x, int y)
        {
            coordinate.Column = y;
            coordinate.Row = x;
        }

        override public void Move()
        {

        }
    }
}
