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
    public class OrdersController : Controller
    {
        public IRepository _repository {get;}
        public OrdersController(IRepository repository){
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get(){
            try{
                 var result = await _repository.GetAllOrderAsync(true, true, true);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetOrderById(int OrderId){
            try{
                var result = await _repository.GetOrderAsyncById(OrderId, true, true, true);
                return Ok(result);
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Order model){
            try {
                _repository.Add(model);
                if(await _repository.SaveChangesAsync()){
                    return Created($"/api/orders/register/{model.Id}", model);
                }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

         [HttpPut("edit/{OrderId}")]
        public async Task<IActionResult> Put(int OrderId, Order model){
            try{
                var order = await _repository.GetOrderAsyncById(OrderId, true, true, true);
                if(order ==  null) return NotFound();

                _repository.Update(model);

                    if(await _repository.SaveChangesAsync()){
                        order = await _repository.GetOrderAsyncById(OrderId, true, true, true);
                        return Created($"/api/orders/edit/{order.Id}", model);
                    }
            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error code 500: Internal Server Error");
            }
            return BadRequest();
        }

        [HttpDelete("{OrderId}")]
        public async Task<IActionResult> Delete(int OrderId){
            try{
                var order = await _repository.GetOrderAsyncById(OrderId, false, false, false);
                if(order == null) return NotFound();

                _repository.Delete(order);

                    if(await _repository.SaveChangesAsync()){
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
