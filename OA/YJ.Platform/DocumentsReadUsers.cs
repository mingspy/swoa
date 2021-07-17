using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class DocumentsReadUsers
    {
        private YJ.Data.Interface.IDocumentsReadUsers dataDocumentsReadUsers;
        public DocumentsReadUsers()
        {
            this.dataDocumentsReadUsers = YJ.Data.Factory.Factory.GetDocumentsReadUsers();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.DocumentsReadUsers model)
        {
            return dataDocumentsReadUsers.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.DocumentsReadUsers model)
        {
            return dataDocumentsReadUsers.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.DocumentsReadUsers> GetAll()
        {
            return dataDocumentsReadUsers.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.DocumentsReadUsers Get(Guid documentid, Guid userid)
        {
            return dataDocumentsReadUsers.Get(documentid, userid);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid documentid, Guid userid)
        {
            return dataDocumentsReadUsers.Delete(documentid, userid);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataDocumentsReadUsers.GetCount();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Guid documentid)
        {
            return dataDocumentsReadUsers.Delete(documentid);
        }

        /// <summary>
        /// 更新一个文档为已读
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="userID"></param>
        public void UpdateRead(Guid docID, Guid userID)
        {
            dataDocumentsReadUsers.UpdateRead(docID, userID);
        }
    }
}
