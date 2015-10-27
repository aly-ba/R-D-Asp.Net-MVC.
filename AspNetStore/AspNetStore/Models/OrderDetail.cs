﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetStore.Models
{
    public class OrderDetail
    {
        [Key]
        public int OderDetailId { get; set; }

        public int OderderId { get; set; }
        public int ProductId { get; set; }
        public  int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public virtual Product Product { get; set; }
        public virtual Order  Order { get; set; }

    }
    
}