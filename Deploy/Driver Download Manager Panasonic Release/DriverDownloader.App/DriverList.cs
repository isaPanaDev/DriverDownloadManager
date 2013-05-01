using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DriverDownloader.Core.Common;
using DriverDownloader.JSON;
using System.Collections;
using BrightIdeasSoftware;

namespace DriverDownloader
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);
    
      public partial class DriverList : UserControl
    {
        public event ChangedEventHandler CheckmarkChanged;
        		
        public bool selectAll;
        private string colDownload = Resources.common.DriverList_Columns_Download;
        private string colDriverName = Resources.common.DriverList_Columns_DriverName;
        private string colVersion = Resources.common.DriverList_Columns_Version;
        private string colCategory = Resources.common.DriverList_Columns_Category;
        private string colSize = Resources.common.DriverList_Columns_Size;
        private string colDate = Resources.common.DriverList_Columns_Date;
        private List<Driver> drivers;
        private string sortOrder = "";
        private int nsortColumnIndex = -1;
        

        public DriverList()
        {
            InitializeComponent();

            drivers = new List<Driver>();
            selectAll = false;
        }
        internal bool CreateDriverList(List<Driver> driverList)
        {
            bool existsAlready = false;
            drivers = driverList;
       
            // Reset the look of few fields that might be blank
            for (int i = 0; i < drivers.Count; i++)
            {
                // Version
                if (drivers[i].Version.ToString() == "")
                {
                    drivers[i].Version = Resources.common.DriverList_Content_NA;
                }
                // Size
                if (drivers[i].Size.ToString() == "")
                {
                    drivers[i].Size = Resources.common.DriverList_Content_NA;
                }
                else
                {
                    // Converting size to a round number!
                    string text = drivers[i].Size.ToString();
                    drivers[i].Size = ByteFormatter.ToString(Convert.ToInt32(text));
                }
                if (existsAlready == false && drivers[i].Downloaded == true)
                    existsAlready = true;
            }
            // sort drivers if listview's sortorder is not empty
            // after your first search, if you click column to sort. 
            // When you do another search, the list view will remember previous sort order and will sort automatically.
            // ifyou don't sort the driver list, then the list view items doesn't match the items in driverlist. 
            // commented by George 09/13/2012
            if (sortOrder != "")
            {
                // 
                if (nsortColumnIndex != -1)
                {
                    sortColumn(nsortColumnIndex);
                }
                else
                {
                    // this shall never happen.
                    MessageBox.Show("Unexpected error occured!");
                }
            }

            // ************* DEBUGGING BEGIN
            //List<Driver> list= DebugList();
            //drivers = null;
            //drivers = list;
            // ************* DEBUGGING END

            this.driverListView.SetObjects(driverList);
         
            this.driverListView.ItemChecked += new ItemCheckedEventHandler(driverListView_ItemChecked);
            this.driverListView.ColumnClick += new ColumnClickEventHandler(driverListView_ColumnClick);
          
            return existsAlready;
        }

        private List<Driver> DebugList()
        {
            List<Driver> masterList = new List<Driver>();
            Driver d = new Driver();
            d.Checkbox = true;
            d.Name = "Alana Roderick";
            d.Category = "cat";
            d.Version = "33.e.";
            d.Size = "23B";
            d.Date = "339";
            Driver d1 = new Driver();
            d1.Name = "Brianka Roderick";
            d1.Category = "cat1";
            d1.Version = "ver33.e.";
            d1.Size = "size1";
            d1.Date = "339";
            Driver d2 = new Driver();
            d2.Name = "west Roderick";
            d2.Category = "cat1";
            d2.Version = "33.e.";
            d2.Size = "23B";
            d2.Date = "339";
            masterList.Add(d);
            masterList.Add(d1);
            masterList.Add(d2);
            masterList.Add(d);

            return masterList;
        }
        void driverListView_AfterSorting(object sender, AfterSortingEventArgs e)
        {
            // Make sure to disable, so checkall selection doesn't trigger sorting!
            // this.driverListView.BeforeSorting -= new EventHandler<BeforeSortingEventArgs>(driverListView_BeforeSorting);
            // this.driverListView.AfterSorting -= new EventHandler<AfterSortingEventArgs>(driverListView_AfterSorting);
        }
        void driverListView_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {
            // Save previous state's sorting order!
            sortOrder = e.SortOrder.ToString().ToLower();
            nsortColumnIndex = e.ColumnToSort.Index;
        }

        void sortColumn(int ColumnIndex)
        {
            switch (ColumnIndex)
            {
                case 1: // Name column
                    if (sortOrder == "ascending" || sortOrder == "")
                    {
                        drivers.Sort(Driver.SortNameAscending);
                    }
                    else
                    {
                        drivers.Sort(Driver.SortNameDescending);
                    }
                    break;
                case 2: // Version column
                    if (sortOrder == "ascending" || sortOrder == "")
                    {
                        drivers.Sort(Driver.SortVersionAscending);
                    }
                    else
                    {
                        drivers.Sort(Driver.SortVersionDescending);
                    }
                    break;
                case 3: // Category column
                    if (sortOrder == "ascending" || sortOrder == "")
                    {
                        drivers.Sort(Driver.SortCategoryAscending);
                    }
                    else
                    {
                        drivers.Sort(Driver.SortCategoryDescending);
                    }
                    break;
                case 4: // Size column
                    if (sortOrder == "ascending" || sortOrder == "")
                    {
                        drivers.Sort(Driver.SortSizeAscending);
                    }
                    else
                    {
                        drivers.Sort(Driver.SortSizeDescending);
                    }
                    break;
                case 5: // Date column
                    if (sortOrder == "ascending" || sortOrder == "")
                    {
                        drivers.Sort(Driver.SortDateAscending);
                    }
                    else
                    {
                        drivers.Sort(Driver.SortDateDescending);
                    }
                    break;
                default:
                    // drivers.Sort(Driver.SortAscending);
                    break;
            }
        }

        void driverListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.driverListView.BeforeSorting += new EventHandler<BeforeSortingEventArgs>(driverListView_BeforeSorting);
            this.driverListView.AfterSorting += new EventHandler<AfterSortingEventArgs>(driverListView_AfterSorting);

            // Need to sort the drivers objects to stay current with UI!!
            sortColumn(e.Column);

            Console.WriteLine("SORTED DRIVERS -> " + sortOrder.ToUpper());
            foreach (Driver d in drivers)
                Console.WriteLine(d.Checkbox.ToString() + " - " + d.Name.ToString());
        }

          // Checkbox EventHandler
        void driverListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            bool checkedValue = e.Item.Checked;
            int index = e.Item.Index;
            Driver checkDriver = drivers[index];
            checkDriver.Checkbox = !checkDriver.Checkbox; // set the Checkmark
            Console.WriteLine("----------------------");
            Console.WriteLine("SET DRIVER: " + checkDriver.Name + " TO: " + checkDriver.Checkbox.ToString());
            Console.WriteLine("----------------------"); 
            if (checkDriver.Checkbox.ToString().ToLower() == "false")
            {
                selectAll = false;
                OnCheckmarkChanged(EventArgs.Empty);
            }
            else
            {
                selectAll = true;
               foreach(Driver d in drivers)
                {
                    if (d.Checkbox == false)
                    {
                        selectAll = false;
                        OnCheckmarkChanged(EventArgs.Empty);
                        break;
                    }
                    OnCheckmarkChanged(EventArgs.Empty);
                }
            }
            this.driverListView.Refresh();
        }

        private void driverListView_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            Driver driver = (Driver)e.Model;
            if (driver.Downloaded)
            {            
                e.Item.UseItemStyleForSubItems = true;
                e.Item.BackColor = Color.LawnGreen; 

                // Force the redraw when color has changed!
                OLVColumn column;
                Point pt = this.driverListView.PointToClient(e.Item.Position);
                OLVListItem item = this.driverListView.GetItemAt(1, 1, out column);
            }
            else
            {
                e.Item.UseItemStyleForSubItems = true;
                e.Item.BackColor = SystemColors.Control;
            }
        }

        protected virtual void OnCheckmarkChanged(EventArgs e)
        {
            if (CheckmarkChanged != null)
                CheckmarkChanged(this, e);
        }

        internal void CheckAll(bool checkAll)
        {
           foreach (Driver d in drivers) {
               d.Checkbox = checkAll;
           }
        
           driverListView.Refresh();
           driverListView.Update();
        }
        internal void InitializeOrder()
        {
            driverListView.Refresh();
            driverListView.Update();
        }
        internal List<Driver> SelectedDrivers()
        {
            List<Driver> selectedDrivers = new List<Driver>();
            foreach (Driver driver in drivers)
            {
                if (driver.Checkbox)
                {
                    // Check for if the executable exists
                    if (driver.ExeName != null)
                        selectedDrivers.Add(driver);
                }
            }
            return selectedDrivers;
        }

        internal void CleanDriverList()
        {            
            this.driverListView.ItemChecked -= new ItemCheckedEventHandler(driverListView_ItemChecked);
            this.driverListView.ColumnClick -= new ColumnClickEventHandler(driverListView_ColumnClick);
            drivers = null;
            drivers = new List<Driver>();
            //sortOrder = "";
            this.driverListView.ClearObjects();
        }
    }    
}
