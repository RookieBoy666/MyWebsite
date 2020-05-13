using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace Utilities
{
    public static class JSONHelper
    {
        /// <summary>
        /// 对象转为JSON格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson<T>(T t)
        {
            //return new JavaScriptSerializer().Serialize(t);

            return JsonConvert.SerializeObject(t);
            //return JObject.FromObject(t).ToString();

        }

        public static string JavaScriptToJson<T>(T t)
        {
            return new JavaScriptSerializer().Serialize(t);
        }

        /// <summary>
        /// JSON格式转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T FromJson<T>(string str)
        {
            return new JavaScriptSerializer().Deserialize<T>(str);
        }

        /// <summary>
        /// 格式化EASYUI DATAGRID JSON
        /// </summary>
        /// <param name="recordcount">总记录数</param>
        /// <param name="rows">每页记录的JSON格式</param>
        /// <returns></returns>
        public static string FormatJSONForEasyuiDataGrid(int recordcount, object rowsList)
        {
            return ToJson(new { total = recordcount, rows = rowsList });
        }

        /// <summary>
        /// 获取树格式对象
        /// </summary>
        /// <param name="list">线性数据</param>
        /// <param name="id">ID的字段名</param>
        /// <param name="pid">PID的字段名</param>
        /// <returns></returns>
        public static object ArrayToTreeData(IList<Hashtable> list, string id, string pid)
        {
            var h = new Hashtable(); //数据索引 
            var r = new List<Hashtable>(); //数据池,要返回的 
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                h[item[id].ToString()] = item;
            }
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                if (!item.ContainsKey(pid) || item[pid] == null || !h.ContainsKey(item[pid].ToString()))
                {
                    r.Add(item);
                }
                else
                {
                    var pitem = h[item[pid].ToString()] as Hashtable;
                    if (!pitem.ContainsKey("children"))
                        pitem["children"] = new List<Hashtable>();
                    var children = pitem["children"] as List<Hashtable>;
                    children.Add(item);
                }
            }
            return r;
        }

        /// <summary>
        /// 获取树格式对象
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="id">ID的字段名</param>
        /// <param name="pid">pid的字段名</param>
        /// <returns></returns>
        public static object DataTableToTreeData(DataTable dt, string id, string pid)
        {
            IList<Hashtable> list = DbTableToHash(dt);

            return ArrayToTreeData(list, id, pid);

        }


        public static string DataTableToTreeDataString(DataTable dt, string id, string pid)
        {
            return new JavaScriptSerializer().Serialize(DataTableToTreeData(dt, id, pid));
        }

        /// <summary>
        /// 将db reader转换为Hashtable列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IList<Hashtable> DbReaderToHash(IDataReader reader)
        {
            using (reader)
            {
                var list = new List<Hashtable>();
                while (reader.Read())
                {
                    var item = new Hashtable();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        var value = reader[i];
                        item[name] = value;
                    }
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 将DataTable转换为Hashtable列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<Hashtable> DbTableToHash(DataTable dt)
        {
            IList<Hashtable> list = new List<Hashtable>();



            foreach (DataRow row in dt.Rows)
            {
                Hashtable item = new Hashtable();

                for (int i = 0, len = dt.Columns.Count; i < len; i++)
                {
                    item[dt.Columns[i].ColumnName] = row[i];
                }
                list.Add(item);

            }

            return list;
        }

        /// <summary>
        /// 把DataTable表中的数据转换为JSON格式数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            IList<Hashtable> list = DbTableToHash(dt);

            return ToJson<IList<Hashtable>>(list);
        }
        /// <summary>
        /// 把Ilist<t>表中的数据转换为JSON格式数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list)
        {
            // IList<Hashtable> list = DbTableToHash(dt);

            return ToJson<IList<T>>(list);
        }

        /// <summary>
        /// DataTable转为JSON格式，通过拼接字符串方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTable2Json(DataTable dt)
        {
            if (dt.Rows.Count < 1)
                return "";
            StringBuilder sb = new StringBuilder("[");

            int iCols = dt.Columns.Count;

            //表字段存放的数组
            string[] aColName = new string[iCols];
            for (int i = 0; i < iCols; i++)
                aColName[i] = dt.Columns[i].ColumnName;

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("{");

                for (int i = 0; i < iCols - 1; i++)
                {
                    sb.AppendFormat("\"{0}\":\"{1}\",", aColName[i], row[i].ToString());
                }
                sb.AppendFormat("\"{0}\":\"{1}\"", aColName[iCols - 1], row[iCols - 1].ToString());

                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',') + "]";
        }

        /// <summary>
        /// 返回EasyUI中DataGrid格式数据
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToDataGrid(int total, string data)
        {
            return null;
        }
        /// <summary>
        /// 根据DataTable生成Json树表格结构
        /// </summary>
        /// <param name="tabel">数据源</param>
        /// <param name="rela">关系字段(字典表中的树结构字段)</param>
        public static string GetTreeGridJsonByTable(DataTable dt, string rela)
        {
            if (dt.Rows.Count < 1)
                return "";
            StringBuilder sb = new StringBuilder("[");

            int iCols = dt.Columns.Count;

            //表字段存放的数组
            string[] aColName = new string[iCols];
            for (int i = 0; i < iCols; i++)
                aColName[i] = dt.Columns[i].ColumnName;

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("{");

                for (int i = 0; i < iCols - 1; i++)
                {
                    if (aColName[i] == rela)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", "_parentId", row[i].ToString());
                    }
                    else
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", aColName[i], row[i].ToString());
                    }
                }
                sb.AppendFormat("\"{0}\":\"{1}\"", aColName[iCols - 1], row[iCols - 1].ToString());

                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',') + "]";
        }



        /// <summary>
        /// 根据DataTable生成Json树结构
        /// </summary>
        /// <param name="tabel">数据源</param>
        /// <param name="idCol">ID列</param>
        /// <param name="txtCol">Text列</param>
        /// <param name="rela">关系字段(字典表中的树结构字段)</param>
        /// <param name="pId">父ID(0)</param>


        public static string GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId, string state = "open")
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            if (tabel.Rows.Count > 0)
            {
                sb.Append("[");

                string filer = string.Format("{0}='{1}'", rela, pId);
                if (pId == null)
                {
                    filer = string.Format("{0} is null", rela);
                }
                DataRow[] rows = tabel.Select(filer);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\"");
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            sb.Append(",\"children\":");
                            sb.Append(GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol]));

                        }

                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");

            }
            return sb.ToString();
        }
        public static string GetTreeJsonByTableSelect(DataTable tabel, string idCol, string txtCol, string rela, object pId, string state = "open")
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            if (tabel.Rows.Count > 0)
            {
                sb.Append("[");
                sb.Append("{\"id\":\"\",\"text\":\"请选择\",\"state\":\"open\"},");

                string filer = string.Format("{0}='{1}'", rela, pId);
                if (pId == null)
                {
                    filer = string.Format("{0} is null", rela);
                }
                DataRow[] rows = tabel.Select(filer);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"" + state + "\"");
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            sb.Append(",\"children\":");
                            sb.Append(GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol]));

                        }

                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");

            }
            return sb.ToString();
        }
        public static string GetTreeJsonByList<T>(List<T> obj, string idCol, string txtCol, string rela, object pId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            if (obj.Count > 0)
            {
                DataTable tabel = ToDataTable<T>(obj);
                sb.Append("[");
                string filer = string.Format("{0}='{1}'", rela, pId);
                if (pId == null)
                {
                    filer = string.Format("{0} is null", rela);
                }
                DataRow[] rows = tabel.Select(filer);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\"");
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            sb.Append(",\"children\":");
                            sb.Append(GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol]));

                        }

                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");

            }
            return sb.ToString();
        }

        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        /// <summary>
        /// 对象集合转换Json
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";

            foreach (object item in array)
            {

                jsonString += ToJson(item) + ",";

            }

            jsonString.Remove(jsonString.Length - 1, jsonString.Length);

            return jsonString + "]";

        }
        /// <summary> 
        /// 对象转换为Json 
        /// </summary> 
        /// <param name="jsonObject">对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }
    }
}
