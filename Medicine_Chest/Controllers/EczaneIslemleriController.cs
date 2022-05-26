
using Business.Concrete;
using ENTITIES;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;

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
{    [Authorize(Roles="admin")]
    public class EczaneIslemleriController : Controller
    {
    
        private PharmaciesManager _pharmaciesManager = new PharmaciesManager(new DATA.Concrete.PHARMACIESDAL());
        //public EczaneIslemleriController(PharmaciesManager pharmaciesManager)
        //{
        //    _pharmaciesManager = pharmaciesManager;
            
        //}
    

   


        /// <summary>
        /// Sayfa ilk yüklendiğinde filtre ile ilgili elemanların hazırlanması için geçerli olan bir durumdur
        /// </summary>
        /// <param name="model">Model nesnesi</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<ActionResult> FiltreleEczane(EczaneViewModel model)
        {
            try
            {

                var kullanicilar = (IEnumerable<PHARMACIES>)(await _pharmaciesManager.getAll());



                if (!string.IsNullOrEmpty(model.EczaneAdiSorgu))
                {
                    kullanicilar = kullanicilar.Where(eczane => eczane.EczaneAdi == model.EczaneAdiSorgu);
                }
                if (!string.IsNullOrEmpty(model.AdresiSorgu))
                {
                    kullanicilar = kullanicilar.Where(eczane => eczane.Adresi == model.AdresiSorgu);
                }
                if (!string.IsNullOrEmpty(model.TelefonSorgu))
                {
                    kullanicilar = kullanicilar.Where(eczane => eczane.Telefon == model.TelefonSorgu);
                }
               

                model.PharmaciesList = (IEnumerable<PHARMACIES>)kullanicilar;
                return View("EczaneIslemleri", model);
            }
            catch (Exception exception)
            {
                return null; /*Mesaj.Goster(ArayuzSabitler.SorgulamaBaslik, "Sorgulama İşlemi Sırasında Bir Hata Oluştu: " + exception.GetAllMessages(), MesajTip.Hata);*/
            }
        }

        [HttpPost]
        public async Task<JsonResult> Sil(string Id)
        {
            var eczane = (await _pharmaciesManager.getAll(x => x.ID == Id)).FirstOrDefault();
            if (eczane != null)
            {
                _pharmaciesManager.delete(eczane);
            }
            return Json("Başarılı");
        }


        public async Task<IActionResult> EczaneIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            var model = new EczaneViewModel();
            model.PharmaciesList = (IEnumerable<PHARMACIES>)(await _pharmaciesManager.getAll());
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EczaneIslemi(string Id, string islemTuru)
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
                    var eczane = (await _pharmaciesManager.getAll(x=> x.ID ==Id)).FirstOrDefault();
                    if (eczane != null)
                    {

                        return PartialView("_EczaneIslemi", new PharmaciesIslemViewModel()
                        {
                            Id = eczane.ID,
                            EczaneAdi = eczane.EczaneAdi,
                            Adresi = eczane.Adresi,
                            Semt = eczane.Semt,
                            Telefon=eczane.Telefon,
                            Sehir = eczane.Sehir,
                            ilce = eczane.ilce,
                            //EmailConfirmed = eczane.EmailConfirmed,
                          
                            //IsDelete = eczane.IsDelete,
                            //SelectedRoles = selectedRoles
                            IslemTuru = islemTuru,

                        });
                    }
                    return PartialView("_EczaneIslemi", new PharmaciesIslemViewModel());
                }
                else
                {

                    return PartialView("_EczaneIslemi", new PharmaciesIslemViewModel()
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
                return PartialView("_EczaneIslemi", new PharmaciesIslemViewModel());
                //return Json(new JsonSonuc { HataMi = true, Mesaj = mesaj.Mesaj }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] ///form güvenliği için sadece post methodları için olur gelen formun gönderilen formla aynı mi ?
        public async Task<IActionResult> EczaneIslemi(PharmaciesIslemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EczaneIslemi", model);
            }
            if (model.IslemTuru != IslemTurSabitler.IslemTuruKayitEkleme)
            {
                var eczane =(await _pharmaciesManager.getAll(x => x.ID == model.Id)).FirstOrDefault();
                if (eczane != null)
                {

                    eczane.EczaneAdi = model.EczaneAdi;
                    eczane.Adresi = model.Adresi;
                    eczane.Semt = model.Semt;
                    eczane.Telefon = model.Telefon;
                    eczane.Sehir = model.Sehir;
                    eczane.ilce = model.ilce;
                    //eczane.IsDelete = model.IsDelete;
                    _pharmaciesManager.update(eczane);
                    //if (result.Succeeded)
                    //{
                    //    //var eczaneRoles = await _eczaneManager.GetRolesAsync(eczane);
                    //    //selectedRoles = selectedRoles ?? new string[] { };
                    //    //await _eczaneManager.AddToRolesAsync(eczane, selectedRoles.Except(eczaneRoles).ToArray<string>());
                    //    //await _eczaneManager.RemoveFromRolesAsync(eczane, eczaneRoles.Except(selectedRoles).ToArray<string>());

                    //}
                }
            }
            else
            {

                var eczane = new PHARMACIES
                {  ID = System.Guid.NewGuid().ToString(),

                    EczaneAdi = model.EczaneAdi,
                    Adresi = model.Adresi,
                    Semt = model.Semt,
                    Telefon = model.Telefon,
                    Sehir = model.Sehir,
                    ilce = model.ilce,
                };

                _pharmaciesManager.add(eczane);

                //if (result.Succeeded)
                //{

                //    //generate token
                //    var token = await _eczaneManager.GenerateEmailConfirmationTokenAsync(eczane);
                //    var url = Url.Action("ConfirmEmail", "Account", new
                //    {
                //        eczaneId = eczane.Id,
                //        token = token
                //    });

                //    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44303{url}'> tıklayınız.</a>");
                //    return RedirectToAction("EczaneIslemleri", "Account");
                //}
            }
            return RedirectToAction("EczaneIslemleri", "EczaneIslemleri");
        }

   

        public IActionResult EczaneListesi()
        {
            return PartialView("_EczaneListesi", _pharmaciesManager.getAll());

        }


    }
}
