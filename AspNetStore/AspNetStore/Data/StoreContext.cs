using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AspNetStore.Models;

namespace AspNetStore.Data
{
    public class StoreContext :DbContext
    {
        public StoreContext() : base("LDConnection")
        {
            
        }

        public DbSet<Category> Categories {get;set;} 
        public DbSet<Product>  Products { get;set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }
    }
}