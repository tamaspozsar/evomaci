using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Backend
{
    public struct Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public static bool operator ==(Coordinate obj1, Coordinate obj2)
        {
            return obj1.Column == obj2.Column && obj1.Row == obj2.Row;
        }

        public static bool operator !=(Coordinate obj1, Coordinate obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
