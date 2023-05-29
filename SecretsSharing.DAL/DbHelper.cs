using Dapper;
using Npgsql;

namespace SecretsSharing.DAL;

public static class DbHelper
{
    public static string ConnectionString { get; set; } =
        "Server=postgres_db;Port=5432;User id=postgres;password=password;database=secret-sharing-db";

    public static async Task ExecuteAsync(string sql, object model)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, model);
        }
    }

    public static async Task<T> QueryScalarAsync<T>(string sql, object model)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
        }
    }

    public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(sql, model);
        }
    }
}