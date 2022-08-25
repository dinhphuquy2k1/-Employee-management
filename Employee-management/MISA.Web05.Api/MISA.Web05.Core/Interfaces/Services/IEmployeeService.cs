using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Interfaces.Services
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Thực hiện import dữ liệu
        /// </summary>
        /// <param name="fileImport">Tên file import</param>
        /// <returns>Danh sách trong tệp import kèm theo trạng thái đã nhập khẩu được hay chưa</returns>
        /// Created by: DPQuy (6/7/2022)
        IEnumerable<Employee> Import(IFormFile fileImport);
    }
}
