using Dapper;
using MISA.Web05.Core;
using MISA.Web05.Core.Interfaces.Repository;
using MySqlConnector;


namespace MISA.Web05.Infrastructor.Repository
{
    public class EmployeeRepository :BaseRepository<Employee>, IEmployeeRepository
    {
        #region Method
        /// <summary>
        /// kiểm tra có bị trùng mã 
        /// createby : DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public bool CheckEmployeeCodeExits(string employeeCode)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
                var parametes = new DynamicParameters();
                parametes.Add("@EmployeeCode", employeeCode);
                var res = MySqlConnection.QueryFirstOrDefault(sql: sql, param: parametes);
                if (res == null)
                    return false;
                return true;
            }
        }

        public bool CheckTelephoneNumber(string telephoneNumber)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_CheckTelephoneNumberExits";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TelephoneNumber", telephoneNumber);
                var res = MySqlConnection.QueryFirstOrDefault(sql: sql, param: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (res == null)
                    return false;
                return true;
            }
        }


        /// <summary>
        /// lấy danh sách để export
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeExcel> Export()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                ///khởi tạo kết nối
                //createby: DPQuy (6/7/2022)
                using (MySqlConnection = new MySqlConnection(ConnectionString))
                {
                    //câu lệnh truy vấn
                    var sql = $"`MISA.WEB05.DPQUY`.Proc_GetEmployeeExcel";

                    //thực hiện truy vấn
                    var employees = MySqlConnection.Query<EmployeeExcel>(sql, commandType: System.Data.CommandType.StoredProcedure);

                    //trả về dữ liệu
                    return employees;
                }
            }
        }






        /// <summary>
        /// Lấy toàn bộ danh sách 
        /// created by : DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Employee> Get()
        {

            //khởi tạo kết nối
            //createby: DPQuy (6/7/2022)
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                //câu lệnh truy vấn
                var sql = $"Proc_GetEmployee";

                //thực hiện truy vấn
                var employees = MySqlConnection.Query<Employee>(sql, commandType: System.Data.CommandType.StoredProcedure);

                //trả về dữ liệu
                return employees;
            }
        }
        /// <summary>
        /// Lấy mã nhân viên mới
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        public string GetNewEmployeeCode()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = "Proc_GetNewEmployeeCode";
                var res = MySqlConnection.QueryFirst<string>(sql: sql, commandType: System.Data.CommandType.StoredProcedure);
                return res;

            }
        }





        /// <summary>
        /// import excel
        /// created by : DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public int Import(IEnumerable<Employee> employees)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var employeeInsertd = 0;
                foreach (var employee in employees)
                {
                    var rowInsert = MySqlConnection.Execute("Proc_InsertEmployee", employee, commandType: System.Data.CommandType.StoredProcedure);
                    if (rowInsert != null)
                    {
                        employee.IsImported = true;
                        employeeInsertd += rowInsert;
                    }
                }
                return employeeInsertd;

            }
        }
        #endregion

    }
}
