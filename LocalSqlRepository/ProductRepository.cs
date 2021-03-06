﻿using Contracts.Models;
using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LocalSqlRepository
{
    /// <summary>
    /// retrive/store products from database (CRUD methods)
    /// </summary>
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository() { }

        /// <summary>
        /// get all products
        /// </summary>
        /// <returns>list of all products</returns>
        public async Task<List<IProduct>> GetAllAsync()
        {
            var result = new List<IProduct>();

            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Id,Name,Description,Price,DeliveryPrice FROM product", conn);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync()) result.Add(new ProductModel(reader));

                reader.Close();
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// get products by name
        /// </summary>
        /// <param name="name">product name</param>
        /// <returns>list of matching products</returns>
        public async Task<List<IProduct>> GetByNameAsync(string name)
        {
            var result = new List<IProduct>();

            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Id,Name,Description,Price,DeliveryPrice FROM product " +
                    "WHERE name LIKE '%'+@name+'%'", conn);
                cmd.Parameters.AddWithValue("@name", name);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync()) result.Add(new ProductModel(reader));

                reader.Close();
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// get product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product</returns>
        public async Task<IProduct> GetByIdAsync(Guid id)
        {
            IProduct result = null;

            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Id,Name,Description,Price,DeliveryPrice FROM product " +
                    "WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync()) result = new ProductModel(reader);

                reader.Close();
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// create product
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        public async Task CreateAsync(IProduct product)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("INSERT INTO product (Id,Name,Description,Price,DeliveryPrice) " +
                    "VALUES (@id,@name,@description,@price,@deliveryPrice)", conn);
                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                if (string.IsNullOrWhiteSpace(product.Description)) cmd.Parameters.AddWithValue("@description", DBNull.Value);
                else cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@deliveryPrice", product.DeliveryPrice);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();
            }
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        public async Task UpdateAsync(IProduct product)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("UPDATE product SET Name=@name,Description=@description,Price=@price,DeliveryPrice=@deliveryPrice " +
                    "WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                if (string.IsNullOrWhiteSpace(product.Description)) cmd.Parameters.AddWithValue("@description", DBNull.Value);
                else cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@deliveryPrice", product.DeliveryPrice);

                await cmd.ExecuteNonQueryAsync();

                conn.Close();
            }
        }

        /// <summary>
        /// delete product including product options
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
                var tran = conn.BeginTransaction();

                try
                {
                    var cmd = new SqlCommand("DELETE FROM productoption WHERE productid=@id", conn, tran);
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();

                    cmd.CommandText = "DELETE FROM product WHERE Id=@id";
                    await cmd.ExecuteNonQueryAsync();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}