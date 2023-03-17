using BusinessObjects;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Util
{
    public class Mapper
    {
        public static ProductDTO mapToDTO(Product product)
        {
            ProductDTO productDTO = new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitInStock = product.UnitInStock,
                CategoryId = product.CategoryId,
            };
            return productDTO;
        }
        public static Product mapToEntity(ProductDTO productDTO)
        {
            Product product = new Product
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
                UnitPrice = productDTO.UnitPrice,
                UnitInStock = productDTO.UnitInStock,
                CategoryId = productDTO.CategoryId,
            };
            return product;
        }
    }
}
