using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Api.Models;


namespace EmployeeManager.Api.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> SelectAll();
        Employee SelectByID(int id);
        void Insert(Employee emp);
        void Update(Employee emp);
        void Delete(int id);
    }
}
