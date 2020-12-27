using LocalOfferts.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LocalOfferts.Service
{
    public interface IProductService
    {
        Task<bool> CreateProduct(Product product);
        Task<IEnumerable<Product>> GetProductsByName();
        Task<IEnumerable<Product>> GetProductsByCity(string city);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsFromTwoWeeks();
        Task<bool> DeleteProduct(int productId);
        Task<Product> SingleProduct(int idproductId);

        Task<bool> EditProduct(int productId, Product product);
        Task<bool> EditProductAdmin(int productId, Product product);
        Task<bool> RefreshProduct(int productId);
    }
}