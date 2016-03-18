using ASY.Iissy.Util.UploadHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class ASYController : Controller
    {
        [HttpPost]
        [Route("upload")]
        public void upload()
        {
            UploadList upList = new UploadList();
            upList.List = new List<UploadObject>();

            HttpPostedFileBase postFile;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                UploadObject upObj = new UploadObject();
                postFile = Request.Files[i];
                if (postFile != null)
                {
                    Uploador uploador = new Uploador(postFile);
                    uploador.Save();

                    upObj.Status = uploador.Status;
                    upObj.Message = uploador.Message;
                    upObj.Url = uploador.FileUrl;

                    upList.List.Add(upObj);
                }
            }

            Response.AddHeader("Access-Control-Allow-Origin", "*");
            Response.ContentType = "application/json";
            Response.Charset = "utf-8";
            Response.Write(JsonConvert.SerializeObject(upList));
            Response.End();
        }
    }
}