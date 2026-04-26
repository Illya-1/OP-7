namespace DoubleLinkedList;

public class DoubleLinkedList : BiDirectionalLinkedList<double>
{
    public Node<double>? FindFirstLessThanAvg()
    {
        if (Count == 0)
        {
            return null;
        }
        double avg = this.Sum() / Count;
        return Find(el => el < avg);
    }

    public double FindSumAfterMax()
    {
        if (GetHead() is null)
        {
            return 0;
        }

        double sum = 0;
        double max = this.Max();
        foreach (var el in this)
        {
            if (el > max)
            {
                sum = 0.0;
            }
            else
            {
                sum += el;
            }
        }

        return sum;
    }

    public DoubleLinkedList GetListOfMoreThanEl(double val)
    {
        DoubleLinkedList newList = new();

        foreach (var el in this)
        {
            if (el > val)
            {
                newList.Add(el);
            }
        }

        return newList;
    }

    public void DeleteBeforeMax()
    {
        if (GetHead() is null)
        {
            return;
        }
        
        Node<double> max = GetHead()!;
        Node<double> current = GetHead()!;

        while (current.Next is not null)
        {
            current = current.Next;
            if (current.Item > max.Item)
            {
                max = current;
            }
        }

        while (max.Previous is not null)
        {
            Remove(max.Previous);
        }
    }

    public void AddRange(double[] elements)
    {
        foreach (var el in elements)
        {
            Add(el);
        }
    }
}