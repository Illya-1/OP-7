namespace App;
using DoubleLinkedList;

public static class Program
{
    public static void Main(string[] args)
    {
        DoubleLinkedList list = new();
        list.AddRange(GenRandomArray(5));
        PrintList(list);
        Console.WriteLine("Task 1. Find first element less than avg: ");
        Node<double> node = list.FindFirstLessThanAvg()!;
        Console.WriteLine($"{node.Item}");
        Console.WriteLine("Task 2. Find sum after max el: ");
        double sumAfterMax = list.FindSumAfterMax();
        Console.WriteLine($"{sumAfterMax}");
        Console.WriteLine("Task 3. Get new list of elements larger than val");
        Console.WriteLine("Enter val:");
        double val = double.Parse(Console.ReadLine()!);
        DoubleLinkedList newList = list.GetListOfMoreThanEl(val);
        Console.WriteLine("New list: ");
        PrintList(newList);
        Console.WriteLine("Task 4. Remove elements before max");
        Console.WriteLine("List before:");
        PrintList(list);
        list.DeleteBeforeMax();
        Console.WriteLine("List after:");
        PrintList(list);
    }

    private static double[] GenRandomArray(int amount)
    {
        double[] arr = new double[amount];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = (Math.Round(Random.Shared.NextDouble() * 100) / 100) * Random.Shared.Next(1, 100);
        }

        return arr;
    }

    private static void PrintList(DoubleLinkedList list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("List is empty!");
            return;
        }
        foreach (double el in list)
        {
            Console.Write($" {el:N} ");
        }

        Console.WriteLine();
    }
}