using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataLayers.SqlServer
{
    public abstract class _BaseDAL
    {
        protected string connectionString = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected System.Data.SqlClient.SqlConnection GetConnection()
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            return cn;
        }
        protected SqlParameter CreateOutputParameter(string parameterName, SqlDbType dbType, int size)
        {
            SqlParameter para = new SqlParameter();
            para.ParameterName = parameterName;
            para.SqlDbType = dbType;
            para.Size = size;
            para.Direction = ParameterDirection.Output;
            return para;
        }
        protected SqlParameter CreateParameter(string parameterName, object value)
        {
            if (value == null)
                value = DBNull.Value;
            SqlParameter para = new SqlParameter(parameterName, value);
            return para;
        }
        /// <summary>
        /// Đổi 1 giá trị sang giá trị để tương thích với dữ liệu được lưu cơ sở dữ liệu
        /// (Giải thích: vì giá trị null muốn lưu vào CSDL phải chuyển thành DBNull.Value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected object ToDBValue(object value)
        {
            if (value != null)
                return value;
            return DBNull.Value;
        }
        /// <summary>
        /// Chuyển giá trị từ trong CSDL sang kiểu Nullable int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected int? DBValueToNullableInt(object value)
        {
            try
            {
                if (value == DBNull.Value)
                    return null;
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Chuyển giá trị từ trong CSDL sang kiểu Nullable DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected DateTime? DBValueToNullableDateTime(object value)
        {
            try
            {
                if (value == DBNull.Value)
                    return null;
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Tạo đối tượng SqlCommand sử dụng Stored Procedure làm Command Text
        /// </summary>
        /// <param name="procedureName">Tên của stored procedure</param>
        /// <param name="connection">Đối tượng SqlConnection</param>
        /// <returns></returns>
        protected SqlCommand CreateCommand(string procedureName, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand(procedureName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
        /// <summary>
        /// Tạo đối tượng SqlCommand
        /// </summary>
        /// <param name="commandText">Chuỗi câu lệnh</param>
        /// <param name="connection">Đối tượng SqlConnection</param>
        /// <param name="commandType">Kiểu của câu lệnh</param>
        /// <returns></returns>
        protected SqlCommand CreateCommand(string commandText, SqlConnection connection, CommandType commandType)
        {
            SqlCommand cmd = new SqlCommand(commandText, connection);
            cmd.CommandType = commandType;
            return cmd;
        }
    }
}
