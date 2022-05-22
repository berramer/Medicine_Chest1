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


        public async Task addAsync(ORDER order)
        {

            await _orderDal.AddAsync(order);


        }

        public void add(ORDER order)
        {

            _orderDal.Add(order);


        }
        public async Task updateAsync(ORDER order)
        {

            await _orderDal.UpdateAsync(order);


        }

        public void update(ORDER order)
        {

            _orderDal.Update(order);


        }
        public void delete(ORDER order)
        {

            _orderDal.Delete(order);


        }
        public async Task deleteasync(ORDER order)
        {

            await _orderDal.DeleteAsync(order);


        }

        public async Task<List<ORDER>> getAll(Expression<Func<ORDER, bool>> filter = null)
        {

            return await _orderDal.getAll(filter);


        }
    }
}
