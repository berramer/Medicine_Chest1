using DATA.Absract;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task AddAsync(Tentity tentity)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var addedEntity = tcontext.Entry(tentity);
                addedEntity.State = EntityState.Added;
               await tcontext.SaveChangesAsync();
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

        public async Task DeleteAsync(Tentity tentity)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                var deletedEntity = tcontext.Entry(tentity);
                deletedEntity.State = EntityState.Deleted;
                await tcontext.SaveChangesAsync();
            }

        }

        public  async Task<List<Tentity>> getAll(Expression<Func<Tentity, bool>> filter = null)
        {
            using (Tcontext tcontext = new Tcontext())
            {
                if (filter == null)
                {
                    return await tcontext.Set<Tentity>().ToListAsync();
                }
                else
                {
                    return await tcontext.Set<Tentity>().Where(filter).ToListAsync();
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

        public async Task UpdateAsync(Tentity tentity)
        {
            using(Tcontext tcontext = new Tcontext())
            {
                var UpdatetedEntity = tcontext.Entry(tentity);
                UpdatetedEntity.State = EntityState.Modified;
              await  tcontext.SaveChangesAsync();
            }
        }
    }
}





