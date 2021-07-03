using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.DTO
{
    public class PriceCategoryDTO
    {
        public int Id
        {
            get;
            set;
        }

        public CategoryDTO Category
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public DateTime? StartDate
        {
            get;
            set;
        }

        public DateTime? EndDate
        {
            get;
            set;
        }
    }
}
