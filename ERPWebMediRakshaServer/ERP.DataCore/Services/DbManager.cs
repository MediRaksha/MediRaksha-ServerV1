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
        public async Task<List<T>> GetListAsync<T>(
            string procedureName,
            SqlParameter[] parameters)
            where T : new()
        {
            List<T> list = new();

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(procedureName, connection);

            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();

            SqlDataReader reader =
                await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                T item = new();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(
                        reader.GetOrdinal(property.Name)))
                    {
                        property.SetValue(
                            item,
                            reader[property.Name]);
                    }
                }

                list.Add(item);
            }

            return list;
        }

        public async Task<int> ExecuteAsync(
            string procedureName,
            SqlParameter[] parameters)
        {
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(procedureName, connection);

            command.CommandType =
                CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }
    }


}

