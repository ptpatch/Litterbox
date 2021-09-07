using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}