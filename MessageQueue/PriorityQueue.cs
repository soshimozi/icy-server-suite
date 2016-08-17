//**********************************************************
//* PriorityQueue                                          *
//* Copyright (c) Julian M Bucknall 2004                   *
//* All rights reserved.                                   *
//* This code can be used in your applications, providing  *
//*    that this copyright comment box remains as-is       *
//**********************************************************
//* .NET priority queue class (heap algorithm              *
//**********************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectEventQueue
{
    public struct HeapEntry<T> {
        private T item;
        private int priority;
        public HeapEntry(T item, int priority) {
            this.item = item;
            this.priority = priority;
        }
        public T Item {
            get {return item;}
        }
        public int Priority {
            get {return priority;}
        }
    }

    public class PriorityQueue<T> : ICollection<T> {
        private const int DEFAULT_PRIORITY = 1;

        private int count;
        private int capacity;
        private int version;
        private HeapEntry<T>[] heap;

        public PriorityQueue() {
            capacity = 15; // 15 is equal to 4 complete levels
            heap = new HeapEntry<T>[capacity];
        }

        public T Dequeue() {
            if (count == 0) 
                throw new InvalidOperationException();
            
            T result = heap[0].Item;
            count--;
            trickleDown(0, heap[count]);
            version++;
            return result;
        }

        public void Enqueue(T item, int priority) {
            if (count == capacity)  
                growHeap();
            count++;
            bubbleUp(count - 1, new HeapEntry<T>(item, priority));
            version++;
        }

        private void bubbleUp(int index, HeapEntry<T> he) {
            int parent = getParent(index);
            // note: (index > 0) means there is a parent
            while ((index > 0) && (heap[parent].Priority < he.Priority)) {
                heap[index] = heap[parent];
                index = parent;
                parent = getParent(index);
            }
            heap[index] = he;
        }

        private int getLeftChild(int index) {
            return (index * 2) + 1;
        }

        private int getParent(int index) {
            return (index - 1) / 2;
        }

        private void growHeap() {
            capacity = (capacity * 2) + 1;
            HeapEntry<T>[] newHeap = new HeapEntry<T>[capacity];
            System.Array.Copy(heap, 0, newHeap, 0, count);
            heap = newHeap;
        }

        private void trickleDown(int index, HeapEntry<T> he) {
            int child = getLeftChild(index);
            while (child < count) {
                if (((child + 1) < count) && 
                    (heap[child].Priority < heap[child + 1].Priority)) {
                    child++;
                }
                heap[index] = heap[child];
                index = child;
                child = getLeftChild(index);
            }
            bubbleUp(index, he);
        }
        
        #region IEnumerable implementation
        public IEnumerator<T> GetEnumerator() {
            return new PriorityQueueEnumerator<T>(this);
        }
        #endregion

        #region ICollection implementation
        public int Count {
            get {return count;}
        }

        public bool Contains(T item)
        {
            return ((from he in heap
                        where he.Item.Equals(item)
                        select he.Item).FirstOrDefault() != null);

        }

        public void CopyTo(T[] array, int index) {
            System.Array.Copy(heap, 0, array, index, count);
        }

        public object SyncRoot {
            get {return this;}
        }

        public bool IsSynchronized { 
            get {return false;}
        }
        #endregion

        #region Priority Queue enumerator
        private class PriorityQueueEnumerator<T> : IEnumerator<T> {
            private int index;
            private PriorityQueue<T> pq;
            private int version;

            public PriorityQueueEnumerator(PriorityQueue<T> pq) {
                this.pq = pq;
                Reset();
            }

            private void checkVersion() {
                if (version != pq.version)
                    throw new InvalidOperationException();
            }

            #region IEnumerator Members

            public void Reset() {
                index = -1;
                version = pq.version;
            }

            //public object Current {
            //    get { 
            //        checkVersion();
            //        return pq.heap[index].Item; 
            //    }
            //}

            public bool MoveNext() {
                checkVersion();
                if (index + 1 == pq.count) 
                    return false;
                index++;
                return true;
            }

            #endregion

            #region IEnumerator<T> Members

            T IEnumerator<T>.Current
            {
                get
                {
                    checkVersion();
                    return pq.heap[index].Item;
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    checkVersion();
                    return pq.heap[index].Item;
                }
            }

            #endregion
        }
        #endregion


        #region ICollection<T> Members

        public void Add(T item)
        {
            Enqueue(item, DEFAULT_PRIORITY);
        }

        public void Clear()
        {
            capacity = 15;
            heap = new HeapEntry<T>[15];
        }


        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new PriorityQueueEnumerator<T>(this);
        }

        #endregion
    }
}
