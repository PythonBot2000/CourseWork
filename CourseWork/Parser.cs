using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    internal static class Parser
    {
        static string firstPath = @"first.txt";
        static string secondPath = @"second.txt";

        internal static void ReadFirst(HashTable table)
        {
            string input = File.ReadAllText(firstPath);
            string curTopic;
            string curTitle;
            Date date1;
            int endIndex;
            while(input.Length != 0)
            {
                endIndex = input.IndexOf(";");
                curTopic = input.Substring(0, endIndex);
                input = input.Substring(endIndex + 1);
                endIndex = input.IndexOf(";");
                curTitle = input.Substring(0, endIndex);
                input = input.Substring(endIndex + 1);

                endIndex = input.IndexOf(".");
                date1.day = int.Parse(input.Substring(0, 2));
                input = input.Substring(endIndex + 1);
                endIndex = input.IndexOf(".");
                date1.month = int.Parse(input.Substring(0, 2));
                input = input.Substring(endIndex + 1);
                endIndex = input.IndexOf("\n");
                date1.year = int.Parse(input.Substring(0, 4));

                if(endIndex == -1) input = input.Substring(4);
                else input = input.Substring(endIndex + 1);

                News news = new News { title = curTitle, topic = curTopic, date = date1 };

                table.Add(news);
            }

        }

        internal static void ReadSecond(RedBlackTree tree, HashTable table)
        {
            string input = File.ReadAllText(secondPath);
            string curAuthor;
            string curTitle;
            Date date1;
            int endIndex;
            while(input.Length != 0)
            {
                endIndex = input.IndexOf(";");
                curAuthor = input.Substring(0, endIndex);
                input = input.Substring(endIndex + 1);
                endIndex = input.IndexOf(";");
                curTitle = input.Substring(0, endIndex);
                input = input.Substring(endIndex + 1);

                endIndex = input.IndexOf(".");
                date1.day = int.Parse(input.Substring(0, 2));
                input = input.Substring(endIndex + 1);
                endIndex = input.IndexOf(".");
                date1.month = int.Parse(input.Substring(0, 2));
                input = input.Substring(endIndex + 1);
                endIndex = input.IndexOf("\n");
                date1.year = int.Parse(input.Substring(0, 4));

                if (endIndex == -1) input = input.Substring(4);
                else input = input.Substring(endIndex + 1);

                Comment comment = new Comment { author = curAuthor, title = curTitle, date = date1 };

                if (table.IsNewsExist(comment))
                {
                    tree.AddComment(comment);
                }
            }
        }

        internal static Date ParseDate(string _date)
        {
            Date dateRec = new Date();
            dateRec.day = int.Parse(_date.Substring(0, 2));
            dateRec.month = int.Parse(_date.Substring(3, 2));
            dateRec.year = int.Parse(_date.Substring(6, 4));
            return dateRec;
        }

        internal static void WriteFirst(TableRecords[] table)
        {
            if (table != null)
            {
                File.WriteAllText(firstPath, string.Empty);
                for (int i = 0; i < table.Length; i++)
                {
                    File.AppendAllText(firstPath, $"{table[i].news.topic};{table[i].news.title};{table[i].news.date.PrintDate()}\n");
                }
            }
        }

        internal static void WriteSecond(Comment[] comments)
        {
            if (comments != null)
            {
                File.WriteAllText(secondPath, string.Empty);
                for (int i = 0; i < comments.Length; i++)
                {
                    File.AppendAllText(secondPath, $"{comments[i].author};{comments[i].title};{comments[i].date.PrintDate()}\n");
                }
            }
        }

    }
}
