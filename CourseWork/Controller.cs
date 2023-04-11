using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    internal class Controller
    {
        HashTable table;
        RedBlackTree tree;
        internal Controller()
        {
            table = new HashTable();
            tree = new RedBlackTree();
            Parser.ReadFirst(table);
            Parser.ReadSecond(tree, table);
        }

        internal string SearchForTopic(string author)
        {
            MyList list = tree.FindNodeRecords(author);
            if(list == null)
            {
                return "";
            }
            else
            {
                News[] arr = list.TitlesToString();
                string res = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i].topic = table.FindTopic(arr[i]);
                }

                bool isRepeat = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    for(int j = 0; j < i; j++)
                    {
                        if (arr[i].topic == arr[j].topic)
                        {
                            isRepeat = true;
                            j = i;
                        }

                    }
                    if (!isRepeat)
                    {
                        res = res + arr[i].topic + "\n";
                    }
                    isRepeat = false;
                }
                return res;
            }
        }

        internal bool AddNews(News news)
        {
            return table.Add(news);
        }

        internal bool DeleteNews(News news)
        {
            if (tree.IsCommentsExist(news))
            {
                return false;
            }
            else
            {
                return table.Remove(news);
            }
        }

        internal bool AddComment(Comment comment)
        {;
            if(table.IsNewsExist(comment))
            {
                return tree.AddComment(comment);
            }
            else
            {
                return false;
            }
        }

        internal bool DeleteComment(Comment comment)
        {
            return tree.DeleteComment(comment);
        }

        internal Comment[] GetTree()
        {
            MyList list = new MyList();
            tree.GetTreeRecords(list);
            return list.ConvertToCommentsMas();
        }

        internal TableRecords[] GetTable()
        {
            TableRecords[] news = new TableRecords[table.bufferSize];
            table.GetTableRecords(ref news);
            //table.GetFullTable(ref news);
            return news;
        }

        internal string PrintTree()
        {
            return tree.PrintTreeColorless(2);
        }
    }
}
