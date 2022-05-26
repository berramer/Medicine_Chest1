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
   public class PharmaciesManager
    {

        private PHARMACIESDAL _pharmaciesDal;

        public PharmaciesManager(PHARMACIESDAL pharmaciesDal)
        {
           _pharmaciesDal = pharmaciesDal;
        }
        public PharmaciesManager()
        {
           
        }




        public async Task addAsync(PHARMACIES PHARMACIES)
        {

            await _pharmaciesDal.AddAsync(PHARMACIES);


        }

        public void add(PHARMACIES PHARMACIES)
        {

            _pharmaciesDal.Add(PHARMACIES);


        }
        public async Task updateAsync(PHARMACIES PHARMACIES)
        {

            await _pharmaciesDal.UpdateAsync(PHARMACIES);


        }

        public void update(PHARMACIES PHARMACIES)
        {

            _pharmaciesDal.Update(PHARMACIES);


        }
        public void delete(PHARMACIES PHARMACIES)
        {

            _pharmaciesDal.Delete(PHARMACIES);


        }
        public async Task deleteasync(PHARMACIES PHARMACIES)
        {

            await _pharmaciesDal.DeleteAsync(PHARMACIES);


        }



        public async Task<List<PHARMACIES>> getAll(Expression<Func<PHARMACIES, bool>> filter = null)
        {

            return await  _pharmaciesDal.getAll(filter);


        }
    }

}
