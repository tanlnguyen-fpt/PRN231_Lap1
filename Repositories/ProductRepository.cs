using BusinessObjects;
using DataAccess;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        ProductDAO productDAO = new ProductDAO();
        public void SaveProduct(Product p)
        {
            productDAO.SaveProduct(p);
        }
        public Product GetProductById(int id)
        {
            return productDAO.FindProductById(id);
        }
        public void DeleteProduct(int id)
        {
            productDAO.DeleteProduct(id);
        }
        public void UpdateProduct(Product p)
        {
            productDAO.UpdateProduct(p);
        }
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public List<Product> GetProducts()
        {
            return productDAO.GetProducts();
        }
    }
}
