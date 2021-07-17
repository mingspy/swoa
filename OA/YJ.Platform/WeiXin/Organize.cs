using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YJ.Platform.WeiXin
{
    /// <summary>
    /// 微信组织机构操作类
    /// </summary>
    public class Organize
    {
        private delegate bool del_Save(Data.Model.Organize organize);
        private delegate bool del_SaveUser(Data.Model.Users user);
        private delegate bool del_SaveWorkGroup(Data.Model.WorkGroup wg);
        private delegate bool del_Delete(int orgID);
        private delegate bool del_DeleteUser(string account);
        private delegate bool del_DeleteUser1(string[] account);
        private string secret = string.Empty;

        public Organize()
        {
            string sec = Config.GetSecret("weixinagents_organize");
            this.secret = sec.IsNullOrEmpty() ? Config.OrganizeSecret : sec;
        }
        
        /// <summary>
        /// 带相关应用的ID-企业微信使用
        /// </summary>
        /// <param name="agentId"></param>
        public Organize(int agentId)
        {
            this.secret = Config.GetSecret(agentId);
        }

        /// <summary>
        /// 带相关应用的代码-企业微信使用
        /// </summary>
        /// <param name="agentId"></param>
        public Organize(string agentCode)
        {
            this.secret = Config.GetSecret(agentCode);
        }

        /// <summary>
        /// 得到密钥(默认为管理人员管理应用的secret，旧版本企业号使用)
        /// </summary>
        /// <returns></returns>
        private string GetAccessToken()
        {
            return Config.GetAccessToken(secret);
        }

        /// <summary>
        /// 过滤名称中的特殊字符,字符中不能有\:*?"<>｜
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string replaceName(string name)
        {
            return name.IsNullOrEmpty() ? name : 
                name.Replace("\\", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("｜", "");
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        public bool AddDept(Data.Model.Organize organize)
        {
            //{ "name": "广州研发中心",--字符不能包括\:*?"<>｜ "parentid": 1,"order": 1,"id": 1}

            if (organize.IntID == 0)//由于是自增ID，第一次新增时可能是0，要重新从数据库读取
            {
                organize = new Platform.Organize().Get(organize.ID);
            }

            int parentID = 1;
            if (!organize.ParentID.IsEmptyGuid())
            {
                var parentOrganize = new Platform.Organize().Get(organize.ParentID);
                if (parentOrganize != null)
                {
                    parentID = parentOrganize.IntID;
                }
            }
            string url = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=" + GetAccessToken();
            string data = "{\"name\":\"" + replaceName(organize.Name) + "\"," +
                "\"parentid\":" + parentID.ToString() + "," +
                "\"order\":" + organize.Sort.ToString() + "," +
                "\"id\":" + organize.IntID.ToString() +
                "}";
            //{"errcode": 0,"errmsg": "created","id": 2}
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信添加部门-" + organize.Name + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, organize.Serialize(), data);
            return isSuccess;
        }

        public void AddDeptAsync(Data.Model.Organize organize)
        {
            del_Save save = new del_Save(AddDept);
            save.BeginInvoke(organize, null, null);
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="organize"></param>
        /// <returns></returns>
        public bool EditDept(Data.Model.Organize organize)
        {
            int parentID = 1;
            if (!organize.ParentID.IsEmptyGuid())
            {
                var parentOrganize = new Platform.Organize().Get(organize.ParentID);
                if (parentOrganize != null)
                {
                    parentID = parentOrganize.IntID;
                }
            }
            string url = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token=" + GetAccessToken();
            string data = "{\"id\":" + organize.IntID.ToString() + "," +
                "\"name\":\"" + replaceName(organize.Name) + "\"," +
                "\"parentid\":" + parentID.ToString() + "," +
                "\"order\":" + organize.Sort.ToString() +
                "}";
            //{"errcode": 0,"errmsg": "created","id": 2}
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信修改部门-" + organize.Name + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, organize.Serialize(), data);
            return isSuccess;
        }

        public void EditDeptAsync(Data.Model.Organize organize)
        {
            del_Save save = new del_Save(EditDept);
            save.BeginInvoke(organize, null, null);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="organizeIntID"></param>
        /// <returns></returns>
        public bool DeleteDept(int organizeIntID)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token=" + GetAccessToken() + "&id=" + organizeIntID.ToString();
            string returnString = Utility.HttpHelper.SendGet(url);
            Platform.Log.Add("调用了微信删除部门", "返回：" + returnString, Log.Types.微信企业号, url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            return isSuccess;
        }

        public void DeleteDeptAsync(int organizeIntID)
        {
            del_Delete del = new del_Delete(DeleteDept);
            del.BeginInvoke(organizeIntID, null, null);
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUser(string userAccount)
        { 
            //{
            //   "errcode": 0,
            //   "errmsg": "ok",
            //   "userid": "zhangsan",
            //   "name": "李四",
            //   "department": [1, 2],
            //   "position": "后台工程师",
            //   "mobile": "15913215421",
            //   "gender": "1",
            //   "email": "zhangsan@gzdev.com",
            //   "weixinid": "lisifordev",  
            //   "avatar": "http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0",
            //   "status": 1,
            //   "extattr": {"attrs":[{"name":"爱好","value":"旅游"},{"name":"卡号","value":"1234567234"}]}
            //}

            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=" + GetAccessToken() + "&userid=" + userAccount;
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信查询人员-" + userAccount + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号);
            return isSuccess ? returnString : "";
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(Data.Model.Users user)
        {
            //{
            //   "userid": "zhangsan",
            //   "name": "张三",
            //   "department": [1, 2],
            //   "position": "产品经理",
            //   "mobile": "15913215421",
            //   "gender": "1",
            //   "email": "zhangsan@gzdev.com",
            //   "weixinid": "zhangsan4dev",
            //   "avatar_mediaid": "2-G6nrLmr5EC3MNb_-zL1dDdzkd0p7cNliYu9V5w7o8K0",
            //   "extattr": {"attrs":[{"name":"爱好","value":"旅游"},{"name":"卡号","value":"1234567234"}]}
            //}
            if (user.Mobile.IsNullOrEmpty() && user.Email.IsNullOrEmpty() && user.WeiXin.IsNullOrEmpty())
            {
                return false;
            }
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token=" + GetAccessToken();
            var urs = new Platform.UsersRelation().GetAllByUserID(user.ID);
            Platform.Organize borg = new Platform.Organize();
            StringBuilder sb = new StringBuilder();
            foreach (var ur in urs)
            {
                var ur1 = borg.Get(ur.OrganizeID);
                if (ur1 != null)
                {
                    sb.Append(ur1.IntID);
                    sb.Append(",");
                }
            }
            string data = "{\"userid\":\"" + user.Account + "\"," +
                "\"name\":\"" + replaceName(user.Name) + "\"," +
                "\"department\":[" + sb.ToString().TrimEnd(',') + "]," +
                "\"position\":\"\"," +
                "\"mobile\":\"" + user.Mobile + "\"," +
                (user.Sex.HasValue ? "\"gender\":\"" + (user.Sex.Value + 1).ToString() + "\"," : "") +
                "\"weixinid\":\"" + user.WeiXin + "\"" +
                "}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信添加人员-" + user.Name + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, user.Serialize(), data);
            return isSuccess;
        }

        public void AddUserAsync(Data.Model.Users user)
        {
            del_SaveUser save = new del_SaveUser(AddUser);
            save.BeginInvoke(user, null, null);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EditUser(Data.Model.Users user)
        {
            //{
            //   "userid": "zhangsan",
            //   "name": "张三",
            //   "department": [1, 2],
            //   "position": "产品经理",
            //   "mobile": "15913215421",
            //   "gender": "1",
            //   "email": "zhangsan@gzdev.com",
            //   "weixinid": "zhangsan4dev",
            //   "enable": 1, 0禁用 1启用
            //   "avatar_mediaid": "2-G6nrLmr5EC3MNb_-zL1dDdzkd0p7cNliYu9V5w7o8K0",
            //   "extattr": {"attrs":[{"name":"爱好","value":"旅游"},{"name":"卡号","value":"1234567234"}]}
            //}
            if (user.Mobile.IsNullOrEmpty() && user.Email.IsNullOrEmpty() && user.WeiXin.IsNullOrEmpty())
            {
                return false;
            }

            //先查询，没有则新增
            if (GetUser(user.Account).IsNullOrEmpty())
            {
                return AddUser(user);
            }

            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token=" + GetAccessToken();
            var urs = new Platform.UsersRelation().GetAllByUserID(user.ID);
            Platform.Organize borg = new Platform.Organize();
            StringBuilder sb = new StringBuilder();
            foreach (var ur in urs)
            {
                var ur1 = borg.Get(ur.OrganizeID);
                if (ur1 != null)
                {
                    sb.Append(ur1.IntID);
                    sb.Append(",");
                }
            }
            string data = "{\"userid\":\"" + user.Account + "\"," +
                "\"name\":\"" + replaceName(user.Name) + "\"," +
                "\"department\":[" + sb.ToString().TrimEnd(',') + "]," +
                "\"position\":\"\"," +
                "\"mobile\":\"" + user.Mobile + "\"," +
                (user.Sex.HasValue ? "\"gender\":\"" + (user.Sex.Value + 1).ToString() + "\"," : "") +
                "\"email\":\"" + user.Email + "\"," +
                "\"weixinid\":\"" + user.WeiXin + "\"," +
                "\"enable\":" + (user.Status == 0 ? 1 : 0).ToString() +
                "}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信修改人员-" + user.Name + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, user.Serialize(), data);
            return isSuccess;
        }

        public void EditUserAsync(Data.Model.Users user)
        {
            del_SaveUser save = new del_SaveUser(EditUser);
            save.BeginInvoke(user, null, null);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userAccount">帐号</param>
        /// <returns></returns>
        public bool DeleteUser(string userAccount)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token=" + GetAccessToken() + "&userid=" + userAccount;
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信删除人员-" + userAccount + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, url);
            return isSuccess;
        }

        public void DeleteUserAsync(string userAccount)
        {
            del_DeleteUser save = new del_DeleteUser(DeleteUser);
            save.BeginInvoke(userAccount, null, null);
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userAccountList">帐号数组</param>
        /// <returns></returns>
        public bool DeleteUser(string[] userAccountList)
        {
            //{
            //   "useridlist": ["zhangsan", "lisi"]
            //}
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token=" + GetAccessToken();
            string data = "{" +
                "\"useridlist\":[" + Utility.Tools.GetSqlInString(userAccountList).Replace("'", "\"") +
                "]}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信批量删除人员" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, data);
            return isSuccess;
        }

        public void DeleteUserAsync(string[] userAccountList)
        {
            del_DeleteUser1 save = new del_DeleteUser1(DeleteUser);
            save.BeginInvoke(userAccountList, null, null);
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool AddGroup(Data.Model.WorkGroup group)
        {
            if (group == null) return false;
            string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token=" + GetAccessToken();
            string data = "{\"tagname\":\"" + replaceName(group.Name) + "\",\"tagid\":" + group.IntID.ToString() + "}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信添加标签" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, data);
            if (isSuccess)
            {
                if (!AddGroupUser(group))
                {
                    return false;
                }
            }
            return isSuccess;
        }

        public void AddGroupAsync(Data.Model.WorkGroup group)
        {
            del_SaveWorkGroup save = new del_SaveWorkGroup(AddGroup);
            save.BeginInvoke(group, null, null);
        }


        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool EditGroup(Data.Model.WorkGroup group)
        {
            if (group == null) return false;
            string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token=" + GetAccessToken();
            string data = "{\"tagid\":" + group.IntID.ToString() + ",\"tagname\":\"" + replaceName(group.Name) + "\"}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信修改标签" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, data);
            return isSuccess;
        }

        public void EditGroupAsync(Data.Model.WorkGroup group)
        {
            del_SaveWorkGroup save = new del_SaveWorkGroup(EditGroup);
            save.BeginInvoke(group, null, null);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool DeleteGroup(Data.Model.WorkGroup group)
        {
            if (group == null) 
            {
                return false;
            }
            if (DeleteGroupUser(group.IntID))
            {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token=" + GetAccessToken() + "&tagid=" + group.IntID.ToString();
                string returnString = Utility.HttpHelper.SendGet(url);
                LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
                bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
                Platform.Log.Add("调用了微信删除标签-" + group.Name + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, group.Serialize());
                return isSuccess;
            }
            else
            {
                return false;
            }
        }

        public void DeleteGroupAsync(Data.Model.WorkGroup group)
        {
            del_SaveWorkGroup save = new del_SaveWorkGroup(DeleteGroup);
            save.BeginInvoke(group, null, null);
        }

        /// <summary>
        /// 得到标签下所有人员
        /// </summary>
        /// <param name="groupIntID">组数字ID</param>
        /// <returns>List<Tuple<账号,姓名>></returns>
        public List<Tuple<string,string>> GetGroupUsers(int groupIntID)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token=" + GetAccessToken() + "&tagid=" + groupIntID.ToString();
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            List<Tuple<string, string>> users = new List<Tuple<string, string>>();
            Platform.Log.Add("调用了微信获取标签成员-" + groupIntID + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号);
            if (isSuccess)
            {
                LitJson.JsonData userlist = jd["userlist"];
                if (userlist.IsArray)
                {
                    foreach (LitJson.JsonData u in userlist)
                    {
                        string account = u["userid"].ToString();
                        string name = u["name"].ToString();
                        if (!account.IsNullOrEmpty() && !name.IsNullOrEmpty())
                        {
                            users.Add(new Tuple<string, string>(account, name));
                        }
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// 更新标签成员，先删除再添加
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool AddGroupUser(Data.Model.WorkGroup group)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token=" + GetAccessToken();
            if (group.Members.IsNullOrEmpty())
            {
                return true;
            }
            var users = new Platform.Organize().GetAllUsers(group.Members);
            List<string> userAccounts = new List<string>();
            foreach (var user in users)
            {
                userAccounts.Add(user.Account);
            }
            string userAccounts1 = Utility.Tools.GetSqlInString(userAccounts.ToArray()).Replace("'", "\"");
            if (!DeleteGroupUser(group.IntID, userAccounts1))
            {
                return false;
            }
            string data = "{\"tagid\":" + group.IntID.ToString() + ",\"userlist\":[" + userAccounts1 + "]}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信更新标签成员" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, data);
            return isSuccess;
        }

        public void AddGroupUserAsync(Data.Model.WorkGroup group)
        {
            del_SaveWorkGroup save = new del_SaveWorkGroup(AddGroupUser);
            save.BeginInvoke(group, null, null);
        }

        /// <summary>
        /// 删除标签成员
        /// </summary>
        /// <param name="groupIntID">标签数字ID</param>
        /// <param name="userAccounts">成员帐号</param>
        /// <returns></returns>
        public bool DeleteGroupUser(int groupIntID, string userAccounts)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token=" + GetAccessToken();
            if (userAccounts.IsNullOrEmpty())
            {
                return true;
            }

            string data = "{\"tagid\":" + groupIntID.ToString() + ",\"userlist\":[" + userAccounts + "]}";
            string returnString = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信删除标签成员" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, data);
            return isSuccess;
        }

        /// <summary>
        /// 删除标签所有成员
        /// </summary>
        /// <param name="groupIntID">标签数字ID</param>
        /// <returns></returns>
        public bool DeleteGroupUser(int groupIntID)
        {
            var users = GetGroupUsers(groupIntID);
            if (users.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var user in users)
                {
                    sb.Append("\"" + user.Item1 + "\",");
                }
                string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token=" + GetAccessToken();
                string data = "{\"tagid\":" + groupIntID.ToString() + ",\"userlist\":[" + sb.ToString().TrimEnd(',') + "]}";
                string returnString = Utility.HttpHelper.SendPost(url, data);
                LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
                bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
                Platform.Log.Add("调用了微信删除标签成员" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号, data);
                return isSuccess;
            }
            return true;
        }

        /// <summary>
        /// 得到所有标签
        /// </summary>
        /// <returns>List<Tuple<id,名称>></returns>
        public List<Tuple<int, string>> GetGroups()
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/list?access_token=" + GetAccessToken();
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            bool isSuccess = jd.ContainsKey("errcode") && 0 == jd["errcode"].ToString().ToInt();
            Platform.Log.Add("调用了微信所有标签" + "-" + (isSuccess ? "成功" : "失败"), "返回：" + returnString, Log.Types.微信企业号);
            List<Tuple<int, string>> tags = new List<Tuple<int, string>>();
            if (isSuccess)
            {
                LitJson.JsonData userlist = jd["taglist"];
                if (userlist.IsArray)
                {
                    foreach (LitJson.JsonData u in userlist)
                    {
                        string tagid = u["tagid"].ToString();
                        string tagname = u["tagname"].ToString();
                        if (tagid.IsInt() && !tagname.IsNullOrEmpty())
                        {
                            tags.Add(new Tuple<int, string>(tagid.ToInt(), tagname));
                        }
                    }
                }
            }
            return tags;
        }

        /// <summary>
        /// 同步整个组织架构
        /// </summary>
        /// <returns></returns>
        public void UpdateAllOrganize()
        {
            Platform.Organize borg = new Platform.Organize();
            var orgs = borg.GetAll();
            System.Data.DataTable orgDt = new System.Data.DataTable();
            orgDt.Columns.Add("部门名称", "".GetType());
            orgDt.Columns.Add("部门ID", 1.GetType());
            orgDt.Columns.Add("父部门ID", 1.GetType());
            orgDt.Columns.Add("排序", 1.GetType());
            foreach (var org in orgs)
            {
                int parentID = -1;
                if(org.ParentID.IsEmptyGuid())
                {
                    parentID = 1;
                }
                else
                {
                    var parent = borg.Get(org.ParentID);
                    if (parent != null)
                    {
                        parentID = parent.IntID;
                    }
                }
                if (parentID == -1)
                {
                    continue;
                }
                System.Data.DataRow dr = orgDt.NewRow();
                dr["部门名称"] = replaceName(org.Name);
                dr["部门ID"] = org.IntID;
                dr["父部门ID"] = parentID;
                dr["排序"] = org.Sort;
                orgDt.Rows.Add(dr);
            }
            string filePath = YJ.Platform.Files.FilePath + "WeiXinCsv\\";
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string fileName = filePath + Guid.NewGuid().ToString("N") + ".csv";
            Utility.NPOIHelper.ExportCSV(orgDt, fileName);
            string media_id = new Media(Config.GetSecret("weixinagents_organize")).UploadTemp(fileName, "file");
            if (!media_id.IsNullOrEmpty())
            { 
                string url = "https://qyapi.weixin.qq.com/cgi-bin/batch/replaceparty?access_token=" + GetAccessToken();
                string data = "{\"media_id\":\"" + media_id + "\"}";
                string returnString = Utility.HttpHelper.SendPost(url, data);
                Platform.Log.Add("调用了微信同步整个组织架构", "返回：" + returnString, Log.Types.微信企业号, data);
            }
        }

        /// <summary>
        /// 同步所有人员
        /// </summary>
        /// <returns></returns>
        public void UpdateAllUsers()
        {
            Platform.Organize borg = new Platform.Organize();
            Platform.UsersRelation bur = new UsersRelation();
            Platform.Users buser = new Users();
            var users = buser.GetAll();
            System.Data.DataTable userDt = new System.Data.DataTable();
            userDt.Columns.Add("姓名", "".GetType());
            userDt.Columns.Add("帐号", "".GetType());
            userDt.Columns.Add("微信号", "".GetType());
            userDt.Columns.Add("手机号", "".GetType());
            userDt.Columns.Add("邮箱", "".GetType());
            userDt.Columns.Add("所在部门", "".GetType());
            userDt.Columns.Add("职位", "".GetType());
            foreach (var user in users)
            {
                StringBuilder depts = new StringBuilder();
                var urs = bur.GetAllByUserID(user.ID);
                foreach (var ur in urs)
                {
                    var org = borg.Get(ur.OrganizeID);
                    if (org != null)
                    {
                        depts.Append(org.IntID);
                        depts.Append(",");
                    }
                }
                System.Data.DataRow dr = userDt.NewRow();
                dr["姓名"] = replaceName(user.Name);
                dr["帐号"] = user.Account;
                dr["微信号"] = user.WeiXin;
                dr["手机号"] = user.Mobile;
                dr["邮箱"] = user.Email;
                dr["所在部门"] = depts.ToString().TrimEnd(',');
                dr["职位"] = "";
                userDt.Rows.Add(dr);
            }
            string filePath = YJ.Platform.Files.FilePath + "WeiXinCsv\\";
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string fileName = filePath + Guid.NewGuid().ToString("N") + ".csv";
            Utility.NPOIHelper.ExportCSV(userDt, fileName);
            string media_id = new Media(Config.GetSecret("weixinagents_organize")).UploadTemp(fileName, "file");
            if (!media_id.IsNullOrEmpty())
            {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/batch/replaceuser?access_token=" + GetAccessToken();
                string data = "{\"media_id\":\"" + media_id + "\"}";
                string returnString = Utility.HttpHelper.SendPost(url, data);
                Platform.Log.Add("调用了微信同步所有人员", "返回：" + returnString, Log.Types.微信企业号, data);
            }
        }

        /// <summary>
        /// 微信登录人员的ID
        /// </summary>
        public static Guid CurrentUserID
        {
            get
            {
                //return "EB03262C-AB60-4BC6-A4C0-96E66A4229FE".ToGuid();
                try
                {
                    var obj = System.Web.HttpContext.Current.Session[YJ.Utility.Keys.SessionKeys.UserID.ToString()];
                    if (obj != null && obj.ToString().IsGuid())
                    {
                        return obj.ToString().ToGuid();
                    }
                    else
                    {
                        var cookie = System.Web.HttpContext.Current.Request.Cookies.Get("weixin_userid");
                        if (cookie != null && cookie.Value.IsGuid())
                        {
                            return cookie.Value.ToGuid();
                        }
                        else
                        {
                            //2018-4-18新增，，APP通过token得到用户ID
                            //string token = System.Web.HttpContext.Current.Request.QueryString["token"];
                            //Guid? userId = YJ.Utility.Config.GetUserIdByToken(token);
                            //if (userId.HasValue && !userId.Value.IsEmptyGuid())
                            //{
                            //    System.Web.HttpContext.Current.Session[YJ.Utility.Keys.SessionKeys.UserID.ToString()] = userId.Value;
                            //    System.Web.HttpContext.Current.Response.Cookies.Add(new System.Web.HttpCookie(YJ.Utility.Keys.SessionKeys.UserID.ToString(), userId.Value.ToString()) { Expires = DateTime.Now.AddDays(7) });
                            //    return userId.Value;
                            //}
                            return Guid.Empty;
                        }
                    }
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }

        /// <summary>
        /// 微信登录人员的姓名
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                var cookie = System.Web.HttpContext.Current.Request.Cookies.Get("weixin_username");
                if (cookie != null)
                {
                    return cookie.Value;
                }
                else
                {
                    string name = new Platform.Users().GetName(CurrentUserID);
                    System.Web.HttpContext.Current.Response.Cookies.Add(new System.Web.HttpCookie("weixin_username", name));
                    return name;
                }
            }
        }
        /// <summary>
        /// 当前登录实体
        /// </summary>
        public static Data.Model.Users CurrentUser
        {
            get
            {
                return new Users().Get(CurrentUserID);
            }
        }

        /// <summary>
        /// 检查移动端是否在线
        /// </summary>
        /// <returns></returns>
        public static bool CheckLogin()
        {
            if (CurrentUserID.IsEmptyGuid())
            {
                if (Config.IsUse)
                {
                    System.Web.HttpContext.Current.Response.Cookies.Add(new System.Web.HttpCookie("LastURL", System.Web.HttpContext.Current.Request.Url.PathAndQuery));
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Config.CorpID + "&redirect_uri=" + Config.GetAccountUrl + "&response_type=code&scope=snsapi_base&state=a#wechat_redirect";
                    //Log.Add("调用了微信获取人员CODE", url, Log.Types.微信企业号, url);
                    System.Web.HttpContext.Current.Response.Redirect(url);
                }
                return false;
            }
            else
            {
                return true; //return Tools.CheckLogin(isRedirect);
            }
        }
        
        /// <summary>
        /// 根据Code得到用户账号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetUserAccountByCode(string code)
        {
            if (Config.OrganizeSecret.IsNullOrEmpty())
            {
                var dicts = new Dictionary().GetChilds("weixinagents");
                if (dicts.Count == 0)
                {
                    return "";
                }
                else
                {
                    this.secret = dicts.OrderBy(p => p.Sort).First().Note.Trim1();
                }
            }
            else
            {
                this.secret = Config.OrganizeSecret;
            }
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + GetAccessToken() + "&code=" + code;
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            string account = jd.ContainsKey("UserId") ? jd["UserId"].ToString() : "";
            Log.Add("调用了微信获取人员帐号", url, Log.Types.微信企业号, returnString);
            return account;
        }
    }
}
