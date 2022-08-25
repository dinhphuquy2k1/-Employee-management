using MISA.Web05.Core.Enum;
using MISA.Web05.Core.Models;

namespace MISA.Web05.Core
{
    public class Employee:BaseModel
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        //Họ và tên
        public string EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }


        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName {
            get
            {
                var lagCode = Common.LanguageCode;
                switch (Gender)
                {
                    case Enum.Gender.male:
                        return Resources.ResourceVN.ResourceManager.GetString($"Gender_{lagCode}_Male");
                    case Enum.Gender.female:    
                        return Resources.ResourceVN.ResourceManager.GetString($"Gender_{lagCode}_Female");

                     default:
                        return Resources.ResourceVN.ResourceManager.GetString($"Gender_{lagCode}_Other");
                        break;
                }
                
            }
            set
            {

            }
        }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }


        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid DepartmentId { get; set; }


        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get;
            set;
        }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string? TelephoneNumber { get; set; }
        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? PositionId { get; set; }

        //Số chứng minh thư
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        //Nơi cấp CMND
        public string? IdentityPlace { get; set; }
        //Mức lương
        public decimal? Salary { get; set; }
        //Tên ngân hàng
        public string? BankName { get; set; }

        //Số tài khoản
        public string? BankNumber { get; set; }

        //tên chi nhánh
        public string? BankBranchName { get; set; }
        //Địa chỉ
        public string? Address { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
         #endregion



        #region Import

        /// <summary>
        /// khi thêm thành công từ excel
        /// 
        /// </summary>
        public bool? IsValidImport { get; set; } = true;

        /// <summary>
        /// List lỗi khi import không thành công
        /// </summary>
        public List<string> ListErrorImport { get; set; } = new List<string>();

        /// <summary>
        /// true: import thành công
        /// flase: không thành công
        /// </summary>
        public bool? IsImported { get; set; } =true;
        #endregion
       


    }
}
