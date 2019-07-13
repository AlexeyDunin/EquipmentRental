using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository basketRepository) => _repository = basketRepository;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Models.BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Models.BasketModel>> GetBasketByIdAsync(string id)
        {
            return Ok(await _repository.GetBasketAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Models.BasketModel>> UpdateBasketAsync([FromBody]Models.BasketModel value)
        {
            return Ok(await _repository.UpdateBasketAsync(value));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            await _repository.DeleteBasketAsync(id);
        }
    }
}
