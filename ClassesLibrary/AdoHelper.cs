using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizations.DbEntity;


namespace Organizations
{
    public static class AdoHelper
    {
        private static string GetConnectionString()
        {
            return Properties.Settings.Default.TestConnectionString;
        }

        public static DataTable GetDataTable(string queryString)
        {
            var table = new DataTable();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                var adapter = new SqlDataAdapter(queryString, connection);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return table;
        }

        public static DataTableReader GetDataTableReader(DataTable table)
        {
            return table.CreateDataReader();
        }

        public static void ExecCommand(string queryString)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                var command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}



