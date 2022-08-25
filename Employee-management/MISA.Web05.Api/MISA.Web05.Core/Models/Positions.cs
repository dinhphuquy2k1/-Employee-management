namespace MISA.Web05.Core
{
    public class Positions
    {
        #region Properties

        //Khóa chính
        public Guid PositionId { get; set; }

        //Tên vị trí
        public string PositionName { get; set; }

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
