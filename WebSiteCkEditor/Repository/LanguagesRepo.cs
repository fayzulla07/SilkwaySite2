using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository
{
    public class LanguagesRepo : IStructure<Languages>
    {
        SqlGenerator<Languages> repo;
        public LanguagesRepo()
        {
            repo = new SqlGenerator<Languages>();
        }
        public int Add(Languages item)
        {
            return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }

        public Languages Get(int Id)
        {
            return repo.GetById(Id);
        }

        public IEnumerable<Languages> GetAll()
        {
            return repo.GetAll();
        }

        public IEnumerable<Languages> GetByAny(string ColumnName, object value)
        {
           return repo.GetByAny(ColumnName, value);
        }

        public int Update(Languages item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
    }
}