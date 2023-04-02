using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ManufacturersController : Controller
    {
        public IRepository _repo{get;}
        public ManufacturersController(IRepository repo){
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(){
            try{
                var result = await _repo.GetAllManufacturersAsync();
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpGet("{ManufacturerId}")]
        public async Task<IActionResult> GetManufacturerById(int ManufacturerId){
            try{
                var result = await _repo.GetManufacturerAsyncById(ManufacturerId);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post(Manufacturer model){
            try{
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/manufacturers/register/{model.Id}", model);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpPut("edit/{ManufacturerId}")]
        public async Task<IActionResult> Put(int ManufacturerId, Manufacturer model){
            try {
                var manufacturer = await _repo.GetManufacturerAsyncById(ManufacturerId);
                if(manufacturer == null)
                return NotFound();
                _repo.Update(model);
                    if(await _repo.SaveChangesAsync()){
                    manufacturer = await _repo.GetManufacturerAsyncById(ManufacturerId);
                    return Created($"/api/[controller]/edit/{manufacturer.Id}", manufacturer);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpDelete("{ManufacturerId}")]
        public async Task<IActionResult> Delete(int ManufacturerId){
            try{
                var manufacturer = await _repo.GetManufacturerAsyncById(ManufacturerId);
                if(manufacturer == null)
                return NotFound();
                _repo.Delete(manufacturer);
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
