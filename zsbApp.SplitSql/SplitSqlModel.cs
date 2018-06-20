/*
 * 2018年6月20日 16:45:02 郑少宝
 */
using System.Collections.Generic;

namespace zsbApp.SplitSql
{
    public class SplitSqlModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// 字段数据
        /// 注意 用单引号的是常量数据，没有单引号的是可能内置函数
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}
