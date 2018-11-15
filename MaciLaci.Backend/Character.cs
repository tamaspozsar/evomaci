using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Backend
{
    abstract class Character
    {
        public Coordinate coordinate = new Coordinate();
        public abstract void Move();
    }
}
