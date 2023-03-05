using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fate.Models;
using Wheel_OF_Fate.DA.Data;
using Wheel_OF_Fate.DA.Repository.IRepository;

namespace Wheel_OF_Fate.DA.Repository
{
    public class EmployeesRepository: Repositoy<Employee>, IEmployeesRepository
    {
        private ApplicationDbContext _context;
        public EmployeesRepository(ApplicationDbContext context): base(context)
        {
            _context = context; 
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
             _context.Employees.Update(employee);
            await Save();
            return employee;
        }
    }
}
