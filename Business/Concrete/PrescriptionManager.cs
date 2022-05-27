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

        public PrescriptionManager(PRESCRIPTIONDAL prescriptionDal)
        {
            _prescriptionDal = prescriptionDal;
        }
        public PrescriptionManager()
        {

        }




        public async Task addAsync(PRESCRIPTION PRESCRIPTION)
        {

            await _prescriptionDal.AddAsync(PRESCRIPTION);


        }

        public void add(PRESCRIPTION PRESCRIPTION)
        {

            _prescriptionDal.Add(PRESCRIPTION);


        }
        public async Task updateAsync(PRESCRIPTION PRESCRIPTION)
        {

            await _prescriptionDal.UpdateAsync(PRESCRIPTION);


        }

        public void update(PRESCRIPTION PRESCRIPTION)
        {

            _prescriptionDal.Update(PRESCRIPTION);


        }
        public void delete(PRESCRIPTION PRESCRIPTION)
        {

            _prescriptionDal.Delete(PRESCRIPTION);


        }
        public async Task deleteasync(PRESCRIPTION PRESCRIPTION)
        {

            await _prescriptionDal.DeleteAsync(PRESCRIPTION);


        }



        public async Task<List<PRESCRIPTION>> getAll(Expression<Func<PRESCRIPTION, bool>> filter = null)
        {

            return await _prescriptionDal.getAll(filter);


        }
    }

}
