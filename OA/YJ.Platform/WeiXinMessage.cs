using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class WeiXinMessage
    {
        private YJ.Data.Interface.IWeiXinMessage dataWeiXinMessage;
        public WeiXinMessage()
        {
            this.dataWeiXinMessage = YJ.Data.Factory.Factory.GetWeiXinMessage();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.WeiXinMessage model)
        {
            return dataWeiXinMessage.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.WeiXinMessage model)
        {
            return dataWeiXinMessage.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WeiXinMessage> GetAll()
        {
            return dataWeiXinMessage.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.WeiXinMessage Get(Guid id)
        {
            return dataWeiXinMessage.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataWeiXinMessage.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataWeiXinMessage.GetCount();
        }
    }
}
