using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ASY.Iissy.Util.SqlHelpers
{
    public class ParameterObject
    {
        public ParameterObject()
        {
            parameters = new List<IDataParameter>();
        }

        IList<IDataParameter> parameters;
        public IList<IDataParameter> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public void AddInParameter(string parameterName, SqlDbType dbType, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.SqlDbType = dbType;
            parameter.Value = value;

            parameters.Add(parameter);
        }
        public void AddInParameter(string parameterName, SqlDbType dbType, int size, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.SqlDbType = dbType;
            parameter.Size = size;
            parameter.Value = value;

            parameters.Add(parameter);
        }

        public void AddOutParameter(string parameterName, SqlDbType dbType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.Output;
            parameter.SqlDbType = dbType;

            parameters.Add(parameter);
        }
        public void AddOutParameter(string parameterName, SqlDbType dbType, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.Output;
            parameter.SqlDbType = dbType;
            parameter.Size = size;

            parameters.Add(parameter);
        }
    }
}