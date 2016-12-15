using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data;

namespace ServerDAL
{
    /// <summary>
    /// SQL Server数据库帮助类
    /// </summary>
    public class SqlHelper
    {
        #region 字段 && 属性

        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["QQConnectionString"].ConnectionString;

        #endregion

        #region 方法

        public static DataTable ExecuteDataTable(string sql)
        {
            DataTable dt = new  DataTable();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    cmd.ExecuteScalar();
                }
            }
            return dt;
        }

        
        public static List<T> ExecuteList<T>(string sql)
        {
            List<T> lists = new List<T>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            T obj = ExecuteReader<T>(dr);
                            if (obj != null)
                            {
                                lists.Add(obj);
                            }
                        }
                    }
                }
            }
            return lists;
        }

        /// <summary>
        /// 根据查询出来的数据创建相应的实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">查询的SQL语句</param>
        /// <returns>实体类的对象</returns>
        public static T ExecuteEntity<T>(string sql)
        {
            T obj = default(T);
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            obj = ExecuteReader<T>(dr);
                            break;
                        }
                    }
                }
            }
            return obj;
        }

        private static T ExecuteReader<T>(SqlDataReader dr)
        {
            T obj = default(T);
            Type type = typeof(T);
            obj = Activator.CreateInstance<T>();
            PropertyInfo[] propertyInfos = type.GetProperties();
            int columnCount = dr.FieldCount;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    string columnName = dr.GetName(i);
                    string propertyName = propertyInfo.Name;
                    if (string.Compare(columnName, propertyName, true) == 0)
                    {
                        object value = dr[i];
                        if (value != null
                            && value != DBNull.Value)
                        {
                            if (propertyName == "FaceImage")
                               propertyInfo.SetValue(obj, (byte[])value, null);
                            else
                                propertyInfo.SetValue(obj, value, null);
                            break;
                        }
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 执行不查询的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>受引影的行数</returns>
        public static int ExecuteNonQuery(string sql)
        {
            int result = -1;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static int ExecuteNonQuery(string sql, byte[] faceimg)
        {
            int result = -1;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@FaceImg", SqlDbType.Image).Value = faceimg;
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        //public static int ExecuteNonQuery(string sql,byte[] faceimg)
        //{
        //    int result = -1;
        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        if (con.State != System.Data.ConnectionState.Open)
        //        {
        //            con.Open();
        //        }
        //        using (SqlCommand cmd = new SqlCommand(sql, con))
        //        {
        //            cmd.Parameters.Add("@FImage", SqlDbType.Image).Value = faceimg;
        //            result = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    return result;
        //}
        /// <summary>
        /// 执行返回一行一列的操作（聚合函数的操作）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>查询返回的一个值</returns>
        public static object ExecuteScalar(string sql)
        {
            object result = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }
   
        #endregion
    }
}
