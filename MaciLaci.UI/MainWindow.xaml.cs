using MaciLaci.Backend;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaciLaci.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Defining variables and initializing classes
        private Grid grid = new Grid();
        private Engine Core = new Engine();
        private string mapPath = @"Maps\Map1.txt";
        private FieldObject[,] myImageMatrix = new FieldObject[10, 10];
        private ColumnDefinition[] columns = new ColumnDefinition[10];
        private RowDefinition[] rows = new RowDefinition[10];


        public MainWindow()
        {
            InitializeComponent();
            Core.MapChanged += Core_MapChanged;
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
            Window.KeyDown += Window_KeyDown;
            myImageMatrix = Core.GetMapElements();
            LoadMatrix(myImageMatrix);
        }

        private void Core_MapChanged(object sender, MapChangedEventArgs e)
        {
            Dispatcher.Invoke(delegate
            {
                grid.Children.Clear();
                LoadMatrix(e.CurrentMap);
            });
        }

        //Loads the image matrix of the map
        public void LoadMatrix(FieldObject[,] imageMatrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (imageMatrix[i, j] != null)
                    {
                        if (imageMatrix[i, j] is Barrier)
                        {
                            LoadTree(i, j);
                        }
                        else if (imageMatrix[i, j] is Basket)
                        {
                            LoadBasket(i, j);
                        }
                        else if (imageMatrix[i, j] is Hunter)
                        {
                            LoadHunter(i, j);
                        }
                        else if (imageMatrix[i, j] is Bear bear)
                        {
                            LoadBear(i, j, bear.NumberOfCollectedBaskets, bear.Health);
                        }
                    }
                }
            }
        }

        //When a key press occurs this method is called, which then calls the Engine to move the player in accordance with the key being pressed
        // Left arrow goes left | Right arrow goes right | Up arrow goes up | Down arrow goes down
        //When this is done the method redraws the map
        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Enum.TryParse(e.Key.ToString(), out Direction direction))
            {
                Core.MovePlayer(direction);
            }
        }

        //Generates an Image and an absolute path to the image, then sets in in the grid specified by the parameter coordinates
        public void LoadBear(int x, int y, int numberOfCollectedBaskets, int health)
        {
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };
            Image image = new Image
            {
                Width = 60,
                Height = 60
            };

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@"Images\bear.jpg", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            image.Source = bitmapImage;

            StackPanel imageStackPanel = new StackPanel();
            imageStackPanel.Orientation = Orientation.Horizontal;
            imageStackPanel.Children.Add(image);

            Grid basketGrid = new Grid();
            for (int i = 0; i < 3; i++)
            {
                basketGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                Rectangle r = new Rectangle { Width = 5, Height = 18, Margin = new Thickness(1) };
                Grid.SetRow(r, i);

                if (i < 3 - numberOfCollectedBaskets)
                {
                    r.Fill = Brushes.LightGreen;
                }
                else
                {
                    r.Fill = Brushes.DarkGreen;
                }

                basketGrid.Children.Add(r);
            }

            imageStackPanel.Children.Add(basketGrid);

            Grid healthGrid = new Grid();
            for (int i = 0; i < 3; i++)
            {
                healthGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                Rectangle r = new Rectangle();
                r.Width = 20;
                r.Height = 6;
                Grid.SetColumn(r, i);

                if (i < health)
                {
                    r.Fill = Brushes.Green;
                }
                else
                {
                    r.Fill = Brushes.Red;
                }

                healthGrid.Children.Add(r);
            }


            stackPanel.Children.Add(imageStackPanel);
            stackPanel.Children.Add(healthGrid);


            Grid.SetColumn(stackPanel, y);
            Grid.SetRow(stackPanel, x);
            grid.Children.Add(stackPanel);
        }
        public void LoadTree(int x, int y)
        {
            Image tree = new Image { Width = 60, Height = 60 };

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

        public void LoadHunter(int x, int y)
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
