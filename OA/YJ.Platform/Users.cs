using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YJ.Platform
{
    public class Users
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public const string PREFIX = "u_";
        private YJ.Data.Interface.IUsers dataUsers;
        public Users()
        {
            this.dataUsers = Data.Factory.Factory.GetUsers();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.Users model)
        {
            return dataUsers.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.Users model)
        {
            return dataUsers.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Users> GetAll()
        {
            return dataUsers.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.Users Get(Guid id)
        {
            return dataUsers.Get(id);
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
            return dataUsers.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataUsers.GetCount();
        }

        /// <summary>
        /// 根据帐号查询一条记录
        /// </summary>
        public YJ.Data.Model.Users GetByAccount(string account)
        {
            return account.IsNullOrEmpty() ? null : dataUsers.GetByAccount(account);
        }

        /// <summary>
        /// 得到系统初始密码
        /// </summary>
        /// <returns></returns>
        public string GetInitPassword()
        {
            return YJ.Utility.Config.SystemInitPassword;
        }
        /// <summary>
        /// 得到加密后的密码字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptionPassword(string password)
        {
            if (password.IsNullOrEmpty())
            {
                return "";
            }
            YJ.Utility.HashEncrypt hash = new YJ.Utility.HashEncrypt();
            return hash.MD5System(hash.MD5System(password)); //hash.SHA512Encrypt(hash.SHA512Encrypt(password.Trim()));
        }

        /// <summary>
        /// 得到一个用户加密后的密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetUserEncryptionPassword(string userID, string password)
        {
            return password.IsNullOrEmpty() || userID.IsNullOrEmpty() ? "" : EncryptionPassword(userID.Trim().ToLower() + password.Trim());
        }

        /// <summary>
        /// 初始化一个用户密码
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool InitPassword(Guid userID)
        {
            return dataUsers.UpdatePassword(GetUserEncryptionPassword(userID.ToString(), GetInitPassword()), userID);
        }

        /// <summary>
        /// 查询一个岗位下所有人员
        /// </summary>
        /// <param name="organizeID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Users> GetAllByOrganizeID(Guid organizeID)
        {
            return dataUsers.GetAllByOrganizeID(organizeID);
        }

        /// <summary>
        /// 得到一个用户的所有岗位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Dictionary<Guid, bool> Guid 岗位ID bool 是否主要岗位</returns>
        public Dictionary<Guid, bool> GetAllStation(Guid userID)
        {
            UsersRelation ur = new UsersRelation();
            var urs = ur.GetAllByUserID(userID);
            Dictionary<Guid, bool> dict = new Dictionary<Guid, bool>();
            foreach (var u in urs)
            {
                if (!dict.ContainsKey(u.OrganizeID))
                {
                    dict.Add(u.OrganizeID, u.IsMain == 1);
                }
            }
            return dict;
        }
        /// <summary>
        /// 得到一个用户的主要岗位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Guid GetMainStation(Guid userID)
        {
            var ur = new UsersRelation().GetMainByUserID(userID);
            return ur == null ? Guid.Empty : ur.OrganizeID;
        }

        /// <summary>
        /// 查询一组岗位下所有人员
        /// </summary>
        /// <param name="organizeID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Users> GetAllByOrganizeIDArray(Guid[] organizeIDArray)
        {
            return dataUsers.GetAllByOrganizeIDArray(organizeIDArray);
        }

        /// <summary>
        /// 得到一个用户所在单位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public YJ.Data.Model.Organize GetUnitByUserID(Guid userID)
        {
            string cacheKey = YJ.Utility.Keys.CacheKeys.UserUnit.ToString();
            object obj = Cache.IO.Opation.Get(cacheKey);
            Dictionary<Guid, YJ.Data.Model.Organize> deptDict = new Dictionary<Guid, Data.Model.Organize>();
            if (obj is Dictionary<Guid, YJ.Data.Model.Organize>)
            {
                deptDict = (Dictionary<Guid, YJ.Data.Model.Organize>)obj;
                if (deptDict.ContainsKey(userID))
                {
                    return deptDict[userID];
                }
            }

            Guid stationID = GetMainStation(userID);
            if (stationID == Guid.Empty)
            {
                return null;
            }
            var parents = new YJ.Platform.Organize().GetAllParent(stationID);
            parents.Reverse();
            foreach (var parent in parents)
            {
                if (parent.Type == 1)
                {
                    deptDict.Add(userID, parent);
                    Cache.IO.Opation.Set(cacheKey, deptDict);
                    return parent;
                }
            }
            return null;
        }

        /// <summary>
        /// 当前用户单位
        /// </summary>
        public static Data.Model.Organize CurrentUnit
        {
            get
            {
                return new Users().GetUnitByUserID(CurrentUserID);
            }
        }

        /// <summary>
        /// 当前用户单位ID
        /// </summary>
        public static Guid CurrentUnitID
        {
            get
            {
                var unit = new Users().GetUnitByUserID(CurrentUserID);
                return unit == null ? Guid.Empty : unit.ID;
            }
        }

        /// <summary>
        /// 当前用户单位名称
        /// </summary>
        public static string CurrentUnitName
        {
            get
            {
                var unit = new Users().GetUnitByUserID(CurrentUserID);
                return unit == null ? "" : unit.Name;
            }
        }

        /// <summary>
        /// 得到一个用户所在部门
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public YJ.Data.Model.Organize GetDeptByUserID(Guid userID)
        {
            string cacheKey = YJ.Utility.Keys.CacheKeys.UserDept.ToString();
            object obj = Cache.IO.Opation.Get(cacheKey);
            Dictionary<Guid, YJ.Data.Model.Organize> deptDict = new Dictionary<Guid,Data.Model.Organize>();
            if (obj is Dictionary<Guid, YJ.Data.Model.Organize>)
            {
                deptDict = (Dictionary<Guid, YJ.Data.Model.Organize>)obj;
                if(deptDict.ContainsKey(userID))
                {
                    return deptDict[userID];
                }
            }
            
            Guid stationID = GetMainStation(userID);
            if (stationID == Guid.Empty)
            {
                return null;
            }
            var parents = new YJ.Platform.Organize().GetAllParent(stationID);
            parents.Reverse();
            foreach (var parent in parents)
            {
                if (parent.Type == 2)
                {
                    deptDict.Add(userID, parent);
                    Cache.IO.Opation.Set(cacheKey, deptDict);
                    return parent;
                }
            }
            return null;
        }

        /// <summary>
        /// 当前用户部门
        /// </summary>
        public static Data.Model.Organize CurrentDept
        {
            get
            {
                return new Users().GetDeptByUserID(CurrentUserID);
            }
        }

        /// <summary>
        /// 当前用户部门ID
        /// </summary>
        public static Guid CurrentDeptID
        {
            get
            {
                var dept = new Users().GetDeptByUserID(CurrentUserID);
                return dept == null ? Guid.Empty : dept.ID;
            }
        }

        /// <summary>
        /// 当前用户部门名称
        /// </summary>
        public static string CurrentDeptName
        {
            get
            {
                var dept = new Users().GetDeptByUserID(CurrentUserID);
                return dept == null ? "" : dept.Name;
            }
        }

        /// <summary>
        /// 检查帐号是否重复
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="userID">人员ID(此人员除外)</param>
        /// <returns></returns>
        public bool HasAccount(string account, string userID = "")
        {
            return account.IsNullOrEmpty() ? false : dataUsers.HasAccount(account.Trim(), userID);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">明文的密码</param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdatePassword(string password, Guid userID)
        {
            if (Utility.Config.IsDemo)
            {
                return true;
            }
            return password.IsNullOrEmpty() ? false : dataUsers.UpdatePassword(GetUserEncryptionPassword(userID.ToString(), password.Trim()), userID);
        }
        /// <summary>
        /// 得到当前登录用户ID
        /// </summary>
        public static Guid CurrentUserID
        {
            get
            {
                try
                {
                    object session = System.Web.HttpContext.Current.Session[YJ.Utility.Keys.SessionKeys.UserID.ToString()];
                    return session == null ? WeiXin.Organize.CurrentUserID : session.ToString().ToGuid();
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }
        /// <summary>
        /// 得到当前登录用户
        /// </summary>
        public static YJ.Data.Model.Users CurrentUser
        {
            get
            {
                Guid userID = CurrentUserID;
                if (userID == Guid.Empty)
                {
                    return null;
                }
                else
                {
                    var user = new Users().Get(userID);
                    return user;
                }
            }
        }

        /// <summary>
        /// 当前用户名称
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                string sessionKey = YJ.Utility.Keys.SessionKeys.UserName.ToString();
                object session = System.Web.HttpContext.Current.Session[sessionKey];
                string userName = string.Empty;
                if (session == null)
                {
                    userName = CurrentUser == null ? "" : CurrentUser.Name;
                    System.Web.HttpContext.Current.Session[sessionKey] = userName;
                }
                else
                {
                    userName = session.ToString();
                }
                if (userName.IsNullOrEmpty())
                {
                    userName = WeiXin.Organize.CurrentUserName;
                }
                return userName;
            }
        }

        /// <summary>
        /// 当前用户账号
        /// </summary>
        public static string CurrentUserAccount
        {
            get
            {
                return CurrentUser == null ? "" : CurrentUser.Account;
            }
        }

        
        /// <summary>
        /// 得到一个不重复的帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetAccount(string account)
        {
            if (account.IsNullOrEmpty())
            {
                return "";
            }
            string newAccount = account.Trim();
            int i = 0;
            while (HasAccount(newAccount))
            {
                newAccount += (++i).ToString();
            }
            return newAccount;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid userID, int sort)
        {
            return dataUsers.UpdateSort(userID, sort);
        }
        /// <summary>
        /// 根据ID得到名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(Guid id)
        {
            var user = Get(id);
            return user == null ? "" : user.Name;
        }
        /// <summary>
        /// 去除ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string RemovePrefix(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return "";
            }
            if (id.StartsWith(PREFIX))
            {
               return id.Replace(PREFIX, "");
            }
            return id;
        }

        /// <summary>
        /// 去除ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemovePrefix1(string id)
        {
            return id.IsNullOrEmpty() ? "" : id.Replace(PREFIX, "");
        }

        /// <summary>
        /// 得到一个人员的主管
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetLeader(Guid userID)
        {
            var mainStation = GetMainStation(userID);
            if (mainStation == null)
            {
                return "";
            }
            YJ.Platform.Organize borg = new Organize();
            var station = borg.Get(mainStation);
            if (station == null)
            {
                return "";
            }
            if (!station.Leader.IsNullOrEmpty())
            {
                return station.Leader;
            }
            var parents = borg.GetAllParent(station.Number);
            foreach (var parent in parents)
            {
                if (!parent.Leader.IsNullOrEmpty())
                {
                    return parent.Leader;
                }
            }
            return "";
        }

        /// <summary>
        /// 得到一个人员的分管领导
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetChargeLeader(Guid userID)
        {
            var mainStation = GetMainStation(userID);
            if (mainStation == null)
            {
                return "";
            }
            YJ.Platform.Organize borg = new Organize();
            var station = borg.Get(mainStation);
            if (station == null)
            {
                return "";
            }
            if (!station.ChargeLeader.IsNullOrEmpty())
            {
                return station.ChargeLeader;
            }
            var parents = borg.GetAllParent(station.Number);
            foreach (var parent in parents)
            {
                if (!parent.ChargeLeader.IsNullOrEmpty())
                {
                    return parent.ChargeLeader;
                }
            }
            return "";
        }

        /// <summary>
        /// 得到一个人员的上级部门主管
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetParentDeptLeader(Guid userID)
        {
            var mainStation = GetMainStation(userID);
            if (mainStation == null)
            {
                return "";
            }
            YJ.Platform.Organize borg = new Organize();
            var station = borg.Get(mainStation);
            if (station == null)
            {
                return "";
            }
            var parents = borg.GetAllParent(station.Number);
            foreach (var parent in parents)
            {
                if (!parent.Leader.IsNullOrEmpty())
                {
                    return parent.Leader;
                }
            }
            return "";
        }

        /// <summary>
        /// 判断一个人员是否是部门主管
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsLeader(Guid userID)
        {
            string leader = GetLeader(userID);
            return leader.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// 判断一个人员是否是部门分管领导
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsChargeLeader(Guid userID)
        {
            string leader = GetChargeLeader(userID);
            return leader.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// 判断一个人员是否在一个组织机构字符串里
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="memberString"></param>
        /// <returns></returns>
        public bool IsContains(Guid userID, string memberString)
        {
            if (memberString.IsNullOrEmpty())
            {
                return false;
            }
            var user = new Organize().GetAllUsers(memberString).Find(p => p.ID == userID);
            return user != null;
        }

        /// <summary>
        /// 得到一个人员的手机号
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetMobileNumber(Guid userID)
        {
            var user = new Users().Get(userID);
            return user != null ? user.Mobile : "";
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Users> GetAllByIDString(string idString)
        {
            if (idString.IsNullOrEmpty())
            {
                return new List<Data.Model.Users>();
            }
            return dataUsers.GetAllByIDString(idString);
        }

        /// <summary>
        /// 查询一个角色组下的所有人员
        /// </summary>
        /// <param name="workgroupid"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Users> GetAllByWorkGroupID(Guid workgroupid)
        {
            return dataUsers.GetAllByWorkGroupID(workgroupid);
        }

        /// <summary>
        /// 根据人员ID得到帐号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAccountByID(Guid id)
        {
            var user = Get(id);
            return user == null ? "" : user.Account;
        }

        /// <summary>
        /// 得到一个人员的所有上级部门领导
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> GetAllParentsDeptLeader(Guid userID)
        {
            List<string> leaders = new List<string>();
            Guid stationID = GetMainStation(userID);
            if (stationID.IsEmptyGuid())
            {
                return leaders;
            }
            Organize org = new Organize();
            var mainStation = org.Get(stationID);
            if(mainStation == null)
            {
                return leaders;
            }
            if(!mainStation.Leader.IsNullOrEmpty())
            {
                leaders.Add(mainStation.Leader);
            }
            var parents = new Organize().GetAllParent(stationID);
            foreach (var parent in parents)
            {
                if (!parent.Leader.IsNullOrEmpty())
                {
                    leaders.Add(parent.Leader);
                }
            }
            return leaders;
        }

    }

    public class UsersEqualityComparer : IEqualityComparer<YJ.Data.Model.Users>
    {
        public bool Equals(YJ.Data.Model.Users user1, YJ.Data.Model.Users user2)
        {
            return user1 == null || user2 == null || user1.ID == user2.ID;
        }
        public int GetHashCode(YJ.Data.Model.Users user)
        {
            return user.ToString().GetHashCode();
        }
    }

}
