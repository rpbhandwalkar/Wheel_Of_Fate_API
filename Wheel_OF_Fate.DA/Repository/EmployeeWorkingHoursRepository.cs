using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_OF_Fate.DA.Repository.IRepository;
using Wheel_Of_Fate.Models;
using Wheel_OF_Fate.DA.Data;

namespace Wheel_OF_Fate.DA.Repository
{
    public class EmployeeWorkingHoursRepository : Repositoy<EmployeeWorkingHours>, IEmployeeWorkingHoursRepository
    {

        private ApplicationDbContext _context;
        public EmployeeWorkingHoursRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EmployeeWorkingHours> UpdateAsync(EmployeeWorkingHours employeeWorking)
        {
            _context.EmployeeWorkingHours.Update(employeeWorking);
            await Save();
            return employeeWorking;
        }
    }
}
