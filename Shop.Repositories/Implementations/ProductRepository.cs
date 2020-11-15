using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Implementations
{
    class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> CreateAsync(ProductModel productModel)
        {
            _context.Add(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task DeleteAsync(int? id)
        {
            var productModel = await _context.Product.FindAsync(id);
            _context.Product.Remove(productModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ProductModel> GetAsync(int? id)
        {
            var productModel = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            return productModel;
        }

        public bool ProductExists(int? id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public async Task<ProductModel> UpdateAsync(ProductModel productModel)
        {
            _context.Update(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<IList<ProductModel>> SearchProducts(string searchString)
        {
            //todo: handle searchString == null case
	        return await _context.Product.Where(s => s.Name.Contains(searchString)).ToListAsync();
        }

        public async Task<List<ProductModel>> SearchedProducts(string searchString)
        {
            List<ProductModel> foundProducts = new List<ProductModel>();
            string queryString = $"SELECT * FROM Product" +
                $" WHERE Name LIKE '%@search%'";
            using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                var parameter = new SqlParameter("search", SqlDbType.NVarChar)
					                {
						                Value = searchString
					                };

                command.Parameters.Add(parameter);

                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        ProductModel product = new ProductModel();
                        product.Id = (int)reader.GetValue(0);
                        product.Name = (string)reader.GetValue(1);
                        product.Price = (decimal)reader.GetValue(2);
                        product.Count = (int)reader.GetValue(4);
                        product.SKU = (string)reader.GetValue(3);
                        foundProducts.Add(product);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return foundProducts;
        }

        public async Task<IEnumerable<ProductModel>> GetAllWithCategories()
        {
            return await _context.Product.Include(x => x.Category).ToListAsync();
        }
    }
}