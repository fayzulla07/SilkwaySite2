using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlData
{
    public class GenerateMethods<T>
    {
        private PropertyInfo[] properties;
        private string[] propertiesNames;
        private string[] propertiesNamesWithScope;
        private string typeName;
        public GenerateMethods()
        {
            var type = typeof(T);
            properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            propertiesNamesWithScope = properties.Select(a => "[" + a.Name + "]").ToArray();
            propertiesNames = properties.Select(a => "" + a.Name + "").ToArray();
            typeName = type.Name;
        }

        public string GeneratePartInsert(string key, T entity, bool allowNulls = true)
        {
            List<string> pksFields = new List<string>();
            if (!allowNulls)
            {
                foreach (var item in propertiesNames)
                {
                    if (GetPropValue(entity, item) != null)
                    {
                        pksFields.Add(item);
                    }
                }
            }
            else
                pksFields = propertiesNames.ToList();
            pksFields.Remove(key);
            var result = string.Empty;

            var sb = new StringBuilder($"INSERT INTO {typeName} (");
            var propertiesNamesDef = pksFields.Select(a=> "[" + a + "]").ToArray();
         
            string camps = string.Join(",", propertiesNamesDef);

            sb.Append($"{camps}) VALUES (");
            propertiesNamesDef = pksFields.Select(a => a).ToArray();
            string[] parametersCampsCol = propertiesNamesDef.Select(a => $"@{a}").ToArray();

            string campsParameter = string.Join(",", parametersCampsCol);

            sb.Append($"{campsParameter})");

            result = sb.ToString();

            return result;
        }

        public string GenerateSelect()
        {
            var result = string.Empty;

            var sb = new StringBuilder("SELECT ");

            string separator = $",{Environment.NewLine}";

            string selectPart = string.Join(separator, propertiesNamesWithScope);

            sb.AppendLine(selectPart);

            string fromPart = $"FROM {typeName}";

            sb.Append(fromPart);

            result = sb.ToString();

            return result;
        }

        public string GenerateDelete(string ParamName)
        {
            var where = GenerateById(ParamName);

            var result = $"DELETE FROM {typeName} {where} ";

            return result;
        }

        public string GenerateUpdate(string key, T entity, bool allowNulls = true)
        {
            List<string> pksFields = new List<string>();
            if (!allowNulls)
            {
                foreach (var item in propertiesNames)
                {
                    if (GetPropValue(entity, item) != null)
                    {
                        pksFields.Add(item);
                    }
                }
            }
            else
                pksFields = propertiesNames.ToList();
            pksFields.Remove(key);
            var sb = new StringBuilder($"UPDATE {typeName} SET ");

            var propertiesSet = pksFields.Select(a => $"[{a}] = @{a}").ToArray();

            var strSet = string.Join(",", propertiesSet);

            var where = GenerateById(key);

            sb.Append($" {strSet} {where} ");

            var result = sb.ToString();

            return result;
        }
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public string GenerateSelects()
        {
            var initialSelect = GenerateSelect();
            var where = GenerateWhere();
            var result = $" {initialSelect} {where}";
            return result;
        }


        private string GenerateWhere()
        {
            var filtersPksFields = properties.Select(a => a.Name).ToArray();
            if (!filtersPksFields?.Any() ?? true) throw new ArgumentException($"El parameter filtersPks isn't valid. This parameter must be a class type");
            var propertiesWhere = filtersPksFields.Select(a => $"{a} = @{a}").ToArray();
            var strWhere = string.Join(" AND ", propertiesWhere);
            var result = $" WHERE {strWhere} ";
            return result;
        }

        private string GenerateWhere(string param)
        {
            var filtersPksFields = typeof(T).GetType().GetProperties().Where(t=>t.Name == param).Select(a => a.Name).ToArray();
            if (!filtersPksFields?.Any() ?? true) throw new ArgumentException($"El parameter filtersPks isn't valid. This parameter must be a class type", nameof(T));
            var propertiesWhere = filtersPksFields.Select(a => $"{a} = @{a}").ToArray();
            var strWhere = string.Join(" AND ", propertiesWhere);
            var result = $" WHERE {strWhere} ";
            return result;
        }


        public string GenerateById(string FilterName)
        {
            return " WHERE " + FilterName + " = @" + FilterName;
        }
    }
}
