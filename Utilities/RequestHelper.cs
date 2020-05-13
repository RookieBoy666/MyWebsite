using System;
using System.Collections.Specialized;
using System.Reflection;

namespace Utilities
{
    public static class RequestHelper
    {
        /// <summary>
        /// 把集合转换为对象，（表单数据转为对象）
        /// </summary>
        /// <typeparam name="T">引用类型对象才能把值传出</typeparam>
        /// <param name="t"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int FormToModel<T>(T t, NameValueCollection form)
        {
            int result = 0;

            Type type = t.GetType();//获取类型

            PropertyInfo[] pi = type.GetProperties();//获取属性集合

            foreach (PropertyInfo p in pi)
            {
                try
                {
                    var s = form[p.Name];
                    if (!string.IsNullOrEmpty(form[p.Name]))
                    {
                       
                        p.SetValue(t, Convert.ChangeType(form[p.Name], p.PropertyType), null);
                        result++;
                    }
                }
                catch (Exception e)
                { }
            }

            return result;
        }

        /// <summary>
        /// 返回DataGrid格式的json字符串
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string ResponseGridJSON(string rows, int total)
        {
            return "{\"total\":" + total + ",\"rows\":" + rows + "}";
        }
    }
}
