using System.Collections.Generic;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Inventory.Controllers
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly List<Equipment> _equipments;

        public EquipmentsController(IOptions<AppSettings> settings)
        {
            _equipments = settings.Value.Equipments;
        }
        [HttpGet, Route("")]
        public ActionResult<IEnumerable<Equipment>> Get()
        {
            return _equipments;
        }
    }
}
