using ASY.Iissy.Model.Entities;
using System.Collections.Generic;

namespace ASY.Iissy.BLL.IService
{
    public interface IASYService
    {
        IEnumerable<Article> Fun(int catid);
    }
}