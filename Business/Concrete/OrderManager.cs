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
    public class OrderManager
    {
        private ORDERDAL _orderDal;

        public OrderManager(ORDERDAL ORDERDAL)
        {
            _orderDal = ORDERDAL;
        }
        public OrderManager()
        {

        }


        public async Task addAsync(ORDER ORDER)
        {

            await _orderDal.AddAsync(ORDER);


        }

        public void add(ORDER ORDER)
        {

            _orderDal.Add(ORDER);


        }
        public async Task updateAsync(ORDER ORDER)
        {

            await _orderDal.UpdateAsync(ORDER);


        }

        public void update(ORDER ORDER)
        {

            _orderDal.Update(ORDER);


        }
        public void delete(ORDER ORDER)
        {

            _orderDal.Delete(ORDER);


        }
        public async Task deleteasync(ORDER ORDER)
        {

            await _orderDal.DeleteAsync(ORDER);


        }

        public async Task<List<ORDER>> getAll(Expression<Func<ORDER, bool>> filter = null)
        {

            return await _orderDal.getAll(filter);


        }
    }
}
