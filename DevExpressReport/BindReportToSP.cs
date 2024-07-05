using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpressReport
{
    public class BindReportToSP
    {
        public static void GenerateCustomerOrderDetailReport()
        {
            //1. create sql data source and set up its connection parameters
            var sqlDtaSource = new SqlDataSource
            {
                Name = "sqlDataSource1",
                ConnectionName = "localhost_Northwind_Connection"
            };

            var connectionParams = new MsSqlConnectionParameters()
            {
                ServerName = "localhost",
                DatabaseName = "Northwind",
                AuthorizationType = MsSqlAuthorizationType.Windows
            };

            sqlDtaSource.ConnectionParameters = connectionParams;

            // 2. create store procedure query
            var queryName = "CustomersOrdersDetail";

            var storedProcedureQuery = new StoredProcQuery()
            {
                Name = queryName,
                StoredProcName = "CustOrdersDetail"
            };

            sqlDtaSource.Queries.Add(storedProcedureQuery);

            // 3. Pass the report's orderId parameter to the stored procedure
            var queryParameter = new QueryParameter()
            {
                Name = "@OrderID",
                Type = typeof(DevExpress.DataAccess.Expression),
                Value = new DevExpress.DataAccess.Expression("?orderID", typeof(int))
            };

            storedProcedureQuery.Parameters.Add(queryParameter);

            // 4. create a report instance and specify its DataSource and DataMember properties
            var report = new XtraReport();
            report.DataSource = sqlDtaSource;
            report.DataMember = queryName;

            // 5. optionally, set up the report's parameters and disable the RequestParameters property
            // to apply the default parameter values to the report when you show its preview
            //report.Parameters["orderID"].Value = 10249;
            //report.RequestParameters = false;
        }
    }
}
