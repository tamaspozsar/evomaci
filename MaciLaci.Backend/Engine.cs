using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MaciLaci.Backend
{
    public class Engine
    {
        Map map = new Map("1");

        public void LoadMap(string path)
        {
            map.ReadMap(path);
        }
        //Copies the MapCell matrix into the given parameter matrix so the UI can load the map that has been read last time
        public void GetMapElements(string[,] Matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Matrix[i, j] = map.GetMapCells(i, j);
                }
            }
        }
    }
}
