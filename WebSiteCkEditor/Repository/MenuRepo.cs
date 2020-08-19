using SqlData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebSiteCkEditor.Models;

namespace WebSiteCkEditor.Repository
{
    public class MenuRepo : IStructure<Menu>
    {
        SqlGenerator<Menu> repo;
        public MenuRepo()
        {
            repo = new SqlGenerator<Menu>();
        }
        public int Add(Menu item)
        {
            return repo.Add(item);
        }

        public int Delete(int Id)
        {
            return repo.Remove(Id);
        }

        public Menu Get(int Id)
        {
            return repo.GetById(Id);
        }
        public IEnumerable<Menu> GetByAny(string ColumnName, object value)
        {
            return repo.GetByAny(ColumnName, value);
        }
        public IEnumerable<Menu> GetAll()
        {
           return repo.GetAll();
        }

        public int Update(Menu item, bool allownulls = true)
        {
            return repo.Update(item, allownulls);
        }
    }
}