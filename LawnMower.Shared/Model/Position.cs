namespace LawnMower.Shared.Model
{
    public class Location
    {
        public Location()
        {

        }
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Location(Location location)
        {
            X = location.X;
            Y = location.Y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
