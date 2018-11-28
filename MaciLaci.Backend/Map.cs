using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MyNamespace;

namespace MaciLaci.Backend
{
    public class Map
    {
        private const int numberOfLines = 10;

        private readonly FieldObject[,] mypCells;

        internal List<FieldObject> fieldObjects = new List<FieldObject>();

        public int Width { get; private set; }

        public int Height { get; private set; }

        private string MapPath { get; set; }

        public Map(string mapNumber)
        {
            MapPath = $@"Maps\Map{mapNumber}.txt";
            ReadMap(MapPath);
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
        private void ReadMap(string filepath)
        {
            string[] rows = File.ReadAllLines(filepath);
            Height = rows.Length;
            string[] cell = null;
            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];
                cell =  row.Split(';');
                for (int j = 0; j < cell.Length; j++)
                {
                    FieldObject character = null;
                    switch (cell[j])
                    {
                        case "T":
                        {
                            character = new Barrier(i, j);
                        }
                            break;
                        case "B":
                        {
                            character = new Basket(i, j);
                        }
                            break;
                        case "H":
                        {
                            character = new Hunter(i, j);

                        }
                            break;
                        case "L":
                        {
                            character = new Bear(i, j);

                        }
                            break;
                        default:
                        {
                            character = new Empty(i, j);
                        }
                            break;
                    }
                    fieldObjects.Add(character);
                }
               
            }
            Width = cell.Length;

            /*
            using (StreamReader File = new StreamReader(filepath))
            {
                for (int i = 0; i < numberOfLines; i++)
                {
                    string line = File.ReadLine();

                    string[] splittedLine = line.Split(';');
                    for (int j = 0; j < splittedLine.Length; j++)
                    {
                        FieldObject character = null;
                        switch (splittedLine[j])
                        {
                            case "T":
                                {
                                    character = new Barrier(i, j);
                                }
                                break;
                            case "B":
                                {
                                    character = new Basket(i, j);
                                }
                                break;
                            case "H":
                                {
                                    character = new Hunter(i, j);

                                }
                                break;
                            case "L":
                                {
                                    character = new Bear(i, j);

                                }
                                break;
                            default:
                                {
                                    character = new Empty(i,j);
                                }
                                break;
                        }
                        fieldObjects.Add(character);
                        //MapCells[i, j] = character;
                    }
                }

                File.Close();
            }*/
        }

        internal bool IsCoordinateOnMap(Coordinate coordinate)
        {
            return coordinate.Row >= 0 && coordinate.Row < Width && coordinate.Column >= 0 && coordinate.Column < Height;
        }

        internal bool IsFree(Coordinate target)
        {
            var obj = fieldObjects.FirstOrDefault(item => item.Coordinate == target);
            return obj is Empty;
        }

        internal bool IsBasketOn(Coordinate coordinate)
        {
            var obj = fieldObjects.FirstOrDefault(item => item.Coordinate == coordinate);
            return obj is Basket;
        }

        internal void RemoveBasket(Basket basket)
        {
            fieldObjects.Remove(basket);
        }

        internal void AddEmpty(Empty e)
        {
            fieldObjects.Add(e);
        }

        internal Bear GetBear()
        {
            return fieldObjects.FirstOrDefault(item => item is Bear) as Bear;
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

        public T GetMapCells<T>(int x, int y) where T : FieldObject
        {
            FieldObject obj = null;

            foreach (var fieldObject in fieldObjects)
            {
                if (fieldObject is T variable)
                {
                    if (variable.Coordinate.Row == x && variable.Coordinate.Column == y)
                    {
                        obj = variable;
                        break;
                    }
                }
            }
            return (T)obj;
        }

        public T GetMapCells<T>(Coordinate coordinate) where T : FieldObject
        {
            return GetMapCells<T>(coordinate.Row, coordinate.Column);
        }

        internal FieldObject[,] GetTwoDimension()
        {
            FieldObject[,] result = new FieldObject[10,10];
            List<string>ss = new List<string>();
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Add("a");
            ss.Dump();

            fieldObjects.Dump();
            foreach (FieldObject fieldObject in fieldObjects)
            {
                if (result[fieldObject.Coordinate.Row, fieldObject.Coordinate.Column] == null)
                {
                    result[fieldObject.Coordinate.Row, fieldObject.Coordinate.Column] = fieldObject;
                }
                else
                {
                    // just for debugging
                    FieldObject ott = result[fieldObject.Coordinate.Row, fieldObject.Coordinate.Column];
                    Console.WriteLine($"[{fieldObject.Coordinate.Column}:{fieldObject.Coordinate.Row}]:{fieldObject.GetType()} <> {ott.GetType()}");
                }
            }

            return result;

        }

    }


}

namespace MyNamespace
{
    public static class ListExtensions
    {
        public static void Dump<T>(this List<T> fo)
        {
            foreach (var o in fo)
            {
                Console.WriteLine(o.ToString());
            }
        }
    }

}
