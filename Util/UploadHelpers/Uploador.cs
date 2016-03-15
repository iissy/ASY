using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ASY.Iissy.Util.UploadHelpers
{
    public class Uploador
    {
        Dictionary<string, string> ExtTable = new Dictionary<string, string>();
        HttpPostedFileBase PostFile;
        long MaxSize = 4 * 1024 * 1024;
        public Uploador(HttpPostedFileBase postFile)
        {
            ExtTable.Add("image", "gif,jpg,jpeg,png,bmp");
            ExtTable.Add("flash", "swf,flv");
            ExtTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            ExtTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
            this.PostFile = postFile;
        }

        public string Domain { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; }

        public string FileType { get; set; }

        public string Extension
        {
            get
            {
                string fileName = this.PostFile.FileName;
                string fileExt = Path.GetExtension(fileName).ToLower();
                return fileExt;
            }
        }

        public long Length
        {
            get
            {
                return this.PostFile.InputStream.Length;
            }
        }

        private bool Filter()
        {
            bool isFilter = false;

            if (this.Length <= MaxSize)
            {
                Func<string, bool> fun = delegate (string p)
                {
                    if (ExtTable[p].Split(',').Contains(this.Extension.Substring(1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };

                IEnumerable<string> exts = ExtTable.Keys.Where<string>(fun);
                if (exts.Any())
                {
                    this.FileType = exts.FirstOrDefault();
                    isFilter = true;
                }
                else
                {
                    this.Message = "不支持的文件类型 ";
                }
            }
            else
            {
                this.Message = "上传文件超出最大限制";
            }

            return isFilter;
        }

        private string InitDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return path;
            }
            else
            {
                return Directory.CreateDirectory(path).FullName;
            }
        }

        public string FileUrl { get; set; }

        public void Save()
        {
            if (this.Filter())
            {
                string fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff", DateTimeFormatInfo.InvariantInfo), this.Extension);
                // get file url
                this.FileUrl = this.Domain + string.Concat("/upfiles/", this.FileType, "/", DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo), "/", fileName);
                // create file directory
                string fileDir = Path.Combine(HttpContext.Current.Server.MapPath("/upfiles"), this.FileType, DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo));
                // if dir not exists, create dir
                string file = Path.Combine(this.InitDirectory(fileDir), fileName);
                // save file,done
                this.PostFile.SaveAs(file);
                // set status
                this.Status = true;
            }
        }
    }
}