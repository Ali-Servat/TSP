using static DataHandler;

while (true)
{
    Console.Clear();
    Console.WriteLine("TSP Assignment \n \n");
    double learningRate = Getinput("Enter learningRate: ");
    double radius = Getinput("Enter radius: ");
    int epochs = (int)Getinput("Enter total iterations: ");

    var cities = GetData("TSP51.txt");
    SOM som = new SOM(learningRate, radius);
    som.Train(cities, epochs, cities.Count * 2);

    var path = som.ExtractPath(cities);
    PrintPath(path);
    double solutionCost = GetPathCost(path);
    Console.WriteLine($"solution cost: {solutionCost}");
    Console.Write("\n \n Press any key to restart the program: ");
    Console.ReadKey();
}

static double GetPathCost(List<City> cities)
{
    double sum = 0;
    for (int i = 0; i < cities.Count - 1; i++)
    {
        sum += cities[i].DistanceFrom(cities[1]);
    }
    sum += cities[cities.Count - 1].DistanceFrom(cities[0]);
    return sum;
}

static void PrintPath(List<City> path)
{
    Console.Write("path: ");
    for (int i = 0; i < path.Count - 1; i++)
    {
        Console.Write(path[i].Id);
        Console.Write(" -> ");
    }
    Console.Write(path[path.Count - 1].Id);
    Console.WriteLine();
}

static double Getinput(string message)
{
    double output = 0;
    bool isValidInput = false;
    while (!isValidInput)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        isValidInput = double.TryParse(input, out output);
    }
    return output;
}