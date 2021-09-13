using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Shared.Models
{
    [Table("Sale")]
    public class Sale
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }
        public long? Id { get; set; }
        public string Customer { get; set; }
        public DateTime? SaleDate { get; set; }
        public string Employee { get; set; }
        [NotMapped]
        public decimal? Total
        {
            get
            {
                decimal? total = 0;
                foreach(SaleDetail sd in SaleDetails)
                {
                    total += sd.SalePrice * sd.Quantity;
                }
                return total;
            }
        }
        [InverseProperty("Sale")]
        public HashSet<SaleDetail> SaleDetails { get; set; }
    }
}
