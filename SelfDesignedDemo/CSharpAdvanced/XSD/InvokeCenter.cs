using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace CSharpAdvanced.XSD
{
    public class InvokeCenter
    {
        public void InvokeData()
        {
            DataSetOne dataSetOne = new DataSetOne();
            Database database = DatabaseFactory.CreateDatabase("CSharpAdvanced.Properties.Settings._1119ConnectionString");
            DbCommand dbCommand = database.GetSqlStringCommand("");
            database.LoadDataSet(null, dataSetOne,"");
            
            //DataSetOneTableAdapters.TableAdapterManager all = new DataSetOneTableAdapters.TableAdapterManager();
            //all.GetData();
            //DataSetOne.StatementApprovedStatusDataTable st = new DataSetOne.StatementApprovedStatusDataTable();
        

        }
    }
}
