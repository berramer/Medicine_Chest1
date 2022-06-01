using Business.Concrete;
using ENTITIES;
using ENTITIES.Dtos;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Medicine_Chest.Models.MedicineIslemleri;
using Medicine_Chest.Models.OrderIslemleri;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Uno.Expressions;

namespace Medicine_Chest.Controllers
{
    public class OrderIslemleriController : Controller
    {
        private OrderManager _orderManager = new OrderManager(new DATA.Concrete.ORDERDAL());
        private UserManager<User> _userManager;
        private IEmailSender _emailSender;
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());
        private StockManager _stockManager = new StockManager(new DATA.Concrete.STOCKDAL());
        private User user ;
        private PrescriptionManager _prescriptionManager = new PrescriptionManager(new DATA.Concrete.PRESCRIPTIONDAL());

        public OrderIslemleriController(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        [HttpPost]
        public async Task<ActionResult> FiltreleOrder(OrderViewModel model)
        {
            try
            {
                IEnumerable<ORDER> kullanicilar = null;
                user = await _userManager.GetUserAsync(User);
                if (User.IsInRole("kargocu"))
                {
                   kullanicilar = (IEnumerable<ORDER>)(await _orderManager.getAll(x => x.PharmaciesID == user.PharmaciesId && x.CargoId == user.Id));
                }
                else if (!string.IsNullOrEmpty(user.PharmaciesId))
                {
                  kullanicilar = (IEnumerable<ORDER>)(await _orderManager.getAll(x => x.PharmaciesID == user.PharmaciesId));
                }



                if (!string.IsNullOrEmpty(model.UserIDSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.UserID == model.UserIDSorgu);
                }
                if (!string.IsNullOrEmpty(model.UserNameSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.UserName == model.UserNameSorgu);
                }
                if (!string.IsNullOrEmpty(model.UserSurnameSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.UserSurname == model.UserSurnameSorgu);
                }
                if (!string.IsNullOrEmpty(model.AddressSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.Adress == model.AddressSorgu);
                }
                if (!string.IsNullOrEmpty(model.PhonenumberSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.Phonenumber == model.PhonenumberSorgu);
                }
                if (!string.IsNullOrEmpty(model.MailAddressSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.MailAddress == model.MailAddressSorgu);
                }
                if (!string.IsNullOrEmpty(model.PharmaciesIDSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.PharmaciesID == model.PharmaciesIDSorgu);
                }
                if (!string.IsNullOrEmpty(model.MedicineIDSorgu))
                {
                    kullanicilar = kullanicilar.Where(order => order.MedicineID == model.MedicineIDSorgu);
                }
                if (model.PriceSorgu!=null)
                {
                    kullanicilar = kullanicilar.Where(order => order.Price == model.PriceSorgu);
                }

                model.OrderList = (IEnumerable<ORDER>)kullanicilar;
                return View("OrderIslemleri", model);
            }
            catch (Exception exception)
            {
                return null; /*Mesaj.Goster(ArayuzSabitler.SorgulamaBaslik, "Sorgulama İşlemi Sırasında Bir Hata Oluştu: " + exception.GetAllMessages(), MesajTip.Hata);*/
            }
        }


        public async Task<IActionResult> OrderIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            var model = new OrderViewModel();
            user= await _userManager.GetUserAsync(User);
            if (User.IsInRole("kargocu"))
            {
                model.OrderList = (IEnumerable<ORDER>)(await _orderManager.getAll(x => x.PharmaciesID == user.PharmaciesId&& x.CargoId==user.Id));
            }else if (!string.IsNullOrEmpty(user.PharmaciesId))
            {
                model.OrderList = (IEnumerable<ORDER>)(await _orderManager.getAll(x => x.PharmaciesID == user.PharmaciesId));
            }
            return View(model);
        }

        public async Task<IActionResult> OrderDetail(string id)
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            user = await _userManager.GetUserAsync(User);
            var kargolist = (await _userManager.GetUsersInRoleAsync("kargocu")).Where(x=>x.PharmaciesId==user.PharmaciesId)
                        .Select(a => new SelectListItem()
                        {
                            Value = a.Id,
                            Text = a.Name
                        })
                        .ToList();

