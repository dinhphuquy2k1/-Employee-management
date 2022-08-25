
using MISA.Web05.Core.Enum;

namespace MISA.Web05.Core
{
    public class EmployeeExcel
    {
        #region Properties
            /// <summary>
            /// Mã nhân viên
            /// </summary>
            public string? EmployeeCode { get; set; }

            //Họ và tên
            public string? EmployeeName { get; set; }


        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName
        {
            get;
            set;
        }
        //số tài khoản
        public string? BankNumber { get; set; }
        //Tên ngân hàng
        public string? BankName { get; set; }

        
        #endregion
    }
}
