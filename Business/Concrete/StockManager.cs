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
    public class StockManager
    {

        private STOCKDAL _stockDal;

        public StockManager(STOCKDAL stockDal)
        {
            _stockDal = stockDal;
        }
        public StockManager()
        {

        }



        public void add(STOCK stock)
        {

            _stockDal.Add(stock);


        }
        public void update(STOCK stock)
        {

            _stockDal.Update(stock);


        }
        public void delete(STOCK stock)
        {

            _stockDal.Delete(stock);


        }


        public async Task<List<STOCK>> getAll(Expression<Func<STOCK, bool>> filter = null)
        {

            return await _stockDal.getAll(filter);


        }
    }

}
