using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace DriverDownloader
{
    class Mark
    {
        private DataSet marksDataset;
        private string markName;
        private List<string> operatings;
        private DataTable osTable;
        public string MarkName { get { return markName; } set { markName = value; } }
        public DataTable OSTable { get { return this.osTable; } }

         public Mark(string name)
         {
             this.markName = name;
             this.marksDataset = new DataSet();
         }

         internal void AddOS(List<string> list)
         {
             this.operatings = list;
             LoadOsTable();
         }
        
         private void LoadOsTable()
         {
             this.osTable = new DataTable(this.markName);
             osTable.Columns.Add("OS", Type.GetType("System.String"));

             for (int o = 0; o < operatings.Count; o++)
             {
                 DataRow dr = osTable.NewRow();
                 dr["OS"] = operatings[o];
                 osTable.Rows.Add(dr);
             }
             marksDataset.Tables.Add(osTable);
         }
    }
}
