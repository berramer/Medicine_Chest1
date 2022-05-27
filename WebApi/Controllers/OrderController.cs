using Business.Concrete;
using ENTITIES;
using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<User> _userManager;
        private BucketManager _bucketManager = new BucketManager(new DATA.Concrete.BUCKETDAL());
        public OrderController(UserManager<User> userManager)
        {
            _userManager = userManager;
       
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderManager.getAll());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetbyId(string id)

        {
            List<BUCKET> a = await _orderManager.getAll(x => x.UserId == id);

            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ORDER order)
        {

            order.ID = System.Guid.NewGuid().ToString();
            var user =await  _userManager.FindByNameAsync(order.UserName);
            order.UserID = user.Id;
            order.Phonenumber = user.PhoneNumber;
            order.MailAddress = user.Email;
            order.UserSurname =user.Name+" "+ user.Surname;
            order.OrderDate = System.DateTime.Now;
            order.Adress = user.Address;
            var bucketList = await _bucketManager.getAll(x => x.UserId == order.UserName);
            foreach(BUCKET a in bucketList)
            {
               await _bucketManager.deleteasync(a);
            }

            try
            {
                await _orderManager.addAsync(order);
            }catch(Exception ex)
            {
                var a = 5;
            }
            return Ok(order);
        }


       
       
    }
}

