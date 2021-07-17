using System;
using System.Data;
using System.Data.SqlClient;
using YJ.Platform;


namespace WebMvc.Common
{
    public class CustomFormSave
    {
        public static object GetJson(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            return new YJ.Data.MSSQL.DBHelper().GetDataTable("select * from users");
        }

        public static string QianShi(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            string t = System.Web.HttpContext.Current.Request["TempTest.Title"];
            YJ.Platform.Log.Add("获取值测试", t + "--");
            return "";
        }
        public static string UpdateSample(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            try
            {

            
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            DataTable samples=db.GetDataTable(string.Format("select *,b.name from AmApplySample a left join Organize b on a.UDeptID=Convert(nvarchar(50),b.id) where a.id='{0}'", eventParams.InstanceID));
          db.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address1])VALUES('{0}','{1}','{2}')", samples.Rows[0]["SampleId"], samples.Rows[0]["UID"], samples.Rows[0]["name"]));
                db.Execute(string.Format(" update AmSample set DisposeResult=2 where  '{0}' like '%'+bgbh+'%'", samples.Rows[0]["SampleId"]));
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("样品调用审批流程-发放样品-提交后事件", e.Message, YJ.Platform.Log.Types.流程相关);
                return "0";
            }
        }
        /// <summary>
        /// 修改批量加班完成状态
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string UpdateWorkOver(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("update OaWorkOverTime set IsFinished=1 where  groupid='{0}'", eventParams.InstanceID));
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("修改批量加班完成状态--提交后事件", e.Message, YJ.Platform.Log.Types.流程相关);
                return "0";
            }
        }
        /// <summary>
        /// 修改批量调休完成状态
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string UpdateLeave(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("update OaLeave set IsFinished=1 where  groupid='{0}'", eventParams.InstanceID));
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("修改批量调休完成状态--提交后事件", e.Message, YJ.Platform.Log.Types.流程相关);
                return "0";
            }
        }
        /// <summary>
        /// 测试用方法返回人员
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string GetMembers(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {

            return  "u_EB03262C-AB60-4BC6-A4C0-96E66A4229FE,u_8086D01F-7AE3-402E-B543-D34F1059F79A";
        }
        /// <summary>
        /// 发送会议通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SendNotice(YJ.Data.Model.WorkFlowCustomEventParams data)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            YJ.Platform.Users user = new YJ.Platform.Users();
            YJ.Platform.WorkFlowTask btask = new YJ.Platform.WorkFlowTask();
            YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
            YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
            YJ.Data.Model.ShortMessage sm = new YJ.Data.Model.ShortMessage();
            Users bUsers = new Users();
            ShortMessage shorMsg = new ShortMessage();
            string ID = data.InstanceID;
            string sql = "select Participants from CRMeetingRequest where ID='" + ID + "'";
            string Participants = db.ExecuteScalar(sql);
            string[] strArray = Participants.Split(','); //字符串转数组
            Guid groupID = Guid.NewGuid();
            foreach (var item in strArray)
            {
                string userid = user.RemovePrefix1(item);
                task.FlowID = data.FlowID;
                task.GroupID = data.GroupID;
                task.ID = Guid.NewGuid();
                task.Type = 0;
                task.InstanceID = data.InstanceID;
                task.Note = "会议通知";
                task.PrevID = "".ToGuid();
                task.PrevStepID = "950bc1a7-2be7-461a-a982-b6d1a87be1a2".ToGuid();
                task.ReceiveID = userid.ToGuid();
                task.ReceiveName = user.GetName(userid.ToGuid());
                task.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                task.SenderID = YJ.Platform.Users.CurrentUserID;
                task.SenderName = YJ.Platform.Users.CurrentUserName;
                YJ.Platform.WeiXin.Agents bAgents = new YJ.Platform.WeiXin.Agents();
                task.SenderTime = task.ReceiveTime;
                task.Status = 0;
                task.StepID = "029b5b49-adbd-4647-be26-1fe2c9196d94".ToGuid();
                task.StepName = "参会员工回执";
                task.Sort = 0;
                task.Title = "会议通知";
                btask.Add(task);
                Guid msgID = Guid.NewGuid();
                string msgContents = "您有一个新的待办任务，流程:会议通知。";
                string linkUrl = "javascript:openApp('/WorkFlowRun/Index?flowid=" + task.FlowID + "&stepid=" + task.StepID + "&instanceid=" + task.InstanceID + "&taskid=" + task.ID + "&groupid=" + task.GroupID + "',0,'" + task.Title.RemoveHTML().RemovePunctuationOrEmpty() + "','tab_" + task.ID + "');closeMessage('" + msgID + "');";
                ShortMessage.Send(task.ReceiveID, task.ReceiveName, "流程待办提醒", msgContents, 1, linkUrl, task.ID.ToString(), msgID.ToString(), groupID.ToString());
                new YJ.Platform.WeiXin.Message().SendText(msgContents, bUsers.GetAccountByID(task.ReceiveID), agentid: bAgents.GetAgentIDByCode("weixinagents_waittasks"), async: true);
            }
            return "通知已发送";

        }

        /// <summary>
        /// 车辆申请流程（院办公室审批提交后）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdataCar1(YJ.Data.Model.WorkFlowCustomEventParams data)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();//获取数据库连接对象
            YJ.Platform.Users user = new YJ.Platform.Users();//获取当前用户
            YJ.Platform.WorkFlowTask btask = new YJ.Platform.WorkFlowTask();
            YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
            YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
            YJ.Data.Model.ShortMessage sm = new YJ.Data.Model.ShortMessage();
            Users bUsers = new Users();
            ShortMessage shorMsg = new ShortMessage();
            string ID = data.InstanceID;//获取当前实例ID
            string sql1= "select CarNumber from OaVehicleApplication where ID='"+ID+"'";
            string CARID= db.ExecuteScalar(sql1);
            string sql = "update OaCar set Status='1' where ID='"+ CARID + "'";
            db.Execute(sql);
            return "修改车辆状态成功";
        }
        /// <summary>
        /// 车辆申请流程（院办公室审批退回后）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdataCar0(YJ.Data.Model.WorkFlowCustomEventParams data)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            YJ.Platform.Users user = new YJ.Platform.Users();
            YJ.Platform.WorkFlowTask btask = new YJ.Platform.WorkFlowTask();
            YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
            YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
            YJ.Data.Model.ShortMessage sm = new YJ.Data.Model.ShortMessage();
            Users bUsers = new Users();
            ShortMessage shorMsg = new ShortMessage();
            string ID = data.InstanceID;
            string sql1 = "select CarNumber from OaVehicleApplication where ID='" + ID + "'";
            string CARID = db.ExecuteScalar(sql1);
            string sql = "update OaCar set Status='0' where ID='" + CARID + "'";
            db.Execute(sql);
            return "修改车辆状态成功";
        }



        /// <summary>
        /// 标准物质领用流程完成后，把“标准物质出库记录对中应的数据出库标志更为1”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string RefMaterialFinishedAfter(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 标准物质出库记录 SET 是否完成出库=1 WHERE 申领流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("标准物质领取流程完成后，将流程中涉及的标物领取标志设置为1", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("标准物质领取流程完成后，将流程中涉及的标物领取标志设置为1", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }


        /// <summary>
        /// 办公用品领用流程完成后，把“办公用品出库记录对中应的数据出库标志更为1”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string OfficeSupFinishedAfter(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 办公用品出库记录 SET 是否完成出库=1 WHERE 申领流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("办公用品领取流程完成后，将流程中涉及的办公用品领取标志设置为1", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("办公用品领取流程完成后，将流程中涉及的办公用品领取标志设置为1", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }

        /// <summary>
        /// 办公用品采购阶段1，把“办公用品采购记录对中应的采购进度标志更为1”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string OfficeAcquisitionPhase1(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 办公用品采购记录 SET 采购进度=1 WHERE 申请流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("办公用品采购记录对中应的采购进度标志更为1", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("办公用品采购记录对中应的采购进度标志更为1", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }

        /// <summary>
        /// 办公用品采购阶段2完成采购，把“办公用品采购记录对中应的采购进度标志更为2”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string OfficeAcquisitionPhase2(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 办公用品采购记录 SET 采购进度=2 WHERE 申请流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("办公用品采购记录对中应的采购进度标志更为2", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("办公用品采购记录对中应的采购进度标志更为2", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }

        /// <summary>
        /// 办公用品采购阶段3完成入库，把“办公用品采购记录对中应的采购进度标志更为3”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string OfficeAcquisitionPhase3(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 办公用品采购记录 SET 采购进度=3 WHERE 申请流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("办公用品采购记录对中应的采购进度标志更为3", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("办公用品采购记录对中应的采购进度标志更为3", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }

        /// <summary>
        /// 化学试剂领用流程完成后，把“化学试剂出库记录对中应的数据出库标志更为1”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string ReagentFinishedAfter(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 化学试剂出库记录 SET 是否完成出库=1 WHERE 申领流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("化学试剂领取流程完成后，将流程中涉及的化学试剂领取标志设置为1", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("化学试剂领取流程完成后，将流程中涉及的化学试剂领取标志设置为1", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }


        /// <summary>
        /// 检验耗材领用流程完成后，把“检验耗材出库记录对中应的数据出库标志更为1”。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string ConsumableFinishedAfter(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;

            try
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                db.Execute(string.Format("UPDATE 检验耗材出库记录 SET 是否完成出库=1 WHERE 申领流程ID='{0}'", eventParams.InstanceID));
                YJ.Platform.Log.Add("检验耗材领取流程完成后，将流程中涉及的检验耗材领取标志设置为1", "", YJ.Platform.Log.Types.其它分类);
                return "1";
            }
            catch (Exception e)
            {
                YJ.Platform.Log.Add("检验耗材领取流程完成后，将流程中涉及的检验耗材领取标志设置为1", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";
            }
        }

        /// <summary>
        /// 检验耗材领用提交之前验证是否上传了关键验收记录。
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string ConsumableSubmitBefore(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            string ID = eventParams.InstanceID;
            //eventParams.InstanceID = null;
            //execute.ExecuteType = YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;

            return "alert('error')";


        }
        





        /// <summary>
        /// 子流程激活前事件（示例）
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static YJ.Data.Model.WorkFlowExecute.Execute SubFlowActivationBefore(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            
            //在这里添加插入子流程业务数据代码

            YJ.Platform.Log.Add("执行了子流程激活前事件", "", YJ.Platform.Log.Types.其它分类);
            //execute.Title = "";
            return execute;
        }

        /// <summary>
        /// 子流程结束后事件（示例）
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static void SubFlowCompletedBefore(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
           
            //在这里添加子流程结束后代码

            YJ.Platform.Log.Add("执行了子流程结束后事件", "", YJ.Platform.Log.Types.其它分类);
        }


      
        /// <summary>
        /// 检验耗材子流程完成时触发事件（与子流程事件不同）
        /// </summary>
        /// <param name="eventParams"></param>
        /// <returns></returns>
        public static string ConsumableSubFlowCompletedBefore(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {

            //在这里添加子流程结束后代码
            string ID = eventParams.InstanceID; //实例ID（检验耗材申领流程子表的ID）
            try {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                //db.Execute(string.Format("UPDATE 检验耗材出库记录 SET 是否完成出库=1 WHERE 申领流程ID='{0}'", eventParams.InstanceID));
                db.Execute(
                    string.Format(
                    "UPDATE 检验耗材出库记录 SET 申领流程ID = ("+
                    "SELECT top 1 InstanceID FROM WorkFlowTask WHERE SubFlowGroupID ="+
                    "(SELECT top 1 GroupID FROM WorkFlowTask WHERE InstanceID = '{0}'))"
                    , ID));

                YJ.Platform.Log.Add("执行了检验耗材子流程完成", "", YJ.Platform.Log.Types.其它分类);
                return "1";

            }

            catch (Exception e) {
                YJ.Platform.Log.Add("执行了检验耗材领取子流程结束后事件出错", e.Message, YJ.Platform.Log.Types.其它分类);
                return "0";

            }

        }



        public static string TestFunction(YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {

            //在这里添加子流程结束后代码
            string ID = eventParams.InstanceID; //实例ID（检验耗材申领流程子表的ID）
            try
            {

                return "false";

            }

            catch (Exception e)
            {
                return "0";

            }

        }







    }

    
}