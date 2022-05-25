using Business.Concrete;
using ENTITIES;
using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BucketController : ControllerBase
    {

        private BucketManager _bucketManager = new BucketManager(new DATA.Concrete.BUCKETDAL());
       
        [HttpGet]
        public async Task<IActionResult> Get()

        {
             List < BUCKET > a= await _bucketManager.getAll();

            return Ok(a);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetbyId(string id)

        {
            List<BUCKET> a = await _bucketManager.getAll(x=>x.UserId==id);

            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BUCKET bucket)

        {
            var bucketList =( await _bucketManager.getAll(x => x.UserId == bucket.UserId && x.MedicineId ==bucket.MedicineId)).FirstOrDefault();
            if (bucketList != null)
            {
                bucketList.Item += bucket.Item;
                await _bucketManager.updateAsync(bucketList);
            }
            else
            {

                bucket.ID = System.Guid.NewGuid().ToString();
                await _bucketManager.addAsync(bucket);
            }
            return Ok(bucket);
        }


        [HttpPut]
        public async Task<IActionResult> Update(BUCKET bucket)
        {
          


            await _bucketManager.updateAsync(bucket);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var bucket = (await _bucketManager.getAll(x => x.ID == id)).FirstOrDefault();
            if (bucket != null)
            {

                await _bucketManager.deleteasync(bucket);


                return Ok("success");
            }
            return NotFound();
        }
    }
}

