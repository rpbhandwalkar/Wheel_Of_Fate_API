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
    public class ShiftRepository : Repositoy<Shift>, IShiftRepository
    {
        private ApplicationDbContext _context;
        public ShiftRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Shift> UpdateAsync(Shift shift)
        {
            _context.shifts.Update(shift);
            await Save();
            return shift;
        }
    }
}
