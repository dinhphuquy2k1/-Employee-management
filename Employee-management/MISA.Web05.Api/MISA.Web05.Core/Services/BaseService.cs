using MISA.Web05.Core.Exceptions;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region Constructor
        IBaseRepository<TEntity> _baseRepository;
        protected bool IsValid = true;
        protected List<string> ErrorListValidateMsg;
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            ErrorListValidateMsg = new List<string>();
        }
        #endregion

        #region Method
        /// <summary>
        /// Thêm mới dữ liệu
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        public int InsertService(TEntity entity)
        {
            //validate dữ liệu
            var isValid = ValidateObjectInsert(entity);


            //dữ liệu hợp lệ==> thực hiện thêm mới
            if (isValid == true)
            {
                //thực hiện thêm mới
                var res = _baseRepository.Insert(entity);
                return res;
            }
            else
            {
                throw new ValidateException(ErrorListValidateMsg);
            }

        }


        /// <summary>
        /// Validate dữ liệu khi insert
        /// true- hợp lệ
        /// false-  không hợp lệ
        /// createby: DPQuy (7/7/2022)
        /// </summary>
        /// <param name="pq"></param>
        /// <returns></returns>
        protected virtual bool ValidateObjectInsert(TEntity entity)
        {
            return true;
        }


        /// <summary>
        /// Validate dữ liệu khi update
        /// created  by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool ValidateObjectUpdate(TEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Cập nhật thông tin
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateService(TEntity entity)
        {


            //validate dữ liệu
            var isValid = ValidateObjectUpdate(entity);


            //dữ liệu hợp lệ==> thực hiện thêm mới
            if (isValid == true)
            {
                //thực hiện thêm mới
                var res = _baseRepository.Update(entity);
                return res;
            }
            else
            {
                throw new ValidateException(ErrorListValidateMsg);
            }
        }
        #endregion

    }
}
