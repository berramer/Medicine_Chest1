using Business.Concrete;
using ENTITIES;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Medicine_Chest.Models.CargoIslemleri;



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
    public class CargoIslemleriController : Controller
    {
        private CargoManager _cargoManager = new CargoManager(new DATA.Concrete.CARGODAL());
        //public EczaneIslemleriController(MedicineManager pharmaciesManager)
        //{
        //    _cargoManager = pharmaciesManager;

        //}





        /// <summary>
        /// Sayfa ilk yüklendiğinde filtre ile ilgili elemanların hazırlanması için geçerli olan bir durumdur
        /// </summary>
        /// <param name="model">Model nesnesi</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<ActionResult> FiltreleCargo(CargoViewModel model)
        {
            try
            {

                var kullanicilar = (IEnumerable<CARGO>)(await _cargoManager.getAll());



                if (!string.IsNullOrEmpty(model.OrderIdSorgu))
                {
                    kullanicilar = kullanicilar.Where(cargo => cargo.OrderId == model.OrderIdSorgu);
                }
                if (model.OrderDateCSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(cargo => cargo.OrderDateC == model.OrderDateCSorgu);
                }
                if (model.IsDeliveredSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(cargo => cargo.IsDelivered == model.IsDeliveredSorgu);
                }
                if (model.ScoreSorgu != null)
                {
                    kullanicilar = kullanicilar.Where(cargo => cargo.Score == model.ScoreSorgu);
                }


                model.CargoList = (IEnumerable<CARGO>)kullanicilar;
                return View("CargoIslemleri", model);
            }
            catch (Exception exception)
            {
                return null; /*Mesaj.Goster(ArayuzSabitler.SorgulamaBaslik, "Sorgulama İşlemi Sırasında Bir Hata Oluştu: " + exception.GetAllMessages(), MesajTip.Hata);*/
            }
        }
        public async Task<IActionResult> CargoIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];

            var model = new CargoViewModel();
            model.CargoList = (IEnumerable<CARGO>)(await _cargoManager.getAll());
            return View(model);
        }







        public IActionResult CargoListesi()
        {
            return PartialView("_CargoListesi", _cargoManager.getAll());

        }
    }
}
