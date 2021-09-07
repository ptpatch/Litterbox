using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Litterbox.Entities
{
    public class OrderItem : BaseEntity
    {
        [NotMapped]
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}