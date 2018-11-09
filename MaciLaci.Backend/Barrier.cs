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
        Coordinate BarrierCoordinate = new Coordinate();

        public Barrier(int x, int y)
        {
            this.objectPath = Path.Combine(base.objectPath, "tree.jpg");
            BarrierCoordinate.Row = x;
            BarrierCoordinate.Column = y;
        }

        override public string GetPath()
        {
            return this.objectPath;
        }
    }
}
