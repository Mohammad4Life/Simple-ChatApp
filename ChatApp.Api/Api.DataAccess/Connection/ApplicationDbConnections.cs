using Api.DataAccess.Context;
using Api.DataAccess.DomainRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.DataAccess.Connection;

public class ApplicationReadDbConnections : IApplicationReadDbConnection, IDisposable
{
    private readonly IDbConnection connection;
    public ApplicationReadDbConnections(IConfiguration configuration)
    {
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public void Dispose()
    {
        connection.Dispose();
    }

    public async Task<List<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        return (await connection.QueryAsync<T>(sql, param, transaction, commandType: commandType)).AsList();
    }

    public async Task<GridReader> QueryMultipleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        return await connection.QueryMultipleAsync(sql, param, transaction, commandType: commandType);
    }

    public async Task<T> QueryScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        return await connection.ExecuteScalarAsync<T>(sql, param, transaction, commandType: commandType);
    }
}

public class ApplicationWriteDbConnection : IApplicationWriteDbConnection
{
    private readonly IApplicationDbContext context;
    public ApplicationWriteDbConnection(IApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await context.Connection.ExecuteAsync(sql, param, transaction);
    }
}
