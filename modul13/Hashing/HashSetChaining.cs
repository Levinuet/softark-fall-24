using Hashing;

public class HashSetChaining : HashSet
{
    private Node[] buckets;
    private int currentSize;

    private class Node
    {
        public Node(Object data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public Object Data { get; set; }
        public Node Next { get; set; }
    }

    public HashSetChaining(int size)
    {
        buckets = new Node[size];
        currentSize = 0;
    }

    public bool Contains(Object x)
    {
        int h = HashValue(x);
        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                bucket = bucket.Next;
            }
        }
        return found;
    }

    //Lav en forbedret udgave af HashSetChaining der håndterer når arrayet bliver fyldt.
    //Lav en rehash-metode der anvendes fra add-metoden, så antallet af pladser fordobles, når load-faktoren bliver større end 0.75 (dvs. mindst 75% af arrayet er fyldt op).
    //Skriv nogle nye tests der tester, at settet kan fyldes op og stadig fungerer efter hensigten.

    private void Rehash()
    {
        Node[] oldBuckets = buckets;
        buckets = new Node[2 * oldBuckets.Length];
        for (int i = 0; i < oldBuckets.Length; i++)
        {
            Node temp = oldBuckets[i];
            while (temp != null)
            {
                Node next = temp.Next;
                int h = HashValue(temp.Data);
                temp.Next = buckets[h];
                buckets[h] = temp;
                temp = next;
            }
        }
    }


    public bool Add(Object x)
    {
        int h = HashValue(x);

        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                bucket = bucket.Next;
            }
        }

        if (!found)
        {
            Node newNode = new Node(x, buckets[h]);
            buckets[h] = newNode;
            currentSize++;
        }

        return !found;
    }

    public bool Remove(Object x)
    {
        
        int h = HashValue(x);
        Node bucket = buckets[h];
        Node prev = null;
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                prev = bucket;
                bucket = bucket.Next;
            }
        }
        if (!found)
        {
            return false;
        }
        if (prev == null)
        {
            buckets[h] = bucket.Next;
        }
        else
        {
            prev.Next = bucket.Next;
        }
        currentSize--;

        return false;
    }

    private int HashValue(Object x)
    {
        int h = x.GetHashCode();
        if (h < 0)
        {
            h = -h;
        }
        h = h % buckets.Length;
        return h;
    }

    public int Size()
    {
        return currentSize;
    }

    public override String ToString()
    {
        String result = "";
        for (int i = 0; i < buckets.Length; i++)
        {
            Node temp = buckets[i];
            if (temp != null)
            {
                result += i + "\t";
                while (temp != null)
                {
                    result += temp.Data + " (h:" + HashValue(temp.Data) + ")\t";
                    temp = temp.Next;
                }
                result += "\n";
            }
        }
        return result;
    }
}
