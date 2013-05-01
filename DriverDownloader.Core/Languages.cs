using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public partial class Languages : UserControl
    {
        public Languages()
        {
            InitializeComponent();
        }

        public Languages(DataTable Languages, string SelectedLanguage)  : this()
        {           
            Text = "Languages";

            // init the combobox            
            cmbLanguages.DataSource = Languages;
            cmbLanguages.DisplayMember = "Name";
            cmbLanguages.ValueMember = "Code";
                        
            // Set Init Value
            if (Settings.Default.DefaultLanguage.Length > 0)
            {
                //int i = cmbLanguages.FindStringExact(Settings.Default.DefaultLanguage);
                //cmbLanguages.SelectedIndex = i;
                cmbLanguages.SelectedValue = Settings.Default.DefaultLanguage;
            }
            else
            { // First time when no default language 
                if (cmbLanguages.Items.Count > 0)
                {
                    // string defaultLang = "English(North America)";
                    int i = cmbLanguages.FindStringExact(SelectedLanguage);
                    cmbLanguages.SelectedIndex = i;
                    Settings.Default.DefaultLanguage = cmbLanguages.SelectedValue.ToString();
                }
            }                                          
        }

        public string DefaultLanuage
        {
            get { return cmbLanguages.SelectedValue.ToString();}
        }

    }
}
