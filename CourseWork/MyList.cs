using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourseWork
{
    internal class MyList
    {
        class Node
        {
            internal string author;
            internal Date date;
            internal string title;
            internal Node next;
            internal Node prev;
            
            internal Node(string author, string title, Date date)
            {
                this.author = author;
                this.title = title;
                this.date = date;
                next = null;
                prev = null;
            }
        }

        int count = 0;
        Node Head = null;
        internal News[] TitlesToString()
        {
            if (count == 0) return null;

            News[] arr = new News[count];
            Node cur1 = Head;
            int i = 0;
            while (cur1 != null)
            {
                if(IsNotRepeat(arr, new News { title = cur1.title, date = cur1.date}, i))
                {
                    arr[i].title = cur1.title;
                    arr[i].date = cur1.date;
                    i++;
                }
                cur1 = cur1.next;
            }

            return arr;
        }

        bool IsNotRepeat(News[] arr, News news, int i)
        {
            for(int j = 0; j < i; j++)
            {
                if ((arr[j].title == news.title) && (arr[j].date == news.date))
                {
                    return false;    
                }
            }
            return true;
        }

        internal bool IsEmpty()
        {
            if (Head == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool Add(Comment comment)
        {
            Node node = new Node(comment.author, comment.title, comment.date);

            if (Head == null)
            {
                Head = node;
            }
            else
            {
                Node cur1 = Head;
                while (cur1.next != null)
                {
                    if ((cur1.author == comment.author) && (cur1.title == comment.title)
                        && (cur1.date == comment.date)) return false;
                    cur1 = cur1.next;
                }
                cur1.next = node;
                node.prev = cur1;
                
            }
            count++;
            return true;
        }

        internal int Find(Comment comment)
        {
            int count = 0;
            Node cur = Head;
            
            while (cur != null)
            {
                if ((comment.title == cur.title) && (comment.date == cur.date))
                {
                    return count;
                }
                cur = cur.next;
                count++;
            }
            return -1;
        }

        internal Comment[] ConvertToCommentsMas()
        {
            if(count > 0)
            {
                Comment[] comments = new Comment[count];
                Node node = Head;
                for (int i = 0; i < count; i++)
                {
                    comments[i].author = node.author;
                    comments[i].title = node.title;
                    comments[i].date = node.date;
                    node = node.next;
                }
                return comments;
            }
            else
            {
                return null;
            }
            
        }

        internal bool Delete(Comment comment)
        {
            Node cur1 = Head;
            //Node cur2 = null;
            bool isDeleted = false;
            while (cur1 != null)
            {
                if ((comment.title == cur1.title) && (comment.date == cur1.date))
                {
                    if(cur1.prev != null)
                    {
                        //cur2.next = cur1.next;
                        cur1.prev.next = cur1.next;
                        cur1.next.prev = cur1.prev;
                        cur1 = cur1.next;
                    }
                    else
                    {
                        Head = Head.next;
                        cur1 = Head;
                        Head.prev = null;
                    }
                    count--;
                    isDeleted = true;
                }
                else
                {
                    cur1 = cur1.next;
                }
            }
            return isDeleted;
        }

        internal void CopyList(MyList list, string _author)
        {
            if (Head == null)
            {
                return;
            }
            else
            {
                Node cur1 = Head;
                Node cur2 = null;
                while (cur1 != null)
                {
                    list.Add(new Comment { author = _author, title = cur1.title, date = cur1.date });
                    cur2 = cur1;
                    cur1 = cur1.next;
                }
            }
        }

        internal string Print(string tempStr)
        {
            string str = tempStr;
            Node cur = Head;
            while (cur != null)
            {
                str += cur.title + " ";
                str += cur.date.PrintDate() + tempStr;
                cur = cur.next;
            }
            return str;
        }
    }
}
