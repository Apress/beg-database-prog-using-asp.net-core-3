using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Blazor.ServerSide.Models;


namespace EmployeeManager.Blazor.ServerSide.Repositories
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
