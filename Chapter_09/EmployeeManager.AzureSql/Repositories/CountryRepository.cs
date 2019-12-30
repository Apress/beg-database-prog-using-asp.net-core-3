using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.AzureSql.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.AzureSql.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private string connectionString;

        public CountryRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("AppDb");
        }

        public List<Country> SelectAll()
        {
            using (SqlConnection cnn=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CountryID, Name FROM Countries ORDER BY Name ASC";

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Country> countries = new List<Country>();
                while(reader.Read())
                {
                    Country item = new Country();
                    item.CountryID = reader.GetInt32(0);
                    item.Name = reader.GetString(1);
                    countries.Add(item);
                }
                cnn.Close();
                return countries;
            }
        }
    }
}
