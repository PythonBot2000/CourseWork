using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourseWork
{
    internal class RedBlackTree
    {
        enum Color { Black, Red }

        class Node
        {
            internal Node right;
            internal Node left;
            internal Node parent;
            internal Color color;
            internal string key;
            internal MyList list;
            //internal Node() { }
            internal Node(Node right, Node left, Node parent, Color color, string key)
            {
                this.right = right;
                this.left = left;
                this.parent = parent;
                this.color = color;
                this.key = key;
                list = new MyList();
            }   
        }

        Node root;

        void rotateLeft(Node x)
        {
            Node y = x.right;
            if(y != null)
            {
                x.right = y.left;
                y.parent = x.parent;

                if (y.left != null) y.left.parent = x;

                if (x.parent != null)
                {
                    if (x == x.parent.left)
                    {
                        x.parent.left = y;
                    }
                    else
                    {
                        x.parent.right = y;
                    }
                }
                else
                {
                    root = y;
                }

                y.left = x;
                x.parent = y;
            }
        }

        void rotateRight(Node x)
        {
            Node y = x.left;
            if (y != null)
            {
                x.left = y.right;
                y.parent = x.parent;

                if (y.right != null) y.right.parent = x;

                if (x.parent != null)
                {
                    if (x == x.parent.right)
                    {
                        x.parent.right = y;
                    }
                    else
                    {
                        x.parent.left = y;
                    }
                }
                else
                {
                    root = y;
                }

                y.right = x;
                x.parent = y;
            }
        }

        internal bool AddComment(Comment comment)
        {
            Node node = FindNode(comment.author);
            if (node == null)
            {
                InsertNode(comment.author);
                node = FindNode(comment.author);
                return node.list.Add(comment);
                
            }
            else
            {
                return node.list.Add(comment);
            }
            
        }

        internal bool DeleteComment(Comment comment)
        {
            Node node = FindNode(comment.author);
            bool isDeleted = false;
            if (node != null)
            {
                isDeleted = node.list.Delete(comment);

                if (node.list.IsEmpty())
                {
                    this.DeleteNode(node.key);
                }
            }
            return isDeleted;
        }

        internal MyList FindNodeRecords(string key)
        {
            Node current = FindNode(key);
            if(current != null)
            {
                return current.list;
            }
            else
            {
                return null;
            }
        }

        Node FindNode(string key)
        {
            Node current = root;
            while (current != null)
            {
                if (key == current.key)
                {
                    return current;
                }
                else
                {
                    current = StringCompare(key, current.key) ? current.left : current.right;
                }
            }
            return null;
        }

        void InsertNode(string key)
        {
            Node current = root;
            Node parent = null;
            while (current != null)
            {
                if (key == current.key) return;
                parent = current;
                current = StringCompare(key, current.key) ? current.left : current.right;
            }
            Node x = new Node(null, null, parent, Color.Red, key);

            if (parent != null)
            {
                if(StringCompare(key, parent.key))
                {
                    parent.left = x;
                }
                else
                {
                    parent.right = x;
                }
            }
            else
            {
                root = x;
            }
            InsertFixup(x);
        }

        void InsertFixup(Node x)
        {
            while ((x != root) && (x.parent.color == Color.Red))
            {
                if (x.parent == x.parent.parent.left)
                {
                    Node y = x.parent.parent.right;
                    if((y != null) && (y.color == Color.Red))
                    {
                        x.parent.color = Color.Black;
                        y.color = Color.Black;
                        x.parent.parent.color = Color.Red;
                        x = x.parent.parent;
                    }
                    else
                    {
                        if (x == x.parent.right)
                        {
                            x = x.parent;
                            rotateLeft(x);
                        }

                        x.parent.color = Color.Black;
                        x.parent.parent.color = Color.Red;
                        rotateRight(x.parent.parent);
                    }
                }
                else
                {
                    Node y = x.parent.parent.left;
                    if((y != null) && (y.color == Color.Red))
                    {
                        x.parent.color = Color.Black;
                        y.color = Color.Black;
                        x.parent.parent.color = Color.Red;
                        x = x.parent.parent;
                    }
                    else
                    {
                        if (x == x.parent.left)
                        {
                            x = x.parent;
                            rotateRight(x);
                        }

                        x.parent.color = Color.Black;
                        x.parent.parent.color = Color.Red;
                        rotateLeft(x.parent.parent);
                    }
                }
            }
            root.color = Color.Black;
        }

        internal void DeleteNode(string key)
        {
            Node z;
            z = FindNode(key);

            if (z == null) return;

            Node x, y;

            if ((z.left == null) || (z.right == null))
            {
                y = z;
            }
            else
            {
                y = z.right;
                while (y.left != null) y = y.left;
            }

            if (y.left != null)
            {
                x = y.left;
            }
            else
            {
                x = y.right;
            }

            if (x != null) x.parent = y.parent;

            bool xSideLeft = true;
            if (y.parent != null)
            {
                if (y == y.parent.left)
                {
                    xSideLeft = true;
                    y.parent.left = x;
                }
                else
                {
                    xSideLeft = false;
                    y.parent.right = x;
                }
            }
            else
            {
                root = x;
            }

            if (y != z)
            {
                z.key = y.key;
                z.list = y.list;
            }

            if (y.color == Color.Black) DeleteFixup(x, y.parent, xSideLeft);
        }

        void DeleteFixup(Node x, Node xParent, bool xSideLeft)
        {
            while (x != root && ((x == null) || (x.color == Color.Black)))
            {
                if (xSideLeft)
                {
                    Node w = xParent.right;
                    if ((w != null) && (w.color == Color.Red))
                    {
                        w.color = Color.Black;
                        xParent.color = Color.Red;
                        rotateLeft(xParent);
                        w = xParent.right;
                    }

                    bool wLeftIsBlack = true;
                    bool wRightIsBlack = true;
                    if ((w == null) || (w.left == null || w.left.color == Color.Black)) wLeftIsBlack = true;
                    else wLeftIsBlack = false;
                    if ((w == null) || (w.right == null || w.right.color == Color.Black)) wRightIsBlack = true;
                    else wRightIsBlack = false;

                    if (wLeftIsBlack && wRightIsBlack)
                    {
                        if (w != null) w.color = Color.Red;
                        x = xParent;
                        xParent = x.parent;
                    }
                    else
                    {
                        if (wRightIsBlack)
                        {
                            w.left.color = Color.Black;
                            w.color = Color.Red;
                            rotateRight(w);
                            w = xParent.right;
                        }
                        w.color = xParent.color;
                        xParent.color = Color.Black;
                        if (w.right != null) w.right.color = Color.Black;
                        rotateLeft(xParent);
                        x = root;
                    }
                }
                else
                {
                    Node w = xParent.left;
                    if (w != null && (w.color == Color.Red))
                    {
                        w.color = Color.Black;
                        xParent.color = Color.Red;
                        rotateRight(xParent);
                        w = xParent.left;
                    }

                    bool wLeftIsBlack = true;
                    bool wRightIsBlack = true;
                    if (w == null || (w.left == null || w.left.color == Color.Black)) wLeftIsBlack = true;
                    else wLeftIsBlack = false;
                    if (w == null || (w.right == null || w.right.color == Color.Black)) wRightIsBlack = true;
                    else wRightIsBlack = false;

                    if (wLeftIsBlack && wRightIsBlack)
                    {
                        if (w != null) w.color = Color.Red;
                        x = xParent;
                        xParent = x.parent;
                    }
                    else
                    {
                        if (wLeftIsBlack)
                        {
                            w.right.color = Color.Black;
                            w.color = Color.Red;
                            rotateLeft(w);
                            w = xParent.left;
                        }
                        w.color = xParent.color;
                        xParent.color = Color.Black;
                        if (w.left != null) w.left.color = Color.Black;
                        rotateRight(xParent);
                        x = root;
                    }
                }

                if (x != root)
                {
                    if (x == xParent.left)
                    {
                        xSideLeft = true;
                    }
                    else
                    {
                        xSideLeft = false;
                    }
                }
            }
            x.color = Color.Black;
        }

        internal void PrintTree(int l)
        {
            PrintTree(root, l);
            Console.ResetColor();
        }

        void PrintTree(Node snode, int l)
        {
            Node node = snode;
            if (node != null)
            {
                PrintTree(node.right, l + 1);
                for (int i = 0; i < l; i++) Console.Write("     ");
                if (node.color == Color.Red)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write(node.key);
                PrintTree(node.left, l + 1);
            }
            else
            {
                Console.WriteLine();
            }
        }

        internal string PrintTreeColorless(int l)
        {
            string str = "";
            PrintTreeColorless(root, l, ref str);
            Console.ResetColor();
            return str;
        }

        void PrintTreeColorless(Node snode, int l, ref string str)
        {
            Node node = snode;
            if (node != null)
            {
                PrintTreeColorless(node.right, l + 1, ref str);
                for (int i = 0; i < l; i++) str += "     ";
                if (node.color == Color.Red)
                {
                    str += "(R)";
                }
                else
                {
                    str += "(B)";
                }
                string tempStr = "\n";
                str += node.key;
                for (int i = 0; i < l; i++) tempStr += "     ";
                str += node.list.Print(tempStr);
                PrintTreeColorless(node.left, l + 1, ref str);
            }
            else
            {
                str += "\n";
            }
        }

        bool StringCompare(string s1, string s2)
        {
            int len = s1.Length < s2.Length ? s1.Length : s2.Length;

            for (int i = 0; i < len; i++)
            {
                if (s1[i] < s2[i])
                {
                    return true;
                }
                else if (s1[i] > s2[i])
                {
                    return false;
                }
            }

            if (s1.Length <= s2.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool IsCommentsExist(News news)
        {
            bool isCommentsExist = false;
            Node node = root;
            Comment comment = new Comment { title = news.title, date = news.date };
            TreeWalk(root, comment, ref isCommentsExist);
            return isCommentsExist;
        }

        void TreeWalk(Node node, Comment comment, ref bool f)
        {
            if (node == null) return;
            if(node.list.Find(comment) != -1)
            {
                f = true;
                return;
            }
            TreeWalk(node.left, comment, ref f);
            if(f) return;
            TreeWalk(node.right, comment, ref f);
        }

        internal void GetTreeRecords(MyList list)
        {
            Node node = root;
            TreeWalkForComments(node, list);
        }

        void TreeWalkForComments(Node node, MyList list)
        {
            if (node == null)
            {
                return;
            }
            else
            {
                node.list.CopyList(list, node.key);
            }
            TreeWalkForComments(node.left, list);
            TreeWalkForComments(node.right, list);
        }
    }
}
