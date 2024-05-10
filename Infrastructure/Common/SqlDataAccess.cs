using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infrastructure.Common;

public class SqlDataAccess(MySqlConnection connection) : ISqlDataAccess, IDisposable
{

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return (await connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.QuerySingleAsync<T>(sql, param, transaction);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.ExecuteAsync(sql, param, transaction: transaction);
    }

    public IDbConnection GetDbConnection()
    {
        return connection;
    }

    public void Dispose()
    {
        connection.Dispose();
    }
}
