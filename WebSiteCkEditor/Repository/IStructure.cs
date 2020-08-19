using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteCkEditor.Repository
{
    interface IStructure<T> where T : class
    {
         IEnumerable<T> GetAll();
         T Get(int Id);
         int Add(T item);
         int Update(T item, bool allownulls = true);
         int Delete(int Id);
         IEnumerable<T> GetByAny(string ColumnName, object value);
    }
}
