using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    internal class HashTable
    {
        struct Node
        {
            internal enum State { exist, notExist, deleted }

            internal int h1;
            internal News value;
            internal State state;
            Node(News _value, State _state = State.notExist)
            {
                h1 = -1;
                value = _value;
                state = _state;
            }
        }

        const int Q = 3;
        const int defaultSize = 10;
        const double rehashSize = 0.75;
        Node[] arr;
        int size;
        internal int bufferSize;
        int sizeAllNonNull;

        internal HashTable()
        {
            bufferSize = defaultSize;
            size = 0;
            sizeAllNonNull = 0;
            arr = new Node[bufferSize];
            for(int i = 0; i < bufferSize; i++)
            {
                arr[i].state = Node.State.notExist;
            }
        }


        int returnState(int i)
        {
            return (int)arr[i].state;
        }


        int HashFunction(News news, int tableSize)
        {
            int result = 0;
            for(int i = 0; i < news.title.Length; i++)
            {
                result += news.title[i];
            }
            result += news.date.day + news.date.month + news.date.year;
            result = result % tableSize;
            return result;
        }

        int ExtraHashFunction(int h1)
        {
            return (h1 + Q) % bufferSize;
        }

        void Resize()
        {
            int pastBufferSize = bufferSize;
            bufferSize *= 2;
            sizeAllNonNull = 0;
            size = 0;
            Node[] arr1 = new Node[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
                arr1[i].state = Node.State.notExist;
            }

            Node[] arr2 = arr;
            arr = arr1;
            for (int i = 0; i < pastBufferSize; i++)
            {
                if (arr2[i].state == Node.State.exist)
                {
                    Add(arr2[i].value);
                }
            }
        }

        void Rehash()
        {
            sizeAllNonNull = 0;
            size = 0;
            Node[] arr1 = new Node[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
                arr1[i].state = Node.State.notExist;
            }
            Node[] arr2 = arr;
            arr = arr1;
            for (int i = 0; i < bufferSize; i++)
            {
                if (arr2[i].state == Node.State.exist)
                {
                    Add(arr2[i].value);
                }
            }
        }

        internal string FindTopic(News news)
        {
            int h1 = HashFunction(news, bufferSize);
            int firstH1 = h1;
            do
            {
                if ((arr[h1].state == Node.State.exist) && (arr[h1].value.title == news.title) && (arr[h1].value.date == news.date))
                {
                    return arr[h1].value.topic;
                }
                h1 = ExtraHashFunction(h1);
            } while ((arr[h1].state != Node.State.notExist) && (h1 != firstH1));
            return "";
        }

        internal int Find(News value)
        {
            int h1 = HashFunction(value, bufferSize);
            int firstH1 = h1;
            do
            {
                if ((arr[h1].state == Node.State.exist) && (arr[h1].value == value))
                {
                    return h1;
                }
                h1 = ExtraHashFunction(h1);
            } while ((arr[h1].state != Node.State.notExist) && (h1 != firstH1));

            return -1;
        }

        internal bool IsNewsExist(Comment comment)
        {
            News news = new News { title = comment.title, date = comment.date };
            int h1 = HashFunction(news, bufferSize);
            int firstH1 = h1;

            do
            {
                if ((arr[h1].state == Node.State.exist) && (arr[h1].value.title == news.title) && (arr[h1].value.date == news.date))
                {
                    return true;
                }
                h1 = ExtraHashFunction(h1);
            } while ((arr[h1].state != Node.State.notExist) && (h1 != firstH1));

            return false;
        }

        internal bool Remove(News value)
        {
            int num = Find(value);
            if(num != -1)
            {
                arr[num].state = Node.State.deleted;
                size--;
                return true;
            }
            return false;
        }

        internal bool Add(News value)
        {
            if (size + 1 > (rehashSize * bufferSize))
            {
                Resize();
            }
            else if (sizeAllNonNull > 2 * size)
            {
                Rehash();
            }
            int h1 = HashFunction(value, bufferSize);
            int firstH1 = h1;
            int firstDeleted = -1;
            do
            {
                if ((arr[h1].state == Node.State.exist) && (arr[h1].value == value))
                {
                    return false;
                }
                if ((arr[h1].state == Node.State.deleted) && (firstDeleted == -1))
                {
                    firstDeleted = h1;
                }
                h1 = ExtraHashFunction(h1);
            } while ((arr[h1].state != Node.State.notExist) && (h1 != firstH1));

            if (firstDeleted == -1)
            {
                arr[h1].h1 = ExtraHashFunction(firstH1);
                arr[h1].value = value;
                arr[h1].state = Node.State.exist;
                sizeAllNonNull++;
            }
            else
            {
                arr[firstDeleted].h1 = ExtraHashFunction(firstH1);
                arr[firstDeleted].value = value;
                arr[firstDeleted].state = Node.State.exist;
            }
            size++;
            return true;
        }

        internal void GetTableRecords(ref TableRecords[] news)
        {
            int num = 0;
            for(int i = 0; i < bufferSize; i++)
            {
                if (arr[i].state == Node.State.exist)
                {
                    news[num].news = arr[i].value;
                    news[num].h1 = arr[i].h1;
                    news[num].h2 = i;
                    num++;
                }
            }
            Array.Resize(ref news, num);
        }

        internal void GetFullTable(ref TableRecords[] news)
        {
            int num = 0;
            for (int i = 0; i < bufferSize; i++)
            {
                news[num].news = arr[i].value;
                news[num].h1 = arr[i].h1;
                news[num].h2 = i;
                num++;
            }
            Array.Resize(ref news, num);
        }

    }
}
