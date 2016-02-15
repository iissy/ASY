using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ASY.Iissy.Util.SqlHelpers
{
    public static class SqlHelpers
    {
        /// <summary>
        /// 返回单个值
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static object ExecuteScalar(string databaseName, string storedProcName, params object[] paramValues)
        {
            object obj = null;
            Database db = DBFactory.Create(databaseName);

            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                obj = db.ExecuteScalar(cmd);
            }

            return obj;
        }

        /// <summary>
        /// 返回单个结果集合，例如一个select结果集
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static XElement ExecuteReader(string databaseName, string storedProcName, params object[] paramValues)
        {
            XElement xmldoc;
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                cmd.Connection = db.CreateConnection();
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    int colNum = dr.FieldCount;
                    xmldoc = Convertor.DataReaderToXml(dr, colNum);
                }
            }

            return xmldoc;
        }

        /// <summary>
        /// 返回多个结果集合，例如多个select结果集
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static XElement ExecuteDataSet(string databaseName, string storedProcName, params object[] paramValues)
        {
            XElement xmldoc = new XElement("root");
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                cmd.Connection = db.CreateConnection();
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    do
                    {
                        int colNum = dr.FieldCount;
                        xmldoc.Add(Convertor.DataReaderToXml(dr, colNum));
                    } while (dr.NextResult());
                }

                return xmldoc;
            }
        }

        /// <summary>
        /// 返回一个datatable
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static DataTable ExecuteDateTable(string databaseName, string storedProcName, params object[] paramValues)
        {
            DataTable dt = new DataTable("TABLE");
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                cmd.Connection = db.CreateConnection();
                using (DbDataAdapter da = db.GetDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        /// <summary>
        /// 输入一个datatable，返回一个datatable
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="parameters">参数化传入</param>
        /// <returns></returns>
        public static DataTable ExecuteDateTable(string databaseName, string storedProcName, IEnumerable<IDataParameter> parameters)
        {
            DataTable dt = new DataTable("TABLE");
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName))
            {
                foreach (IDataParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.Connection = db.CreateConnection();
                using (DbDataAdapter da = db.GetDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string databaseName, string storedProcName, params object[] paramValues)
        {
            int result = -1;
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                result = db.ExecuteNonQuery(cmd);
            }

            return result;
        }

        /// <summary>
        /// 带输出参数的增删改
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="outValues">输出参数</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string databaseName, string storedProcName, ref ArrayList outValues, params object[] paramValues)
        {
            int result = -1;
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                result = db.ExecuteNonQuery(cmd);
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                        outValues.Add(param.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// 带DataTable输入参数的增删改
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static int ExecuteWithDataTableNonQuery(string databaseName, string storedProcName, IEnumerable<IDataParameter> parameters)
        {
            int result = -1;
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName))
            {
                foreach (IDataParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                result = db.ExecuteNonQuery(cmd);
            }

            return result;
        }

        /// <summary>
        /// 带DataTable输入参数的增删改，同时包含输出参数
        /// </summary>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="outValues">输出参数</param>
        /// <returns></returns>
        public static int ExecuteWithDataTableNonQuery(string databaseName, string storedProcName, IEnumerable<IDataParameter> parameters, ref ArrayList outValues)
        {
            int result = -1;
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName))
            {
                foreach (IDataParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                result = db.ExecuteNonQuery(cmd);
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                        outValues.Add(param.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// 带DataTable输入参数，同时包含输出参数，返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="databaseName"></param>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="outValues"></param>
        /// <returns></returns>
        public static IList<T> ExecuteWithDataTableNonQuery<T>(string databaseName, string storedProcName, IEnumerable<IDataParameter> parameters, ref ArrayList outValues) where T : new()
        {
            IList<T> list = new List<T>();
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName))
            {
                foreach (IDataParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.Connection = db.CreateConnection();
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        list.Add(Convertor.DataToEntity<T>(dr));
                    }
                }
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                        outValues.Add(param.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 返回实体对象集
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static IList<T> ExecuteEntity<T>(string databaseName, string storedProcName, params object[] paramValues) where T : new()
        {
            IList<T> list = new List<T>();
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                cmd.Connection = db.CreateConnection();
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        list.Add(Convertor.DataToEntity<T>(dr));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 返回实体对象集
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="databaseName">数据库</param>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="outValues">输出参数</param>
        /// <param name="paramValues">存储过程参数，必须按照顺序，没有的需要用null</param>
        /// <returns></returns>
        public static IList<T> ExecuteEntity<T>(string databaseName, string storedProcName, ref ArrayList outValues, params object[] paramValues) where T : new()
        {
            IList<T> list = new List<T>();
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName, paramValues))
            {
                cmd.Connection = db.CreateConnection();
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        list.Add(Convertor.DataToEntity<T>(dr));
                    }
                }
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                        outValues.Add(param.Value);
                }
            }

            return list;
        }

        public static IEnumerable<T> ExecuteEntityWithDataTable<T>(string databaseName, string storedProcName, IEnumerable<IDataParameter> parameters) where T : new()
        {
            IList<T> list = new List<T>();
            Database db = DBFactory.Create(databaseName);
            using (DbCommand cmd = DBFactory.CreateDbCommand(db, storedProcName))
            {
                foreach (IDataParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.Connection = db.CreateConnection();
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        list.Add(Convertor.DataToEntity<T>(dr));
                    }
                }
            }

            return list;
        }
    }
}