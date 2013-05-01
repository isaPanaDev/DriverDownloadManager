using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace DriverDownloader
{
    class Model
    {
        private string name;
        private List<string> markList;
        private List<string> operatings;
        private string currentMarkName;
        private Mark currentMarkObj;
        private List<Mark> markObjList;
        private string selectedMark;
        private string selectedOS;

        public string Name { get { return this.name; } set { this.name = value; } }
        public string SelectedMark { get {return this.selectedMark; } set { this.selectedMark = value; }}
        public string SelectedOS { get { return this.selectedOS; } set { this.selectedOS = value; } }
        public List<string> AllMarks { get { return markList; } }
        public List<Mark> AllMarksObj { get { return markObjList; } }
        public string CurrentMarkName { get { return this.currentMarkName; } } 

        public Model() {
            this.markList = new List<string>();
            this.operatings = new List<string>();
            this.markObjList = new List<Mark>();
        }

        public void ParseMarkName(string line)
        {
            StringBuilder marks = new StringBuilder();
            string matchModel = @"CF-([0-9aA-Z]\w+.*?(?=<BR>))|CF-([0-9aA-Z]\w+.*?(?=</TD>))";//first occurrence of <BR> or </TD>
            Regex regModel = new Regex(matchModel);
            Match modelResult = regModel.Match(line);
            marks.Append(modelResult.Groups[0].Value.Replace("&nbsp;", "") +' ');

            string regMatch = @"\(.+\)";
            Regex regMMark1 = new Regex(regMatch);
            Match markResult = regMMark1.Match(line);
            marks.Append(markResult.Groups[0].Value);

            currentMarkName = marks.ToString();           
        }
        public void AddMark()
        {
            if (!markList.Contains(currentMarkName))
            {
                this.currentMarkObj = new Mark(currentMarkName);
                this.currentMarkObj.MarkName = currentMarkName;
                this.currentMarkObj.AddOS(this.operatings);

                this.markObjList.Add(this.currentMarkObj);

                this.operatings = new List<string>();
                markList.Add(currentMarkName);
            }
        }
        public void AddOS(string line)
        {
            // find the operating system
            string regMatch = @"lang='\)""";
            Regex regOs = new Regex(regMatch);
            Match langResult = regOs.Match(line);
            string osFound = langResult.Groups[0].Value;
            if (osFound != "")
            { // now get the value
                string osMatch = @"value=""(.+)""";
                Regex osReg = new Regex(osMatch);
                Match osResult = osReg.Match(line);
                string os = osResult.Groups[1].Value;

                this.operatings.Add(os);
            }
        }
    }
}
