using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class UsersRelation
    {
        private YJ.Data.Interface.IUsersRelation dataUsersRelation;
        public UsersRelation()
        {
            this.dataUsersRelation = Data.Factory.Factory.GetUsersRelation();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.UsersRelation model)
        {
            return dataUsersRelation.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.UsersRelation model)
        {
            return dataUsersRelation.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.UsersRelation> GetAll()
        {
            return dataUsersRelation.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.UsersRelation Get(Guid userid, Guid organizeid)
        {
            return dataUsersRelation.Get(userid, organizeid);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid userid, Guid organizeid)
        {
            if (Utility.Config.IsDemo)
            {
                return 0;
            }
            return dataUsersRelation.Delete(userid, organizeid);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataUsersRelation.GetCount();
        }
        /// <summary>
        /// 查询一个岗位下所有记录
        /// </summary>
        public List<YJ.Data.Model.UsersRelation> GetAllByOrganizeID(Guid organizeID)
        {
            return dataUsersRelation.GetAllByOrganizeID(organizeID);
        }
        /// <summary>
        /// 查询一个用户所有记录
        /// </summary>
        public List<YJ.Data.Model.UsersRelation> GetAllByUserID(Guid userID)
        {
            return dataUsersRelation.GetAllByUserID(userID);
        }
        /// <summary>
        /// 查询一个用户主要岗位
        /// </summary>
        public YJ.Data.Model.UsersRelation GetMainByUserID(Guid userID)
        {
            return dataUsersRelation.GetMainByUserID(userID);
        }
        /// <summary>
        /// 删除一个用户记录
        /// </summary>
        public int DeleteByUserID(Guid userID)
        {
            if (Utility.Config.IsDemo)
            {
                return 0;
            }
            return dataUsersRelation.DeleteByUserID(userID);
        }
        /// <summary>
        /// 删除一个用户的兼职记录
        /// </summary>
        public int DeleteNotIsMainByUserID(Guid userID)
        {
            return dataUsersRelation.DeleteNotIsMainByUserID(userID);
        }
        /// <summary>
        /// 删除一个机构下所有记录
        /// </summary>
        public int DeleteByOrganizeID(Guid organizeID)
        {
            return dataUsersRelation.DeleteByOrganizeID(organizeID);
        }
        /// <summary>
        /// 得到最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort(Guid organizeID)
        {
            return dataUsersRelation.GetMaxSort(organizeID);
        }

        

    }
}
