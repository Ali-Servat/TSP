class DataHandler
{
    public static List<City> GetData(string path)
    {
        List<City> cities = new List<City>();

        var rows = ReadData(path);
        foreach (var row in rows)
        {
            var rowNumbers = row.Split(" ");
            City newCity = new(int.Parse(rowNumbers[0]), double.Parse(rowNumbers[1]), double.Parse(rowNumbers[2]));
            cities.Add(newCity);
        }
        return cities;
    }

    private static List<string> ReadData(string path)
    {
        List<string> rows = new();
        using (var sr = new StreamReader(path))
        {
            string? currentLine;
            while (true)
            {
                currentLine = sr.ReadLine();
                if (currentLine == null) break;
                rows.Add(currentLine);
            }
        }
        return rows;
    }
}