using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        public List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listProducts = context.Products.Include(p => p.Category).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }
        public Product FindProductById(int prodId)
        {
            Product p = new Product();
            try
            {
                using (var context = new MyDbContext())
                {
                    p = context.Products.Include(x => x.Category).SingleOrDefault(x => x.ProductId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }
        public void SaveProduct(Product p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteProduct(int id)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var pDelete = context.Products.SingleOrDefault(x => x.ProductId == id);
                    context.Products.Remove(pDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
