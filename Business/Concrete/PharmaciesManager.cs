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



        public void add(PHARMACIES pharmacies)
        {

            _pharmaciesDal.Add(pharmacies);


        }
        public void update(PHARMACIES pharmacies)
        {

            _pharmaciesDal.Update(pharmacies);


        }
        public void delete(PHARMACIES pharmacies)
        {

            _pharmaciesDal.Delete(pharmacies);


        }


        public List<PHARMACIES> getAll(Expression<Func<PHARMACIES, bool>> filter = null)
        {

            return _pharmaciesDal.getAll(filter);


        }
    }

}
