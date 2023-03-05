using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wheel_Of_Fate.Models;
using Wheel_OF_Fate.DA.Repository.IRepository;

namespace Wheel_Of_Fate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllHoursController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IShiftRepository _repo;
        private readonly IEmployeesRepository _EmpRepo;
        private readonly IEmployeeWorkingHoursRepository _employeeWorking;
        private int _totalWorkingShifts, _WorkingHourID = 0;
        public AllHoursController(IShiftRepository repo, IMapper mapper, IEmployeeWorkingHoursRepository employeeWorking,
        IEmployeesRepository EmpRepo)
        {
            _response = new();
            _repo = repo;
            _employeeWorking = employeeWorking;
            _EmpRepo = EmpRepo;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAvaliableEmployees()
        {
            try
            {
                // _response.Result = _mapper.Map<List<EmployeeDTO>>(employees);
                IEnumerable<Employee> employees = await _EmpRepo.GetAllAsync();
                IEnumerable<EmployeeWorkingHours> workingHours = await _employeeWorking
                    .GetAllAsync(x => x.WorkedDate.Date >= DateTime.Today.Date.AddDays(-14));

                IEnumerable<Shift> shifts = await _repo.GetAllAsync();
                //List<EmployeeDTO> employees1 = _mapper.Map<List<EmployeeDTO>>(employees);
                List<Merged> mergeds = new List<Merged>();
                bool isShift = false;

                foreach (var employee in employees)
                {
                    foreach (var item in workingHours)
                    {

                        if (employee.EmpId == item.EmpsId)
                        {
                            if (item.WorkingShift == 0)
                            {
                                isShift = true;
                            }

                            _totalWorkingShifts += 1;
                            _WorkingHourID = item.Id;
                        }
                    }
                    mergeds.Add(new Merged
                    {
                        WorkingHourID = _WorkingHourID,
                        EmpId = employee.EmpId,
                        Hours = _totalWorkingShifts,
                        Name = employee.Name
                    });

                    _totalWorkingShifts = 0;
                }

                _response.Result = mergeds;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return Ok(_response);
            }
        }
    }
}
