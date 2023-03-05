using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_Of_Fate.Models;

namespace Wheel_OF_Fate.DA.Repository.IRepository
{
    public interface IEmployeeWorkingHoursRepository : IRepository<EmployeeWorkingHours>
    {
        Task<EmployeeWorkingHours> UpdateAsync(EmployeeWorkingHours employee);
    }
}
