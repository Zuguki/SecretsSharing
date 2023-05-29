using Dapper;
using Npgsql;

namespace SecretsSharing.DAL;

public static class DbHelper
{
    public static string ConnectionString { get; set; } =
        "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=postgres";

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