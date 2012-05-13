using System;
using System.Data;
using System.Data.SqlClient;	// 封装SQL Sevrer 的访问方法的命名空间
using SagaLib;

namespace SagaDB
{
    public class MSSQLOperator
    {
        /// <summary>
		/// 数据库连接
		/// </summary>
		private SqlConnection _conn;
		/// <summary>
		/// 事务处理类
		/// </summary>
		private SqlTransaction _trans;
		/// <summary>
		/// 获取当前是否处于事务处理中，默认值false
		/// </summary>
		private bool isTransaction = false;
		
		public MSSQLOperator(string strConnection)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this._conn = new SqlConnection(strConnection);
		}

		/// <summary>
		/// 获取当前SQL Server连接
		/// </summary>
		public IDbConnection Connection
		{
			get
			{
				return this._conn;
			}
		}

        public ConnectionState State
        {
            get
            {
                if (this._conn == null)
                    return ConnectionState.Closed;
                return this._conn.State;
            }
        }


		/// <summary>
		/// 打开SQL Server连接
		/// </summary>
		public void Open()
		{
			if (_conn.State != ConnectionState.Open)
			{
				try
				{
					_conn.Open();                    
				}
				catch(Exception ex)
				{
                    Logger.ShowSQL(ex, null);
				}
			}
		}

		/// <summary>
		/// 关闭SQL Server连接
		/// </summary>
		public void Close()
		{
			if (_conn.State == ConnectionState.Open)
			{
				try
				{
					_conn.Close();
				}
				catch(Exception ex)
				{
                    Logger.ShowSQL(ex, null);
				}
			}
		}

		/// <summary>
		/// 开始一个SQL Server事务
		/// </summary>
		public void BeginTrans()
		{
			_trans = _conn.BeginTransaction();
			isTransaction = true;
		}

		/// <summary>
		/// 提交一个SQL Server事务
		/// </summary>
		public void CommitTrans()
		{
			_trans.Commit();
			isTransaction = false;
		}

		/// <summary>
		/// 回滚一个SQL Server事务
		/// </summary>
		public void RollbackTrans()
		{
			_trans.Rollback();
			isTransaction = false;
		}

		/// <summary>
		/// 执行一个SQL语句(UPDATE,INSERT)
		/// </summary>
		/// <param name="sql">SQL语句</param>
		public void ExeSql(string sql)
		{
			// 打开
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this._conn;
			if (isTransaction == true)
			{
				cmd.Transaction = this._trans;
			}
			cmd.CommandText = sql;
			try
			{
				cmd.ExecuteNonQuery();                
			}
			catch(Exception ex)
			{
                Logger.ShowSQL("Error on query:" + sql, null);
                Logger.ShowSQL(ex, null);
			}
            DatabaseWaitress.LeaveCriticalArea();
            if (criticalarea)
                ClientManager.EnterCriticalArea();
			// 释放
			//this.Close();
		}

		/// <summary>
		/// 执行一个SQL语句(INSERT)返回当前ID
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="a">临时变量</param>
		/// <returns>当前ID</returns>
		public int ExeSql(string sql, int a)
		{
			int identity = -1;
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            
			// 打开
			this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this._conn;
			if (isTransaction == true)
			{
				cmd.Transaction = this._trans;
			}
			cmd.CommandText = sql + " select @@identity as 'identity'";
			try
			{
				// 第一行第一列的值为当前ID
				SqlDataReader dr = cmd.ExecuteReader();
				
				if (dr.Read())
				{
					identity = int.Parse(dr[0].ToString());
				}

				dr.Close();
			}
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sql, null);
                Logger.ShowSQL(ex, null);
            }
            DatabaseWaitress.LeaveCriticalArea();
            if (criticalarea)
                ClientManager.EnterCriticalArea();
			// 释放
			//this.Close();

			return identity;
		}

		/// <summary>
		/// 执行SQL语句返回第一行第一列的值
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <returns>第一行第一列的值</returns>
		public string ExeSqlScalar(string sql)
		{
			DataTable dt = null;
			try
			{
				dt = this.GetDataTable( sql);
				if (dt.Rows.Count > 0)
				{
					string v_Value = dt.Rows[0][0].ToString();
					dt.Dispose();
					return v_Value;
				}
				else
				{
					return "";
				}
			}
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sql, null);
                Logger.ShowSQL(ex, null);
                return "";
            }
		}


		/// <summary>
		/// 执行SQL语句返回影响行数
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <returns>影响行数</returns>
		public int ExeSqlRows(string sql)
		{
			DataTable dt = null;
			try
			{
				dt = this.GetDataTable( sql);
				int v_RowsCount = dt.Rows.Count;
				dt.Dispose();
				return v_RowsCount;
			}
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sql, null);
                Logger.ShowSQL(ex, null);
                return -1;
            }
		}

		/// <summary>
		/// 获取DataSet
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <returns>DataSet</returns>
		public DataSet GetDataSet(string sql)
		{
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            // 打开
			this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this._conn;
			if (isTransaction == true)
			{
				cmd.Transaction = this._trans;
			}
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter();
			cmd.CommandText = sql;
			da.SelectCommand = cmd;
			try
			{
				da.Fill(ds);
			}
			catch(Exception ex)
			{
                Logger.ShowSQL("Error on query:" + sql, null);
                Logger.ShowSQL(ex, null);
			}			
			// 释放
			//this.Close();
            DatabaseWaitress.LeaveCriticalArea();
            if (criticalarea)
                ClientManager.EnterCriticalArea();
			return ds;
		}

		/// <summary>
		/// 获取DataTable
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <returns>DataTable</returns>
		public DataTable GetDataTable(string sql)
		{
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            // 打开
			this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this._conn;
			if (isTransaction == true)
			{
				cmd.Transaction = this._trans;
			}
			DataTable dt = new DataTable();
			SqlDataAdapter da = new SqlDataAdapter();
			cmd.CommandText = sql;
			da.SelectCommand = cmd;
			try
			{
				da.Fill(dt);
			}
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sql, null);
                Logger.ShowSQL(ex, null);
            }

			// 释放
			//this.Close();
            DatabaseWaitress.LeaveCriticalArea();
            if (criticalarea)
                ClientManager.EnterCriticalArea();
			return dt;
		}

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="p_ProcedureName">存储过程名</param>
		/// <param name="p_SqlParameterArray">存储过程参数</param>
		public void ExeProcedure(string p_ProcedureName, SqlParameter[] p_SqlParameterArray)
		{
			// 打开
			this.Open();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = p_ProcedureName;
			cmd.Connection = this._conn;
			cmd.CommandType = CommandType.StoredProcedure;
			foreach (SqlParameter Sq in p_SqlParameterArray)
			{
				cmd.Parameters.Add( Sq);
			}
			cmd.ExecuteNonQuery();
			// 释放
			//this.Close();
		}

		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="p_ProcedureName">存储过程名</param>
		/// <param name="p_SqlParameterArray">存储过程参数</param>
		/// <param name="p_TableIndex">表索引（多个表时）</param>
		/// <returns>DataSet</returns>
		public DataSet ExeProcedure(string p_ProcedureName, SqlParameter[] p_SqlParameterArray, int p_TableIndex)
		{
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(p_ProcedureName, this._conn);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			foreach(SqlParameter Sq in p_SqlParameterArray)
			{
				da.SelectCommand.Parameters.Add( Sq);
			}
			da.Fill(ds);
			return ds;
		}
    }
}
