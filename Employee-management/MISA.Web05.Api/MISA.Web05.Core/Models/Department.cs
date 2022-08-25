
using MISA.Web05.Core.Models;

namespace MISA.Web05.Core
{

    //Phòng ban
    //CreateBy: DPQuy (26/6/2022)

    public class Department:BaseModel
    {
        #region Constructor
        public Department()
        {
            this.DepartmentId = Guid.NewGuid();
        }
        #endregion


        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }

       /// <summary>
       /// Tên phòng ban
       /// </summary>
        public string? DepartmentName { get; set; }


        #endregion


        #region Methods

        #endregion
    }
}
