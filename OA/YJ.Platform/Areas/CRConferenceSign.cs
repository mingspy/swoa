using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Platform.Areas
{
    public class CRConferenceSign
    {
        private YJ.Data.Interface.Areas.ICRConferenceSign dataCRConferenceSign;
        public CRConferenceSign()
        {
            this.dataCRConferenceSign =YJ.Data.Factory.Factory.GetCRConferenceSign();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.Areas.CRConferenceSign model)
        {
            return dataCRConferenceSign.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.Areas.CRConferenceSign model)
        {
            return dataCRConferenceSign.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Areas.CRConferenceSign> GetAll()
        {
            return dataCRConferenceSign.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.Areas.CRConferenceSign Get(int id)
        {
            return dataCRConferenceSign.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(int id)
        {
            return dataCRConferenceSign.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataCRConferenceSign.GetCount();
        }
    }
}
