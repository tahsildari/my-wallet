using Dapper;
using Dapper.Contrib.Extensions;
using MicroOrm.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MyWallet.Service
{
    public class DapperService<TEntity> : IDapperService<TEntity> where TEntity : class
    {
        public IConfiguration _configuration { get; }

        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = GetConnection();
        }

        private SqlConnection connection;

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("MainConnectionString"));
        }

        public virtual long Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Insert<TEntity>(entity, transaction, commandTimeout);

            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public async virtual Task<long> InsertAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return await connection.InsertAsync<TEntity>(entity, transaction, commandTimeout);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw error;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public virtual bool Update(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                bool result = false;
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (predicate == null)
                    result = connection.Update<TEntity>(entity, transaction, commandTimeout);
                else
                    result = new DapperRepository<TEntity>(connection).Update(predicate, entity, transaction);

                return result;
            }
            catch (Exception ex)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }
        
        public virtual async Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (predicate == null)
                    return await connection.UpdateAsync<TEntity>(entity, transaction, commandTimeout);
                else
                    return await new DapperRepository<TEntity>(connection).UpdateAsync(predicate, entity, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public virtual bool Delete(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (predicate == null)
                    return connection.Delete<TEntity>(entity, transaction, commandTimeout);
                else
                    return new DapperRepository<TEntity>(connection).Delete(predicate, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (predicate == null)
                    return await connection.DeleteAsync<TEntity>(entity, transaction, commandTimeout);
                else
                    return await new DapperRepository<TEntity>(connection).DeleteAsync(predicate, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            try
            {

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return new DapperRepository<TEntity>(connection).Find(predicate, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            try
            {
                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return await new DapperRepository<TEntity>(connection).FindAsync(predicate, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }
        
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            try
            {
                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return new DapperRepository<TEntity>(connection).FindAll(predicate, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }
        
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            try
            {
                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return await new DapperRepository<TEntity>(connection).FindAllAsync(predicate, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }
        
        public TEntity FindById(object id, IDbTransaction transaction = null)
        {
            try
            {
                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return new DapperRepository<TEntity>(connection).FindById(id, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }
        
        public async Task<TEntity> FindByIdAsync(object id, IDbTransaction transaction = null)
        {
            try
            {
                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return await new DapperRepository<TEntity>(connection).FindByIdAsync(id, transaction);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }

        public int Execute(string sql, CommandType commandType, object parameters = null, IDbTransaction transaction = null, SqlConnection sqlConnection = null, int? commandTimeout = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;


                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;


                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Execute(sql, parameters, transaction, commandTimeout, commandType: commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public async Task<int> ExecuteAsync(string sql, CommandType commandType, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout, commandType: commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }
        
        public IEnumerable<TModel> Query<TModel>(string sql, object parameters = null, CommandType commandType = CommandType.Text, IDbTransaction transaction = null, bool buffer = false, int? commandTimeout = null, SqlConnection sqlConnection = null) where TModel : class
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<TModel>(sql,parameters, transaction, buffer, commandTimeout, commandType);
                

            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection); throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public async Task<IEnumerable<TModel>> QueryAsync<TModel>(string sql, object parameters=null, CommandType? commandType = null, IDbTransaction transaction = null, bool buffer = false, int? commandTimeout = null, SqlConnection sqlConnection = null) where TModel : class
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();


                return await connection.QueryAsync<TModel>(sql, parameters, transaction,commandTimeout, commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public IEnumerable<TEntity> Query<TFirst, TSecond>(string sql, Func<TFirst, TSecond, TEntity> map, object parameters, CommandType commandType, string splitOn, IDbTransaction transaction = null, bool buffer = false, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<TFirst, TSecond, TEntity>(
                sql,
                map,
                parameters,
                transaction,
                buffer,
                splitOn,
                null,
                commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }

        }
        
        public IEnumerable<TEntity> Query<TFirst, TSecond, T1>(string sql, Func<TFirst, TSecond, T1, TEntity> map, object parameters, CommandType commandType, string splitOn, IDbTransaction transaction = null, bool buffer = false, SqlConnection sqlConnection = null)// where TSecond: class
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<TFirst, TSecond, T1, TEntity>(
                 sql,
                 map,
                 parameters,
                 transaction,
                 buffer,
                 splitOn,
                 null,
                 commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public IEnumerable<TEntity> Query<TFirst, TSecond, T1, T2>(string sql, Func<TFirst, TSecond, T1, T2, TEntity> map, object parameters, CommandType commandType, string splitOn, IDbTransaction transaction = null, bool buffer = false, SqlConnection sqlConnection = null)// where TSecond: class
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;


                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<TFirst, TSecond, T1, T2, TEntity>(
                sql,
                map,
                parameters,
                transaction,
                buffer,
                splitOn,
                null,
                commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public IEnumerable<TEntity> Query<TFirst, TSecond, T1, T2, T3>(string sql, Func<TFirst, TSecond, T1, T2, T3, TEntity> map, object parameters, CommandType commandType, string splitOn, IDbTransaction transaction = null, bool buffer = false, SqlConnection sqlConnection = null)// where TSecond: class
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;


                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<TFirst, TSecond, T1, T2, T3, TEntity>(
                sql,
                map,
                parameters,
                transaction,
                buffer,
                splitOn,
                null,
                commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public IEnumerable<TEntity> Query<TFirst, TSecond, T1, T2, T3, T4>(string sql, Func<TFirst, TSecond, T1, T2, T3, T4, TEntity> map, object parameters, CommandType commandType, string splitOn, IDbTransaction transaction = null, bool buffer = false, SqlConnection sqlConnection = null)// where TSecond: class
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;


                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<TFirst, TSecond, T1, T2, T3, T4, TEntity>(
                sql,
                map,
                parameters,
                transaction,
                buffer,
                splitOn,
                null,
                commandType);
            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection);
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }

        public object ExecuteScalar(string sql, object parameters = null, CommandType commandType = CommandType.Text, IDbTransaction transaction = null, int? commandTimeout = null, SqlConnection sqlConnection = null)
        {
            try
            {
                if (sqlConnection != null)
                    connection = sqlConnection;

                if (transaction != null)
                    if (transaction.Connection != null)
                        connection = (SqlConnection)transaction.Connection;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.ExecuteScalar(sql, parameters, transaction, commandTimeout, commandType);

            }
            catch (Exception error)
            {
                connection.Close();
                SqlConnection.ClearPool(connection); throw;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP
        /// </summary>
        /// <typeparam name="T">type of enumerbale</typeparam>
        /// <param name="enumerable">list of values</param>
        /// <param name="typeName">For example the name of the created TVP in SQL </param>
        /// <param name="orderedColumnNames">if more than one column in a TVP, 
        /// columns order must mtach order of columns in TVP</param>
        /// <returns>a custom query parameter</returns>
        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>
            (this IEnumerable<T> enumerable,
            string typeName, IEnumerable<string> orderedColumnNames = null)
        {
            var dataTable = new DataTable();
            if (typeof(T).IsValueType || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add(orderedColumnNames == null ?
                    "NONAME" : orderedColumnNames.First(), typeof(T));

                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(obj);
                }
            }
            else
            {
                PropertyInfo[] properties = typeof(T).GetProperties
                    (BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] readableProperties = properties.Where
                    (w => w.CanRead).ToArray();
                if (readableProperties.Length > 1 && orderedColumnNames == null)
                    throw new ArgumentException("Ordered list of column names must be provided when TVP contains more than one column");

                var columnNames = (orderedColumnNames ??
                    readableProperties.Select(s => s.Name)).ToArray();
                try
                {
                    foreach (string name in columnNames)
                    {
                        dataTable.Columns.Add(name, Nullable.GetUnderlyingType(readableProperties.Single(s => s.Name.Equals(name))?.PropertyType) ?? readableProperties.Single(s => s.Name.Equals(name))?.PropertyType);
                    }
                }
                catch 
                {
                    throw new Exception("Field names do not match!");
                }

                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(
                        columnNames.Select(s => readableProperties.Single
                            (s2 => s2.Name.Equals(s)).GetValue(obj))
                            .ToArray());
                }
            }
            return dataTable.AsTableValuedParameter(typeName);
        }
    }
}