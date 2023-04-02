using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ordermanager_dotnet.Data;
using ordermanager_dotnet.Models;

namespace ordermanager_dotnet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        public IRepository _repository {get;}
        public EmployeesController(IRepository repository){
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(){
            try{
                var result = await _repository.GetAllEmployeesAsync();
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpGet("{EmployeeId}")]
        public async Task<IActionResult> GetEmployeeById(int EmployeeId){
            try{
                var result = await _repository.GetEmployeeAsyncById(EmployeeId);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpPost("{register}")]
        public async Task<IActionResult> Post(Employee model){
            try{
                _repository.Add(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created("$/api/employees/register/{model.Id}", model);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpPut("edit/{EmployeeId}")]
        public async Task<IActionResult> Put(int EmployeeId, Employee model){
            try {
                var employee = await _repository.GetEmployeeAsyncById(EmployeeId);
                if(employee == null)
                return NotFound();

                _repository.Update(model);

                if(await _repository.SaveChangesAsync()){
                    employee = await _repository.GetEmployeeAsyncById(EmployeeId);
                    return Created("$/api/employees/edit/{model.Id}", employee);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpDelete("{EmployeeId}")]
        public async Task<IActionResult> Delete(int EmployeeId){
            try{
                var employee = await _repository.GetEmployeeAsyncById(EmployeeId);
                if(employee == null)
                return NotFound();

                _repository.Delete(employee);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }
    }
}
