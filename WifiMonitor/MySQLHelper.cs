using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace WifiMonitor
{
    [Serializable]
    //MySQL连接类
    static class MySQLHelper
    {
        private static string connectionString = "Database='projectmanager';Data Source='localhost';User Id='root';Password='12345';charset='utf8';pooling=true";

        private static Hashtable paraCache = Hashtable.Synchronized(new Hashtable());//用于缓存参数

        /// <summary>
        /// 给定连接参数用假设参数执行一条SQL指令
        /// </summary>
        /// <param name="cmdType">命令类型（存储过程、文本等）</param>
        /// <param name="cmdText">存储过程名称或SQL语句</param> 
        /// <param name="cmdParameters">执行命令的参数集合</param>
        /// <returns>执行命令影响的行数</returns>
        public static int ExecuteNonQuerty(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 现有数据库连接执行一个SQL命令
        /// </summary>
        /// <param name="connection">现有的数据库连接</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名称或SQL语句</param>
        /// <param name="cmdParameters">执行命令的参数集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuerty(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 使用现有SQL事务执行一个SQL命令（不返回数据集）
        /// </summary>
        /// <param name="transcation">一个现有的事务</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名称或SQL语句</param>
        /// <param name="cmdParameters">执行命令的参数集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuerty(MySqlTransaction transcation, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, transcation.Connection, transcation, cmdType, cmdText, cmdParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 已连接数据库执行一个返回数据集的SQL指令
        /// </summary>
        /// <remarks>
        /// MySqlReader r = ExecuteReader(connectionString, commandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名称或SQL语句</param>
        /// <param name="cmdParameters">执行命令参数集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameters);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return reader;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名称或SQL语句</param>
        /// <param name="cmdParameter">执行命令参数集合</param>
        /// <returns>结果数据集</returns>
        public static DataSet GetDataSet(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParameter)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameter);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.Parameters.Clear();
                connection.Close();
                return ds;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }


        /// <summary>
        /// 用指定的连接字符串执行命令并返回结果数据集第一列
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParameter"></param>
        /// <returns>第一列数据，使用Convert转化</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParameter)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameter);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 更新数据库为当前表格
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="dt"></param>
        /// <param name="cmdParameter"></param>
        public static void UpdateData(CommandType cmdType, string cmdText, DataTable dt, params MySqlParameter[] cmdParameter)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameter);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
                adapter.UpdateCommand = cb.GetUpdateCommand();
                adapter.Update(dt);
            }
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">Sql命令</param>
        /// <param name="connection">OleDb连接</param>
        /// <param name="transcation">oledb事物</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本（SQL语句）</param>
        /// <param name="parameters">命令参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection connection, MySqlTransaction transcation, CommandType cmdType,
            string cmdText, MySqlParameter[] parameters)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            cmd.Connection = connection;
            cmd.CommandText = cmdText;

            if (transcation != null)
                cmd.Transaction = transcation;

            cmd.CommandType = cmdType;
        }

        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="commandParameters">添加到缓存的sql变量集</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            paraCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 获得缓存中的参数集合
        /// </summary>
        /// <param name="cachekey">参数关键字</param>
        /// <returns>缓存参数集合</returns>
        public static MySqlParameter[] GetCachedParameter(string cachekey)
        {
            MySqlParameter[] cachedParameters = (MySqlParameter[])paraCache[cachekey];
            if (cachedParameters == null)
            {
                return null;
            }

            MySqlParameter[] cloneParameter = new MySqlParameter[cachedParameters.Length];

            for (int i = 0; i < cachedParameters.Length; i++)
            {
                cloneParameter[i] = (MySqlParameter)((ICloneable)cachedParameters[i]).Clone();
            }
            return cloneParameter;
        }

    }
}
