using System;
using System.ComponentModel;
using System.Threading;

namespace MaciLaci.Backend
{
    public class Engine
    {

        public Engine()
        {
            myBackgroundWorker = new BackgroundWorker();
            myBackgroundWorker.DoWork += MyBackgroundWorker_DoWork;
            myBackgroundWorker.RunWorkerCompleted += MyBackgroundWorker_RunWorkerCompleted;
        }

        private void MyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        
            MapChanged?.Invoke(null, new MapChangedEventArgs { CurrentMap = map.GetTwoDimension() });
        }

        private void MyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is WorkerArgs args)
            {
                MoveInternal(args.FieldObject, args.Direction);
            }
        }

        private Map map;


        public event EventHandler<MapChangedEventArgs> MapChanged;

        private BackgroundWorker myBackgroundWorker;

        public void LoadMap(string path)
        {
            map = new Map("1");
        }
        //Copies the MapCell matrix into the given parameter matrix so the UI can load the map that has been read last time
        public FieldObject[,] GetMapElements()
        {
            return map.GetTwoDimension();
        }
        private Bear CurrentPlayer => map.GetBear();

        private class WorkerArgs
        {
            public FieldObject FieldObject { get; set; }

            public Direction Direction { get; set; }
        }

        private void Move(FieldObject fieldObject, Direction direction)
        {
            if (!myBackgroundWorker.IsBusy)
            {
                myBackgroundWorker.RunWorkerAsync(new WorkerArgs {Direction = direction, FieldObject = fieldObject});
            }
        }

        private void MoveInternal(FieldObject fieldObject, Direction direction)
        {

            FieldObject current;
            
            Coordinate currentCoordinate = fieldObject.Coordinate;
            Coordinate targetCoordinate = GetTargetCoordinate(currentCoordinate, direction);
            if (map.IsCoordinateOnMap(targetCoordinate))
            {
                if (map.IsFree(targetCoordinate))
                {
                    FieldObject o = map.GetMapCells<FieldObject>(targetCoordinate);

                    current = map.GetMapCells<FieldObject>(currentCoordinate);
                    //o.Coordinate = currentCoordinate;
                    o.Move(currentCoordinate);
                    current.Move(targetCoordinate);
                   // MapChanged?.Invoke(null, new MapChangedEventArgs { CurrentMap = map.GetTwoDimension() });
                }

                else if (map.IsBasketOn(targetCoordinate))
                {
                    Basket o = map.GetMapCells<Basket>(targetCoordinate);

                    current = map.GetMapCells<FieldObject>(currentCoordinate);
                    current.Move(targetCoordinate);
                    if (current is Bear bear)
                    {
                        bear.NumberOfCollectedBaskets++;
                        map.RemoveBasket(o);
                        o = null;
                    }
                    Empty e = new Empty(currentCoordinate);
                    map.AddEmpty(e);
                    }
            }
        }

        private Coordinate GetTargetCoordinate(Coordinate source, Direction direction)
        {
            Coordinate target = source;
            switch (direction)
            {
                case Direction.Down:
                    {
                        target.Row++;
                    }
                    break;
                case Direction.Up:
                    {
                        target.Row--;
                    }
                    break;
                case Direction.Left:
                    {
                        target.Column--;
                    }
                    break;
                case Direction.Right:
                    {
                        target.Column++;
                    }
                    break;
            }

            return target;
        }

        //Moves the player's coordinates depending on which key was pressed
        public void MovePlayer(Direction direction)
        {
            Move(CurrentPlayer, direction);
            //var prevCoordinate = CurrentPlayer.Coordinate;
            //FieldObject previous = null;
            //if (direction == Direction.Right && CurrentPlayer.Coordinate.Column < 9 && map.GetMapCells<Barrier>(CurrentPlayer.Coordinate.Row, CurrentPlayer.Coordinate.Column + 1) == null)
            //{
            //    previous = map.GetMapCells<Empty>(CurrentPlayer.Coordinate.Row, CurrentPlayer.Coordinate.Column + 1);
            //    CurrentPlayer.Coordinate.Column += 1;
            //}
            //if (direction == Direction.Down && CurrentPlayer.Coordinate.Row < 9 && map.GetMapCells<Barrier>(CurrentPlayer.Coordinate.Row + 1, CurrentPlayer.Coordinate.Column) == null)
            //{
            //    previous = map.GetMapCells<Empty>(CurrentPlayer.Coordinate.Row + 1, CurrentPlayer.Coordinate.Column);
            //    CurrentPlayer.Coordinate.Row += 1;
            //}
            //if (direction == Direction.Up && CurrentPlayer.Coordinate.Row > 0 && map.GetMapCells<Barrier>(CurrentPlayer.Coordinate.Row - 1, CurrentPlayer.Coordinate.Column) == null)
            //{
            //    previous = map.GetMapCells<Empty>(CurrentPlayer.Coordinate.Row - 1, CurrentPlayer.Coordinate.Column);
            //    CurrentPlayer.Coordinate.Row -= 1;
            //}
            //if (direction == Direction.Left && CurrentPlayer.Coordinate.Column > 0 && map.GetMapCells<Barrier>(CurrentPlayer.Coordinate.Row, CurrentPlayer.Coordinate.Column - 1) == null)
            //{
            //    previous = map.GetMapCells<Empty>(CurrentPlayer.Coordinate.Row, CurrentPlayer.Coordinate.Column - 1);
            //    CurrentPlayer.Coordinate.Column -= 1;
            //}
            //if (previous != null)
            //{
            //    previous.Coordinate = prevCoordinate;
            //}
            //MapChanged?.Invoke(null, new MapChangedEventArgs { CurrentMap = map.GetTwoDimension() });
        }

    }
}
