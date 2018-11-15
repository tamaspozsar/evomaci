using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MaciLaci.Backend
{
    public class Map
    {
        const int numberOfLines = 10;
        string[,] MapCells;
        string[] WhatObjectIsHere = new string[10];

        private string mapPath;

        public string MapPath
        {
            get { return mapPath; }
            set { mapPath = value; }
        }

        public Map(string mapNumber)
        {
            MapPath = $@"Maps\Map{mapNumber}.txt";
            MapCells = new string[10, 10];
        }
        /*Reads the .txt file for the map layout by splitting each of line string in the .txt
        *the letters inside the .txt can be of 5 different types
        * N for Nothing
        * T for Tree
        * B for Basket
        * H for Hunter
        * and L for Laszlo the Bear
        * after each line is read the indentifier are copied into the MapCell matrix for map generation
        */
        public void ReadMap(string filepath)
        {
            string Line;
            StreamReader File = new StreamReader(filepath);
            for (int i = 0; i < numberOfLines; i++)
            {
                Line = File.ReadLine();
                for (int j = 0; j < numberOfLines; j++)
                {
                    WhatObjectIsHere = Line.Split(';');
                    MapCells[i, j] = WhatObjectIsHere[j];
                }
            }
            File.Close();
        }

        public void GenerateMap(string mapPath)
        {
            Random rand = new Random();

            StreamWriter writer = new StreamWriter(mapPath, false, Encoding.Default);
            List<string> fieldObject = new List<String>();
            fieldObject.Add("T");
            fieldObject.Add("N");
            fieldObject.Add("N");

            fieldObject.Add("B");

            int randomIndex;
            string randomObject;
            const int numberOfLines = 10;


            for (int i = 0; i < numberOfLines; i++)
            {

                for (int j = 0; j < numberOfLines; j++)
                {

                    randomIndex = rand.Next(fieldObject.Count);

                    randomObject = fieldObject[randomIndex] + ";";

                    writer.Write(randomObject);
                }

                writer.WriteLine();

            }
            writer.Close();
        }

        public string GetMapCells(int x, int y)
        {
            return MapCells[x, y];
        }

    }
}
