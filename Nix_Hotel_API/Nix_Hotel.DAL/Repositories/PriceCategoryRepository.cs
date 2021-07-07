using Nix_Hotel.DAL.EF;
using Nix_Hotel.DAL.Enteties;
using Nix_Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.Repositories
{
    class PriceCategoryRepository : IRepository<PriceCategory>
    {
        private HotelModel db;

        public PriceCategoryRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<PriceCategory> GetAll()
        {
            return db.Prices;
        }

        public PriceCategory Get(int id)
        {
            return db.Prices.Find(id);
        }

        public void Create(PriceCategory price)
        {
            db.Prices.Add(price);
        }

        public void Delete(int id)
        {
            PriceCategory price = Get(id);
            if (price != null)
                db.Prices.Remove(price);
        }

        public void Update(int id, PriceCategory item)
        {
            throw new NotImplementedException();
        }
    }
}
