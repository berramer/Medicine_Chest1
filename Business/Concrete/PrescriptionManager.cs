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
    public class PrescriptionManager
    {

        private PRESCRIPTIONDAL _prescriptionDal;

        public PrescriptionManager(PRESCRIPTIONDAL PRESCRIPTIONDAL)
        {
            _prescriptionDal = PRESCRIPTIONDAL;
        }
        public PrescriptionManager()
        {

        }



        public void add(PRESCRIPTION PRESCRIPTION)
        {

            _prescriptionDal.Add( PRESCRIPTION);


        }
        public void update(PRESCRIPTION PRESCRIPTION)
        {

            _prescriptionDal.Update(PRESCRIPTION);


        }
        public void delete(PRESCRIPTION PRESCRIPTION)
        {

            _prescriptionDal.Delete(PRESCRIPTION);


        }


        public List<PRESCRIPTION> getAll(Expression<Func<PRESCRIPTION, bool>> filter = null)
        {

            return _prescriptionDal.getAll(filter);


        }
    }
}
