class City
{
    public int Id { get; set; }
    public double X { get; }
    public double Y { get; }

    public City(int id, double x, double y)
    {
        Id = id;
        X = x;
        Y = y;
    }

    public double DistanceFrom(City targetCity)
    {
        return Math.Sqrt(Math.Pow(X - targetCity.X, 2) + Math.Pow(Y - targetCity.Y, 2));
    }
}