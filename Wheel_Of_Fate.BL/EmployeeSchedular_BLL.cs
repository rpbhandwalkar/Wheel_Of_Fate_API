using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fate.BL
{
    public class EmployeeSchedular_BLL
    {
		private readonly APIResponse _response;
		private readonly IEmployeesRepository _repo;
		private readonly IMapper _mapper;

		public EmployeeSchedular_BLL(IEmployeesRepository repo, IMapper mapper)
		{
			_response = new();
			_repo = repo;
			_mapper = mapper;
		}


		
	}
}
