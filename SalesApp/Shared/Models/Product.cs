using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Shared.Models
{
    [Table("Product")]
    public class Product
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public decimal? Price { get; set; }
    }
}
