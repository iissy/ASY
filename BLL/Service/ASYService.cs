using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASY.Iissy.BLL.IService;
using ASY.Iissy.DAL.Repository;

namespace ASY.Iissy.BLL.Service
{
    public class ASYService : IASYService
    {
        public void Fun()
        {
            ASYRepository.Fun();
        }
    }
}