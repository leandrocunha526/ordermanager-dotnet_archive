using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ordermanager_dotnet.Data;
using ordermanager_dotnet.Models;

namespace ordermanager_dotnet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : Controller
    {
        public IRepository _repo{get;}
        public MachinesController(IRepository repo){
            _repo = repo;
        }
        [HttpGet("list")]
        public async Task<IActionResult> Get(){
            try{
                var result = await _repo.GetAllMachineAsync(true);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpGet("{MachineId}")]
        public async Task<IActionResult> GetMachineById(int MachineId){
            try{
                var result = await _repo.GetMachineAsyncById(MachineId, true);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Machine model){
            try{
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/machines/register/{model.Id}", model);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpPut("edit/{MachineId}")]
        public async Task<IActionResult> Put(int MachineId, Machine model){
            try {
                var machine = await _repo.GetMachineAsyncById(MachineId, false);
                if(machine == null)
                return NotFound();

                _repo.Update(model);

                    if(await _repo.SaveChangesAsync()){
                        machine = await _repo.GetMachineAsyncById(MachineId, true);
                        return Created($"/api/machines/edit/{machine.Id}", machine);
                    }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpDelete("{MachineId}")]
        public async Task<IActionResult> Delete(int MachineId){
            try{
                var machine = await _repo.GetMachineAsyncById(MachineId, false);
                if(machine == null)
                return NotFound();

                _repo.Delete(machine);

                    if(await _repo.SaveChangesAsync()){
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
