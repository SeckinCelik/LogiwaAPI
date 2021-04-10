using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logiwa.Models.Dto
{
    public class FilterDto
    {
        public string Name { get; set; }
        public uint[] StockRange { get; set; }
    }
}