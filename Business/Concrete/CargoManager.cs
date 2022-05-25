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
    public class CargoManager
    {
        private CARGODAL _cargoDal;

        public CargoManager(CARGODAL CARGODAL)
        {
            _cargoDal = CARGODAL;
        }
        public CargoManager()
        {

        }


        public async Task addAsync(CARGO CARGO)
        {

            await _cargoDal.AddAsync(CARGO);


        }

        public void add(CARGO CARGO)
        {

            _cargoDal.Add(CARGO);


        }
        public async Task updateAsync(CARGO CARGO)
        {

            await _cargoDal.UpdateAsync(CARGO);


        }

        public void update(CARGO CARGO)
        {

            _cargoDal.Update(CARGO);


        }
        public void delete(CARGO CARGO)
        {

            _cargoDal.Delete(CARGO);


        }
        public async Task deleteasync(CARGO CARGO)
        {

            await _cargoDal.DeleteAsync(CARGO);


        }

        public async Task<List<CARGO>> getAll(Expression<Func<CARGO, bool>> filter = null)
        {

            return await _cargoDal.getAll(filter);


        }
    }
}
