using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public struct Date
    {
        internal int day;
        internal int month;
        internal int year;
        public static bool operator ==(Date date1, Date date2)
        {
            if ((date1.day == date2.day) && (date1.month == date2.month) && (date1.year == date2.year))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Date date1, Date date2)
        {
            if ((date1.day != date2.day) || (date1.month != date2.month) || (date1.year != date2.year))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal string PrintDate()
        {
            string result;
            result = day < 10 ? "0" + day : "" + day;
            result = result + ".";
            result = month < 10 ? result + "0" + month : result + "" + month;
            result = result + "." + year;

            return result;
        }
    }

    struct News
    {
        internal string title;
        internal string topic;
        internal Date date;

        public static bool operator ==(News news1, News news2)
        {
            if ((news1.title == news2.title) && (news1.topic == news2.topic) && (news1.date == news2.date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(News news1, News news2)
        {
            if ((news1.title != news2.title) || (news1.topic != news2.topic) || (news1.date != news2.date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    struct Comment
    {
        internal string author;
        internal string title;
        internal Date date;
    }

    struct TableRecords
    { 
        internal News news;
        internal int h1;
        internal int h2;
    }

}
