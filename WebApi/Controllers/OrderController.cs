using Business.Concrete;
using ENTITIES;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrderManager _orderManager = new OrderManager(new DATA.Concrete.ORDERDAL());

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderManager.getAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ORDER order)
        {

            order.ID = System.Guid.NewGuid().ToString();
            await _orderManager.addAsync(order);
            return Ok(order);
        }


       
       
    }
}

