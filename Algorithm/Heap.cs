using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Algorithm
{
    public class Heap<T> where T : IComparable
    {
        private enum HeapType
        {
            Min,
            Max
        }

        private readonly HeapType _heapType;
        private T[] _heap;
        public int maxSize;
        private int _count;

        public Heap(int minMaxSize, bool isMaxHeap = false)
        {
            _heapType = isMaxHeap ? HeapType.Max : HeapType.Min;
            // Automatic resize
            // _heap = new T[(int)Math.Pow(2, Math.Ceiling(Math.Log(minSize, 2)))];
            maxSize = minMaxSize;
            _heap = new T[minMaxSize];
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Insert(T val)
        {
            if (_count == _heap.Length)
            {
                DoubleHeap();
            }

            _heap[_count] = val;
            ShiftUp(_count);
            _count++;
        }

        public T Peek()
        {
            if (_heap.Length == 0)
            {
                throw new ArgumentOutOfRangeException($"No values in heap");
            }

            return _heap[0];
        }

        public T Remove()
        {
            T output = Peek();
            _count--;
            _heap[0] = _heap[_count];
            ShiftDown(0);
            return output;
        }

        public void ShiftUp(int heapIndex)
        {
            if (heapIndex == 0) return;
            int parentIndex = (heapIndex - 1) / 2;
            bool shouldShift = DoCompare(parentIndex, heapIndex) > 0;
            if (!shouldShift) return;
            Swap(parentIndex, heapIndex);
            ShiftUp(parentIndex);
        }

        private void ShiftDown(int heapIndex)
        {
            int child1 = heapIndex * 2 + 1;
            if (child1 >= _count) return;
            int child2 = child1 + 1;

            int preferredChildIndex = (child2 >= _count || DoCompare(child1, child2) <= 0) ? child1 : child2;
            if (DoCompare(preferredChildIndex, heapIndex) > 0) return;
            Swap(heapIndex, preferredChildIndex);
            ShiftDown(preferredChildIndex);
        }

        private void Swap(int index1, int index2)
        {
            T temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }

        private int DoCompare(int initialIndex, int contenderIndex)
        {
            T initial = _heap[initialIndex];
            T contender = _heap[contenderIndex];
            int value = initial.CompareTo(contender);
            if (_heapType == HeapType.Max) value = -value;
            return value;
        }

        private void DoubleHeap()
        {
            var copy = new T[_heap.Length * 2];
            for (int i = 0; i < _heap.Length; i++)
            {
                copy[i] = _heap[i];
            }

            _heap = copy;
        }
    }
}