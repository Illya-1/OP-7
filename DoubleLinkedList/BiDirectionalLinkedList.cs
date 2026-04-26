using System.Collections;

namespace DoubleLinkedList;

public class BiDirectionalLinkedList<T> : IList<T> where T : notnull
{
    private Node<T>? head;
    private Node<T>? tail;

    private int count;

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = head;
        while (current != null)
        {
            yield return current.Item;
            current = current.Next;
        }
    }

    public IEnumerator<Node<T>> GetNodeEnumerator()
    {
        Node<T>? current = head;
        while (current != null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        AddNode(new Node<T>(item, this));
    }
    
    public void Add(Node<T> node)
    {
        AddNode(node);
    }

    public void AddLast(T item)
    {
        Node<T> node = new Node<T>(item, this);
        AddLastNode(node);
    }
    
    public void AddLast(Node<T> node)
    {
        AddLastNode(node);
    }

    public Node<T>? Find(T item)
    {
        return FindNode(current => item.Equals(current));
    }

    public Node<T>? Find(Predicate<T> predicate)
    {
        return FindNode(predicate);
    }

    public void Clear()
    {
        head = null;
        tail = null;
        Count = 0;
    }

    public bool Contains(T item)
    {
        return FindNode(current => item.Equals(current)) != null;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (Count == 0)
        {
            return;
        }

        ArgumentNullException.ThrowIfNull(array);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arrayIndex, array.Length);
        ArgumentOutOfRangeException.ThrowIfLessThan(arrayIndex, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(arrayIndex, array.Length - Count);

        Node<T>? current = head;
        int i = arrayIndex;
        while (current != null)
        {
            array[i] = current.Item;
            i++;
            current = current.Next;
        }
    }

    public bool Remove(T item)
    {
        Node<T>? node = FindNode(current => item.Equals(current));
        if (node == null)
        {
            return false;
        }

        return RemoveNode(node);
    }

    public bool Remove(Node<T> node)
    {
        return RemoveNode(node);
    }

    public int Count
    {
        get => count;
        private set => count = Math.Max(0, value);
    }
    public bool IsReadOnly => false;
    
    public int IndexOf(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        Node<T>? current = head;
        int i = 0;
        while (current != null && !current.Item.Equals(item))
        {
            current = current.Next;
            i++;
        }

        return current != null ? i : -1;
    }

    public void Insert(int index, T item)
    {
        if (index == Count)
        {
            Add(item);
            return;
        }

        Node<T> node = FindNode(index);
        Node<T> newNode = new Node<T>(item, this);
        Node<T>? previousNode = node.Previous;

        if (previousNode != null)
        {
            previousNode.Next = newNode;
        }
        else
        {
            head = newNode;
        }

        newNode.Next = node;

        Count++;
    }

    public void RemoveAt(int index)
    {
        Node<T> node = FindNode(index);
        RemoveNode(node);
    }

    public T this[int index]
    {
        get => FindNode(index).Item;
        set => FindNode(index).Item = value;
    }

    public Node<T>? GetHead()
    {
        return head;
    }

    public Node<T>? GetTail()
    {
        return tail;
    }

    private Node<T>? FindNode(Predicate<T> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        Node<T>? current = head;
        while (current != null && !predicate.Invoke(current.Item))
        {
            current = current.Next;
        }

        return current;
    }

    private Node<T> FindNode(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
        
        Node<T>? current = head;
        for (int i = 0; i < index; i++)
        {
            if (current == null)
            {
                throw new InvalidOperationException();
            }
            
            current = current.Next;
        }

        return current ?? throw new InvalidOperationException();
    }

    private void AddNode(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if (node.List != this)
        {
            throw new InvalidOperationException();
        }

        if (head != null)
        {
            head.Previous = node;
            head = node;
        }
        else
        {
            head = node;
            tail = node;
        }

        Count++;
    }

    private void AddLastNode(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if (node.List != this)
        {
            throw new InvalidOperationException();
        }

        if (tail != null)
        {
            tail.Next = node;
            tail = node;
        }
        else
        {
            head = node;
            tail = node;
        }

        Count++;
    }

    private bool RemoveNode(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if (node.List != this)
        {
            throw new InvalidOperationException();
        }

        Node<T>? previous = node.Previous;
        Node<T>? next = node.Next;
        if (previous == null && next == null)
        {
            Clear();
        }
        else if (previous == null)
        {
            next!.Previous = null;
            head = next;
        }
        else if (next == null)
        {
            previous.Next = null;
            tail = previous;
        }
        else
        {
            previous.Next = next;
        }

        Count--;
        return true;
    }
}