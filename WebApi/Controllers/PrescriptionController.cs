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

