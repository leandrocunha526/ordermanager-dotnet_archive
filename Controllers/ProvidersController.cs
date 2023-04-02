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
    public class ProvidersController : Controller
    {
        public IRepository _repository {get;}
        public ProvidersController(IRepository repository){
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(){
            try{
                var result = await _repository.GetAllProvidersAsync();
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpGet("{ProviderId}")]
        public async Task<IActionResult> GetProviderById(int ProviderId){
            try{
                var result = await _repository.GetProviderAsyncById(ProviderId);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Provider model){
            try{
                _repository.Add(model);

                if(await _repository.SaveChangesAsync()){
                    return Created("$/api/providers/register/{model.Id}", model);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpPut("edit/{ProviderId}")]
        public async Task<IActionResult> Put(int ProviderId, Provider model){
            try{
                var provider = await _repository.GetProviderAsyncById(ProviderId);
                if(provider == null)
                return NotFound();

                _repository.Update(model);

                if(await _repository.SaveChangesAsync()){
                    provider = await _repository.GetProviderAsyncById(ProviderId);
                    return Created("$/api/providers/edit/{model.Id}", provider);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpDelete("{ProviderId}")]
        public async Task<IActionResult> Delete(int ProviderId){
            try{
                var provider = await _repository.GetProviderAsyncById(ProviderId);
                if(provider == null) return NotFound();

                _repository.Delete(provider);

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
