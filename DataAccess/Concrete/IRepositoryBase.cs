using DATA.Absract;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAcces.Concrete
{
    public class IRepositoryBase<Tentity, Tcontext> : IRepository<Tentity> where Tcontext : DbContext, new()
        where Tentity : class, new()
    {
        public void Add(Tentity tentity)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var addedEntity = tcontext.Entry(tentity);
                addedEntity.State = EntityState.Added;
                tcontext.SaveChanges();
            }
        }

        public void Delete(Tentity tentity)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var deletedEntity = tcontext.Entry(tentity);
                deletedEntity.State = EntityState.Deleted;
                tcontext.SaveChanges();
            }
        }

        public List<Tentity> getAll(Expression<Func<Tentity, bool>> filter = null)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                if (filter == null)
                {
                    return tcontext.Set<Tentity>().ToList();
                }
                else
                {
                    return tcontext.Set<Tentity>().Where(filter).ToList();
                }
            }
        }

        public void Update(Tentity tentity)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var UpdatetedEntity = tcontext.Entry(tentity);
                UpdatetedEntity.State = EntityState.Modified;
                tcontext.SaveChanges();
            }
        }
    }
}





