﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Entities
{
    public class OrderHistory : BaseEntity
    {
        public int OrderID { get; set; }
        public int OrderStatus { get; set; }
        public string Note { get; set; }
    }
}