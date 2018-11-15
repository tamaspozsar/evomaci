using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;

namespace MaciLaci.Backend
{
    public class Engine
    {
        Map map = new Map("1");
        Bear player = new Bear(0, 2);

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

        //Fetches the player's coordinates to be drawn for the MainWindow
        public int GetPlayerCoordinateX()
        {
            return player.coordinate.Row;
        }
        public int GetPlayerCoordinateY()
        {
            return player.coordinate.Column;
        }

        //Moves the player's coordinates depending on which key was pressed
        public void MovePlayer(KeyEventArgs e)
        {
            if (e.Key == Key.Right && player.coordinate.Column < 9 && map.GetMapCells(player.coordinate.Row, player.coordinate.Column + 1) != "T")
            {
                player.coordinate.Column += 1;
            }
            if (e.Key == Key.Down && player.coordinate.Row < 9 && map.GetMapCells(player.coordinate.Row + 1, player.coordinate.Column) != "T")
            {
                player.coordinate.Row += 1;
            }
            if (e.Key == Key.Up && player.coordinate.Row > 0 && map.GetMapCells(player.coordinate.Row - 1, player.coordinate.Column) != "T")
            {
                player.coordinate.Row -= 1;
            }
            if (e.Key == Key.Left && player.coordinate.Column > 0 && map.GetMapCells(player.coordinate.Row, player.coordinate.Column - 1) != "T")
            {
                player.coordinate.Column -= 1;
            }
        }

    }
}
