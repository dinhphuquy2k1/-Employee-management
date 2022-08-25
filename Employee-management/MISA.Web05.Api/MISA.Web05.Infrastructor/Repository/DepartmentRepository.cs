using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MISA.Web05.Core;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.Web05.Core.Interfaces.Repository;

namespace MISA.Web05.Infrastructor.Repository
{
    public class DepartmentRepository : BaseRepository<Department>,IDepartmentRepository
    {
    }
}
