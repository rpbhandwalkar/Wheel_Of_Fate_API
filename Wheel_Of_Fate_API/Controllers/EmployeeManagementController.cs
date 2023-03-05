using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Wheel_Of_Fate.Models;
using Wheel_Of_Fate.Models.DTO;
using Wheel_OF_Fate.DA.Repository.IRepository;

namespace Wheel_Of_Fate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagementController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IEmployeesRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeManagementController(IEmployeesRepository repo, IMapper mapper)
        {
            _response = new();
            _repo = repo;
            _mapper = mapper;
            
        }

        

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetEmployees()
        {
            try
            {
                IEnumerable<Employee> employees = await _repo.GetAllAsync();
                _response.Result = _mapper.Map<List<EmployeeDTO>>(employees);
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

        [HttpGet("{EmpId:int}", Name = "GetEmployeeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployeeByID(int EmpId)
        {
            try
            {
                if (EmpId == 0)
                {
                    //logger.LogError("Caught not supported Id =" + id);
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Employee employee = await _repo.Get(x => x.EmpId == EmpId);
                if (employee == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //logger.LogInformation("Getting Villas with respect to id");
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
                _response.IsSuccess = true;

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
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeCreateDTO createDTO)
        {
            try
            {
                if (await _repo.Get(x => x.EmpId == createDTO.EmpId) != null)
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
                Employee model = _mapper.Map<Employee>(createDTO);
                await _repo.Create(model);

                //logger.LogInformation("Create new Villas");
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = _mapper.Map<EmployeeCreateDTO>(model);
                _response.IsSuccess = true;
                return CreatedAtRoute("GetEmployeeByID", new { EmpId = model.EmpId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return Ok(_response);
            }
        }


        [HttpPut("{EmpID:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int EmpID, [FromBody] EmployeeUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || EmpID != updateDTO.EmpId)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    
                }

                Employee model = _mapper.Map<Employee>(updateDTO);

                await _repo.UpdateAsync(model);
                //logger.LogInformation("Update Villas");
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

        [HttpDelete("{EmpId:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int EmpId)
        {
            try
            {
                if (EmpId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Employee employee = await _repo.Get(x => x.EmpId == EmpId);
                if (employee == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return NotFound(_response);
                }
                //logger.LogInformation("Delete Villa");
                await _repo.Remove(employee);

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
