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

        [HttpGet]
        public void Fun()
        {
            this.ASYService.Fun();
        }
    }
}