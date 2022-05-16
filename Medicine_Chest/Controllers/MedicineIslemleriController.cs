using Business.Concrete;
using ENTITIES;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Medicine_Chest.Models.MedicineIslemleri;



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
    public class MedicineIslemleriController : Controller
    {

        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());
        //public EczaneIslemleriController(MedicineManager pharmaciesManager)
        //{
        //    _medicineManager = pharmaciesManager;

        //}





        /// <summary>
        /// Sayfa ilk yüklendiğinde filtre ile ilgili elemanların hazırlanması için geçerli olan bir durumdur
        /// </summary>
        /// <param name="model">Model nesnesi</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult FiltreleMedicine(MedicineViewModel model)
        {
            try
            {

                var kullanicilar = (IEnumerable<MEDICINE>)_medicineManager.getAll();



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

                model.MedicineList = (IEnumerable<MEDICINE>)kullanicilar;
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
            var medicine = (await _medicineManager.getAll(x => x.ID == Id)).FirstOrDefault();
            if (medicine != null)
            {
                _medicineManager.delete(medicine);
            }
            return Json("Başarılı");
        }


        public async Task< IActionResult> MedicineIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            var model = new MedicineViewModel();
            model.MedicineList = (IEnumerable<MEDICINE>)(await _medicineManager.getAll());
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> MedicineIslemi(string Id, string islemTuru)
        {
            var dict = new Dictionary<string, string>();

            var mesaj = "";
            try
            {
                //LogMesaj = SessionManagement.AktifEczane.Id + " id'li kullanıcı" + kodRafineriTuruId + " idli Kod Arıza Türü ile ilgili bilgileri getirdi";
                //// işlem türü de burada seçiliyor
                //LogIslemTuruId = SessionManagement.LogIslemTuruListesi.First(j => j.Kod == LogIslemTur.DetayGoruntuleme.GetHashCode()).Id;
                //LogIslem.EkleLog(LogAnaTurId, LogAltTurId, LogIslemTuruId, LogMesaj, dict, Request, ControllerContext);
                if (islemTuru != IslemTurSabitler.IslemTuruKayitEkleme || !string.IsNullOrEmpty(Id))
                {
                    var medicine = (await _medicineManager.getAll(x => x.ID == Id)).FirstOrDefault();
                    if (medicine != null)
                    {

                        return PartialView("_MedicineIslemi", new MedicineIslemViewModel()
                        {
                            Id = medicine.ID,
                            Name = medicine.Name,
                            ExpireDate = medicine.ExpireDate,
                            BasisWeight=medicine.BasisWeight,
                            Producer = medicine.Producer,

                            

                            //EmailConfirmed = medicine.EmailConfirmed,
                            //IsDelete = medicine.IsDelete,
                            //SelectedRoles = selectedRoles
                            IslemTuru = islemTuru,

                        });
                    }
                    return PartialView("_MedicineIslemi", new MedicineIslemViewModel());
                }
                else
                {

                    return PartialView("_MedicineIslemi", new MedicineIslemViewModel()
                    {
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
                var medicine =(await _medicineManager.getAll(x => x.ID == model.Id)).FirstOrDefault();
                if (medicine != null)
                {
                    medicine.Name = model.Name;
                    medicine.ExpireDate = model.ExpireDate;
                    medicine.BasisWeight = model.BasisWeight;
                    medicine.Producer = model.Producer;
                    //medicine.IsDelete = model.IsDelete;
                    _medicineManager.update(medicine);
                    //if (result.Succeeded)
                    //{
                    //    //var eczaneRoles = await _eczaneManager.GetRolesAsync(medicine);
                    //    //selectedRoles = selectedRoles ?? new string[] { };
                    //    //await _eczaneManager.AddToRolesAsync(medicine, selectedRoles.Except(eczaneRoles).ToArray<string>());
                    //    //await _eczaneManager.RemoveFromRolesAsync(medicine, eczaneRoles.Except(selectedRoles).ToArray<string>());

                    //}
                }
            }
            else
            {

                var medicine = new MEDICINE
                {
                    ID = System.Guid.NewGuid().ToString(),
                    Name = model.Name,
                    ExpireDate = model.ExpireDate,
                    BasisWeight = model.BasisWeight,
                    Producer = model.Producer,

                };

                _medicineManager.add(medicine);

                //if (result.Succeeded)
                //{

                //    //generate token
                //    var token = await _eczaneManager.GenerateEmailConfirmationTokenAsync(medicine);
                //    var url = Url.Action("ConfirmEmail", "Account", new
                //    {
                //        eczaneId = medicine.Id,
                //        token = token
                //    });

                //    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44303{url}'> tıklayınız.</a>");
                //    return RedirectToAction("EczaneIslemleri", "Account");
                //}
            }
            return RedirectToAction("MedicineIslemleri", "MedicineIslemleri");
        }



        public IActionResult MedicineListesi()
        {
            return PartialView("_MedicineListesi", _medicineManager.getAll());

        }
    }
}
