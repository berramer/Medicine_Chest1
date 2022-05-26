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
    public class EczaneController : ControllerBase
    {
        private PharmaciesManager _pharmaciesManager = new PharmaciesManager(new DATA.Concrete.PHARMACIESDAL());

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _pharmaciesManager.getAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PHARMACIES pharmacies)
        {
            await _pharmaciesManager.addAsync(pharmacies);
            return Ok(pharmacies);
        }


        [HttpPut]
        public async Task<IActionResult> Update(PHARMACIES pharmacies)
        {
            await _pharmaciesManager.updateAsync(pharmacies);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pharmacies = (await _pharmaciesManager.getAll(x => x.ID == id)).FirstOrDefault();
            if (pharmacies != null)
            {

                await _pharmaciesManager.deleteasync(pharmacies);


                return Ok("success");
            }
            return NotFound();
        }
    }
}

