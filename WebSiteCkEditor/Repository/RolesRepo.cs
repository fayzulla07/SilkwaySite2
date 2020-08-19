using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository
{
    public class RolesRepo : IStructure<Roles>
    {
        SqlGenerator<Roles> repo;
        public RolesRepo()
        {
            repo = new SqlGenerator<Roles>();
        }
        public int Add(Roles item)
        {
          return repo.Add(item);
        }

        public int Delete(int Id)
        {
           return repo.Remove(Id);
        }

        public Roles Get(int Id)
        {
            return repo.GetById(Id);
        }
        public IEnumerable<Roles> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }
        public IEnumerable<Roles> GetAll()
        {
            return repo.GetAll();
        }

        public int Update(Roles item, bool allownulls = true)
        {
           return repo.Update(item, allownulls);
        }
    }
}