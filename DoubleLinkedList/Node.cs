namespace DoubleLinkedList;

public sealed class Node<T> where T : notnull
{
    private Node<T>? previous;
    private Node<T>? next;

    public Node(T item, BiDirectionalLinkedList<T> list)
    {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentNullException.ThrowIfNull(list);

        Item = item;
        List = list;
    }

    public Node<T>? Previous
    {
        get => previous;
        internal set
        {
            if (previous != null)
            {
                previous.next = null;
            }

            if (value != null)
            {
                if (value.next != null)
                {
                    value.next.previous = null;
                }
                value.next = this;
            }
            
            previous = value;
        }
    }

    public Node<T>? Next
    {
        get => next;
        internal set
        {
            if (next != null)
            {
                next.previous = null;
            }

            if (value != null)
            {
                if (value.previous != null)
                {
                    value.previous.next = null;
                }
                value.previous = this;
            }

            next = value;
        }
    }

    public T Item { get; set; }

    public BiDirectionalLinkedList<T> List { get; }
}