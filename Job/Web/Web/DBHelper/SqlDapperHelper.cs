using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Web.Common;
using Microsoft.Extensions.Configuration;
using Web.Common.LogHleper;

namespace Web.DBHelper
{
    public class SqlDapperHelper
    {
        static string write = AppConfigurtaionServices.connectionStrings.DapperWrite;
        static string reade = AppConfigurtaionServices.connectionStrings.DapperRead;
        static int commontouttime = 100;
        public static IDbConnection GetConnection(bool useWrite)
        {
            if (useWrite)
            {
                return new SqlConnection(write);
            } 
            return new SqlConnection(reade);
        }
        public static IDbConnection GetOpenConnection()
        {

            IDbConnection conn = new SqlConnection(write);
            conn.Open();
            return conn;
        }
        /// <summary>
        /// 返回一个对应对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWrite"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static T ReturnT<T>(string sql, object param = null, bool useWrite = false, IDbTransaction tran = null)
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(useWrite))
                {
                   
                    conn.Open(); 
                   return conn.QueryFirstOrDefault<T>(sql, param, tran, commontouttime); 
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return conn.QueryFirstOrDefault<T>(sql, param, tran, commontouttime);
            }
        }
        /// <summary>
        /// 返回一个对象异步操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWrite"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static async Task<T> ReturnTAsync<T>(string sql, object param = null, bool useWrite = false, IDbTransaction tran = null) 
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(useWrite))
                {
                    conn.Open();
                    return await conn.QueryFirstOrDefaultAsync<T>(sql, param, tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param, tran, commontouttime);
            }
        }

        /// <summary>
        /// 返回多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWrite"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static List<T> ReturnListT<T>(string sql, object param = null, bool useWrite = false, IDbTransaction tran = null) 
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(useWrite))
                {
                    conn.Open();
                    return conn.Query<T>(sql, param, tran, true, commontouttime).ToList();
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return conn.Query<T>(sql, param, tran, true, commontouttime).ToList();
            }
        }

        /// <summary>
        /// 返回多个对象异步操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWrite"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static async Task<List<T>> ReturnListTAsync<T>(string sql, object param = null, bool useWrite = false, IDbTransaction tran = null) 
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(useWrite))
                {
                    conn.Open();
                    var list = await conn.QueryAsync<T>(sql, param, tran, commontouttime).ConfigureAwait(false);
                    return list.ToList();
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                var list = await conn.QueryAsync<T>(sql, param, tran, commontouttime).ConfigureAwait(false);
                return list.ToList();
            }
        }

        /// <summary>
        /// 执行sql，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int ReturnInt(string sql, object param = null, IDbTransaction tran = null)
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return conn.Execute(sql, param, tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return conn.Execute(sql, param, tran, commontouttime);
            }
        }

        /// <summary>
        /// 执行sql，返回受影响行数异步操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static async Task<int> ReturnIntAsync(string sql, object param = null, IDbTransaction tran = null)
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return await conn.ExecuteAsync(sql, param, tran);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return await conn.ExecuteAsync(sql, param, tran);
            }
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static long Insert<T>(T item, IDbTransaction tran = null) where T : class
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return conn.Insert<T>(item, tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return conn.Insert<T>(item, tran, commontouttime);
            }
        }

        /// <summary>
        ///  插入实体异步操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static async Task<long> InsertAsync<T>(T item, IDbTransaction tran) where T : class
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                    return await conn.InsertAsync<T>(item, tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return await conn.InsertAsync<T>(item, tran, commontouttime);
            }
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static T GetById<T>(int id,IDbTransaction tran=null) where T:class
        {
            if (tran==null)
            {
                using (IDbConnection conn=GetConnection(false))
                {
                    conn.Open();
                    return conn.Get<T>(id,tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                return conn.Get<T>(id, tran, commontouttime);
            }
        }

        /// <summary>
        /// 新增实体 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static void InsertList<T>(IEnumerable<T> items, IDbTransaction tran = null) where T : class
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                     conn.Insert(items, tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                 conn.Insert(items, tran, commontouttime);
            }
        }

        /// <summary>
        ///  插入实体集合异步操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static async Task InsertListAsync<T>(IEnumerable<T> items, IDbTransaction tran) where T : class
        {
            if (tran == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    conn.Open();
                     await conn.InsertAsync(items, tran, commontouttime);
                }
            }
            else
            {
                IDbConnection conn = GetOpenConnection();
                 await conn.InsertAsync(items, tran, commontouttime);
            }
        }

    }
}
