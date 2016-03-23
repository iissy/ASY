using System.Collections.Generic;
using ASY.Iissy.Util.SqlHelpers;
using ASY.Iissy.Model.Entities;
using System;

namespace ASY.Iissy.DAL.Repository
{
    public static class ASYRepository
    {
        public static IEnumerable<Article> Fun(int catid)
        {
            List<Article> list = new List<Article>();
            Article article = new Article();
            article.Body = "I'm a body.";
            article.CatId = 3;
            article.Id = 100;
            article.PostDate = DateTime.Now;
            article.Title = "I'm a title.";
            list.Add(article);
            return list;
            //无数据库可用
            //return SqlHelpers.ExecuteEntity<Article>(null, "MyProc", catid);
        }
    }
}