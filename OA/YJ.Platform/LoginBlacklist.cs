using System;
using System.Collections.Generic;
using System.Text;
using YJ.Data.Model;

namespace YJ.Platform
{
    public class LoginBlacklist
    {
        private YJ.Data.Interface.ILoginBlacklist dataLogblacklist;
        private static YJ.Data.Interface.ILoginBlacklist dataLogblacklist1 = Data.Factory.Factory.GetLoginBlacklist();
        private delegate void dgWriteLoginBlacklist(YJ.Data.Model.LoginBlacklist login);
        public LoginBlacklist()
        {
            this.dataLogblacklist = dataLogblacklist1;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.LoginBlacklist model)
        {
            return dataLogblacklist.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.LoginBlacklist> GetAll()
        {
            return dataLogblacklist.GetAll();
        }

        /// <summary>
        /// 通过IP查询单条记录
        /// </summary>
        public YJ.Data.Model.LoginBlacklist GetByIPaddress(string IPaddress)
        {
            return IPaddress.IsNullOrEmpty() ? null : dataLogblacklist.GetByIPaddress(IPaddress);
        }

        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.LoginBlacklist Get(Guid id)
        {
            return dataLogblacklist.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataLogblacklist.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataLogblacklist.GetCount();
        }



        /// <summary>
        /// 新增
        /// </summary>
        private static void add(YJ.Data.Model.LoginBlacklist model)
        {
            dataLogblacklist1.Add(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public static void Add(YJ.Data.Model.LoginBlacklist model)
        {
            dgWriteLoginBlacklist wl = new dgWriteLoginBlacklist(add);
            wl.BeginInvoke(model, null, null);
        }


        /// <summary>
        /// 得到一页黑名单数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="number"></param>
        /// <param name="ID"></param>
        /// <param name="IPaddress"></param>
        /// <param name="Account"></param>
        /// <param name="BlockTime"></param>
        /// <returns></returns>
        public System.Data.DataTable GetPagerData(out string pager, string query = "", string ID = "", string IPaddress = "", string Account = "", string BlockTime = "")
        {
            return dataLogblacklist.GetPagerData(out pager, query, YJ.Utility.Tools.GetPageSize(), YJ.Utility.Tools.GetPageNumber(),
                ID, IPaddress, Account, BlockTime);
        }

        /// <summary>
        /// 得到一页黑名单数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="number"></param>
        /// <param name="ID"></param>
        /// <param name="IPaddress"></param>
        /// <param name="Account"></param>
        /// <param name="BlockTime"></param>
        
        /// <returns></returns>
        public System.Data.DataTable GetPagerData(out long count, int size = 15, int number = 1, string ID = "", string IPaddress = "", string Account = "", string BlockTime = "", string order = "")
        {
            return dataLogblacklist.GetPagerData(out count, size, number, ID, IPaddress, Account, BlockTime, order);
        }
    }
}
