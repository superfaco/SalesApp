using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Shared.Models
{
    [Table("SaleDetail")]
    public class SaleDetail
    {
        public long? Id { get; set; }
        public long? SaleId { get; set; }
        public Sale Sale { get; set; }
        public long? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [NotMapped]
        public string ProductIdAux { get { return ProductId == null ? null : ProductId.ToString(); } set { if (value == null) ProductId = null; else ProductId = long.Parse(value); } }
        public decimal? Quantity { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
