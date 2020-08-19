using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository.RepositoryView
{
    public class NewsVRepo : IStructure<NewsV>
    {
        SqlGenerator<NewsV> repo;
        public NewsVRepo()
        {
            repo = new SqlGenerator<NewsV>();
        }
        public int Add(NewsV item)
        {
            return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }

        public NewsV Get(int Id)
        {
            return repo.GetById(Id);
        }

        public IEnumerable<NewsV> GetAll()
        {
            return repo.GetAll();
        }

        public IEnumerable<NewsV> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }

        public int Update(NewsV item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
        public IEnumerable<NewsV> GetDataSql(string sql, object parameters)
        {
            if(parameters != null)
            return repo.GetData(sql, parameters);
            else
                return repo.GetData(sql);
        }
    }
}