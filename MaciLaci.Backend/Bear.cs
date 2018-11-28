namespace MaciLaci.Backend
{
    public class Bear : Character
    {
        int health = 1;

        public Bear(int x, int y): base(x,y)
        {
            Health = health;
        }

        public int Health { get; internal set; }

        public int NumberOfCollectedBaskets { get; set; }

    }
}
