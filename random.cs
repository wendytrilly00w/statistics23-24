using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Uniform Random Variates Histogram");

        Console.Write("Number of Random Variables (N): ");
        int N = int.Parse(Console.ReadLine());

        Console.Write("Number of Intervals (k): ");
        int k = int.Parse(Console.ReadLine());

        List<double> data = GenerateUniformRandomVariates(N);
        int[] histogram = CreateHistogram(data, k);
        PlotHistogram(histogram, k);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static List<double> GenerateUniformRandomVariates(int N)
    {
        Random random = new Random();
        List<double> data = new List<double>();
        for (int i = 0; i < N; i++)
        {
            data.Add(random.NextDouble());
        }
        return data;
    }

    static int[] CreateHistogram(List<double> data, int k)
    {
        int[] histogram = new int[k];
        foreach (double value in data)
        {
            int interval = (int)(value * k);
            histogram[interval]++;
        }
        return histogram;
    }

    static void PlotHistogram(int[] histogram, int k)
    {
        int maxCount = 0;
        foreach (int count in histogram)
        {
            if (count > maxCount)
                maxCount = count;
        }

        for (int i = 0; i < k; i++)
        {
            double lowerBound = (double)i / k;
            double upperBound = (double)(i + 1) / k;
            Console.WriteLine($"[{lowerBound:F2}, {upperBound:F2}): {new string('*', histogram[i])}");
        }
    }
}
