using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private MedicineManager _medicineManager = new MedicineManager(new DATA.Concrete.MEDICINEDAL());

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_medicineManager.getAll());
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok(_medicineManager.getAll());
        }
    }
}

