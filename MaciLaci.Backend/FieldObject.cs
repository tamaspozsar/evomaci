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
        protected string objectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Images\");

        public abstract string GetPath();
    }
}