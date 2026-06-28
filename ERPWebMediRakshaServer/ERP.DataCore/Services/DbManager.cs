using ERP.DataCore.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataCore.Services
{
    public class DbManager : IDbManager
    {
        private readonly string _connectionString;
        public DbManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<T>> GetListAsync<T>(string procedureName, SqlParameter[] parameters) where T : new()
        {
            List<T> list = new();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(procedureName, connection);

            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                T item = new();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        object value = reader[property.Name];
                        Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(value, targetType));
                    }
                }
                list.Add(item);
            }
            return list;
        }
        public async Task<int> ExecuteAsync(string procedureName, SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
        public async Task<(THeader, List<TItem>)> GetMultipleAsync<THeader, TItem>(string procedureName, SqlParameter[] parameters) where THeader : new() where TItem : new()
        {
            THeader header = new(); List<TItem> items = new();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            // Header
            if (await reader.ReadAsync())
            {
                foreach (var property in typeof(THeader).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        object value = reader[property.Name];
                        Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(header, Convert.ChangeType(value, targetType));
                    }
                }
            }
            await reader.NextResultAsync();
            while (await reader.ReadAsync())
            {
                TItem item = new();
                foreach (var property in typeof(TItem).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        object value = reader[property.Name];
                        Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(value, targetType));
                    }
                }
                items.Add(item);
            }
            return (header, items);
        }
        public async Task<List<T>> GetSafeListAsync<T>(string procedureName, SqlParameter[] parameters) where T : new()
        {
            List<T> list = new();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            HashSet<string> columns = new();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }
            while (await reader.ReadAsync())
            {
                T item = new();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (!columns.Contains(property.Name))
                    {
                        continue;
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        object value = reader[property.Name];
                        Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(value, targetType));
                    }
                }
                list.Add(item);
            }

            return list;
        }
    }


}

