using System.Collections.Generic;
using System.Web.Http;
using ASY.Iissy.BLL.IService;
using ASY.Iissy.Model.Entities;

namespace ApiSite.Controllers
{
    public class ASYController : ApiController
    {
        private IASYService ASYService;
        public ASYController(IASYService ASYService)
        {
            this.ASYService = ASYService;
        }

        [Route("{user}")]
        [HttpGet]
        public IEnumerable<Article> my(string user)
        {
            return this.ASYService.Fun(88);
        }

        [Route("reg/{who}")]
        [HttpGet]
        public IEnumerable<Article> reg(string who)
        {
            return this.ASYService.Fun(99);
        }

        [Route("{user}/{list}")]
        [HttpGet]
        public IEnumerable<Article> list(string user, string list)
        {
            return this.ASYService.Fun(88);
        }
    }
}