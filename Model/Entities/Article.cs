using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASY.Iissy.Model.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CatId { get; set; }
        public DateTime PostDate { get; set; }
    }
}