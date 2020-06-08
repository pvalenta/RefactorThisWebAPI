using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;
using Entities.Models;

namespace LocalSqlRepository
{
    /// <summary>
    /// retrive/store product options from database (CRUD methods)
    /// </summary>
    public class ProductOptionRepository : BaseRepository, IProductOptionRepository
    {
        /// <summary>
        /// get all product options
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns>list of all product options</returns>
        public async Task<List<IProductOption>> GetByProductIdAsync(Guid productId)
        {
            var result = new List<IProductOption>();

            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Id,ProductId,Name,Description FROM productoption WHERE ProductId=@productId", conn);
                cmd.Parameters.AddWithValue("@productId", productId);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync()) result.Add(new ProductOptionModel(reader));

                reader.Close();
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// get product option
        /// </summary>
        /// <param name="optionId">option id</param>
        /// <returns>product option</returns>
        public async Task<IProductOption> GetByIdAsync(Guid id)
        {
            IProductOption result = null;

            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Id,ProductId,Name,Description FROM productoption WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync()) result = new ProductOptionModel(reader);

                reader.Close();
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// create product option
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="option">option</param>
        /// <returns></returns>
        public async Task CreateAsync(Guid productId, IProductOption option)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("INSERT INTO productoption (Id,ProductId,Name,Description) " +
                    "VALUES (@id,@productId,@name,@description)", conn);
                cmd.Parameters.AddWithValue("@id", option.Id);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@name", option.Name);
                if (string.IsNullOrWhiteSpace(option.Description)) cmd.Parameters.AddWithValue("@description", DBNull.Value);
                else cmd.Parameters.AddWithValue("@description", option.Description);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();
            }
        }

        /// <summary>
        /// update product option
        /// </summary>
        /// <param name="option">option</param>
        /// <returns></returns>
        public async Task UpdateAsync(IProductOption option)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("UPDATE productoption SET Name=@name,Description=@description " +
                    "WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", option.Id);
                cmd.Parameters.AddWithValue("@name", option.Name);
                if (string.IsNullOrWhiteSpace(option.Description)) cmd.Parameters.AddWithValue("@description", DBNull.Value);
                else cmd.Parameters.AddWithValue("@description", option.Description);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();
            }
        }

        /// <summary>
        /// delete product option
        /// </summary>
        /// <param name="id">option id</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("DELETE FROM productoption WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.ExecuteNonQueryAsync();

                conn.Close();
            }
        }
    }
}