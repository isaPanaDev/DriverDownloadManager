using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace DriverDownloader.JSON
{
    public class Driver 
    {
        private string version;
        private string name;
        private bool download;
        private bool ischecked;
        private int count;
        private string link;
        private string date;
        private string category;
        private string size;
        private string fullName;
        private string os;
        private string priority;
        private string exe;
        public int queue;
        private string directory;
      
        public string Name { get { return name; } set { name = value; } }
        public string Version { get { return version; } set { version = value; } }
        public bool Checkbox { get { return ischecked; } set { ischecked = value; } } // if checked
        public bool Downloaded { get { return download; } set { download = value; } } // if driver is downloaded
        public int Count { get { return count; } set { count = value; } }
        public string Link { get { return link; } set { link = value; } }
        public string Date { get { return date; } set { date = value; } }
        public string Category { get { return category; } set { category = value; } }
        public string Size { get { return size; } set { size = value; } }
        public string FullName { get { return fullName; } set { fullName = value; } }
        public string OperatingSystem { get { return os; } set { os = value; } }
        public string Priority { get { return priority; } set { priority = value; } }
        public string ExeName { get { return exe; } set { exe = value; } }
        public int Queue { get { return queue; } set { queue = value; } }
        public string Directory { get { return directory; } set { directory = value; } }

        public Driver()
        {
            priority = "high";
        }
       
        // For Sorting Name column
        public static int SortNameDescending(Driver x, Driver y)
        {
            return y.Name.CompareTo(x.Name);
        }
        public static int SortNameAscending(Driver x, Driver y)
        {
            return x.Name.CompareTo(y.Name);
        }

        // For Sorting Version column
        public static int SortVersionDescending(Driver x, Driver y)
        {
            return y.Version.CompareTo(x.Version);
        }
        public static int SortVersionAscending(Driver x, Driver y)
        {
            return x.Version.CompareTo(y.Version);
        }

        // For Sorting Category column
        public static int SortCategoryDescending(Driver x, Driver y)
        {
            return y.Category.CompareTo(x.Category);
        }
        public static int SortCategoryAscending(Driver x, Driver y)
        {
            return x.Category.CompareTo(y.Category);
        }

        // For Sorting Size column
        public static int SortSizeDescending(Driver x, Driver y)
        {
            return y.Size.CompareTo(x.Size);
        }
        public static int SortSizeAscending(Driver x, Driver y)
        {
            return x.Size.CompareTo(y.Size);
        }

        // For Sorting Date column
        public static int SortDateDescending(Driver x, Driver y)
        {
            return y.Date.CompareTo(x.Date);
        }
        public static int SortDateAscending(Driver x, Driver y)
        {
            return x.Date.CompareTo(y.Date);
        }
   
    
    }
}
