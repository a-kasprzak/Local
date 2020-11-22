using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using LocalOfferts.Data;

namespace LocalOfferts.Service
{
    public class ProductService : IProductService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public ProductService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateProduct(Product product, string userName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", product.ProductName, DbType.String);
            parameters.Add("ProductDescription", product.ProductDescription, DbType.String);
            parameters.Add("ProductPrice", product.ProductPrice, DbType.Decimal);
            parameters.Add("ShopeName", product.ShopeName, DbType.String);
            parameters.Add("CreationDate", product.CreationDate, DbType.DateTime);
            parameters.Add("UserName", product.UserName, DbType.String);

            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                try
                {
                    string query = $"INSERT INTO PRODUCTS(ProductName,ProductDescription,ProductPrice,ShopeName,CreationDate,UserName) VALUES (@ProductName,@ProductDescription,@ProductPrice,@ShopeName,getdate(),'{userName}')";
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

        public async Task<IEnumerable<Product>> GetProductsByName(string userName)
        {
            IEnumerable<Product> products;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"SELECT * FROM Products WHERE UserName = '{userName}'";

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

        public async Task<bool> DeleteProduct(int ProductId)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = $"DELETE from dbo.Products where ProductId={ProductId}";
                if (conn.State == ConnectionState.Closed) conn.Open();

                try
                {
                    await conn.ExecuteAsync(query, new { ProductId }, commandType: CommandType.Text);
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

    }
}
