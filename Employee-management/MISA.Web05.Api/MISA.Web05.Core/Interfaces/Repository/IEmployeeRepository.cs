using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Interfaces.Repository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {

        #region Method
        /// <summary>
        ///check mã nhân viên trùng
        ///true-đã tồn tại
        ///false-chưa có
        ///Created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>

        bool CheckEmployeeCodeExits(string employeeCode);



        bool CheckTelephoneNumber(string telephoneNumber);
        /// <summary>
        /// nhập khẩu
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        int Import(IEnumerable<Employee> employees);

        /// <summary>
        /// Lấy danh sách để export
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeExcel> Export();
        /// <summary>
        /// lấy mã nhân viên mới
        /// created by: DPQuy (6/7/20220
        /// </summary>
        /// <returns></returns>
        string GetNewEmployeeCode();
        #endregion

    }
}
