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
    public class AgriculturalInputsController : Controller
    {
        public IRepository _repo{get;}
        public AgriculturalInputsController(IRepository repo){
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(){
            try{
                var result = await _repo.GetAllAgriculturalInputAsync(true);
                return Ok(result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpGet("{AgriculturalInputId}")]
        public async Task<IActionResult> GetAgriculturalInputById(int AgriculturalInputId){
            try{
                var result = await _repo.GetAgriculturalInputAsyncById(AgriculturalInputId, true);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
        }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(AgriculturalInput model){
            try {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/agriculturalinputs/register/{model.Id}", model);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpPut("edit/{AgriculturalInputId}")]
        public async Task<IActionResult> Put(int AgriculturalInputId, AgriculturalInput model){
            try{
                var agriculturalinput = await _repo.GetAgriculturalInputAsyncById(AgriculturalInputId, true);
                if(agriculturalinput ==  null) return NotFound();

                _repo.Update(model);

                    if(await _repo.SaveChangesAsync()){
                        agriculturalinput = await _repo.GetAgriculturalInputAsyncById(AgriculturalInputId, true);
                        return Created($"/api/agriculturalinputs/edit/{agriculturalinput.Id}", model);
                    }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpDelete("{AgriculturalInputId}")]
        public async Task<IActionResult> Delete(int AgriculturalInputId){
            try{
                var agriculturalinput = await _repo.GetAgriculturalInputAsyncById(AgriculturalInputId, false);
                if(agriculturalinput == null) return NotFound();

                _repo.Delete(agriculturalinput);

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
