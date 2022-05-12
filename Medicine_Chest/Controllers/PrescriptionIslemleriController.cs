using Business.Concrete;
using ENTITIES;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Medicine_Chest.Models.PrescriptionIslemleri;

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
    public class PrescriptionIslemleriController :Controller
    {

        private PrescriptionManager _prescriptionManager = new PrescriptionManager(new DATA.Concrete.PRESCRIPTIONDAL());
        //public EczaneIslemleriController(PrescriptionManager PrescriptionManager)
        //{
        //    _prescriptionManager = PrescriptionManager;

        //}





        /// <summary>
        /// Sayfa ilk yüklendiğinde filtre ile ilgili elemanların hazırlanması için geçerli olan bir durumdur
        /// </summary>
        /// <param name="model">Model nesnesi</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult FiltrelePrescription(PrescriptionViewModel model)
        {
            try
            {

                var kullanicilar = (IEnumerable<PRESCRIPTION>)_prescriptionManager.getAll();



                if (!string.IsNullOrEmpty(model.PrescriptionCodeSorgu))
                {
                    kullanicilar = kullanicilar.Where(prescription => prescription.PrescriptionCode == model.PrescriptionCodeSorgu);
                }
                if (!string.IsNullOrEmpty(model.UserIDSorgu))
                {
                    kullanicilar = kullanicilar.Where(prescription => prescription.UserID == model.UserIDSorgu);
                }
                if (model.ValidityDateSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(prescription => prescription.ValidityDate == model.ValidityDateSorgu);
                }
                if (model.MedicineIDSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(prescription => prescription.MedicineID == model.MedicineIDSorgu);
                }

                model.PrescriptionList = (IEnumerable<PRESCRIPTION>)kullanicilar;
                return View("PrescriptionIslemleri", model);
            }
            catch (Exception exception)
            {
                return null; /*Mesaj.Goster(ArayuzSabitler.SorgulamaBaslik, "Sorgulama İşlemi Sırasında Bir Hata Oluştu: " + exception.GetAllMessages(), MesajTip.Hata);*/
            }
        }

        [HttpPost]
        public JsonResult Sil(string Id)
        {
            var prescription = _prescriptionManager.getAll(x => x.ID == Id).FirstOrDefault();
            if (prescription != null)
            {
                _prescriptionManager.delete(prescription);
            }
            return Json("Başarılı");
        }


        public IActionResult PrescriptionIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            var model = new PrescriptionViewModel();
            model.PrescriptionList = (IEnumerable<PRESCRIPTION>)_prescriptionManager.getAll();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> PrescriptionIslemi(string Id, string islemTuru)
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
                    var prescription = _prescriptionManager.getAll(x => x.ID == Id).FirstOrDefault();
                    if (prescription != null)
                    {

                        return PartialView("_PrescriptionIslemi", new PrescriptionIslemViewModel()
                        {
                            Id = prescription.ID,
                            PrescriptionCode = prescription.PrescriptionCode,
                            UserID = prescription.UserID,
                            ValidityDate = prescription.ValidityDate,

                            //EmailConfirmed = prescription.EmailConfirmed,
                            MedicineID = prescription.MedicineID,
                            //IsDelete = prescription.IsDelete,
                            //SelectedRoles = selectedRoles
                            IslemTuru = islemTuru,

                        });
                    }
                    return PartialView("_PrescriptionIslemi", new PrescriptionIslemViewModel());
                }
                else
                {

                    return PartialView("_PrescriptionIslemi", new PrescriptionIslemViewModel()
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
                return PartialView("_PrescriptionIslemi", new PrescriptionIslemViewModel());
                //return Json(new JsonSonuc { HataMi = true, Mesaj = mesaj.Mesaj }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] ///form güvenliği için sadece post methodları için olur gelen formun gönderilen formla aynı mi ?
        public async Task<IActionResult> PrescriptionIslemi(PrescriptionIslemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PrescriptionIslemi", model);
            }
            if (model.IslemTuru != IslemTurSabitler.IslemTuruKayitEkleme)
            {
                var prescription = _prescriptionManager.getAll(x => x.ID == model.Id).FirstOrDefault();
                if (prescription != null)
                {
                    
                    prescription.PrescriptionCode = model.PrescriptionCode;
                    prescription.UserID = model.UserID;

                    prescription.ValidityDate = model.ValidityDate;
                    prescription.MedicineID = model.MedicineID;
                    //prescription.IsDelete = model.IsDelete;
                    _prescriptionManager.update(prescription);
                    //if (result.Succeeded)
                    //{
                    //    //var eczaneRoles = await _eczaneManager.GetRolesAsync(prescription);
                    //    //selectedRoles = selectedRoles ?? new string[] { };
                    //    //await _eczaneManager.AddToRolesAsync(prescription, selectedRoles.Except(eczaneRoles).ToArray<string>());
                    //    //await _eczaneManager.RemoveFromRolesAsync(prescription, eczaneRoles.Except(selectedRoles).ToArray<string>());

                    //}
                }
            }
            else
            {

                var prescription = new PRESCRIPTION
                {
                    ID = System.Guid.NewGuid().ToString(),
                  
                    
                    PrescriptionCode = model.PrescriptionCode,
                    UserID = model.UserID,
                    ValidityDate = model.ValidityDate,
                    MedicineID = model.MedicineID,



            };

                _prescriptionManager.add(prescription);

                //if (result.Succeeded)
                //{

                //    //generate token
                //    var token = await _eczaneManager.GenerateEmailConfirmationTokenAsync(prescription);
                //    var url = Url.Action("ConfirmEmail", "Account", new
                //    {
                //        eczaneId = prescription.Id,
                //        token = token
                //    });

                //    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44303{url}'> tıklayınız.</a>");
                //    return RedirectToAction("PrescriptionIslemleri", "Account");
                //}
            }
            return RedirectToAction("PrescriptionIslemleri", "PrescriptionIslemleri");
        }



        public IActionResult PrescriptionListesi()
        {
            return PartialView("_PrescriptionListesi", _prescriptionManager.getAll());

        }
    }
}
