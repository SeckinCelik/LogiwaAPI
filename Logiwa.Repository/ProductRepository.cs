using Logiwa.Model.Entity;
using Logiwa.Models.Dto;
using Logiwa.Repository.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Logiwa.Repository
{
    public class ProductRepository : IRepository
    {
        ObjectCache cache;
        CacheItemPolicy policy;
        public ProductRepository()
        {
            cache = System.Runtime.Caching.MemoryCache.Default;
            policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) };
        }
        public Product Add(ProductDto item)
        {
            Product product = new Product
            {
                Category = new Category
                {
                    MinimumStockQuantity = item.Category.MinimumStockQuantity,
                    Name = item.Category.Name
                },
                Description = item.Description,
                ProductId = Guid.NewGuid(),
                StockQuantity = item.StockQuantity,
                Title = item.Title
            };

            var productList = cache.Get("productData") as List<Product>;
            if (productList != null)
            {
                cache.Remove("productData");
                productList.Add(product);
            }
            else
            {
                productList = new List<Product> { product };
            }
            cache.Add("productData", productList, policy);

            return product;
        }

        public IEnumerable<Product> Filter(FilterDto filter)
        {
            var productList = cache.Get("productData") as IEnumerable<Product>;

            if (productList == null)
            {
                return null;
            }
            productList = productList.Where(x => x.StockQuantity >= x.Category.MinimumStockQuantity);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                productList = productList.Where(x => x.Title.IndexOf(filter.Name, StringComparison.OrdinalIgnoreCase) != -1
                                                            || x.Description.IndexOf(filter.Name, StringComparison.OrdinalIgnoreCase) != -1
                                                            || x.Category.Name.IndexOf(filter.Name, StringComparison.OrdinalIgnoreCase) != -1);
            }
            if (filter.StockRange != null && filter.StockRange.Any())
            {
                var min = Math.Min(filter.StockRange[0], filter.StockRange[1]);
                var max = Math.Max(filter.StockRange[0], filter.StockRange[1]);

                productList = productList.Where(x => x.StockQuantity >= min && x.StockQuantity <= max);
            }
            return productList;
        }

        public Product Get(string id)
        {
            var productList = cache.Get("productData") as List<Product>;

            if (productList == null)
                return null;

            return productList.Where(x => x.ProductId == Guid.Parse(id)).Select(x => x).FirstOrDefault();
        }

        public IEnumerable<Product> GetAll()
        {
            var productList = cache.Get("productData") as List<Product>;

            return productList;
        }

        public bool Update(ProductDto item)
        {
            if (item == null || item.ProductId == null)
                return false;

            Product product = new Product
            {
                Category = new Category
                {
                    MinimumStockQuantity = item.Category.MinimumStockQuantity,
                    Name = item.Category.Name
                },
                Description = item.Description,
                ProductId = item.ProductId,
                StockQuantity = item.StockQuantity,
                Title = item.Title
            };

            var productList = cache.Get("productData") as List<Product>;

            if (productList != null)
            {
                cache.Remove("productData");

                var existingItem = productList.Where(x => x.ProductId == item.ProductId).Select(x => x).First();
                var index = productList.IndexOf(existingItem);

                if (index != -1)
                    productList[index] = product;
            }
            else
                return false;

            cache.Add("productData", productList, policy);

            return true;
        }
    }
}
