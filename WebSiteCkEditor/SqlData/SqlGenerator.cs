using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SqlData
{
    public class SqlGenerator<T>
    {
        string Key = null;
        GenerateMethods<T> method;
        private readonly string conn = "";
        public SqlGenerator()
        {
            conn = Cs.CsStr;
            method = new GenerateMethods<T>();
            GetKeyName();
        }

        private void GetKeyName()
        {
           var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in propertyInfos)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                if(attribute != null)
                {
                    Key = property.Name;
                    break;
                }
            }
        }
        
        public IEnumerable<T> GetData(string qry, object parameters)
        {
            using (var db = new SqlConnection(conn))
            {
                var result = db.Query<T>(qry, parameters);
                return result;
            }
            
           
        }
        public IEnumerable<T> GetData(string qry)
        {
            using (var db = new SqlConnection(conn))
            {
                var result = db.Query<T>(qry);
                return result;
            }
        }
        public int Execute(string qry, object parameters)
        {
            using (var db = new SqlConnection(conn))
            {
                var result = db.Execute(qry, parameters);
                return result;
            }
        }
        public Task<int> ExecuteAsync(string qry, object parameters)
        {
            using (var db = new SqlConnection(conn))
            {
                var result = db.ExecuteAsync(qry, parameters);
                return result;
            }
        }
        public IEnumerable<T> GetAll()
        {
            var select = method.GenerateSelect();
            return GetData(select);
        }
        public T Find(T pksFields)
        {
            ParameterValidator.ValidateObject(pksFields, "Add");
            string qry = method.GenerateSelect();
            return GetData(qry).FirstOrDefault();
        }
        public T GetById(int key)
        {
            if (key != 0)
            {
                T identity = (T)Activator.CreateInstance(typeof(T));
                var info = identity.GetType().GetProperty(Key);
                info.SetValue(identity, Convert.ChangeType(key, info.PropertyType), null);
                string qry = method.GenerateSelect();
                qry += method.GenerateById(Key);
                return GetData(qry, identity).FirstOrDefault();
            }
            else return default(T);
           
        }
        public IEnumerable<T> GetByAny(string ColumnName, object value)
        {
            ParameterValidator.ValidateObject(value, "Update");
            T identity = (T)Activator.CreateInstance(typeof(T));
            var info = identity.GetType().GetProperty(ColumnName);
            info.SetValue(identity, Convert.ChangeType(value, info.PropertyType), null);
            string qry = method.GenerateSelect();
            qry += method.GenerateById(ColumnName);
            return GetData(qry, identity);
        }

        public int Add(T entity)
        {
            ParameterValidator.ValidateObject(entity, "Add");
            string qry = method.GeneratePartInsert(Key, entity, false);
            int result = Execute(qry, entity);
            return result;
        }

        public int Remove(int Id)
        {
            if (Id != 0)
            {
                T identity = (T)Activator.CreateInstance(typeof(T));
                var info = identity.GetType().GetProperty(Key);
                info.SetValue(identity, Convert.ChangeType(Id, info.PropertyType), null);
                string qry = method.GenerateDelete(Key);
                var result = Execute(qry, identity);
                return result;
            }
            else
                return 0;
            
        }
        public int Update(T entity, bool AllowNulls = true)
        {
            ParameterValidator.ValidateObject(entity, "Update");
            string qry = method.GenerateUpdate(Key, entity, AllowNulls);
            return Execute(qry, entity);
        }
        public int InstertOrUpdate(T entity)
        {
            ParameterValidator.ValidateObject(entity, "InstertOrUpdate");
            var qry = method.GenerateSelect();
            var ob = GetData(qry);
            if(ob == null)
            {
                qry = method.GeneratePartInsert(Key, entity, false);
                return Execute(qry, entity);
            }
            else
            {
                qry = method.GenerateUpdate(Key, entity);
                return Execute(qry, entity);
            }
        }

        
    }
}
