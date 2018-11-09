using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MaciLaci.Backend
{
    public class Basket : FieldObject
    {
        readonly int pont = 10;
        public Coordinate BasketCoordinate = new Coordinate();

        public Basket(int x, int y)
        {
            this.objectPath = Path.Combine(base.objectPath, "basket.jpg");
            BasketCoordinate.Row = x;
            BasketCoordinate.Column = y;
        }

        public int PontAdas()
        {
            return pont;
        }

        override public string GetPath()
        {
            return this.objectPath;
        }
    }
}
