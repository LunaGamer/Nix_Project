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
    class CategoryRepository : IRepository<Category>
    {
        private HotelModel db;

        public CategoryRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
        }

        public void Delete(int id)
        {
            Category category = Get(id);
            if (category != null)
                db.Categories.Remove(category);
        }
    }
}
