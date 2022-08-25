using Dapper;
using MISA.Web05.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructor.Repository
{
    public class BaseRepository<TEntity>: IBaseRepository<TEntity>
    {
        #region Constructor
        protected string ConnectionString;
        protected MySqlConnection MySqlConnection;
        protected string TableName;
        public BaseRepository()
        {

            //thông tin kết nối CSDL
            ConnectionString = "Host=3.0.89.182;" +
             "Port=3306;" +
             "Database=MISA.WEB05.DPQUY;" +
             "User Id =dev;" +
             "Password=12345678";

            //lấy ra kiểu dữ liệu
            TableName = typeof(TEntity).Name;
        }
        #endregion



        #region Method
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {

            //khởi tạo kết nối
            //createby: DPQuy (6/7/2022)
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                //câu lệnh truy vấn
                var sql = $"Select * from {TableName}";

                //thực hiện truy vấn
                var data = MySqlConnection.Query<TEntity>(sql);

                //trả về dữ liệu
                return data;
            }
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// createby: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Select * from {TableName} Where {TableName}Id= @{TableName}Id";
                var parametes = new DynamicParameters();
                parametes.Add($"@{TableName}Id", id);
                var employee = MySqlConnection.QueryFirstOrDefault<TEntity>(sql, param: parametes);
                return employee;
            }
        }


        /// <summary>
        ///Xóa bản ghi theo id
        ///createby: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(Guid id)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Delete from {TableName} where {TableName}Id = @{TableName}Id";
                var parametes = new DynamicParameters();
                parametes.Add($"@{TableName}Id", id);
                var res = MySqlConnection.Execute(sql, param: parametes);
                return res;
            }
        }


        /// <summary>
        /// thêm bản ghi
        /// create: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>

        public int Insert(TEntity entity)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_Insert{TableName}";

                var res = MySqlConnection.Execute(sql, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        /// <summary>
        /// cập nhật thông tin bản ghi
        /// createby: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_Update{TableName}";

                var res = MySqlConnection.Execute(sql, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }


        /// <summary>
        /// thực hiện xóa các bản ghi đã được lựa chọn
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int DeleteMany(string listId)
        {

            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Delete FROM {TableName} where {TableName}Id IN ('{listId}')";
                var res = MySqlConnection.Execute(sql);
                return res;
            }
        }



        /// <summary>
        /// Lấy danh sách theo từ khóa
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Object GetPaging(int pageIndex, int pageSize, string? filter = "")
        {

            //khởi tạo kết nối
            //createby: DPQuy (6/7/2022)
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                //câu lệnh truy vấn
                var sql = $"Proc_GetEmployeePaging";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@m_Filter", filter);
                dynamicParameters.Add("@m_PageIndex", pageIndex);
                dynamicParameters.Add("@m_PageSize", pageSize);

                //số bản ghi
                dynamicParameters.Add("@m_TotalRecord", direction: System.Data.ParameterDirection.Output);

                dynamicParameters.Add("@m_TotalPage", direction: System.Data.ParameterDirection.Output);
                //thực hiện truy vấn
                var employees = MySqlConnection.Query<TEntity>(sql, param: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);


                //tổng số bản ghi
                int totalRecord = dynamicParameters.Get<int>("@m_TotalRecord");

                //tổng số trang theo số bản ghi trên 1 trang 
                int totalPage = dynamicParameters.Get<int>("@m_TotalPage");
                //trả về dữ liệu
                return new
                {
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                    Data = employees,
                    filter
                };
            }
        }
        #endregion


    }
}
