using BusinessObjects;

namespace Repositories
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProductById(int id);
        void UpdateProduct(Product p);
        void DeleteProduct(int id);
        List<Category> GetCategories();
        List<Product> GetProducts();
    }
}
