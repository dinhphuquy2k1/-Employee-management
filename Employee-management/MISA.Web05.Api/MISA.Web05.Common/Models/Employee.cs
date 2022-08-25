namespace MISA.Web05.Common.Models
{
    public class Employee
    {
        #region Properties
        //Khóa chính
        public Guid EmployeeId { get; set; }

        //Mã nhân viên
        public string EmployeeCode { get; set; }

        //Họ và tên
        public string FullName { get; set; }

        //Giới tính
        public int? Gender { get; set; }

        //Ngày sinh
        public DateTime? DateOfBirth { get; set; }

        //Email
        public string Email { get; set; }

        //Số điện thoại
        public string PhoneNumber { get; set; }

        //Khóa ngoại
        public Guid DepartmentId { get; set; }

        //Khóa ngoại
        public Guid PositionId { get; set; }

        //Số chứng minh thư
        public string IdentityNumber { get; set; }

        //Nơi cấp CMND
        public string? IdentityPlace { get; set; }
        //Mức lương
        public decimal? Salary { get; set; }
        //Tên ngân hàng
        public string? BankName { get; set; }

        //Địa chỉ
        public string? Address { get; set; }

        //Ngày tạo
        public DateTime? CreatedDate { get; set; }

        //Ngày chỉnh sửa gần nhất
        public DateTime? ModifedDate { get; set; }

        //Người tạo
        public string? CreatedBy { get; set; }

        //Người thực hiện chỉnh sửa
        public string? ModifedBy { get; set; }
        #endregion
    }
}
