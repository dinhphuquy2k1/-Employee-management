using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core;
using MISA.Web05.Core.Exceptions;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
namespace MISA.Web05.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Constructor
        private readonly IDepartmentService _service;
        private readonly IDepartmentRepository _repository;
        public DepartmentsController(IDepartmentService service, IDepartmentRepository repository)
        {
            _service = service;
            _repository = repository;

        }
        #endregion



        #region Method
        /// <summary>
        /// Thêm bản ghi mới
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _repository.Get();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public IActionResult Post(Department department)
        {
            try
            {
                //validate dữ liệu

                //thực hiện thêm mới dữ liệu

                //Trả kết quả về client

                var res = _service.InsertService(department);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }



        /// <summary>
        /// Xử lý exception
        /// created by : DPQuy(6/7/2022)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private IActionResult HandleException(Exception ex)
        {

            //ghi log vào hệ thống
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ",
            };
            if (ex is ValidateException)
            {
                return StatusCode(400, res);
            }
            else
            {
                return StatusCode(500, res);
            }
        }
        #endregion

    }
}
