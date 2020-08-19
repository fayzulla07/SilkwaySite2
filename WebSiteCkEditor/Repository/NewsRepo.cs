using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;


namespace WebSiteCkEditor.Repository
{
    public class NewsRepo : IStructure<News>
    {
        SqlGenerator<News> repo;
        public NewsRepo()
        {
            repo = new SqlGenerator<News>();
        }
        public int Add(News item)
        {
           return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }

        public News Get(int Id)
        {
            return repo.GetById(Id);
        }
        public IEnumerable<News> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }
        public IEnumerable<News> GetAll()
        {
            return repo.GetAll();
        }

        public int Update(News item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
    }
}