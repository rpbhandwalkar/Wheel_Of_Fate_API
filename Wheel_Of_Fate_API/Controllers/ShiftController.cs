using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wheel_OF_Fate.DA.Repository.IRepository;
using Wheel_Of_Fate.Models;
using System.Net;
using Wheel_Of_Fate.Models.DTO;
using Wheel_OF_Fate.DA.Repository;

namespace Wheel_Of_Fate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IShiftRepository _repo;
        private readonly IEmployeesRepository _EmpRepo;
        private readonly IEmployeeWorkingHoursRepository _employeeWorking;
        private int _totalWorkingShifts, _WorkingHourID = 0;

        public ShiftController(IShiftRepository repo, IMapper mapper, IEmployeeWorkingHoursRepository employeeWorking,
        IEmployeesRepository EmpRepo)
        {
            _response = new();
            _repo = repo;
            _employeeWorking = employeeWorking;
            _EmpRepo = EmpRepo;

        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetShift()
        {
            try
            {
                IEnumerable<Shift> _Shift = await _repo.GetAllAsync();
                _response.Result = _Shift;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }


            return Ok(_response);
        }

       

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateShift([FromBody] Shift createDTO)
        {
            try
            {
                if (await _repo.Get(x => x.Id == createDTO.Id) != null)
                {
                    //ModelState.AddModelError("SameEmployee", "Same Employee already exist!");
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string> { "SameEmployee" };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (createDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //Employee model = _mapper.Map<Employee>(createDTO);
                await _repo.Create(createDTO);

                //logger.LogInformation("Create new Villas");
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = createDTO;
                _response.IsSuccess = true;
                return CreatedAtRoute("GetShift", new { EmpId = createDTO.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return Ok(_response);
            }
        }


        [HttpDelete("{Id:int}", Name = "DeleteShiftr")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Shift _shift = await _repo.Get(x => x.Id == Id);
                if (_shift == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return NotFound(_response);
                }
                //logger.LogInformation("Delete Villa");
                await _repo.Remove(_shift);

                _response.StatusCode = HttpStatusCode.NoContent;
                //_response.Result = mapper.Map<VillaDto>(model);
                _response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }






    }
}
