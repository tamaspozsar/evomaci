using System;
using System.CodeDom;
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

        protected FieldObject(int x, int y)
        {
            Coordinate.Column = y;
            Coordinate.Row = x;
        }

        public virtual void Move(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }
}