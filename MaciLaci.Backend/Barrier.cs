using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MaciLaci.Backend
{
    public class Barrier : FieldObject
    {

        public Barrier(int x, int y) : base(x, y)
        {
            Coordinate.Row = x;
            Coordinate.Column = y;
        }
    }
}
