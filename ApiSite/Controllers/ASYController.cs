using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASY.Iissy.BLL.IService;

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
        public string Index()
        {
            return this.ASYService.Fun();
        }

        [Route("fun")]
        [HttpGet]
        public string fun()
        {
            return this.ASYService.Fun();
        }

        [Route("{user}")]
        [HttpGet]
        public string my(string user)
        {
            return this.ASYService.Fun();
        }

        [Route("reg")]
        [HttpGet]
        public string reg()
        {
            return this.ASYService.Fun();
        }
    }
}