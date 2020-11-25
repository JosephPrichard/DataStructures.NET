namespace DataStructures.structures.tree
{
    public class BtUtils<V>
    {
        public static bool IsBst(Node<V> node) {
            var hasLeft = node.Left != null;
            var hasRight = node.Right!= null;
            var isBst = (!hasLeft || node.Left.Key() < node.Key()) && (!hasRight || node.Right.Key() > node.Key());
            if(hasLeft) {
                isBst = isBst && IsBst(node.Left);
            }
            if(hasRight) {
                isBst = isBst && IsBst(node.Right);
            }
            return isBst;
        }
        
        public static void Swap(Node<V> left, Node<V> right) {
            var parent = left == null ? right.Parent : left.Parent;
            parent.Left = right;
            parent.Right = left;
        }

        public static void Invert(Node<V> root) {
            Invert(root.Left,root.Right);
        }

        private static void Invert(Node<V> left, Node<V> right) {
            var hasLeft = left != null;
            var hasRight = right != null;
            if(hasLeft || hasRight) {
                Swap(left, right);
            }
            if(hasLeft) {
                Invert(left.Left, left.Right);
            }
            if(hasRight) {
                Invert(right.Left, right.Right);
            }
        }
    }
}