using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataStructures.structures.list;
using JetBrains.Annotations;

namespace DataStructures.structures.tree
{
    public class BinarySearchTree<V> where V : IComparable
    {
        private Node<V> Root;
        public int Size { private set; get; }

        public V Min() {
            if(Root == null)
                throw new EmptyTreeException();
            return FindMin(Root).Val();
        }

        public V Max() {
            if(Root == null)
                throw new EmptyTreeException();
            return FindMax(Root).Val();
        }
        
        public void Push(int key, V v) {
            var newNode = new Node<V>(key, v);
            if(Root == null) {
                Root = newNode;
            }
            else {
                Insert(Root,newNode);
            }
            Size++;
        }

        public void Remove(int key) {
            if(Root == null)
                throw new EmptyTreeException();
            FindAndRemove(Root,key);
        }

        private static void Insert(Node<V> parent, Node<V> n) {
            if(n.Key() < parent.Key()) {
                if(parent.Left == null) {
                    parent.Left = n;
                    n.Parent = parent;
                }
                else {
                    Insert(parent.Left,n);
                }
            }
            else if (n.Key() > parent.Key()) {
                if(parent.Right == null) {
                    parent.Right = n;
                    n.Parent = parent;
                }
                else {
                    Insert(parent.Right,n);
                }
            }
            else {
                throw new DuplicateKeyException();
            }
        }

        private void FindAndRemove(Node<V> curr, int key) {
            if(key < curr.Key()) {
                if(curr.Left != null) {
                    FindAndRemove(curr.Left,key);
                }
            } 
            else if(key > curr.Key()) {
                if(curr.Right != null) {
                    FindAndRemove(curr.Right,key);
                }
            }
            else {
                Remove(curr);
                Size--;
            }
        }

        private static Node<V> FindMin(Node<V> node) {
            while(node.Left != null) {
                node = node.Left;
            }
            return node;
        }
        
        private static Node<V> FindMax(Node<V> node) {
            while(node.Right != null) {
                node = node.Right;
            }
            return node;
        }

        private void Remove(Node<V> r) {
            var hasLeft = r.Left != null;
            var hasRight = r.Right != null;
            if(hasRight && hasLeft) {
                var successor = FindMin(r.Right);
                Move(successor,r);
                Remove(successor);
            }  
            else if(hasRight) {
                ReplaceNodeInParent(r,r.Right);
            } 
            else if(hasLeft) {
                ReplaceNodeInParent(r,r.Left);
            }
            else {
                ReplaceNodeInParent(r,null);
            }
        }

        private void ReplaceNodeInParent(Node<V> node, Node<V> newNode) {
            if(node.Parent == null) {
                Root = newNode;
                return;
            }
            if(node.Parent.Left == node) {
                node.Parent.Left = newNode;
            }
            else {
                node.Parent.Right = newNode;
            }
            if(newNode != null) {
                newNode.Parent = node.Parent;
            }
        }

        public static void Swap(Node<V> node1, Node<V> node2) {
            var pair = node1.Data;
            node1.Data = node2.Data;
            node2.Data = pair;
        }
        
        private static void Move(Node<V> from, Node<V> to) {
            to.Data = from.Data;
        }
        
        public IEnumerable<V> PreOrder() {
            var al = new ArrayList<V>(Size);
            PreOrder(Root,al);
            for(var i = 0; i < al.Size; i++) {
                yield return al[i];
            }
        }

        private static void PreOrder(Node<V> node, ArrayList<V> list) {
            if(node == null)
                return;
            list.PushBack(node.Val());
            if(node.Left != null) {
                PreOrder(node.Left,list);
            } 
            if(node.Right != null) {
                PreOrder(node.Right,list);
            }
        }
        
        public IEnumerable<V> PostOrder() {
            var al = new ArrayList<V>(Size);
            PostOrder(Root,al);
            for(var i = 0; i < al.Size; i++) {
                yield return al[i];
            }
        }
        
        private static void PostOrder(Node<V> node, ArrayList<V> list) {
            if(node == null)
                return;
            if(node.Left != null) {
                PostOrder(node.Left,list);
            } 
            if(node.Right != null) {
                PostOrder(node.Right,list);
            }
            list.PushBack(node.Val());
        }
        
        public IEnumerable<V> InOrder() {
            var al = new ArrayList<V>(Size);
            InOrder(Root,al);
            for(var i = 0; i < al.Size; i++) {
                yield return al[i];
            }
        }
        
        private static void InOrder(Node<V> node, ArrayList<V> list) {
            if(node == null)
                return;
            if(node.Left != null) {
                InOrder(node.Left,list);
            } 
            list.PushBack(node.Val());
            if(node.Right != null) {
                InOrder(node.Right,list);
            }
        }

        [AssertionMethod]
        private void EmptyCheck() {
            if(Size == 0)
                throw new EmptyTreeException();
        }
        
        public void PrintConsole() {
            if(Root == null)
                return;
            PrintConsole(Root);
            Console.WriteLine();
        }
        
        private static void PrintConsole(Node<V> node) {
            Console.Write(" | ");
            Console.Write("P: " + node.Key());
            if(node.Left != null) {
                Console.Write(", L: " + node.Left.Key());
            }
            if(node.Right != null) {
                Console.Write(", R: " + node.Right.Key());
            }
            if(node.Left != null) {
                PrintConsole(node.Left);
            }
            if(node.Right != null) {
                PrintConsole(node.Right);
            }
        }
    }
}