﻿using System;
using System.Runtime.InteropServices;

namespace DataStructures.heap
{
    public class ArrayHeap<E> : IHeap<E>
    {
        private E[] Elements;
        public int Size { private set; get; }
        private readonly Func<E, E, bool> Compare;
        
        public ArrayHeap(Func<E, E, bool> compare) {
            Elements = new E[20];
            Compare = compare;
        }

        public ArrayHeap(Func<E, E, bool> compare, int capacity) {
            Elements = new E[capacity];
            Compare = compare;
        }
        
        //heapify - O(n)
        public ArrayHeap(Func<E, E, bool> compare, E[] array) {
            Elements = array;
            Size = array.Length;
            Compare = compare;
            for(var i = Elements.Length-1; i >= 0; i--) {
                SiftDown(i);
            }
        }

        public int Depth() {
            return InternalDepth() + 1;
        }

        private int InternalDepth() {
            return (int) Math.Floor(Math.Log2(Size));
        }

        public bool IsEmpty() {
            return Size == 0;  
        }

        public void PrintConsole() {
            for(var i = 0; i < Size; i++)
                Console.Write(Elements[i] + ",");
            Console.WriteLine();
        }
        
        //O(1) due to probability
        public void Push(E e) {
            EnsureCapacity(Size+1);
            Elements[Size] = e;
            SiftUp(Size);
            Size++;
        }
        
        //O(1)
        public E Peek() {
            return Size != 0 ? Elements[0] : default;
        }

        //O(log(n))
        public E Pop() {
            var val = Elements[0];
            Move(Size-1,0);
            SiftDown(0);
            Size--;
            return val;
        }

        //O(log(n))
        public E PopPush(E e) {
            var val = Elements[0];
            Elements[0] = e;
            SiftDown(0);
            return val;
        }

        private void SiftUp(int pos) {
            var parent = (pos-1)/2;
            while(parent >= 0 && pos >= 0) {
                if(Compare(Elements[pos],Elements[parent])) {
                    Swap(parent,pos);
                    pos = parent;
                    parent = (pos-1)/2;
                }
                else {
                    pos = -1;
                }
            }
        }

        private void SiftDown(int pos) {
            var left = (2*pos)+1;
            var right= (2*pos)+2;
            if(left >= Size)
                return;
            var child = right >= Size || Compare(Elements[left],Elements[right]) ? left : right;
            if(Compare(Elements[child],Elements[pos])) {
                Swap(child,pos);
                SiftDown(child);
            }
        }

        private void EnsureCapacity(int size) {
            if(size < Elements.Length)
                return;
            var capacity = Elements.Length;
            var resizedElements = new E[(size-capacity)+(capacity*2)];
            Array.Copy(Elements,0, resizedElements,0,Size);
            Elements = resizedElements;
        }
        
        private void Swap(int index1, int index2) {
            var val = Elements[index1];
            Elements[index1] = Elements[index2];
            Elements[index2] = val;
        }
        
        private void Move(int from, int to) {
            Elements[to] = Elements[from];
        }
        
    }
}