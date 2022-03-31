using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyWallet.Service
{
    public interface IDapperService<TEntity> where TEntity : class
    {
        SqlConnection GetConnection();
        long Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        Task<long> InsertAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        bool Update(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        bool Delete(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        Task<bool> DeleteAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        TEntity Find(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);
        TEntity FindById(object id, IDbTransaction transaction = null);
        Task<TEntity> FindByIdAsync(object id, IDbTransaction transaction = null);
        int Execute(string sql, CommandType commandType, object parameters = null, IDbTransaction transaction = null, SqlConnection sqlConnection = null, int? commandTimeout = null);
        Task<int> ExecuteAsync(string sql, CommandType commandType, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
        IEnumerable<TModel> Query<TModel>(string sql, object parameters = null, CommandType commandType=CommandType.Text, IDbTransaction transaction = null, bool buffer = false, int? commandTimeout = null, SqlConnection sqlConnection = null) where TModel : class;
        Task<IEnumerable<TModel>> QueryAsync<TModel>(string sql, object parameters=null, CommandType? commandType = null, IDbTransaction transaction = null, bool buffer = false, int? commandTimeout = null, SqlConnection sqlConnection = null) where TModel : class;
        object ExecuteScalar(string sql, object parameters = null, CommandType commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null);
    }
}