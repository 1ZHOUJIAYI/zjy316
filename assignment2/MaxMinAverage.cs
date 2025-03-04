using System;
using System.Linq;

class Program
{
    static void Main()
    { 
        int[] arr = { 10, 20, 30, 40, 50 };
        var result = CalculateArrayProperties(arr);
        Console.WriteLine($"最大值: {result.MaxValue}");
        Console.WriteLine($"最小值: {result.MinValue}");
        Console.WriteLine($"平均值: {result.AverageValue}");
        Console.WriteLine($"数组元素的和: {result.TotalSum}");
    }

    static (int MaxValue, int MinValue, double AverageValue, int TotalSum) CalculateArrayProperties(int[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            throw new ArgumentException("数组不能为空");
        }
        int maxValue = arr.Max();
        int minValue = arr.Min();
        int totalSum = arr.Sum();
        double averageValue = arr.Average();

        return (maxValue, minValue, averageValue, totalSum);
    }
}