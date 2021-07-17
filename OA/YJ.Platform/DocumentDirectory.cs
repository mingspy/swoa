using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YJ.Platform
{
    public class DocumentDirectory
    {
        private YJ.Data.Interface.IDocumentDirectory dataDocumentDirectory;
        private string DocumentsDirectoryCacheKey = Utility.Keys.CacheKeys.DocumentsDirectory.ToString();
        private string DocumentsDirectoryManageUsers = Utility.Keys.CacheKeys.DocumentsDirectoryManageUsers.ToString();
        private string DocumentsDirectoryPublishUsers = Utility.Keys.CacheKeys.DocumentsDirectoryPublishUsers.ToString();
        private string DocumentsDirectoryReadUsers = Utility.Keys.CacheKeys.DocumentsDirectoryReadUsers.ToString();
        public DocumentDirectory()
        {
            this.dataDocumentDirectory = YJ.Data.Factory.Factory.GetDocumentDirectory();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.DocumentDirectory model)
        {
            return dataDocumentDirectory.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.DocumentDirectory model)
        {
            return dataDocumentDirectory.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.DocumentDirectory> GetAll(bool fromCache = true)
        {
            if (!fromCache)
            {
                return dataDocumentDirectory.GetAll();
            }
            else
            {
                var cache = YJ.Cache.IO.Opation.Get(DocumentsDirectoryCacheKey);
                if (cache is List<YJ.Data.Model.DocumentDirectory>)
                {
                    return (List<YJ.Data.Model.DocumentDirectory>)cache;
                }
                else
                {
                    var list = dataDocumentDirectory.GetAll();
                    YJ.Cache.IO.Opation.Set(DocumentsDirectoryCacheKey, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.DocumentDirectory Get(Guid id)
        {
            return GetAll().Find(p => p.ID == id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataDocumentDirectory.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataDocumentDirectory.GetCount();
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            YJ.Cache.IO.Opation.Remove(DocumentsDirectoryCacheKey);
        }

        /// <summary>
        /// 得到下级栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Data.Model.DocumentDirectory> GetChilds(Guid id)
        {
            var dirs = GetAll();
            return dirs.FindAll(p => p.ParentID == id).OrderBy(p=>p.Sort).ToList();
        }

        /// <summary>
        /// 得到可以显示的下级栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Data.Model.DocumentDirectory> GetDisplayChilds(Guid id, Guid userID)
        {
            var dirs = GetAll();
            var childs = dirs.FindAll(p => p.ParentID == id).OrderBy(p => p.Sort).ToList();
            List<Data.Model.DocumentDirectory> dirs1 = new List<Data.Model.DocumentDirectory>();
            foreach (var child in childs)
            {
                if (HasDisplay(child, userID))
                {
                    dirs1.Add(child);
                }
            }
            return dirs1;
        }

        /// <summary>
        /// 得到一个栏目的所有下级栏目ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAllChildIdString(Guid id)
        {
            var childs = GetAllChilds(id);
            StringBuilder sb = new StringBuilder();
            foreach (var child in childs)
            {
                sb.Append(child.ID);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 得到一个栏目的所有有阅读权限的下级栏目ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAllChildIdString(Guid id, Guid userID)
        {
            var childs = GetAllChilds(id);
            StringBuilder sb = new StringBuilder();
            foreach (var child in childs)
            {
                if (HasRead(child.ID, userID))
                {
                    sb.Append(child.ID);
                    sb.Append(",");
                }
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 得到一个栏目的所有上级栏目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isMe"></param>
        /// <returns></returns>
        public List<Data.Model.DocumentDirectory> GetAllParents(Guid id, bool isMe = false)
        {
            List<Data.Model.DocumentDirectory> list = new List<Data.Model.DocumentDirectory>();
            var dir = Get(id);
            if (dir == null)
            {
                return list;
            }
            if (isMe)
            {
                list.Add(dir);
            }
            addParent(list, dir.ParentID);
            return list;
        }

        private void addParent(List<Data.Model.DocumentDirectory> list, Guid id)
        {
            if (id.IsEmptyGuid())
            {
                return;
            }
            var dir = Get(id);
            if (dir == null)
            {
                return;
            }
            list.Add(dir);
            addParent(list, dir.ParentID);
        }

        /// <summary>
        /// 得到所有上级的名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isMe">是否显示自己</param>
        /// <param name="isMe">是否显示根</param>
        /// <returns></returns>
        public string GetAllParentsName(Guid id, bool isMe = true, bool isRoot = true)
        {
            var parents = GetAllParents(id, isMe).FindAll(p => !p.ParentID.IsEmptyGuid());
            StringBuilder sb = new StringBuilder();
            foreach (var parent in parents)
            {
                sb.Append(parent.Name);
                sb.Append(" / ");
            }
            return sb.ToString().TrimEnd('/', ' ');
        }

        /// <summary>
        /// 得到一个栏目的所有下级栏目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isMe">是否包含自己</param>
        /// <returns></returns>
        public List<Data.Model.DocumentDirectory> GetAllChilds(Guid id, bool isMe = true)
        {
            List<Data.Model.DocumentDirectory> list = new List<Data.Model.DocumentDirectory>();
            var dir = Get(id);
            if (dir != null)
            {
                if (isMe)
                {
                    list.Add(dir);
                }
                addChilds(list, id);
            }
            return list;
        }

        private void addChilds(List<Data.Model.DocumentDirectory> list, Guid id)
        {
            var childs = GetChilds(id);
            foreach (var child in childs)
            {
                list.Add(child);
                addChilds(list, child.ID);
            }
        }

        /// <summary>
        /// 得到最大排序
        /// </summary>
        /// <param name="dirID"></param>
        /// <returns></returns>
        public int GetMaxSort(Guid dirID)
        {
            return dataDocumentDirectory.GetMaxSort(dirID);
        }

        /// <summary>
        /// 得到根栏目
        /// </summary>
        /// <returns></returns>
        public Data.Model.DocumentDirectory GetRoot()
        {
            return GetAll().Find(p => p.ParentID == Guid.Empty);
        }

        /// <summary>
        /// 根据ID得到名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(Guid id)
        {
            var dir = Get(id);
            return dir == null ? "" : dir.Name;
        }

        /// <summary>
        /// 得到一个栏目的阅读人员
        /// </summary>
        /// <param name="dirID"></param>
        /// <returns></returns>
        public List<Data.Model.Users> GetReadUsers(Guid dirID)
        {
            var cache = YJ.Cache.IO.Opation.Get(DocumentsDirectoryReadUsers + dirID.ToString("N"));
            if (cache is List<Data.Model.Users>)
            {
                return (List<Data.Model.Users>)cache;
            }
            else
            {
                var dir = Get(dirID);
                if (dir == null)
                {
                    return new List<Data.Model.Users>();
                }
                if (!dir.ReadUsers.IsNullOrEmpty())
                {
                    var users = new Organize().GetAllUsers(dir.ReadUsers);
                    YJ.Cache.IO.Opation.Set(DocumentsDirectoryReadUsers + dirID.ToString("N"), users);
                    return users;
                }
                else
                {
                    var parents = GetAllParents(dirID);
                    foreach (var parent in parents)
                    {
                        if (!parent.ReadUsers.IsNullOrEmpty())
                        {
                            var users = new Organize().GetAllUsers(parent.ReadUsers);
                            YJ.Cache.IO.Opation.Set(DocumentsDirectoryReadUsers + dirID.ToString("N"), users);
                            return users;
                        }
                    }
                    return new List<Data.Model.Users>();
                }
            }
        }

        /// <summary>
        /// 得到一个栏目的发布人员
        /// </summary>
        /// <param name="dirID"></param>
        /// <returns></returns>
        public List<Data.Model.Users> GetPublishUsers(Guid dirID)
        {
            var cache = YJ.Cache.IO.Opation.Get(DocumentsDirectoryPublishUsers + dirID.ToString("N"));
            if (cache is List<Data.Model.Users>)
            {
                return (List<Data.Model.Users>)cache;
            }
            else
            {
                var dir = Get(dirID);
                if (dir == null)
                {
                    return new List<Data.Model.Users>();
                }
                if (!dir.PublishUsers.IsNullOrEmpty())
                {
                    var users = new Organize().GetAllUsers(dir.PublishUsers);
                    YJ.Cache.IO.Opation.Set(DocumentsDirectoryPublishUsers + dirID.ToString("N"), users);
                    return users;
                }
                else
                {
                    var parents = GetAllParents(dirID);
                    foreach (var parent in parents)
                    {
                        if (!parent.PublishUsers.IsNullOrEmpty())
                        {
                            var users = new Organize().GetAllUsers(parent.PublishUsers);
                            YJ.Cache.IO.Opation.Set(DocumentsDirectoryPublishUsers + dirID.ToString("N"), users);
                            return users;
                        }
                    }
                    return new List<Data.Model.Users>();
                }
            }
        }

        /// <summary>
        /// 得到一个栏目的管理人员
        /// </summary>
        /// <param name="dirID"></param>
        /// <returns></returns>
        public List<Data.Model.Users> GetManageUsers(Guid dirID)
        {
            var cache = YJ.Cache.IO.Opation.Get(DocumentsDirectoryManageUsers + dirID.ToString("N"));
            if (cache is List<Data.Model.Users>)
            {
                return (List<Data.Model.Users>)cache;
            }
            else
            {
                var dir = Get(dirID);
                if (dir == null)
                {
                    return new List<Data.Model.Users>();
                }
                if (!dir.ManageUsers.IsNullOrEmpty())
                {
                    var users = new Organize().GetAllUsers(dir.ManageUsers);
                    YJ.Cache.IO.Opation.Set(DocumentsDirectoryManageUsers + dirID.ToString("N"), users);
                    return users;
                }
                else
                {
                    var parents = GetAllParents(dirID);
                    foreach (var parent in parents)
                    {
                        if (!parent.ManageUsers.IsNullOrEmpty())
                        {
                            var users = new Organize().GetAllUsers(parent.ManageUsers);
                            YJ.Cache.IO.Opation.Set(DocumentsDirectoryManageUsers + dirID.ToString("N"), users);
                            return users;
                        }
                    }
                    return new List<Data.Model.Users>();
                }
            }
        }

        /// <summary>
        /// 清空一个栏目的阅读人员，发布人员，管理人员缓存
        /// </summary>
        public void ClearDirUsersCache(Guid dirID)
        {
            YJ.Cache.IO.Opation.Remove(DocumentsDirectoryReadUsers + dirID.ToString("N"));
            YJ.Cache.IO.Opation.Remove(DocumentsDirectoryPublishUsers + dirID.ToString("N"));
            YJ.Cache.IO.Opation.Remove(DocumentsDirectoryManageUsers + dirID.ToString("N"));
        }

        /// <summary>
        /// 清除所有栏目的阅读人员，发布人员，管理人员缓存
        /// </summary>
        public void ClearAllDirUsersCache() 
        {
            var dirs = GetAll();
            foreach (var dir in dirs)
            {
                ClearDirUsersCache(dir.ID);
            }
        }

        /// <summary>
        /// 判断一个人员是否有栏目阅读权限
        /// </summary>
        /// <param name="dirID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool HasRead(Guid dirID, Guid userID)
        {
            var users = GetReadUsers(dirID);
            return users.Find(p => p.ID == userID) != null;
        }

        /// <summary>
        /// 判断一个人员是否有栏目发布权限
        /// </summary>
        /// <param name="dirID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool HasPublish(Guid dirID, Guid userID)
        {
            var users = GetPublishUsers(dirID);
            return users.Find(p => p.ID == userID) != null;
        }

        /// <summary>
        /// 判断一个人员是否有栏目管理权限
        /// </summary>
        /// <param name="dirID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool HasManage(Guid dirID, Guid userID)
        {
            var users = GetManageUsers(dirID);
            return users.Find(p => p.ID == userID) != null;
        }

        /// <summary>
        /// 判断当前栏目是否可以显示
        /// </summary>
        /// <param name="dirID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool HasDisplay(Data.Model.DocumentDirectory dir, Guid userID)
        {
            if (dir == null)
            {
                return false;
            }
            if (HasManage(dir.ID, userID) || HasRead(dir.ID, userID) || HasPublish(dir.ID, userID))
            {
                return true;
            }

            var childs = GetAllChilds(dir.ID);
            foreach (var child in childs)
            {
                if (HasManage(child.ID, userID) || HasRead(child.ID, userID) || HasPublish(child.ID, userID))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 得到一个人可以查看的栏目字典（用于移动端）
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Dictionary<Guid, string> GetDirs(Guid userID)
        {
            var dirs = GetAll();
            Dictionary<Guid, string> dict = new Dictionary<Guid, string>();
            foreach (var dir in dirs)
            {
                if (HasDisplay(dir, userID))
                {
                    dict.Add(dir.ID, GetAllParentsName(dir.ID, true, false));
                }
            }
            return dict;
        }


        /// <summary>
        /// 得到栏目树json
        /// </summary>
        /// <returns></returns>
        public string GetTreeJsonString()
        {
            var dirs = GetChilds(Guid.Empty);
            StringBuilder json = new StringBuilder();
            foreach (var dir in dirs)
            {
                var childs = GetDisplayChilds(dir.ID, Users.CurrentUserID);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", dir.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", dir.ParentID);
                json.AppendFormat("\"title\":\"{0}\",", dir.Name.Replace1("\"", "'"));
                json.AppendFormat("\"type\":\"{0}\",", "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", childs.Count>0 ? "1" : "0");
                json.Append("\"childs\":[");
                foreach (var child in childs)
                {
                    var childs1 = GetChilds(child.ID);
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", child.ID);
                    json.AppendFormat("\"parentID\":\"{0}\",", child.ParentID);
                    json.AppendFormat("\"title\":\"{0}\",", child.Name.Replace1("\"", "'"));
                    json.AppendFormat("\"type\":\"{0}\",", "2"); //类型：0根 1父 2子
                    json.AppendFormat("\"ico\":\"{0}\",", "");
                    json.AppendFormat("\"hasChilds\":\"{0}\",", childs1.Count > 0 ? "1" : "0");
                    json.Append("\"childs\":[");
                    json.Append("]}");
                    if (child.ID != childs.Last().ID)
                    {
                        json.Append(",");
                    }
                }
                json.Append("]},");
            }
            return "[" + json.ToString().TrimEnd(',') + "]";
        }

        public string GetTreeRefreshJsonString(Guid refreshID)
        {
            var dirs = GetDisplayChilds(refreshID, Users.CurrentUserID);
            StringBuilder json = new StringBuilder();
            foreach (var dir in dirs)
            {
                var childs = GetDisplayChilds(dir.ID, Users.CurrentUserID);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", dir.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", dir.ParentID);
                json.AppendFormat("\"title\":\"{0}\",", dir.Name.Replace1("\"", "'"));
                json.AppendFormat("\"type\":\"{0}\",", "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", childs.Count > 0 ? "1" : "0");
                json.Append("\"childs\":[");

                json.Append("]},");
            }
            return "[" + json.ToString().TrimEnd(',') + "]";
        }

    }
}
