using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public partial class FavorateModels : UserControl
    {
        private DataTable m_FavModels;
        private String m_strFavorateFile;

        public FavorateModels()
        {
            InitializeComponent();
            
            m_strFavorateFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Favorates.xml");

            this.Text = "Favorate Models";
            m_FavModels = new DataTable();
            LoadFavorates();

                        
            // bind models to data grid view            
            bindingSourceModels.DataSource = m_FavModels;
            dgvModel.DataSource = bindingSourceModels;
            dgvModel.AutoGenerateColumns = true;
        }

        public bool SaveFavorates()
        {
            try
            {
                m_FavModels.WriteXml(m_strFavorateFile, XmlWriteMode.IgnoreSchema);
            }
            catch
            {
                MessageBox.Show("Error occured when saving favorate models!");
            }

            return true;
        }


        private bool LoadFavorates()
        {
            m_FavModels.TableName = "FavorateModels";
            m_FavModels.Columns.Add("Model Number", System.Type.GetType("System.String"));
            m_FavModels.Columns.Add("Model Version", System.Type.GetType("System.String"));
            m_FavModels.Columns.Add("Operating System", System.Type.GetType("System.String"));

            m_FavModels.Rows.Add("CF-07", "CF-07", "Windows XP");
            
            return true;        
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bindingSourceModels.AddNew();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvModel.SelectedRows.Count > 0)
            {                
                bindingSourceModels.RemoveAt(dgvModel.SelectedRows[0].Index);
            }

        }

        /*
        private void dgvModel_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
            
                DataGridViewComboBoxCell dgvCboB = new DataGridViewComboBoxCell();
                DataTable dt = new DataTable("test");
                dt.Columns.Add("value");
                dt.Rows.Add("1");
                dt.Rows.Add("2");
                dt.Rows.Add("3");
                dgvCboB.DataSource = dt;
                dgvCboB.DisplayMember = "value"; 
                dgvCboB.ValueMember = "value" ;
                dgvCboB.AutoComplete = true; 
                dgvModel[e.ColumnIndex, e.RowIndex] = dgvCboB;
            }

        }
        */
    
    }
}
