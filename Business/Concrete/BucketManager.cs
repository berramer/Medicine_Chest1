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
    public class BucketManager
    {
        private BUCKETDAL _bucketDal;

        public BucketManager(BUCKETDAL BUCKETDAL)
        {
            _bucketDal = BUCKETDAL;
        }
        public BucketManager()
        {

        }


        public async Task addAsync(BUCKET BUCKET)
        {

            await _bucketDal.AddAsync(BUCKET);


        }

        public void add(BUCKET BUCKET)
        {

            _bucketDal.Add(BUCKET);


        }
        public async Task updateAsync(BUCKET BUCKET)
        {

            await _bucketDal.UpdateAsync(BUCKET);


        }

        public void update(BUCKET BUCKET)
        {

            _bucketDal.Update(BUCKET);


        }
        public void delete(BUCKET BUCKET)
        {

            _bucketDal.Delete(BUCKET);


        }
        public async Task deleteasync(BUCKET BUCKET)
        {

            await _bucketDal.DeleteAsync(BUCKET);


        }

        public async Task<List<BUCKET>> getAll(Expression<Func<BUCKET, bool>> filter = null)
        {

            return await _bucketDal.getAll(filter);


        }
    }
}
