using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        //CreateBy : Đinh Phú Quý(6/7/2022)
        //lấy danh sách department
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Lấy bản ghi theo id
        /// createby: DTEntityuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(Guid id);

        /// <summary>
        /// Thêm mới bản ghi
        /// //createby : DTEntityuy (6/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);



        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// createby: DTEntityuy (6/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);


        /// <summary>
        /// Xóa bản ghi theo id
        /// createby; DTEntityuy (6/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(Guid id);


        /// <summary>
        /// Xóa các bản ghi đã được lựa chọn
        /// created by: DPQuy (6/7/2022)
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        int DeleteMany(string listId);



        /// <summary>
        /// Lấy danh sách theo trang và số lượng
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Object GetPaging(int pageIndex, int pageSize, string? filter = "");

    }
}
