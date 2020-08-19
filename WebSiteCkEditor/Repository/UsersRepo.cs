using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository
{
    public class UsersRepo : IStructure<Users>
    {
        SqlGenerator<Users> repo;
        public UsersRepo()
        {
            repo = new SqlGenerator<Users>();
        }
        public int Add(Users item)
        {
           return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }

        public Users Get(int Id)
        {
            return repo.GetById(Id);
        }
        public IEnumerable<Users> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }
        public IEnumerable<Users> GetAll()
        {
            return repo.GetAll();
        }

        public int Update(Users item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
    }
}