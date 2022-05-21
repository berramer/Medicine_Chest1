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
    public class BucketController : ControllerBase
    {
        private BucketManager _bucketManager = new BucketManager(new DATA.Concrete.BUCKETDAL());

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bucketManager.getAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BUCKET bucket)

        {
            bucket.ID = new Guid().ToString();
            await _bucketManager.addAsync(bucket);
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

