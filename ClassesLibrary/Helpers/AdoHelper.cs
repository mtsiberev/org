using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NLog;

namespace Organizations.Helpers
{
    public static class AdoHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string GetConnectionString()
        {
            return ConfigurationManager.
              ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public static DataTableReader GetDataTableReader(string queryString)
        {
            var table = new DataTable();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                using (var adapter = new SqlDataAdapter(queryString, connection))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        return null;
                    }

                    try
                    {
                        adapter.Fill(table);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        return null;
                    }
                }
            }
            return table.CreateDataReader();
        }

        public static void ExecCommand(string queryString)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return;
                }
          
                using (var command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        return;
                    }
                    logger.Trace("SqlCommand: {0} successfully completed", queryString);
                }
            }
        }

    }
}


