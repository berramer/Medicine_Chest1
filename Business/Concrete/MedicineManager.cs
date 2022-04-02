using Core.Aspects;
using DATA.Concrete;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
public     class MedicineManager
    {
        private MEDICINEDAL _medıcınedal;

        [SecuredOperation(Roles="Admin,editör")]
        public List<MEDICINE> getlist()
        {
            return _medıcınedal.getAll();
        }
    }
}
