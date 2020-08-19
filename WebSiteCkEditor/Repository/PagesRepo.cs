using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository
{
    public class PagesRepo : IStructure<Pages>
    {
        SqlGenerator<Pages> repo;
        public PagesRepo()
        {
            repo = new SqlGenerator<Pages>();
        }
        public int Add(Pages item)
        {
            return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }
        public IEnumerable<Pages> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }
        public Pages Get(int Id)
        {
            return repo.GetById(Id);
        }

        public IEnumerable<Pages> GetAll()
        {
            return repo.GetAll();
        }

        public int Update(Pages item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
    }
}