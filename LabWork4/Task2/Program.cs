double[] a = [20, 21, 20, 22, 21, 20, 21, 22, 20, 21, 20, 22];
double[] b = [20, 21, 20, 22, 21, 29, 21, 22, 20, 21, 20, 22];

CheckData(a);
CheckData(b);

void CheckData(double[] data)
{
    Console.WriteLine($"Набор данных - {string.Join(", ", data)}");

    double firstAlgorithm = StatisticalCheck(data);
    double secondAlgorithm = RangeCheck(data);

    Console.WriteLine($"Алгоритм 1 - {double.Round(firstAlgorithm, 2)}");
    Console.WriteLine($"Алгоритм 2 - {double.Round(secondAlgorithm, 2)}");

    if (Math.Abs(firstAlgorithm - secondAlgorithm) > 1)
        Console.WriteLine("В данных есть выбросы");
    else
        Console.WriteLine("Все норм");
}

double StatisticalCheck(double[] data)
{
    double meanDeviation = data.Average();
    double variance = data.Select(n => Math.Pow(n - meanDeviation, 2)).Average();
    double standardDeviation = Math.Sqrt(variance);

    if (standardDeviation == 0)
        return meanDeviation;

    var filtered = data.Where(n => Math.Abs(n - meanDeviation) <= 2 * standardDeviation).ToList();
    return filtered.Average();
}

double RangeCheck(double[] data)
{
    var validData = data.Where(n => n >= 15.0 && n <= 30.0).ToList();
    return validData.Average();
}