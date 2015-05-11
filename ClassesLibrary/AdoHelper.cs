using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizations.DbEntity;
using System.Configuration;


namespace Organizations
{
    public class AdoHelper
    {
        private static volatile AdoHelper s_instance;
        private static object syncRoot = new Object();
        private static List<string> queryList = new List<string>();
        private static string GetConnectionString()
        {
            return ConfigurationManager.
                ConnectionStrings["ConnectionString"].ConnectionString;
            ////return Properties.Settings.Default.ConsoleConnectionString;
            //return Properties.Settings.Default.MvcConnectionString;
        }
        
        private void ClearQueue()
        {
            queryList.Clear();
        }
        private AdoHelper() { }
        
        public static AdoHelper Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (s_instance == null)
                            s_instance = new AdoHelper();
                    }
                }
                return s_instance;
            }
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
        
        public void AddQuery(string query)
        {
            queryList.Add(query);
        }
        public void ExecCommand()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                SqlTransaction transaction = connection.BeginTransaction("Transaction");
                var commandList = new List<SqlCommand>();

                foreach (var query in queryList)
                {
                    commandList.Add(
                        new SqlCommand(query, connection) { Transaction = transaction }
                    );
                }
                
                try
                {
                    foreach (var command in commandList)
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    ClearQueue();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        transaction.Rollback();
                        ClearQueue();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2.Message);
                        ClearQueue();
                    }
                }
            }
        }
        //////////
    }
}



