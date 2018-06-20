/*
 * 2018年6月20日 16:45:02 郑少宝
 */
using System;
using System.Collections.Generic;

namespace zsbApp.SplitSql
{  
    public class SplitSql
    {
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="exsql">执行脚本</param>
        /// <returns></returns>
        public SplitSqlModel Split(string exsql)
        {
            exsql = exsql.ToLower().TrimStart();
            if (exsql.StartsWith("insert"))
                return splitInsert(exsql);
            return splitUpdate(exsql);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="exsql">执行脚本</param>
        /// <returns></returns>
        private SplitSqlModel splitInsert(string exsql)
        {
            SplitSqlModel result = new SplitSqlModel();

            string[] words = exsql.Split(new string[] { "values" }, StringSplitOptions.None);

            //处理插入的字段
            string temp = words[0].Replace("insert", "").Replace("into", "").Trim();
            var fields = temp.Split(',');
            var a = fields[0].Split('(');
            result.Table = a[0];
            fields[0] = a[1];
            fields[fields.Length - 1] = fields[fields.Length - 1].Remove(fields[fields.Length - 1].Length - 1, 1);//移除最后一个括号
            
            //处理字段的值
            temp = words[1].TrimStart().TrimEnd().Remove(0, 1);
            if (temp.EndsWith(";"))
                temp = temp.Remove(temp.Length - 1, 1);
            if (temp.EndsWith(")"))
                temp = temp.Remove(temp.Length - 1, 1);
            var values = temp.Split(',');
            if (values.Length == fields.Length)//如果这两个相等，说明，插入的列值里边没有逗号
            {
                result.Data = new Dictionary<string, string>();
                for (int i = 0; i < fields.Length; i++)
                {
                    result.Data.Add(fields[i], values[i]);
                }
            }
            else
            {
 
            }


            return result;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="exsql">执行脚本</param>
        /// <returns></returns>
        private SplitSqlModel splitUpdate(string exsql)
        {
            SplitSqlModel result = new SplitSqlModel();
            string[] words = exsql.Split(new string[] { "where" }, StringSplitOptions.None);

            //处理插入的字段
            string[] words2 = words[0].Split(new string[] { "set" }, StringSplitOptions.None);
            int x = 1;
            result.Table = words2[0].Replace("update", "").Trim();
            string[] words3 = words2[1].Split(',');
            List<string> list = new List<string>();
            foreach (var item in words3)
            {
                if (item.Contains("="))
                {
                    list.Add(item);
                }
                else
                {
                    list[list.Count - 1] = list[list.Count - 1] + "," + item;
                }

            }
            result.Data = new Dictionary<string, string>();
            foreach (var item in list)
            {
                if (item.Contains("="))
                {
                    var v = item.Split('=');
                    string key = v[0];
                    string value = v[1];
                    for (int i = 2; i < v.Length; i++)
                    {
                        value = value + "=" + v[i];
                    }
                    result.Data.Add(key, value);
                }

            }
            x = 1;

            


            return result;
        }
    }
}
