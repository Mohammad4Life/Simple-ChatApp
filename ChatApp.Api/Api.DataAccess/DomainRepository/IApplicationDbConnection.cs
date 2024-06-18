using System.Data;
using static Dapper.SqlMapper;

namespace Api.DataAccess.DomainRepository;

public interface IApplicationReadDbConnection 
{
    Task<List<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
    Task<T> QueryScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
    Task<GridReader> QueryMultipleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
}

public interface IApplicationWriteDbConnection
{
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
}
