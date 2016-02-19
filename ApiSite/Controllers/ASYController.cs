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

        [Route]
        [HttpGet]
        public IEnumerable<Article> Index()
        {
            return this.ASYService.Fun(33);
        }

        [Route("fun")]
        [HttpGet]
        public IEnumerable<Article> fun()
        {
            return this.ASYService.Fun(66);
        }

        [Route("{user}")]
        [HttpGet]
        public IEnumerable<Article> my(string user)
        {
            return this.ASYService.Fun(88);
        }

        [Route("reg")]
        [HttpGet]
        public IEnumerable<Article> reg()
        {
            return this.ASYService.Fun(99);
        }
    }
}