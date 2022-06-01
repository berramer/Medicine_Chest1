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
    [Route("api/{controller}/{action}")]
    public class MedicineController : ControllerBase
    {
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _medicineManager.getAll());
        }
        [HttpGet]
        public async Task<IActionResult> GetRecetesiz()
        {
            var urunler = (await _medicineManager.getAll(oh => oh.IsPrescription.Equals(0)));
            return Ok(urunler);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MEDICINE medicine)
        {
            await _medicineManager.addAsync(medicine);
            return Ok(medicine);
        }


        [HttpPut]
        public async Task<IActionResult> Update(MEDICINE medicine)
        {
            await _medicineManager.updateAsync(medicine);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var medicine = (await _medicineManager.getAll(x => x.ID == id)).FirstOrDefault();
            if (medicine != null)
            {

                await _medicineManager.deleteasync(medicine);


                return Ok("success");
            }
            return NotFound();
        }


        [HttpGet]
        [Route("{key}")]
        public async Task<ActionResult> Search(string key)
        {
            var urunler = (await _medicineManager.getAll(oh =>( oh.Category.Contains(key) || oh.Name.Contains(key) || oh.Producer.Contains(key)) && oh.IsPrescription.Equals(0)));
            //if (!string.IsNullOrEmpty(key))
            //{
            //    urunler = urunler.Where(oh => oh.Category.Contains(key) || oh.Name.Contains(key) || oh.Producer.Contains(key));
            //}

            return Ok(urunler);
        }


        [HttpGet]
        [Route("{key}")]
        public async Task<ActionResult> SearchCat(string key)
        {
            var urunler = (await _medicineManager.getAll(oh => oh.Category.Contains(key) && oh.IsPrescription.Equals(0)));
            //if (!string.IsNullOrEmpty(key))
            //{
            //    urunler = urunler.Where(oh => oh.Category.Contains(key) || oh.Name.Contains(key) || oh.Producer.Contains(key));
            //}

            return Ok(urunler);
        }
    }
}



