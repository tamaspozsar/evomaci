using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Backend
{
    public abstract class FieldObject
    {
        public Coordinate Coordinate = new Coordinate();

        public FieldObject(int x, int y)
        {
            Coordinate.Column = y;
            Coordinate.Row = x;
        }
    }
}