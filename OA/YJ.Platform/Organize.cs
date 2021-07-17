using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YJ.Platform
{
    public class Organize
    {
        
        private YJ.Data.Interface.IOrganize dataOrganize;
        public Organize()
        {
            this.dataOrganize = Data.Factory.Factory.GetOrganize();
        }

        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.Organize model)
        {
            return dataOrganize.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.Organize model)
        {
            return dataOrganize.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Organize> GetAll()
        {
            return dataOrganize.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.Organize Get(Guid id)
        {
            return dataOrganize.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            if (Utility.Config.IsDemo)
            {
                return 0;
            }
            return dataOrganize.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataOrganize.GetCount();
        }

        /// <summary>
        /// 根据根记录
        /// </summary>
        public YJ.Data.Model.Organize GetRoot()
        {
            return dataOrganize.GetRoot();
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<YJ.Data.Model.Organize> GetChilds(Guid ID)
        {
            return dataOrganize.GetChilds(ID);
        }

        /// <summary>
        /// 机构类型
        /// </summary>
        private Dictionary<int,string> types
        {
            get
            {
                var dict = new Dictionary<int, string>();
                dict.Add(1, "单位");
                dict.Add(2, "部门");
                dict.Add(3, "岗位");
                return dict;
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        private Dictionary<int, string> status
        {
            get
            {
                var dict = new Dictionary<int, string>();
                dict.Add(0, "正常");
                dict.Add(1, "冻结");
                return dict;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private Dictionary<int, string> sexs
        {
            get
            {
                var dict = new Dictionary<int, string>();
                dict.Add(0, "男");
                dict.Add(1, "女");
                return dict;
            }
        }

        /// <summary>
        /// 得到类型选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetTypeRadio(string name, string value = "", string attributes="")
        {
            StringBuilder radios = new StringBuilder();
            foreach (var type in types)
            {
                radios.AppendFormat("<input type=\"radio\" style=\"vertical-align:middle;\" value=\"{0}\" id=\"orgtypes_{0}\" {1} name=\"{2}\" {3} /><label style=\"vertical-align:middle;\" for=\"orgtypes_{0}\">{4}</label>",
                    type.Key,
                    type.Key.ToString() == value ? "checked=\"checked\"" : "",
                    name,
                    attributes,
                    type.Value);
            }
            return radios.ToString();
        }

        /// <summary>
        /// 得到状态选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetStatusRadio(string name, string value = "", string attributes = "")
        {
            StringBuilder radios = new StringBuilder();
            foreach (var statu in status)
            {
                radios.AppendFormat("<input type=\"radio\" style=\"vertical-align:middle;\" value=\"{0}\" id=\"orgstatus_{0}\" {1} name=\"{2}\" {3}/><label style=\"vertical-align:middle;\" for=\"orgstatus_{0}\">{4}</label>",
                    statu.Key,
                    statu.Key.ToString() == value ? "checked=\"checked\"" : "",
                    name,
                    attributes,
                    statu.Value);
            }
            return radios.ToString();
        }

        /// <summary>
        /// 得到性别选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetSexRadio(string name, string value = "", string attributes = "")
        {
            StringBuilder radios = new StringBuilder();
            foreach (var sex in sexs)
            {
                radios.AppendFormat("<input type=\"radio\" style=\"vertical-align:middle;\" value=\"{0}\" id=\"sexstatus_{0}\" {1} name=\"{2}\" {3}/><label style=\"vertical-align:middle;\" for=\"sexstatus_{0}\">{4}</label>",
                    sex.Key,
                    sex.Key.ToString() == value ? "checked=\"checked\"" : "",
                    name,
                    attributes,
                    sex.Value);
            }
            return radios.ToString();
        }

        /// <summary>
        /// 得到一个父级下的最大排序值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort(Guid id)
        {
            return dataOrganize.GetMaxSort(id);
        }

        /// <summary>
        /// 得到一个机构下的所有人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Users> GetAllUsers(Guid id)
        {
            var childs = GetAllChilds(id);
            List<Guid> ids = new List<Guid>();
            ids.Add(id);
            foreach (var child in childs)
            {
                ids.Add(child.ID);
            }
            return new Users().GetAllByOrganizeIDArray(ids.ToArray());
        }

        /// <summary>
        /// 得到一组机构字符串下所有人员
        /// </summary>
        /// <param name="idString"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Users> GetAllUsers(string idString)
        {
            if (idString.IsNullOrEmpty())
            {
                return new List<YJ.Data.Model.Users>();
            }
            string[] idArray = idString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<YJ.Data.Model.Users> userList = new List<YJ.Data.Model.Users>();
            Users busers = new Users();
            WorkGroup bwg = new WorkGroup();
           
            foreach (string id in idArray)
            {
                if (id.StartsWith(Users.PREFIX))//人员
                {
                    userList.Add(busers.Get(Users.RemovePrefix(id).ToGuid()));
                }
                else if (id.IsGuid())//机构
                {
                    userList.AddRange(GetAllUsers(id.ToGuid()));
                }
                else if (id.StartsWith(WorkGroup.PREFIX))//角色组
                {
                    addWorkGroupUsers(userList, WorkGroup.RemovePrefix(id).ToGuid());
                }
            }
            userList.RemoveAll(p => p == null);
            return userList.Distinct(new UsersEqualityComparer()).ToList();
        }

        private void addWorkGroupUsers(List<YJ.Data.Model.Users> userList, Guid workGroupID)
        {
            var wg = new WorkGroup().Get(workGroupID);
            if (wg == null || wg.Members.IsNullOrEmpty())
            {
                return;
            }
            string[] idArray = wg.Members.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Users busers = new Users();
            WorkGroup bwg = new WorkGroup();
            foreach (var id in idArray)
            {
                if (id.StartsWith(Users.PREFIX))//人员
                {
                    userList.Add(busers.Get(Users.RemovePrefix(id).ToGuid()));
                }
                else if (id.IsGuid())//机构
                {
                    userList.AddRange(GetAllUsers(id.ToGuid()));
                }
                else if (id.StartsWith(WorkGroup.PREFIX))//工作组
                {
                    addWorkGroupUsers(userList, WorkGroup.RemovePrefix(id).ToGuid());
                }
            }
        }

       
        /// <summary>
        /// 得到一组机构字符串下所有人员ID
        /// </summary>
        /// <param name="idString"></param>
        /// <returns></returns>
        public List<Guid> GetAllUsersIdList(string idString)
        {
            var users = GetAllUsers(idString);
            List<Guid> list = new List<Guid>();
            foreach (var user in users)
            {
                if (user != null)
                {
                    list.Add(user.ID);
                }
            }
            return list;
        }
        /// <summary>
        /// 得到一组机构字符串下所有人员ID
        /// </summary>
        /// <param name="idString"></param>
        /// <returns></returns>
        public List<Guid> GetAllUsersIdList(Guid id)
        {
            var users = GetAllUsers(id);
            List<Guid> list = new List<Guid>();
            foreach (var user in users)
            {
                if (user != null)
                {
                    list.Add(user.ID);
                }
            }
            return list;
        }

        /// <summary>
        /// 得到一个机构下的所有人员字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAllUsersIdString(Guid id)
        {
            StringBuilder sb = new StringBuilder();
            var idList = GetAllUsersIdList(id);
            foreach (var uid in idList)
            {
                sb.Append(uid);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 得到一个机构下的所有人员字符串
        /// </summary>
        /// <param name="idString"></param>
        /// <returns></returns>
        public string GetAllUsersIdString(string idString)
        {
            StringBuilder sb = new StringBuilder();
            var idList = GetAllUsersIdList(idString);
            foreach (var uid in idList)
            {
                sb.Append(uid);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 更新下级数
        /// </summary>
        /// <returns></returns>
        public int UpdateChildsLength(Guid id)
        {
            int i = 0;
            var org = Get(id);
            if (org == null)
            {
                return i;
            }
            i = GetChilds(id).Count;
            i += GetAllUsers(id).Count;
            dataOrganize.UpdateChildsLength(id, i);
            return i;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <returns></returns>
        public int UpdateSort(Guid id, int sort)
        {
            return dataOrganize.UpdateSort(id, sort);
        }

        /// <summary>
        /// 查询一个组织的所有上级
        /// </summary>
        public List<YJ.Data.Model.Organize> GetAllParent(string number)
        {
            return number.IsNullOrEmpty() ? new List<YJ.Data.Model.Organize>() : dataOrganize.GetAllParent(number);
        }

        /// <summary>
        /// 查询一个组织的所有上级
        /// </summary>
        public List<YJ.Data.Model.Organize> GetAllParent(Guid id)
        {
            var org = Get(id);
            if (org == null)
            {
                return new List<YJ.Data.Model.Organize>();
            }
            return dataOrganize.GetAllParent(org.Number);
        }

        /// <summary>
        /// 查询一个组织的所有下级
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public List<YJ.Data.Model.Organize> GetAllChilds(string number)
        {
            return number.IsNullOrEmpty() ? new List<YJ.Data.Model.Organize>() : dataOrganize.GetAllChild(number);
        }

        /// <summary>
        /// 查询一个组织的所有下级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Organize> GetAllChilds(Guid id)
        {
            var org = Get(id);
            if (org == null)
            {
                return new List<YJ.Data.Model.Organize>();
            }
            return dataOrganize.GetAllChild(org.Number);
        }

        /// <summary>
        /// 查询一个机构的所有上级名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="split">分隔字符串</param>
        /// <param name="reverse">是否倒置</param>
        /// <returns></returns>
        public string GetAllParentNames(Guid id, bool reverse = false, string split = " / ")
        {
            var parents = GetAllParent(id);
            if (reverse)
            {
                parents.Reverse();
            }
            StringBuilder names = new StringBuilder(parents.Count * 100);
            int i=0;
            foreach (var parent in parents)
            {
                names.Append(parent.Name);
                if (i++ < parents.Count - 1)
                {
                    names.Append(split);
                }
            }
            return names.ToString();
        }


        /// <summary>
        /// 将一个机构移动到另一个机构下
        /// </summary>
        /// <param name="fromID">机构ID</param>
        /// <param name="toID">要移动到的机构ID</param>
        /// <returns></returns>
        public bool Move(Guid fromID, Guid toID)
        {
            var from = Get(fromID);
            var to = Get(toID);
            if (from == null || to == null)
            {
                return false;
            }
            if (to.Number.StartsWith(from.Number, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                Guid oldParentID = from.ParentID;

                from.ParentID = toID;
                from.Depth = to.Depth + 1;
                from.Number = to.Number + "," + from.ID.ToString();
                Update(from);

                //更新微信
                if (WeiXin.Config.IsUse)
                {
                    new WeiXin.Organize().EditDept(from);
                }

                var childs = GetAllChilds(fromID).OrderBy(p => p.Depth);
                foreach (var child in childs)
                {
                    child.Number = Get(child.ParentID).Number + "," + child.ID.ToString();
                    child.Depth = child.Number.Split(',').Length - 1;
                    Update(child);
                }

                UpdateChildsLength(toID);
                UpdateChildsLength(oldParentID);
                scope.Complete();
                return true;
            }
        }

        /// <summary>
        /// 根据ID得到名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(Guid id)
        {
            var org = Get(id);
            return org == null ? "" : org.Name;
        }

        /// <summary>
        /// 根据ID得到名称(有前缀的情况)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(string id)
        {
            string name = string.Empty;
            if (id.IsGuid())//机构
            {
                return GetName(id.ToGuid());
            }
            else if (id.StartsWith(Users.PREFIX))//用户
            {
                string uid = Users.RemovePrefix(id);
                Guid userID;
                if (!uid.IsGuid(out userID))
                {
                    return "";
                }
                else
                {
                    return new Users().GetName(userID);
                }
            }
            else if (id.StartsWith(WorkGroup.PREFIX))//工作组
            {
                string uid = WorkGroup.RemovePrefix(id);
                Guid wid;
                if (!uid.IsGuid(out wid))
                {
                    return "";
                }
                else
                {
                    return new WorkGroup().GetName(wid);
                }
            }
            
            return "";
        }

        /// <summary>
        /// 得到一组机构的名称(逗号分隔，有前缀)
        /// </summary>
        /// <param name="idString"></param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public string GetNames(string idString, string split=",")
        {
            if (idString.IsNullOrEmpty())
            {
                return "";
            }
            string[] array = idString.Split(',');
            StringBuilder sb = new StringBuilder(array.Length * 50);
            int i = 0;
            int j = 1;
            foreach (var arr in array)
            {
                if (arr.IsNullOrEmpty())
                {
                    j++;
                    continue;
                }
                string name = GetName(arr);
                if (name.IsNullOrEmpty())
                {
                    j++;
                    continue;
                }
                sb.Append(name);
                if (i++ < array.Length - j)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 删除一个机构及其所有下级(包括下级人员)
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public int DeleteAndAllChilds(Guid orgID)
        {
            int i = 0;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                UsersRelation bur = new UsersRelation();
                Users user = new Users();
                var childs = GetAllChilds(orgID);
                List<string> userAccountList = new List<string>();//记录删除人员的账号，调用微信批量删除用户
                List<Data.Model.Organize> orgList = new List<Data.Model.Organize>();//记录要删除的部门，调用微信删除
                foreach (var child in childs)
                {
                    //删除人员及关系
                    var urs = bur.GetAllByOrganizeID(child.ID).FindAll(p => p.IsMain == 1);
                    foreach (var ur in urs)
                    {
                        var muser = user.Get(ur.UserID);
                        bur.Delete(ur.UserID, ur.OrganizeID);
                        i += user.Delete(ur.UserID);
                        if (muser != null)
                        {
                            userAccountList.Add(muser.Account);
                        }
                    }
                    i += Delete(child.ID);
                    orgList.Add(child);
                }
                //删除人员及关系
                var urs1 = bur.GetAllByOrganizeID(orgID).FindAll(p => p.IsMain == 1);
                foreach (var ur in urs1)
                {
                    bur.Delete(ur.UserID, ur.OrganizeID);
                    i += user.Delete(ur.UserID);
                    var muser1 = user.Get(ur.UserID);
                    if (muser1 != null)
                    {
                        userAccountList.Add(muser1.Account);
                    }
                }
                i += Delete(orgID);
                var org = Get(orgID);
                if (org != null)
                {
                    orgList.Add(org);
                }
                if (WeiXin.Config.IsUse)
                {
                    WeiXin.Organize worg = new WeiXin.Organize();
                    if (userAccountList.Count > 0)
                    {
                        worg.DeleteUserAsync(userAccountList.ToArray());
                    }
                    foreach (var dept in orgList)
                    {
                        worg.DeleteDeptAsync(dept.IntID);
                    }
                }
                scope.Complete();
            }
            return i;
        }
    }
}
