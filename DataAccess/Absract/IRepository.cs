using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Absract
{
    public interface IRepository<Tentity>
    {
        public void Add(Tentity tentity);
        public void Delete(Tentity tentity);
        public void Update(Tentity tentity);
       public Task  AddAsync(Tentity tentity);
       public Task UpdateAsync(Tentity tentity);
        public Task DeleteAsync(Tentity tentity);
        public Task<List<Tentity>> getAll(Expression<Func<Tentity, bool>> filter = null);
    }
}
