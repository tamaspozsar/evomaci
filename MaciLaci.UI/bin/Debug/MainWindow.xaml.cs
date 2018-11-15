using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaciLaci.Backend;

namespace MaciLaci.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Defining variables and initializing classes
        Grid grid = new Grid();
        Engine Core = new Engine();
        string mapPath = @"Maps\Map1.txt";
        string[,] ImageMatrix = new string[10, 10];
        ColumnDefinition[] columns = new ColumnDefinition[10];
        RowDefinition[] rows = new RowDefinition[10];


        public MainWindow()
        {
            InitializeComponent();
            grid.ShowGridLines = true;
            for (int i = 0; i < 10; i++)
            {
                columns[i] = new ColumnDefinition();
                rows[i] = new RowDefinition();
            }
            for (int i = 0; i < 10; i++)
            {
                grid.RowDefinitions.Add(rows[i]);
                grid.ColumnDefinitions.Add(columns[i]);
            }
            Window.Content = grid;
            Core.LoadMap(mapPath);
            Window.KeyDown += MapChanged;
            Core.GetMapElements(ImageMatrix);
            LoadMatrix(ImageMatrix);
        }

        //Loads the image matrix of the map
        public void LoadMatrix(string[,] ImageMatrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (ImageMatrix[i, j])
                    {
                        case "T":
                            LoadTree(i, j);
                            break;
                        case "B":
                            LoadBasket(i, j);
                            break;
                        case "N":
                            break;
                        default:
                            break;
                    }
                }
            }
            LoadBear(Core.GetPlayerCoordinateX(), Core.GetPlayerCoordinateY());
        }

        //When a key press occurs this method is called, which then calls the Engine to move the player in accordance with the key being pressed
        // Left arrow goes left | Right arrow goes right | Up arrow goes up | Down arrow goes down
        //When this is done the method redraws the map
        public void MapChanged(object sender, KeyEventArgs e)
        {
            Core.MovePlayer(e);
            grid.Children.Clear();
            LoadMatrix(ImageMatrix);
        }

        //Generates an Image and an absolute path to the image, then sets in in the grid specified by the parameter coordinates
        public void LoadBear(int x, int y)
        {
            Image tree = new Image();
            tree.Width = 60;
            tree.Height = 60;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"Images\bear.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            tree.Source = bi;

            Grid.SetColumn(tree, y);
            Grid.SetRow(tree, x);
            grid.Children.Add(tree);
        }
        public void LoadTree(int x, int y)
        {
            Image tree = new Image();
            tree.Width = 60;
            tree.Height = 60;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"Images\tree.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            tree.Source = bi;

            Grid.SetColumn(tree, y);
            Grid.SetRow(tree, x);
            grid.Children.Add(tree);
        }
        public void LoadBasket(int x, int y)
        {
            Image tree = new Image();
            tree.Width = 60;
            tree.Height = 60;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"Images\basket.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            tree.Source = bi;

            Grid.SetColumn(tree, y);
            Grid.SetRow(tree, x);
            grid.Children.Add(tree);
        }
    }
}
