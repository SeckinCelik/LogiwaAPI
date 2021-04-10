using Logiwa.Model.Entity;
using Logiwa.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logiwa.Repository.Interface
{
    public interface IRepository
    {
        IEnumerable<Product> GetAll();
        Product Get(string id);
        Product Add(ProductDto item);
        bool Update(ProductDto item);
        IEnumerable<Product> Filter(FilterDto filter);
    }
}
