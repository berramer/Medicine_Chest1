using DATA.Absract;
using DataAcces.Concrete;
using ENTITIES;
using ENTITIES.Dtos;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATA.Concrete
{
    public class MEDICINEDAL : IRepositoryBase<MEDICINE, WebContext>, IRepository<MEDICINE>
    {

        public async Task<List<MedicineDto>> GetListMedicine()
        {

            using (WebContext tcontext = new WebContext())
            {
                var query = (from medicine in tcontext.MEDICINE
                            join stock in tcontext.STOCK on medicine.ID equals stock.MedicineID
                            select new MedicineDto
                            {
                                  Name =medicine.Name,
                                  ExpireDate =medicine.ExpireDate,
                                  BasisWeight =medicine.BasisWeight,
                                  Price =medicine.Price,
                                  Producer =medicine.Producer,
                                  Photo=medicine.Photo,
                                  MedicineID =medicine.ID,
                                   PharmID =stock.PharmID,
                                   Stock =stock.Stock

    });
                var list = await query.ToListAsync();
                return list;
            }
           
        }
    }
}
