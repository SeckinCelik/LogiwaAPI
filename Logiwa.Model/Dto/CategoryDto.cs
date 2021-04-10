using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logiwa.Model.Dto
{
    public class CategoryDto
    {
        [Required]
        [StringLength(200, ErrorMessage = "Title Can Be 200 Characters Long")]
        public string Name { get; set; }
        public uint MinimumStockQuantity { get; set; }
    }
}
