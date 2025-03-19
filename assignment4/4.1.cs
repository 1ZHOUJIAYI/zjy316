using System;

public class LinkedList<T>
{
    private class Node
    {
        public T Data { get; set; }
        public Node Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node head;

    public void Add(T data)
    {
        if (head == null)
        {
            head = new Node(data);
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new Node(data);
        }
    }

    public void ForEach(Action<T> action)
    {
        Node current = head;
        while (current != null)
        {
            action(current.Data);
            current = current.Next;
        }
    }
}
public class Program
{
    public static void Main()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.Add(3);
        list.Add(21);
        list.Add(340);
        list.Add(45);
        list.Add(59);

        // 打印链表元素
        Console.WriteLine("链表元素:");
        list.ForEach(x => Console.WriteLine(x));

        // 求最大值
        int max = int.MinValue;
        list.ForEach(x => { if (x > max) max = x; });
        Console.WriteLine($"最大值: {max}");

        // 求最小值
        int min = int.MaxValue;
        list.ForEach(x => { if (x < min) min = x; });
        Console.WriteLine($"最小值: {min}");

        // 求和
        int sum = 0;
        list.ForEach(x => sum += x);
        Console.WriteLine($"和: {sum}");
    }
}