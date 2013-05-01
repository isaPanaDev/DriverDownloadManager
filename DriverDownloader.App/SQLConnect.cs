using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml;
//using DriverDownloader.Parser;
using DriverDownloader.JSON;

namespace DriverDownloader
{
    class SQLConnect
    {
        // Provide the following information
        private static string userName = "panasonic";
        private static string password = "P@n@s0n1c";
        private static string dataSource = "tsm2ba6t3o.database.windows.net";
        private static string sampleDatabaseName = "DriverManagerIs";

        // Create a connection string for the sample database
        SqlConnectionStringBuilder connString2Builder;
        private string tableName = "Test1";
        private DataSet dataSet;

        public DataSet currentDataSet { get { return dataSet; } }

        public SQLConnect()
        {
            // Create a connection string for the master database
            SqlConnectionStringBuilder connString1Builder;
            connString1Builder = new SqlConnectionStringBuilder();
            connString1Builder.DataSource = dataSource;
            connString1Builder.InitialCatalog = "master";
            connString1Builder.Encrypt = true;
            connString1Builder.TrustServerCertificate = false;
            connString1Builder.UserID = userName;
            connString1Builder.Password = password;

              // Create a connection string for the master database
            connString2Builder = new SqlConnectionStringBuilder();
            connString2Builder.DataSource = dataSource;
            connString2Builder.InitialCatalog = sampleDatabaseName;
            connString2Builder.Encrypt = true;
            connString2Builder.TrustServerCertificate = false;
            connString2Builder.UserID = userName;
            connString2Builder.Password = password;

            // Connect to the master database and create the sample database
            using (SqlConnection conn = new SqlConnection(connString1Builder.ToString()))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    conn.Open();
                    try
                    {
                        // Create the sample database
                        string cmdText = String.Format("CREATE DATABASE {0}", sampleDatabaseName);
                        command.CommandText = cmdText;
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("SQL ERROR: " + ex.Message); }
                }
            }
        }
        // Method created for populate the dataset and grid 
        internal void ReadSqlToXML()
        {
            using (SqlConnection conn = new SqlConnection(connString2Builder.ToString()))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM " + tableName;
                command.CommandType = CommandType.Text;
                command.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(command);
                dataSet = new DataSet();
                da.Fill(dataSet, "Driver"); // node name in Test.xml
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    // grdXML.DataSource = ds;
                    // grdXML.DataBind();
                }
                // Get a StreamWriter object
                StreamWriter xmlDoc = new StreamWriter("Test.xml");

                // Apply the WriteXml method to write an XML document
                dataSet.WriteXml(xmlDoc);
                xmlDoc.Close();
            }
        }         
       
        internal void loadXMLDataToDB()
        {
            string xmlFilePath = @"driverSample.xml"; 
            XmlDocument document = new XmlDocument();
            document.Load(xmlFilePath);

            dataSet = new DataSet();///////debug:load from xml file to dataset
            StreamReader r = new StreamReader("driverSample.xml");
            dataSet.ReadXml(r);
            r.Close();
           // return;////////////////when SQL not connecting
            
            // Loop through XML data
            List<Driver> allDrivers = new List<Driver>();            
            XmlNodeList xmlNodeList = document.SelectNodes("DRIVERINFO/driver");
            List<string> columns = GetColumnNames();
            foreach (XmlNode node in xmlNodeList)
            {
              /*  int c = 0;
                Driver driver = new Driver();
                driver.Id = int.Parse(node[columns[c++]].InnerText);
                driver.Name = node[columns[c++]].InnerText;
                driver.OS = node[columns[c++]].InnerText;
                driver.Size = node[columns[c++]].InnerText;
               
                allDrivers.Add(driver);*/
            }

            using (SqlConnection conn = new SqlConnection(connString2Builder.ToString()))
            {
                Int32 tableExist = 0;
                using (SqlCommand command = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();

                        // Check Table existense
                        string sql = "SELECT 1 as IsExists FROM dbo.sysobjects where id=object_id('[dbo].[" + tableName + "]')";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        tableExist = (Int32)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL CONNECT ERROR: " + ex.Message); 
                    }

                    if (tableExist == 1) // table exist=1;not exist=0
                    {
                        command.CommandText = "DROP TABLE " + tableName;
                        command.ExecuteNonQuery();
                    }
                    StringBuilder columnBuilder = new StringBuilder("CREATE TABLE " + tableName + " (");
                    StringBuilder columnNames = new StringBuilder(" (");
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (i == 0)
                            columnBuilder.Append(columns[i] + " int primary key ");
                        else
                            columnBuilder.Append(columns[i] + " varchar(20) ");
                        columnNames.Append(columns[i]);
                        if (i == columns.Count - 1)
                        {
                            columnBuilder.Append(" ) ");
                            columnNames.Append(")");
                        }
                        else
                        {
                            columnBuilder.Append(", ");
                            columnNames.Append(", ");
                        }
                    }
                    command.CommandText = columnBuilder.ToString();
                    command.ExecuteNonQuery();

                    // sample: "INSERT INTO T1 (col1, col2) values (1, 'string 1'), (2, 'string 2'), (3, 'string 3')";
                    StringBuilder rowBuilder = new StringBuilder("INSERT INTO " + tableName + columnNames + " values ");
                    StringBuilder rowValues = new StringBuilder();
                    for (int i = 0; i < allDrivers.Count; i++)
                    {
                        Driver d = allDrivers[i];
                      ///////// REDO using DRIVER CLASS  rowValues.Append("('" + d.Id + "', '" + d.Name + "', '" + d.OS + "', '" + d.Size + "'");
                        if (i == allDrivers.Count - 1)
                            rowValues.Append(")");
                        else
                            rowValues.Append("), ");
                    }
                    rowBuilder.Append(rowValues.ToString());
                    // debug: StringBuilder newstr = new StringBuilder("INSERT INTO new24 (ID, Name, OS, Size, ReleaseDate, Required) values (30, 'CF31X96KM', 'CF31K/L (mk3)', 'XP', '230GB', '12-09-03', 'True'),(22, 'CF53XMPRKM', 'CF53Q (mk2)', 'Win 7', '20GB', '11-03-4', 'False')");

                    command.CommandText = rowBuilder.ToString();
                    int rowsAdded = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private List<string> GetColumnNames()
        {
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Name");
            columns.Add("OS");
            columns.Add("Size");
            return columns;
        }

    }
}
