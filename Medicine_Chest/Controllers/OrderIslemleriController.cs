﻿using Business.Concrete;
using ENTITIES;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Medicine_Chest.Models.MedicineIslemleri;
using Medicine_Chest.Models.OrderIslemleri;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());
        [HttpPost]
        public async Task<ActionResult> FiltreleOrder(OrderViewModel model)
        {
            try
            {

                var kullanicilar = (IEnumerable<ORDER>)(await _orderManager.getAll());



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
            model.OrderList = (IEnumerable<ORDER>)(await _orderManager.getAll());
            return View(model);
        }

        public async Task<IActionResult> OrderDetail(string id)
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];


            var order = (await _orderManager.getAll(x => x.ID == id)).FirstOrDefault();
           
                var medicine = order.MedicineID.Split(',');
                var medicineList1 = new List<MEDICINE>();

                foreach (var a in medicine)
                {
                    if (!string.IsNullOrEmpty(a))
                    {
                        var medicine2 = (await _medicineManager.getAll(x => x.ID == a)).FirstOrDefault();
                    if(medicine2!=null)
                        medicineList1.Add(medicine2);
                    }
                }

                var detailViewModel = new OrderDetailViewModel()
                {
                    UserID = order.UserID,
                    UserName = order.UserName,
                    UserSurname = order.UserSurname,
                    Address = order.Adress,
                    Phonenumber = order.Phonenumber,
                    MailAddress = order.MailAddress,
                    medicineList = medicineList1,
                    Price = order.Price
                };
             
     
            return View(detailViewModel);
        }




  
   


        public IActionResult OrderListesi()
        {
            return PartialView("_OrderListesi", _orderManager.getAll());

        }


    }
}
