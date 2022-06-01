using Business.Concrete;
using ENTITIES;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Identity;
using Medicine_Chest.Models.OrderIslemleri;
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
        private IEmailSender _emailSender;
      
       
        private BucketManager _bucketManager = new BucketManager(new DATA.Concrete.BUCKETDAL());
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());
        private PharmaciesManager _pharmaciesManager = new PharmaciesManager(new DATA.Concrete.PHARMACIESDAL());
        public OrderController(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
    
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderManager.getAll());
        }
        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> getByUsername(string username)

        {
                var orderList = (await _orderManager.getAll(x => x.UserName == username));
            List<OrderDetailViewModel> result = new List<OrderDetailViewModel>();

            foreach (ORDER order in orderList)
            {
                var medicine = order.MedicineID.Split(',');
                var medicineList1 = new List<MEDICINE>();
                var pharm = (await _pharmaciesManager.getAll(x => x.ID == order.PharmaciesID)).FirstOrDefault();
                foreach (var a in medicine)
                {
                    if (!string.IsNullOrEmpty(a))
                    {
                        var medicine2 = (await _medicineManager.getAll(x => x.ID == a)).FirstOrDefault();
                        if (medicine2 != null)
                            medicineList1.Add(medicine2);
                    }
                }

                var detailViewModel = new OrderDetailViewModel()
                {
                    Id = order.ID,
                    UserID = order.UserID,
                    UserName = order.UserName,
                    UserSurname = order.UserSurname,
                    Address = order.Adress,
                    Phonenumber = order.Phonenumber,
                    MailAddress = order.MailAddress,
                    EczaneAdi = pharm.EczaneAdi,
                    EczaneAdres = pharm.Adresi,
                    EczaneNo = pharm.Telefon,
                    medicineList = medicineList1,
                    Price = order.Price,
                    IsAccepted = order.IsAccepted,
                    IsDeliveredKargo = order.IsDeliveredKargo,
                    OrderDate = order.OrderDate.ToString()
                    
                };
                result.Add(detailViewModel);
            }
                return Ok(result);
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

            Random rastgele = new Random();
            order.TeslimatKodu = rastgele.Next(10000).ToString();
            order.IsAccepted = 0;
            order.IsDeliveredKargo = 0;
            var bucketList = await _bucketManager.getAll(x => x.UserId == order.UserName);
      
            foreach (BUCKET a in bucketList)
            {
               await _bucketManager.deleteasync(a);
            }

            try
            { 
                await _orderManager.addAsync(order);
             
                var eczacılar = (await _userManager.GetUsersInRoleAsync("eczacı")).Where(x => x.PharmaciesId == order.PharmaciesID);
                foreach(var users in eczacılar)
                {
                    await _emailSender.SendEmailAsync(users.Email, "Yeni şiparişiniz var ", "ddd");
                }

            }
            catch(Exception ex)
            {
                var a = 5;
            }
            return Ok(order);
        }


       
       
    }
}