            var order = (await _orderManager.getAll(x => x.ID == id)).FirstOrDefault();
           
                var medicine = order.MedicineID.Split(',');
                 var items = order.ItemList.Split(',');
       
                var medicineList1 = new List<MEDICINE>();
                var itemList = new List<string>();
            var stockList = new List<int>();
                for (int i=0;i<medicine.Length;i++)
                {
                    if (!string.IsNullOrEmpty(medicine[i]))
                    {
                    var medicine2 = (await _medicineManager.getAll(x => x.ID == medicine[i])).FirstOrDefault();

                    if (medicine2 != null)
                    {
                        var stock = (await _stockManager.getAll(x => x.MedicineID == medicine[i] && x.PharmID == user.PharmaciesId)).FirstOrDefault();
                        if (stock != null)
                        {
                            stockList.Add(stock.Stock);
                        }
                        else
                        {
                            stockList.Add(0);
                        }

                        medicineList1.Add(medicine2);
                        itemList.Add(items[i]);
                    }
                    }
                }

            var detailViewModel = new OrderDetailViewModel()
            {
                Id=order.ID,
                UserID = order.UserID,
                UserName = order.UserName,
                UserSurname = order.UserSurname,
                Address = order.Adress,
                Phonenumber = order.Phonenumber,
                MailAddress = order.MailAddress,
                medicineList = medicineList1,
                ItemList = itemList,
                stockList= stockList,
                Price = order.Price,
                KargoList= kargolist
            };
             
     
            return View(detailViewModel);
        }





        [HttpPost]
        public async Task<JsonResult> KabulEt(string id,string kargoId)
        {
            user = await _userManager.GetUserAsync(User);
            var order = (await _orderManager.getAll(x => x.ID == id)).FirstOrDefault();

            var medicine = order.MedicineID.Split(',');
            var items = order.ItemList.Split(',');

       
            for (int i = 0; i < medicine.Length; i++)
            {
                if (!string.IsNullOrEmpty(medicine[i]))
                {
                        var stock = (await _stockManager.getAll(x => x.MedicineID == medicine[i] && x.PharmID == user.PharmaciesId)).FirstOrDefault();
                    if (stock != null)
                    {
                        if (stock.Stock >= int.Parse(items[i]))
                        {
                            stock.Stock -= int.Parse(items[i]);
                            _stockManager.update(stock);
                        }
                    }
                    
                    }
                }

            order.IsAccepted = 1;
            order.CargoId = kargoId;
            var kargo = await _userManager.FindByIdAsync(kargoId);
            await _emailSender.SendEmailAsync(kargo.Email, "Hesabınızı onaylayınız", $"Lütfen şifrenizi yenilemek için linke ");
            if (!String.IsNullOrEmpty(order.ReceteKodu))
            {
                var pres = (await _prescriptionManager.getAll(x => x.PrescriptionCode == order.ReceteKodu)).FirstOrDefault();
                _prescriptionManager.delete(pres);
            }
            _orderManager.update(order);
               return Json("Başarılı"); 
        }





        [HttpPost]
        public async Task<JsonResult> TeslimEt(string id)
        {
            user = await _userManager.GetUserAsync(User);
            var order = (await _orderManager.getAll(x => x.ID == id)).FirstOrDefault();
            DateTime teslim = DateTime.Now;
                TimeSpan sonuc = teslim - order.OrderDate; // Büyük tarihten küçük tarihi çıkardık
            order.IsDeliveredKargo = 1;
            _orderManager.update(order);
            if (sonuc.Hours < 1)
            {
                user.Puan += 2;
            }

            return Json("Başarılı");

        }


        [HttpPost]
        public async Task<JsonResult> RedEt(string id)
        {

            var order = (await _orderManager.getAll(x => x.ID == id)).FirstOrDefault();


            order.IsAccepted = 2;
            _orderManager.update(order);
            return Json("Başarılı");
        }




        public IActionResult OrderListesi()
        {
            return PartialView("_OrderListesi", _orderManager.getAll());

        }


    }
}
