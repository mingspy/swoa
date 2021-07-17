using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace YJ.Platform
{
    public class Documents
    {
        private YJ.Data.Interface.IDocuments dataDocuments;
        public Documents()
        {
            this.dataDocuments = YJ.Data.Factory.Factory.GetDocuments();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.Documents model)
        {
            return dataDocuments.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.Documents model)
        {
            return dataDocuments.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Documents> GetAll()
        {
            return dataDocuments.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.Documents Get(Guid id)
        {
            return dataDocuments.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataDocuments.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataDocuments.GetCount();
        }

        /// <summary>
        /// 得到一页列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetList(out string pager, string dirID, string userID, string query = "", string title = "", string date1 = "", string date2 = "", bool isNoRead = false)
        {
            return dataDocuments.GetList(out pager, dirID, userID, query, title, date1, date2, isNoRead);
        }

        /// <summary>
        /// 得到一页列表
        /// </summary>
        /// <param name="dirID">栏目ID，多个栏目逗号分开</param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetList(out long count, int size, int number, string dirID, string userID, string title = "", string date1 = "", string date2 = "", bool isNoRead = false, string order = "")
        {
            return dataDocuments.GetList(out count, size, number, dirID, userID, title, date1, date2, isNoRead, order);
        }

        /// <summary>
        /// 更新阅读次数+1
        /// </summary>
        /// <param name="id"></param>
        public void UpdateReadCount(Guid id)
        {
            dataDocuments.UpdateReadCount(id);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByDirectoryID(Guid directoryID)
        {
            return dataDocuments.DeleteByDirectoryID(directoryID);
        }
    }
}
