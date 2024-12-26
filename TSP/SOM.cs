class SOM
{
    public List<Neuron> Neurons { get; set; } = [];
    private double _learningRate;
    private double _radius;
    private Random _random = new();

    public SOM(double learningRate, double radius)
    {
        _learningRate = learningRate;
        _radius = radius;
    }

    public void Train(List<City> cities, int epochs, int numOfNeurons)
    {
        Neurons = InitializeNeurons(numOfNeurons);

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            foreach (var city in cities)
            {
                int winnerIndex = FindWinnerNeuronIndex(city);

                for (int i = 0; i < numOfNeurons; i++)
                {
                    double distanceToWinner = Math.Abs(i - winnerIndex);
                    if (distanceToWinner > numOfNeurons / 2)
                    {
                        distanceToWinner = numOfNeurons - distanceToWinner;
                    }

                    if (distanceToWinner <= _radius)
                    {
                        Neurons[i].X += _learningRate * (city.X - Neurons[i].X);
                        Neurons[i].Y = _learningRate * (city.Y - Neurons[i].Y);
                    }
                }
            }
            _learningRate *= 0.9;
            _radius *= 0.9;
        }
    }

    public List<City> ExtractPath(List<City> cities)
    {
        Dictionary<City, int> mapping = [];

        foreach (var city in cities)
        {
            int closestNeuronIndex = FindWinnerNeuronIndex(city);
            mapping.Add(city, closestNeuronIndex);
        }

        return mapping.OrderBy(m => m.Value).Select(m => m.Key).ToList();
    }

    private int FindWinnerNeuronIndex(City city)
    {
        int winnerIndex = -1;
        double minDistance = double.MaxValue;

        for (int i = 0; i < Neurons.Count; i++)
        {
            double distance = Math.Sqrt(Math.Pow(Neurons[i].X - city.X, 2) + Math.Pow(Neurons[i].Y - city.Y, 2));
            if (distance < minDistance)
            {
                minDistance = distance;
                winnerIndex = i;
            }
        }
        return winnerIndex;
    }
    private List<Neuron> InitializeNeurons(int numOfNeurons)
    {
        List<Neuron> initialNeurons = [];

        for (int i = 0; i < numOfNeurons; i++)
        {
            Neuron newNeuron = new(_random.Next(65), _random.Next(65));
            initialNeurons.Add(newNeuron);
        }

        return initialNeurons;
    }
}