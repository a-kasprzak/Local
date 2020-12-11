using LocalOfferts.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LocalOfferts.Service
{
    public interface IProductService
    {
        Task<bool> CreateProduct(Product product,string userName);
        Task<IEnumerable<Product>> GetProductsByName(string userName);
        Task<IEnumerable<Product>> GetProductsByCity(string city);
        Task<IEnumerable<Product>> GetProducts();
        Task<bool> DeleteProduct(int productId);
        Task<Product> SingleProduct(int idproductId);

        Task<bool> EditProduct(int productId, Product product);
    }
}