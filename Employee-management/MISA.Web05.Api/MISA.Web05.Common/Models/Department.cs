namespace MISA.Web05.Common.Models
{

    //Phòng ban
    //CreateBy: DPQuy (26/6/2022)

    public class Department
    {
        #region Constructor
        public Department()
        {
            this.DepartmentId = Guid.NewGuid();
        }
        #endregion


        #region Properties
        //Khóa chính
        public Guid DepartmentId { get; set; }

        //Tên phòng ban
        public string? DepartmentName { get; set; }

        //Ngày tạo
        public DateTime? CreatedDate { get; set; }

        //Ngày chỉnh sửa gần nhất
        public DateTime? ModifedDate { get; set; }

        //Người tạo
        public string? CreatedBy { get; set; }

        //Người thực hiện chỉnh sửa
        public string? ModifedBy { get; set; }
        #endregion


        #region Methods

        #endregion
    }
}
