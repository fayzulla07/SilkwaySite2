using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository
{
    public class NewTypesRepo : IStructure<NewTypes>
    {
        SqlGenerator<NewTypes> repo;
        public NewTypesRepo()
        {
            repo = new SqlGenerator<NewTypes>();
        }
        public int Add(NewTypes item)
        {
            return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }
        public IEnumerable<NewTypes> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }
        public NewTypes Get(int Id)
        {
            return repo.GetById(Id);
        }

        public IEnumerable<NewTypes> GetAll()
        {
            return repo.GetAll();
        }

        public int Update(NewTypes item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
    }
}