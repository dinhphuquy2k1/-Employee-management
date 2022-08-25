using Microsoft.AspNetCore.Http;
using MISA.Web05.Core.Exceptions;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using OfficeOpenXml;


namespace MISA.Web05.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {

        #region Constructor
        IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion

        #region Method
        public IEnumerable<Employee> Import(IFormFile fileImport)
        {
            //validate tệp
            //if (fileImport == null || fileImport.Length <= 0)
            //{
            //}

            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Tệp không đúng định dạng");
            }

            var employees = new List<Employee>();

            using (var stream = new MemoryStream())
            {
                fileImport.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employeeCode = worksheet.Cells[row, 2].Value.ToString();
                        var employeeName = ConvertToString(worksheet.Cells[row, 3].Value);
                        var genderName = ConvertToString(worksheet.Cells[row, 4].Value);
                        var dateOfBirth = ConvertToDate(worksheet.Cells[row, 5].Value);
                        var identityNumber = ConvertToString(worksheet.Cells[row, 6].Value);
                        var identityDate = ConvertToDate(worksheet.Cells[row, 7].Value);
                        var identityPlace = ConvertToString(worksheet.Cells[row, 8].Value);
                        var positionName = ConvertToString(worksheet.Cells[row, 9].Value);
                        var departmentId = worksheet.Cells[row, 10].Value;
                        var departmentName = ConvertToString(worksheet.Cells[row, 11].Value);

                        var employee = new Employee
                        {

                            EmployeeCode = employeeCode,
                            EmployeeName = employeeName,
                            GenderName = genderName,
                            DateOfBirth = dateOfBirth,
                            IdentityNumber = identityNumber,
                            IdentityPlace = identityPlace,
                            IdentityDate = identityDate,
                            PositionName = positionName,
                            //  DepartmentId=departmentId,
                            DepartmentName = departmentName,
                        };


                        //Thực hiện validate dữ liệu
                        //Xóa toàn bộ thông tin lỗi validate của object trước
                        ErrorListValidateMsg.Clear();
                        IsValid = true;
                        var isValid = ValidateObjectInsert(employee);
                        if (!isValid)
                        {
                            employee.IsValidImport = false;
                            employee.ListErrorImport.AddRange(ErrorListValidateMsg);
                        }
                        employees.Add(employee);
                    }
                }
            }

            //lấy danh sách những nhân viên hợp lệ
            var employeeValids = employees.Where(e => e.IsValidImport == true);
            if (employeeValids.Count() > 0)
            {
                var res = _repository.Import(employeeValids);
            }
            return employees;
        }

        /// <summary>
        /// Hàm convert giá trị về chuỗi
        /// created by: DPQuy (7/7/2022)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string? ConvertToString(object? value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert về dạng datetime
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private DateTime? ConvertToDate(object? value)
        {
            DateTime dateConvert;
            if (value != null)
            {
                if (DateTime.TryParse(value.ToString(), out dateConvert))
                {
                    return dateConvert;
                }
                return null;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// validate object khi insert
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        protected override bool ValidateObjectInsert(Employee employee)
        {
            var lagCode = Common.LanguageCode;
            IsValid = ValidataObjectCommon(employee);
            //kiểm tra mã nhân viên có bị trùng
            if (_repository.CheckEmployeeCodeExits(employee.EmployeeCode) == true)
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_DuplicateEmployeeCode"));

            }
            //kiểm tra số điện thoại có bị trùng
            //if (_repository.CheckTelephoneNumber(employee.TelephoneNumber) == true)
            //{
            //    IsValid = false;
            //    ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_TelephoneNumberExit"));
            //}
            // Nếu hợp lệ thì gọi yêu cầu thực hiện thêm mới dữ liệu vào CSDL
            return IsValid;

        }


        /// <summary>
        /// Hàm Validate chung
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool ValidataObjectCommon(Employee employee)
        {
            var lagCode = Common.LanguageCode;

            //kiểm tra mã nhân viên có trống
            if (string.IsNullOrEmpty(employee.EmployeeCode.Trim()))
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_EmployeeCode"));
            }
            //kiểm tra tên nhân viên có trống
            else if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_EmployeeName"));
            }

            // 2. kiểm tra ngày sinh ngày cấp có hợp lệ (không lớn hơn hiện tại)
            if (employee.DateOfBirth > DateTime.Now)
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_DateOfBirth"));
            }
            if (employee.IdentityDate > DateTime.Now)
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_IdentityDate"));
            }

            //4.kiểm tra email có đúng định dạng
            if (!employee.Email.EndsWith("@gmail.com") && !string.IsNullOrEmpty(employee.Email))
            {
                IsValid = false;
                ErrorListValidateMsg.Add(Resources.ResourceVN.ResourceManager.GetString($"ErrorValidate_{lagCode}_Email"));
            }
            // Nếu hợp lệ thì gọi yêu cầu thực hiện thêm mới dữ liệu vào CSDL
            return IsValid;
        }


        /// <summary>
        /// Hàm validate khi update dữ liệu
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        protected override bool ValidateObjectUpdate(Employee employee)
        {
            IsValid = ValidataObjectCommon(employee);

            // Nếu hợp lệ thì gọi yêu cầu thực hiện thêm mới dữ liệu vào CSDL
            return IsValid;

        }
        #endregion

    }
}
