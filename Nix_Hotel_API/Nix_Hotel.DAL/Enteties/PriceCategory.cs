using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.Enteties
{
    public class PriceCategory
    {
        [Key]
        public int Id
        {
            get;
            set;
        }

        public int CategoryId
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

        [ForeignKey("CategoryId")]
        public virtual Category CategoryPrice
        {
            get;
            set;
        }
    }
}
