using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web05.Core.Exceptions;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;


namespace MISA.Web05.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        #region Constructor
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        #endregion


        #region Method
        /// <summary>
        /// Thêm mới
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        public int InsertService(Department department)
        {
            //kiểm tra dữ liệu có hợp lệ
            var isValid = true;
            //validate dữ liệu
            //1.Tên phòng ban ko được phép để trống
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                var resService = new
                {
                    isValid = false,
                    userMsg = "Tên phòng ban không được phép để trống",
                };
                throw new ValidateException("Tên phòng ban không được phép để trông");
            }
            //nếu validate hợp lệ thì gửi yêu cầu thêm mới vào database

            if (isValid)
            {
                var res = _repository.Insert(department);
                return res;
            }
            else
            {
                throw new ValidateException("Dữ liệu đầu vào không hợp lệ");
            }
        }

        public int UpdateService(Department department)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
