using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.AzureSql.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;


namespace EmployeeManager.AzureSql.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string connectionString;

        public EmployeeRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("AppDb");
        }

        public List<Employee> SelectAll()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT EmployeeID, FirstName, LastName, Title, BirthDate, HireDate, Country, Notes FROM Employees ORDER BY EmployeeID ASC";

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();
                while (reader.Read())
                {
                    Employee item = new Employee();
                    item.EmployeeID = reader.GetInt32(0);
                    item.FirstName = reader.GetString(1);
                    item.LastName = reader.GetString(2);
                    item.Title = reader.GetString(3);
                    item.BirthDate = reader.GetDateTime(4);
                    item.HireDate = reader.GetDateTime(5);
                    item.Country = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                    {
                        item.Notes = reader.GetString(7);
                    }
                    employees.Add(item);
                }
                reader.Close();
                cnn.Close();
                return employees;
            }
        }

        public Employee SelectByID(int id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT EmployeeID, FirstName, LastName, Title, BirthDate, HireDate, Country, Notes FROM Employees WHERE EmployeeID=@EmployeeID";

                SqlParameter p = new SqlParameter("@EmployeeID", id);
                cmd.Parameters.Add(p);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();
                while (reader.Read())
                {
                    Employee item = new Employee();
                    item.EmployeeID = reader.GetInt32(0);
                    item.FirstName = reader.GetString(1);
                    item.LastName = reader.GetString(2);
                    item.Title = reader.GetString(3);
                    item.BirthDate = reader.GetDateTime(4);
                    item.HireDate = reader.GetDateTime(5);
                    item.Country = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                    {
                        item.Notes = reader.GetString(7);
                    }
                    employees.Add(item);
                }
                reader.Close();
                cnn.Close();
                return employees.SingleOrDefault();
            }
        }
       
        public void Insert(Employee emp)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Employees(FirstName, LastName, Title, BirthDate, HireDate, Country, Notes)  VALUES(@FirstName, @LastName, @Title, @BirthDate, @HireDate, @Country, @Notes)";

                SqlParameter[] p = new SqlParameter[7];
                p[0] = new SqlParameter("@FirstName", emp.FirstName);
                p[1] = new SqlParameter("@LastName", emp.LastName);
                p[2] = new SqlParameter("@Title", emp.Title);
                p[3] = new SqlParameter("@BirthDate", emp.BirthDate);
                p[4] = new SqlParameter("@HireDate", emp.HireDate);
                p[5] = new SqlParameter("@Country", emp.Country);
                p[6] = new SqlParameter("@Notes", emp.Notes ?? SqlString.Null);

                cmd.Parameters.AddRange(p);

                cnn.Open();
                int i = cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void Update(Employee emp)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Title=@Title, BirthDate=@BirthDate, HireDate=@HireDate, Country=@Country, Notes=@Notes WHERE EmployeeID=@EmployeeID";

                SqlParameter[] p = new SqlParameter[8];
                p[0] = new SqlParameter("@FirstName", emp.FirstName);
                p[1] = new SqlParameter("@LastName", emp.LastName);
                p[2] = new SqlParameter("@Title", emp.Title);
                p[3] = new SqlParameter("@BirthDate", emp.BirthDate);
                p[4] = new SqlParameter("@HireDate", emp.HireDate);
                p[5] = new SqlParameter("@Country", emp.Country);
                p[6] = new SqlParameter("@Notes", emp.Notes ?? SqlString.Null);
                p[7] = new SqlParameter("@EmployeeID", emp.EmployeeID);

                cmd.Parameters.AddRange(p);

                cnn.Open();
                int i = cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Employees WHERE EmployeeID=@EmployeeID";

                SqlParameter p = new SqlParameter("@EmployeeID", id);
                cmd.Parameters.Add(p);

                cnn.Open();
                int i = cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
