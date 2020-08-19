using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository.RepositoryView
{
    public class PagesVRepo : IStructure<PagesV>
    {
        SqlGenerator<PagesV> repo;
        public PagesVRepo()
        {
            repo = new SqlGenerator<PagesV>();
        }
        public int Add(PagesV item)
        {
            return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }

        public PagesV Get(int Id)
        {
            return repo.GetById(Id);
        }

        public IEnumerable<PagesV> GetAll()
        {
            return repo.GetAll();
        }

        public IEnumerable<PagesV> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }

        public int Update(PagesV item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
        public IEnumerable<PagesV> GetDataSql(string sql, object parameters)
        {
            if(parameters != null)
            return repo.GetData(sql, parameters);
            else
                return repo.GetData(sql);
        }
    }
}