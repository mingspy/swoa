using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Collections.Specialized;

namespace WebMvc.PlatformApiControllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage upLoad()
        {
            
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            NameValueCollection str = HttpContext.Current.Request.QueryString;
            string type = str.Get("type");
            string OrganizeID = str.Get("OrganizeID");
            string imgPath = "";
            string PhysicalPath = "";
            string Namebool = "";
      
            if (files.Count > 0)
            {
                imgPath = "/File//" + files[0].FileName;
                PhysicalPath = System.Web.HttpContext.Current.Server.MapPath(imgPath);
                files[0].SaveAs(PhysicalPath);
            }
            try
            {
                if (type == "1")//导入用户信息
                {
                    impotUsersInfo(PhysicalPath, OrganizeID,out Namebool);
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("导入用户数据成功!\r\n" + Namebool+ "")
                    };

                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"type参数错误!\"}", Encoding.UTF8, "application/json")
                    };

                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\"模板错误，请检查模板！\"}", Encoding.UTF8, "application/json")
                };

            }

        }
        ///// <summary>
        ///// 导入考勤打卡记录
        ///// </summary>
        ///// <param name="excelFile"></param>
        //private void importWorkAttendance(string excelFile)
        //{
        //    if (!string.IsNullOrEmpty(excelFile))
        //    {
        //        //读取表格数据信息
        //        System.Data.DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(excelFile);
        //        //插入数据库
                
        //        YJ.Utility.NPOIHelper.InsertWorkAttendance(dt);
                
        //    }
        //}
        //导入用户信息
        private void impotUsersInfo(string excelFile,string OrganizeID,out string Namebool)
        {
            
            YJ.Platform.Users busers = new YJ.Platform.Users();
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            Namebool = "";
            if (excelFile.IsNullOrEmpty()) {

                Namebool = "未找到文件地址中的文件";
            }
            Guid parentID = OrganizeID.ToGuid();
            string userXML = string.Empty;
            //读取表格数据信息
            System.Data.DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(excelFile);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (DataRow dr in dt.Rows)
                {
                    YJ.Data.Model.Users user = new YJ.Data.Model.Users();
                    user.ID = Guid.NewGuid();
                    user.Name = dr["姓名"].ToString().Trim();
                    user.Account = dr["帐号"].ToString().Trim();
                    if (new YJ.Platform.Users().HasAccount(user.Account))
                    {
                        Namebool += ""+ user.Account + "帐号已经被使用了\r\n";
                        continue;
                    }
                    user.Password = busers.GetUserEncryptionPassword(user.ID.ToString(), busers.GetInitPassword());
                    user.Status = 0;
                    user.Sort = Convert.ToInt32(dr["排序"]);
                    user.Note = dr["备注"].ToString().Trim();
                    user.Mobile = dr["手机"].ToString().Trim();
                    user.Tel = dr["办公电话"].ToString().Trim();
                    user.OtherTel = dr["其它联系方式"].ToString().Trim();
                    user.Fax = dr["传真"].ToString().Trim();
                    user.Email = dr["邮箱"].ToString().Trim();
                    user.QQ = dr["QQ"].ToString().Trim();
                    user.HeadImg = dr["头像"].ToString().Trim();
                    user.WeiXin = dr["微信号"].ToString().Trim();
                    if (dr["性别"].ToString().Trim().IsInt())
                    {
                        user.Sex = Convert.ToInt32(dr["性别"].ToString().Trim());
                    }
                    busers.Add(user);
                    //添加关系
                    YJ.Data.Model.UsersRelation userRelation = new YJ.Data.Model.UsersRelation();
                    userRelation.IsMain = 1;
                    userRelation.OrganizeID = parentID;
                    userRelation.Sort = new YJ.Platform.UsersRelation().GetMaxSort(parentID);
                    userRelation.UserID = user.ID;
                    new YJ.Platform.UsersRelation().Add(userRelation);

                    //更新父级[ChildsLength]字段
                    borganize.UpdateChildsLength(parentID);

                    //添加微信
                    if (YJ.Platform.WeiXin.Config.IsUse)
                    {
                        new YJ.Platform.WeiXin.Organize().AddUserAsync(user);
                    }
                    userXML = user.Serialize();
                    
                }
                YJ.Platform.Log.Add("添加了人员", userXML, YJ.Platform.Log.Types.组织机构);
                new YJ.Platform.MenuUser().ClearCache();
                new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                new YJ.Platform.DocumentDirectory().ClearAllDirUsersCache();//清除文档中心栏目缓存
                scope.Complete();
                
            }
        }
       
    }
}
