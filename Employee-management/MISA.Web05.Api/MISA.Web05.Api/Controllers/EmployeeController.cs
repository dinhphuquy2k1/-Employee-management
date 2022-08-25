using Microsoft.AspNetCore.Http;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core;
using MISA.Web05.Core.Exceptions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MISA.Web05.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {

        #region Constructor
        IEmployeeService _service;
        IEmployeeRepository _repository;

        public EmployeeController(IEmployeeService service, IEmployeeRepository repository) : base(repository, service)
        {
            _service = service;
            _repository = repository;
        }
        #endregion






        #region Method
        /// <summary>
        /// import excel
        /// created by :DPQuy (6/7/2022)
        /// </summary>
        /// <param name="fileImport"></param>
        /// <returns></returns>
        [HttpPost("import")]
        public IActionResult Import(IFormFile fileImport)
        {
            try
            {
                var employees = _service.Import(fileImport);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }




        /// <summary>
        /// xuất khẩu 
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            // query data from database  
            await Task.Yield();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var employeeExcel = _repository.Export();
            var stream = new MemoryStream();

            //tạo thêm 3 hàng (vì tiêu đề chiếm 2 ô)
            var length = employeeExcel.Count() + 3;
            var cellLastBorder = "I" + length.ToString();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Danh sách nhân viên");
                for (int i = 1; i <= length -3; i++)
                {
                    workSheet.Cells[$"A{i + 1}"].Value = i;
                   
                }
                workSheet.Cells[$"B1:{cellLastBorder}"].LoadFromCollection(employeeExcel, true);
                workSheet.InsertRow(1, 2);
                workSheet.Cells["A1:I1"].Merge = true;
                //Make all text fit the cells
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                workSheet.Cells["A1"].Value = "DANH SÁCH NHÂN VIÊN";
                workSheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A3"].Value = "STT";
                workSheet.Cells["B3"].Value = "Mã nhân viên";
                workSheet.Cells["C3"].Value = "Tên nhân viên";
                workSheet.Cells["D3"].Value = "Giới tính";
                workSheet.Cells["E3"].Value = "Ngày sinh";
                workSheet.Cells["F3"].Value = "Chức danh";
                workSheet.Cells["G3"].Value = "Tên đơn vị";
                workSheet.Cells["H3"].Value = "Số tài khoản";
                workSheet.Cells["I3"].Value = "Tên ngân hàng";
                workSheet.Columns[5].Style.Numberformat.Format = "dd/mm/yyyy";
                //căn giữa ngày sinh
                workSheet.Columns[5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //căn trái cột STT
                workSheet.Cells[$"A4:A{length.ToString()}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells["A3:I3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ccc"));
                workSheet.Cells["A3:I3"].Style.Font.Bold = true;
                workSheet.Cells["A3:I3"].Style.Font.Size = 10;
                workSheet.Cells["A3:I3"].Style.Font.Name = "Arial";
                workSheet.Cells["A1"].Style.Font.Bold = true;
                workSheet.Cells["A1"].Style.Font.Size = 16;
                workSheet.Cells["A1"].Style.Font.Name = "Arial";
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Bottom.Color.SetColor(Color.Black);

                double minimumSize = 20;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize);


                double maximumSize = 50;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);


                for (int col = 1; col <= workSheet.Dimension.End.Column; col++)
                {
                    workSheet.Column(col).Width = workSheet.Column(col).Width + 1;
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/octet-stream", "DanhSachNhanVien.xlsx");
        }


        /// <summary>
        /// Lấy mã nhân viên mới
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _repository.GetNewEmployeeCode();
                return Ok(newEmployeeCode);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        #endregion






    }
}
