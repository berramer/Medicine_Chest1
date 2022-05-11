using DATA.Concrete;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Business.Concrete
{
    public class MedicineManager
    {
        private MEDICINEDAL _medicineDal;

        public MedicineManager(MEDICINEDAL MEDICINEDAL)
        {
            _medicineDal = MEDICINEDAL;
        }
        public MedicineManager()
        {

        }



        public void add(MEDICINE MEDICINE)
        {

            _medicineDal.Add(MEDICINE);


        }
        public void update(MEDICINE MEDICINE)
        {

            _medicineDal.Update(MEDICINE);


        }
        public void delete(MEDICINE MEDICINE)
        {

            _medicineDal.Delete(MEDICINE);


        }


        public List<MEDICINE> getAll(Expression<Func<MEDICINE, bool>> filter = null)
        {

            return _medicineDal.getAll(filter);


        }
    }
}
