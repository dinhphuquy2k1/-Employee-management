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
    public class BaseController<TEntity> : ControllerBase
    {


        #region Construtor
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IBaseService<TEntity> _baseService;
        public BaseController(IBaseRepository<TEntity> baseRepository, IBaseService<TEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion



        #region Method
        /// <summary>
        /// lấy toàn bộ dữ liệu nhân viên
        /// created by : DPQuy (6/7/2022)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = _baseRepository.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Lấy ra nhân viên theo id
        /// created by : DPQuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var employee = _baseRepository.GetById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thêm nhân viên
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            try
            {
                var res = _baseService.InsertService(entity);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin 
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(TEntity entity)
        {
            try
            {
                var res = _baseService.UpdateService(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>
        /// Xóa nhân viên theo id
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var res = _baseRepository.Delete(id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thực hiện xóa các bản ghi được lựa chọn
        /// created by : DPQuy (6/7/2022)
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [HttpDelete("{listId}")]

        public IActionResult DeleteMany(string listId)
        {
            try
            {
                var res = _baseRepository.DeleteMany(listId);
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên theo từ khóa
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string? filter)
        {
            try
            {
                var employees = _baseRepository.GetPaging(pageIndex, pageSize, filter);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý Exception
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult HandleException(Exception ex)
        {


            if (ex is ValidateException)
            {
                //ghi log vào hệ thống
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = ex.Message,
                };
                return StatusCode(400, res);

            }
            else
            {
                //ghi log vào hệ thống
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.ResourceVN.ResourceManager.GetString($"ErrorException_{Common.LanguageCode}"),
                };
                return StatusCode(500, res);
            }
        }
        #endregion
    }
}
