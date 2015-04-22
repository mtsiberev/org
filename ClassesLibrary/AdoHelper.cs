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

    public class AdoHelper
    {
        public AdoHelper()
        {
        }

        private static string GetConnectionString()
        {
            return Properties.Settings.Default.TestConnectionString;
        }

        public static dynamic GetEntity(Type T, int id)
        {
            if (T == typeof(EmployeeDb))
                return GetEmployeeDb(id);
            if (T == typeof(DepartmentDb))
                return GetDepartmentDb(id);
            if (T == typeof(OrganizationDb))
                return GetOrganizationDb(id);
            return null;
        }

        /// /////////////////////////////////////////
        public static EmployeeDb GetEmployeeDb(int id)
        {
            const string queryString = "SELECT * FROM Employees "
                                       + "WHERE Id = @PARAM_ID;";
            var employeeTable = new DataTable();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                var adapter = new SqlDataAdapter(queryString, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@PARAM_ID", id);
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
                    adapter.Fill(employeeTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //this code should be removed to Mapper class
            var reader = employeeTable.CreateDataReader();
            int depId = 0;
            string name = "";
            while (reader.Read())
            {
                id = (int)reader.GetValue(0);
                depId = (int)reader.GetValue(1);
                name = reader.GetValue(2).ToString();
            }
            return new EmployeeDb(id, depId) { Name = name };
        }

        //////////////////////////////////////////////
        private static DepartmentDb GetDepartmentDb(int id)
        {
            return null;
        }

        private static OrganizationDb GetOrganizationDb(int id)
        {
            return null;
        }


        /*
        public static DataTable GetEmployeeById(int id)
        {
            const string queryString = "SELECT * FROM Employees "
                                       + "WHERE Id = @PARAM_ID;";
            var employee = new DataTable();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                var adapter = new SqlDataAdapter(queryString, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@PARAM_ID", id);
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
                    adapter.Fill(employee);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return employee;
        }
        */

        public string GetQueryString()
        {
            return "SELECT * FROM Employees;";
        }

        public void OpenSqlConnection()
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = GetConnectionString();
                var query = GetQueryString();
                var command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                            reader[0], reader[1], reader[2]);
                    }
                    Console.ReadLine();
                }
            }
        }


    }
}



