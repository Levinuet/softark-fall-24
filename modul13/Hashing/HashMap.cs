using System;
using System.Collections.Generic;

namespace Hashing
{
    // Interface definition
    public interface Map<K, V>
    {
        public V Get(K key);
        public V Put(K key, V value);
        public V Remove(K key);
        public bool IsEmpty();
        public int Size();
    }

    // HashMap implementation
    public class HashMap<K, V> : Map<K, V>
    {
        // Internal linked list node for chaining
        private class Node
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public Node Next { get; set; }

            public Node(K key, V value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        // The underlying array to store the linked lists
        private Node[] buckets;
        private int capacity;
        private int size;

        // Constructor with default capacity
        public HashMap(int capacity = 16)
        {
            this.capacity = capacity;
            buckets = new Node[capacity];
            size = 0;
        }

        // Hash function: key mod capacity
        private int Hash(K key)
        {
            return Math.Abs(key.GetHashCode() % capacity);
        }

        public V Get(K key)
        {
            int index = Hash(key);
            Node current = buckets[index];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }

            throw new KeyNotFoundException($"Key {key} not found in the HashMap");
        }

        public V Put(K key, V value)
        {
            int index = Hash(key);

            // If the bucket is empty, create a new node
            if (buckets[index] == null)
            {
                buckets[index] = new Node(key, value);
                size++;
                return value;
            }

            // Check if key already exists in the chain
            Node current = buckets[index];
            while (current != null)
            {
                // If key exists, update the value
                if (current.Key.Equals(key))
                {
                    V oldValue = current.Value;
                    current.Value = value;
                    return oldValue;
                }

                // If we're at the last node, add a new node
                if (current.Next == null)
                {
                    current.Next = new Node(key, value);
                    size++;
                    return value;
                }

                current = current.Next;
            }

            return default(V);
        }

        public V Remove(K key)
        {
            int index = Hash(key);
            Node current = buckets[index];
            Node previous = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    // If it's the first node in the chain
                    if (previous == null)
                    {
                        buckets[index] = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }

                    size--;
                    return current.Value;
                }

                previous = current;
                current = current.Next;
            }

            throw new KeyNotFoundException($"Key {key} not found in the HashMap");
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public int Size()
        {
            return size;
        }
    }
}