using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace YJ.Platform
{
    public class HomeItems
    {
        private Dictionary<int, string> types = new Dictionary<int, string>();
        private Dictionary<int, string> dstypes = new Dictionary<int, string>();
        private YJ.Data.Interface.IHomeItems dataHomeItems;
        private string cacheKey = string.Empty;
        public HomeItems()
        {
            cacheKey = Utility.Keys.CacheKeys.HomeItems.ToString();
            this.dataHomeItems = YJ.Data.Factory.Factory.GetHomeItems();
            types.Add(0, "顶部统计");
            types.Add(1, "左边列表");
            types.Add(2, "右边列表");

            dstypes.Add(0, "SQL语句");
            dstypes.Add(1, "C#方法");
            dstypes.Add(2, "URL地址");
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.HomeItems model)
        {
            return dataHomeItems.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.HomeItems model)
        {
            return dataHomeItems.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.HomeItems> GetAll()
        {
            return dataHomeItems.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.HomeItems Get(Guid id)
        {
            return dataHomeItems.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataHomeItems.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataHomeItems.GetCount();
        }

        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.HomeItems> GetList(out string pager, string query = "", string name = "", string title = "", string type = "")
        {
            return dataHomeItems.GetList(out pager, query, name, title, type);
        }

        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.HomeItems> GetList(out long count, int size, int number, string name = "", string title = "", string type = "", string order = "")
        {
            return dataHomeItems.GetList(out count, size, number, name, title, type, order);
        }

        /// <summary>
        /// 得到类型下拉选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getTypeOptions(string value = "")
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var dict in types)
            {
                sb.Append("<option value=\"" + dict.Key.ToString() + "\"" + (dict.Key.ToString().Equals(value) ? " selected=\"selected\"" : "") + ">" + dict.Value + "</option>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到数据来源下拉选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getDataSourceOptions(string value = "")
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var dict in dstypes)
            {
                sb.Append("<option value=\"" + dict.Key.ToString() + "\"" + (dict.Key.ToString().Equals(value) ? " selected=\"selected\"" : "") + ">" + dict.Value + "</option>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据类型得到标题
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTypeTitle(int type)
        {
            return types.ContainsKey(type) ? types[type] : "";
        }

        /// <summary>
        /// 根据数据来源得到标题
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetDataSourceTitle(int type)
        {
            return dstypes.ContainsKey(type) ? dstypes[type] : "";
        }

        /// <summary>
        /// 得到数据源显示字符串
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetDataSourceString(Data.Model.HomeItems item)
        {
            if (item == null)
            {
                return "";
            }
            switch (item.DataSourceType)
            { 
                case 0:
                    if (item.DBConnID.HasValue)
                    {
                        return getSqlString(item.DataSource, item.Type, item.DBConnID.Value);
                    }
                    else
                    {
                        return "";
                    }
                case 1:
                    return GetCsharpMethodString(item.DataSource);
                case 2:
                    return GetUrlString(item.DataSource);
            }
            return "";
        }
        /// <summary>
        /// 得到sql字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string getSqlString(string sql, int type, Guid dbconnID)
        {
            switch (type)
            { 
                case 0:
                    return new DBConnection().GetFieldValue(dbconnID, Wildcard.FilterWildcard(sql));
                case 1:
                case 2:
                    DataTable dt = new DBConnection().GetDataTable(dbconnID, Wildcard.FilterWildcard(sql));
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"hometable\"><thead><tr>");
                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        sb.Append("<th>" + dc.ColumnName + "</th>");
                    }
                    sb.Append("</tr></thead><tbody>");
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            sb.Append("<td>" + dr[dc.ColumnName].ToString() + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table>");
                    return sb.ToString();
            }
            return "";
        }

        /// <summary>
        /// 得到c#方法字符串
        /// </summary>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetCsharpMethodString(string method, object param=null)
        {
            object obj = Utility.Tools.ExecuteCsharpMethod(method, param);
            return obj == null ? "" : obj.ToString();
        }

        /// <summary>
        /// 得到URL输出字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetUrlString(string url)
        {
            return Utility.HttpHelper.SendGet(url);
        }

        /// <summary>
        /// 得到一个用户的首页模块
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Data.Model.HomeItems> GetAllByUserID(Guid userID)
        {
            var obj = Cache.IO.Opation.Get(cacheKey);
            List<Data.Model.HomeItems> list = new List<Data.Model.HomeItems>();
            if (obj != null && (obj is List<Data.Model.HomeItems>))
            {
                list = (List<Data.Model.HomeItems>)obj;
            }
            else
            {
                Organize borg = new Organize();
                list = GetAll();
                foreach (var li in list)
                {
                    li.UseUsers = borg.GetAllUsersIdString(li.UseOrganizes);
                }
                Cache.IO.Opation.Set(cacheKey, list);
            }
            return list.FindAll(p => p.UseUsers.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase)).OrderBy(p => p.Type).ThenBy(p => p.Sort).ToList();
        }

        /// <summary>
        /// 得到最大排序号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetMaxSort(int type)
        {
            return dataHomeItems.GetMaxSort(type);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            Cache.IO.Opation.Remove(cacheKey);
        }
    }
}
