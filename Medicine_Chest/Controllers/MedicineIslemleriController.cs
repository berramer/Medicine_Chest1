using Business.Concrete;
using ENTITIES;
using ENTITIES.Dtos;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Medicine_Chest.Models.MedicineIslemleri;



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
    public class MedicineIslemleriController : Controller
    {
        private UserManager<User> _userManager;
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());
        private StockManager _stockManager = new StockManager(new DATA.Concrete.STOCKDAL());
        public MedicineIslemleriController(UserManager<User> userManager)
        {
            _userManager = userManager;

        }





        /// <summary>
        /// Sayfa ilk yüklendiğinde filtre ile ilgili elemanların hazırlanması için geçerli olan bir durumdur
        /// </summary>
        /// <param name="model">Model nesnesi</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<ActionResult> FiltreleMedicine(MedicineViewModel model)
        {
            try
            {

                var kullanicilar = (IEnumerable<MedicineDto>)(await _medicineManager.getList());



                if (!string.IsNullOrEmpty(model.NameSorgu))
                {
                    kullanicilar = kullanicilar.Where(medicine => medicine.Name == model.NameSorgu);
                }
                if (model.ExpireDateSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(medicine => medicine.ExpireDate == model.ExpireDateSorgu);
                }
                if (model.BasisWeightSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(medicine => medicine.BasisWeight == model.BasisWeightSorgu);
                }
                if (!string.IsNullOrEmpty(model.ProducerSorgu))
                {
                    kullanicilar = kullanicilar.Where(medicine => medicine.Producer == model.ProducerSorgu);
                }

                model.MedicineList = (IEnumerable<MedicineDto>)kullanicilar;
                return View("MedicineIslemleri", model);
            }
            catch (Exception exception)
            {
                return null; /*Mesaj.Goster(ArayuzSabitler.SorgulamaBaslik, "Sorgulama İşlemi Sırasında Bir Hata Oluştu: " + exception.GetAllMessages(), MesajTip.Hata);*/
            }
        }

        [HttpPost]
        public async Task<JsonResult> Sil(string Id)
        {
            var user = await _userManager.GetUserAsync(User);
            var medicine = (await _stockManager.getAll()).Where(x => x.MedicineID == Id && x.PharmID == user.PharmaciesId).FirstOrDefault();
            if (medicine != null)
            {
                _stockManager.delete(medicine);
            }
            return Json("Başarılı");
        }


        public async Task< IActionResult> MedicineIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            var model = new MedicineViewModel();
            var user = await _userManager.GetUserAsync(User);
            model.MedicineList = (IEnumerable<MedicineDto>)(await _medicineManager.getList()).Where(x=>x.PharmID==user.PharmaciesId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> MedicineIslemi(string Id, string islemTuru)
        {
            var dict = new Dictionary<string, string>();
            var user = await _userManager.GetUserAsync(User);
            var mesaj = "";
            try
            {
                //LogMesaj = SessionManagement.AktifEczane.Id + " id'li kullanıcı" + kodRafineriTuruId + " idli Kod Arıza Türü ile ilgili bilgileri getirdi";
                //// işlem türü de burada seçiliyor
                //LogIslemTuruId = SessionManagement.LogIslemTuruListesi.First(j => j.Kod == LogIslemTur.DetayGoruntuleme.GetHashCode()).Id;
                //LogIslem.EkleLog(LogAnaTurId, LogAltTurId, LogIslemTuruId, LogMesaj, dict, Request, ControllerContext);
                var medicinelist= (await _medicineManager.getAll())
                           .Select(a => new SelectListItem()
                           {
                               Value = a.ID,
                               Text = a.Name
                           })
                           .ToList();
                if (islemTuru != IslemTurSabitler.IslemTuruKayitEkleme || !string.IsNullOrEmpty(Id))
                {
                    var medicine = (await _medicineManager.getList()).Where(x => x.MedicineID == Id && x.PharmID == user.PharmaciesId).FirstOrDefault();
                
                    if (medicine != null)
                    {    
                   

                        return PartialView("_MedicineIslemi", new MedicineIslemViewModel()
                        {
                            Id = medicine.MedicineID,
                            Name = medicine.Name,
                            ExpireDate = medicine.ExpireDate,
                            BasisWeight = medicine.BasisWeight,
                            Producer = medicine.Producer,
                            Price = medicine.Price,
                            Miktar=medicine.Stock,
                            
                            MedicineList = medicinelist,

                            //EmailConfirmed = medicine.EmailConfirmed,
                            //IsDelete = medicine.IsDelete,
                            //SelectedRoles = selectedRoles
                            IslemTuru = islemTuru,

                        }); 
                    }
                    return PartialView("_MedicineIslemi", new MedicineIslemViewModel() {
                        MedicineList = medicinelist,
                    });
                }
                else
                {

                    return PartialView("_MedicineIslemi", new MedicineIslemViewModel()
                    {
                        MedicineList=medicinelist,
                        IslemTuru = islemTuru,
                    });
                }
            }
            catch (Exception exception)
            {


                ModelState.Clear();
                ModelState.AddModelError("", "esnasında bir hata oluştu.");
                Response.StatusCode = 400;
                return PartialView("_MedicineIslemi", new MedicineIslemViewModel());
                //return Json(new JsonSonuc { HataMi = true, Mesaj = mesaj.Mesaj }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] ///form güvenliği için sadece post methodları için olur gelen formun gönderilen formla aynı mi ?
        public async Task<IActionResult> MedicineIslemi(MedicineIslemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_MedicineIslemi", model);
            }
            if (model.IslemTuru != IslemTurSabitler.IslemTuruKayitEkleme)
            {
                //var medicine =(await _medicineManager.getAll(x => x.ID == model.Id)).FirstOrDefault();
                var stock =(await _stockManager.getAll(x => x.MedicineID == model.Id && x.PharmID == model.EczaneId)).FirstOrDefault();
                if (stock != null)
                {
                    stock.Stock = model.Miktar;
                    _stockManager.update(stock);
                }
            }
            else
            {

                //var medicine = new MEDICINE
                //{
                //    ID = System.Guid.NewGuid().ToString(),
                //    Name = model.Name,
                //    ExpireDate = model.ExpireDate,
                //    BasisWeight = model.BasisWeight,
                //    Producer = model.Producer,

                //};

                //_medicineManager.add(medicine);
      
                var stock = new STOCK
                {
                    ID = System.Guid.NewGuid().ToString(),
                    MedicineID=model.Id,
                    PharmID=model.EczaneId,
                    Stock=model.Miktar

                };
                _stockManager.add(stock);

            }
            return RedirectToAction("MedicineIslemleri", "MedicineIslemleri");
        }



        public IActionResult MedicineListesi()
        {
            return PartialView("_MedicineListesi", _medicineManager.getAll());

        }

        [HttpPost]

        public async Task<JsonResult> Getir(string Id)
        {

            var sozlesme =(await _medicineManager.getAll(x=>x.ID==Id)).FirstOrDefault();
                if (sozlesme != null)
                {
                    var data = new
                    {
                        Name=sozlesme.Name,

                        ExpireDate=sozlesme.ExpireDate,
                        BasisWeight =sozlesme.BasisWeight,
                        Price=sozlesme.Price,
                        Producer=sozlesme.Producer
     
                       };

                    return Json(data);
                }
                else
                {
                    return Json(new JsonSonuc { HataMi = true, Mesaj = "Fire Zamanı ve Fire Oranı Bilgisi alınamadı." } );
                }
            }
          

        
    }
}
