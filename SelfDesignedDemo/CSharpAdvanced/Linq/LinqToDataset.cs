using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CSharpAdvanced.Linq
{
    class LinqToDataset
    {
        public  void testConn()
        {
            DataSet dataSet = new DataSet();
            //Write in code
            string connectionString = "Data Source=10.219.10.172;Initial Catalog=Test;Integrated Security=True";
            //Write in Config
            string connStr = ConfigurationManager.ConnectionStrings["Test"].ConnectionString.ToString();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Users "+ "select * from email", connStr);
            sqlDataAdapter.TableMappings.Add("Table", "Users");
            sqlDataAdapter.TableMappings.Add("Table1", "Email");
            sqlDataAdapter.Fill(dataSet);

            DataTable dataTable = dataSet.Tables["Users"];
            DataTable dataTable1 = dataSet.Tables["Email"];

            DataRelation dataRelation = new DataRelation("relationOne", dataTable.Columns["ID"],dataTable1.Columns["ID"],false);
            dataSet.Relations.Add(dataRelation);

            DataRow dataRow = dataSet.Tables["Users"].Rows[0];
            dataSet.Tables["Users"].Rows[0].Delete();
            
            dataSet.Tables["Users"].PrimaryKey = new DataColumn[] { dataSet.Tables["Users"].Columns["ID"] };


            SqlConnection sqlConnection = new SqlConnection(connStr);
            ////手动生成UpdateCommand语句
            SqlCommand command = new SqlCommand("Update Users set ID=1 where ID=2", sqlConnection);
            sqlDataAdapter.UpdateCommand = command;
            //自动得到command语句
            //sqlDataAdapter.UpdateCommand = new SqlCommandBuilder(sqlDataAdapter).GetUpdateCommand();
            sqlDataAdapter.Update(dataSet, "Users");

            //dataSet.AcceptChanges();
            Console.WriteLine("") ;
        }
    }
}
