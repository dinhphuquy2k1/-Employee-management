using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Thêm bản ghi mới
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertService(TEntity entity);


        /// <summary>
        /// Cập nhật thông tin
        /// Created by: dpquy (07/07/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdateService(TEntity entity);
    }
}
