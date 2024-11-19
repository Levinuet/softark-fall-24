using Hashing;
using System.Runtime.CompilerServices;

public class HashSetLinearProbing : HashSet {
    private Object[] buckets;
    private int currentSize;
    private enum State { DELETED }

    public HashSetLinearProbing(int bucketsLength) {
        buckets = new Object[bucketsLength];
        currentSize = 0;
    }

    public bool Contains(Object x) {
        
        int h = HashValue(x);
        int i = h;
        do
        {
            if (buckets[i] == null)
            {
                return false;
            }
            if (buckets[i].Equals(x))
            {
                return true;
            }
            i = (i + 1) % buckets.Length;
        } while (i != h);



        return false;
    }

    public bool Add(Object x) {      
        if (x == null)
        {
            return false;
        }
        if (currentSize == buckets.Length)
        {
            return false;
        }
        int h = HashValue(x);
        int i = h;
        do
        {
            if (buckets[i] == null || buckets[i].Equals(State.DELETED))
            {
                buckets[i] = x;
                currentSize++;
                return true;
            }
            if (buckets[i].Equals(x))
            {
                return false;
            }
            i = (i + 1) % buckets.Length;
        } while (i != h);

        return false;
    }

    public bool Remove(Object x) {
        
        int h = HashValue(x);
        int i = h;
        do
        {
            if (buckets[i] == null)
            {
                return false;
            }
            if (buckets[i].Equals(x))
            {
                buckets[i] = State.DELETED;
                currentSize--;
                return true;
            }
            i = (i + 1) % buckets.Length;
        } while (i != h);

        return false;
    }

    public int Size() {
        return currentSize;
    }

    private int HashValue(Object x) {
        int h = x.GetHashCode();
        if (h < 0) {
            h = -h;
        }
        h = h % buckets.Length;
        return h;
    }

    public override String ToString() {
        String result = "";
        for (int i = 0; i < buckets.Length; i++) {
            int value = buckets[i] != null && !buckets[i].Equals(State.DELETED) ? 
                    HashValue(buckets[i]) : -1;
            result += i + "\t" + buckets[i] + "(h:" + value + ")\n";
        }
        return result;
    }

}
