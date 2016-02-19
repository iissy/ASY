using System.Collections.Generic;
using ASY.Iissy.BLL.IService;
using ASY.Iissy.DAL.Repository;
using ASY.Iissy.Model.Entities;

namespace ASY.Iissy.BLL.Service
{
    public class ASYService : IASYService
    {
        public IEnumerable<Article> Fun(int catid)
        {
            return ASYRepository.Fun(catid);
        }
    }
}