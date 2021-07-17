using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class AppLibraryButtons
    {
        private YJ.Data.Interface.IAppLibraryButtons dataAppLibraryButtons;
        public AppLibraryButtons()
        {
            this.dataAppLibraryButtons = YJ.Data.Factory.Factory.GetAppLibraryButtons();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.AppLibraryButtons model)
        {
            return dataAppLibraryButtons.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.AppLibraryButtons model)
        {
            return dataAppLibraryButtons.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.AppLibraryButtons> GetAll()
        {
            return dataAppLibraryButtons.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.AppLibraryButtons Get(Guid id)
        {
            return dataAppLibraryButtons.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataAppLibraryButtons.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAppLibraryButtons.GetCount();
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="numbe"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out string pager, string query = "", string title = "")
        {
            return dataAppLibraryButtons.GetPagerData(out pager, query, Utility.Tools.GetPageSize(), Utility.Tools.GetPageNumber(), title);
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out long count, int size, int number, string title = "", string order = "")
        {
            return dataAppLibraryButtons.GetPagerData(out count, size, number, title, order);
        }

        /// <summary>
        /// 得到最大编号
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort()
        {
            return dataAppLibraryButtons.GetMaxSort();
        }

        public string GetAllJson()
        {
            var list = GetAll();
            LitJson.JsonData jd1 = new LitJson.JsonData();
            foreach (var li in list)
            {
                LitJson.JsonData jd = new LitJson.JsonData();
                jd["id"] = li.ID.ToString();
                jd["name"] = li.Name;
                jd["events"] = li.Events;
                jd["ico"] = li.Ico;
                jd["sort"] = li.Sort;
                jd1.Add(jd);
            }
            return jd1.ToJson();
        }

        public string GetShowTypeOptions(string value = "")
        {
            Dictionary<int, string> dicts = new Dictionary<int, string>();
            dicts.Add(1, "普通按钮");
            dicts.Add(0, "工具栏按钮");
            dicts.Add(2, "列表按钮");
            StringBuilder sb = new StringBuilder();
            foreach (var dict in dicts)
            {
                sb.Append("<option value=\"" + dict.Key + "\"" + (dict.Key.ToString() == value ? " selected=\"selected\"" : "") + ">" + dict.Value + "</option>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到按钮选项
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetOptions(string value="")
        {
            var list = GetAll();
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"\"></option>");
            foreach (var li in list)
            {
                sb.Append("<option value=\"" + li.ID + "\"" + (li.ID == value.ToGuid() ? " selected=\"selected\"" : "") + ">" + li.Name + "</option>");
            }
            return sb.ToString();
        }
    }
}
