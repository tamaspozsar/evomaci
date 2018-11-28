using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Backend
{
    public abstract class Character: FieldObject
    {
        protected Character(int x, int y): base (x, y)
        {
            
        }
    }
}
