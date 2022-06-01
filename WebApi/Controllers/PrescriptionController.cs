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
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private PrescriptionManager _prescriptionManager = new PrescriptionManager(new DATA.Concrete.PRESCRIPTIONDAL());
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _prescriptionManager.getAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PRESCRIPTION prescription)
        {
            await _prescriptionManager.addAsync(prescription);
            return Ok(prescription);
        }


        [HttpGet]
        [Route("{code}")]
        public async Task<IActionResult> getByCode(string code)

        {
           
            PRESCRIPTION prescription = (await _prescriptionManager.getAll(x => x.PrescriptionCode == code)).FirstOrDefault();
            if (prescription != null)
            {
                if (prescription.ValidityDate > DateTime.Now)
                {
                    var medicineIdlist = prescription.MedicineID.Split(',');
                    List<MEDICINE> medicineList = new List<MEDICINE>();
                    foreach (string Id in medicineIdlist)
                    {
                        if (!string.IsNullOrEmpty(Id))
                        {
                            var medicine = (await _medicineManager.getAll(x => x.ID == Id)).FirstOrDefault();
                            if (medicine != null)
                            {
                                medicineList.Add(medicine);
                            }

                        }
                    }
                    var result = new
                    {
                        Id = prescription.ID,
                        User = prescription.UserID,
                        ValidateTime = prescription.ValidityDate.ToString(),
                        Medicinelist = medicineList

                    };
                    return Ok(result);
                }
            }
            return NotFound();
         
        }

        [HttpPut]
        public async Task<IActionResult> Update(PRESCRIPTION prescription)
        {
            await _prescriptionManager.updateAsync(prescription);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var prescription = (await _prescriptionManager.getAll(x => x.ID == id)).FirstOrDefault();
            if (prescription != null)
            {

                await _prescriptionManager.deleteasync(prescription);


                return Ok("success");
            }
            return NotFound();
        }
    }
}

