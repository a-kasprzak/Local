using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using LocalOfferts.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LocalOfferts.Service
{
    public class ProductService : IProductService
    {
        private readonly SqlConnectionConfiguration _configuration;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductService(SqlConnectionConfiguration configuration, AuthenticationStateProvider authenticationStateProvider, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
        }


        public async Task<bool> CreateProduct(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", product.ProductName, DbType.String);
            parameters.Add("ProductDescription", product.ProductDescription, DbType.String);
            parameters.Add("ProductPrice", product.ProductPrice, DbType.Double);
            parameters.Add("ShopeName", product.ShopeName, DbType.String);
            parameters.Add("CreationDate", product.CreationDate, DbType.DateTime);
            parameters.Add("UserName", product.UserName, DbType.String);
            parameters.Add("Image", product.Image, DbType.Byte);
            parameters.Add("ProductType", product.ProductType, DbType.String);
            parameters.Add("City", product.City, DbType.String);

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User;
            var currentuser = await _userManager.GetUserAsync(userName);
            string name = currentuser.Email;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                try
                {
                    string query = $"INSERT INTO PRODUCTS(ProductName,ProductDescription,ProductPrice,ShopeName,CreationDate,UserName,Image,ProductType,City) VALUES (@ProductName,@ProductDescription,@ProductPrice,@ShopeName,getdate(),'{name}',@Image,@ProductType,@City)";
                    await conn.ExecuteAsync(query, product);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }

            }
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsByName()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User;
            var currentuser = await _userManager.GetUserAsync(userName);
            string name = currentuser.Email;
        
            IEnumerable<Product> products;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"SELECT * FROM Products WHERE UserName = '{name}'";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                { 
                    products = await conn.QueryAsync<Product>(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByCity(string city)
        {
            IEnumerable<Product> products;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"SELECT * FROM Products WHERE City = '{city}' AND CreationDate >= getdate() - 14";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    products = await conn.QueryAsync<Product>(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"SELECT * FROM Products";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    products = await conn.QueryAsync<Product>(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return products;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"DELETE from dbo.Products where ProductId={productId}";
                if (conn.State == ConnectionState.Closed) conn.Open();

                try
                {
                    await conn.ExecuteAsync(query, new { productId }, commandType: CommandType.Text);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();

                }
            }
            return true;
        }

        public async Task<Product> SingleProduct(int id)
        {
            Product product = new Product();

            using (var conn = new SqlConnection(_configuration.Value)) 
            {
                string query = $"SELECT * from dbo.Products WHERE ProductId={id}";

                if (conn.State == ConnectionState.Open) conn.Close();

                try
                {
                    product = await conn.QueryFirstOrDefaultAsync<Product>(query, new { id }, commandType: CommandType.Text);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }    
 
            }

            return product;
        }

        public async Task<bool> EditProduct(int productId, Product product)
        {
            
            using (var conn = new SqlConnection(_configuration.Value))
            {

                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = $"UPDATE dbo.Products set ProductName = @ProductName, ProductPrice = @ProductPrice , ProductDescription = @ProductDescription, ShopeName = @ShopeName WHERE ProductId = {productId}";
                try
                {
                    await conn.ExecuteAsync(query, new { product.ProductName, product.ProductPrice, product.ProductDescription, product.ShopeName, productId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return true;


        }

        public async Task<bool> EditProductAdmin(int productId, Product product)
        {

            using (var conn = new SqlConnection(_configuration.Value))
            {

                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = $"UPDATE dbo.Products set ProductName = @ProductName, ProductPrice = @ProductPrice , ProductDescription = @ProductDescription, ShopeName = @ShopeName, CreationDate = @CreationDate WHERE ProductId = {productId}";
                try
                {
                    await conn.ExecuteAsync(query, new { product.ProductName, product.ProductPrice, product.ProductDescription, product.ShopeName, product.CreationDate, productId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return true;
        }
        public async Task<bool> RefreshProduct(int productId)
        {

            using (var conn = new SqlConnection(_configuration.Value))
            {

                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = $"UPDATE dbo.Products set CreationDate = getdate()  WHERE ProductId = {productId}";
                try
                {
                    await conn.ExecuteAsync(query, new {query, productId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return true;


        }

        public async Task<IEnumerable<Product>> GetProductsFromTwoWeeks()
        {
            IEnumerable<Product> products;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"SELECT * FROM Products WHERE CreationDate >= getdate() - 14";

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    products = await conn.QueryAsync<Product>(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return products;
        }

    }
}
