using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logiwa.Model.Entity
{
    public class Category
    {
        public string  Name { get; set; }
        public uint MinimumStockQuantity { get; set; }
    }
}