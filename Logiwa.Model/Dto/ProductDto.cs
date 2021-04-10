using Logiwa.Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logiwa.Models.Dto
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title Can Be 200 Characters Long")]
        public string Title { get; set; }
        public string Description { get; set; }
        public uint StockQuantity { get; set; }
        [Required]
        public virtual CategoryDto Category { get; set; }
    }
}