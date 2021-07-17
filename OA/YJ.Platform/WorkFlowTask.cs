using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace YJ.Platform
{
    public class WorkFlowTask : IEqualityComparer<YJ.Data.Model.WorkFlowTask>
    {
        private WorkFlow bWorkFlow = new WorkFlow();
        private YJ.Data.Interface.IWorkFlowTask dataWorkFlowTask;
        private YJ.Data.Model.WorkFlowInstalled wfInstalled;
        private YJ.Data.Model.WorkFlowExecute.Result result;
        private List<YJ.Data.Model.WorkFlowTask> nextTasks = new List<YJ.Data.Model.WorkFlowTask>();
		public WorkFlowTask()
		{
            this.dataWorkFlowTask = Data.Factory.Factory.GetWorkFlowTask();
		}
        

        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.WorkFlowTask model)
		{
			return dataWorkFlowTask.Add(model);
		}
		/// <summary>
		/// 更新
		/// </summary>
		public int Update(YJ.Data.Model.WorkFlowTask model)
		{
			return dataWorkFlowTask.Update(model);
		}
		/// <summary>
		/// 查询所有记录
		/// </summary>
		public List<YJ.Data.Model.WorkFlowTask> GetAll()
		{
			return dataWorkFlowTask.GetAll();
		}
		/// <summary>
		/// 查询单条记录
		/// </summary>
		public YJ.Data.Model.WorkFlowTask Get(Guid id)
		{
			return dataWorkFlowTask.Get(id);
		}
		/// <summary>
		/// 删除
		/// </summary>
		public int Delete(Guid id)
		{
			return dataWorkFlowTask.Delete(id);
		}
		/// <summary>
		/// 查询记录条数
		/// </summary>
		public long GetCount()
		{
			return dataWorkFlowTask.GetCount();
		}

        /// <summary>
        /// 去除重复的接收人，在退回任务时去重，避免一个人收到多条任务。
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns></returns>
        public bool Equals(YJ.Data.Model.WorkFlowTask task1, YJ.Data.Model.WorkFlowTask task2)
        {
            return task1.ReceiveID == task2.ReceiveID && task1.StepID == task2.StepID && task1.Sort == task2.Sort;
        }

        public int GetHashCode(YJ.Data.Model.WorkFlowTask task)
        {
            return task.ToString().GetHashCode();
        }

        /// <summary>
        /// 更新打开时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="openTime"></param>
        /// <param name="isStatus">是否将状态更新为1</param>
        public void UpdateOpenTime(Guid id, DateTime openTime, bool isStatus = false)
        {
            dataWorkFlowTask.UpdateOpenTime(id, openTime, isStatus);
        }

        /// <summary>
        /// 得到一个流程实例的发起者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <param name="isDefault">如果为空是否返回当前登录用户ID</param>
        /// <returns></returns>
        public Guid GetFirstSnderID(Guid flowID, Guid groupID, bool isDefault = false)
        {
            Guid senderID=dataWorkFlowTask.GetFirstSnderID(flowID, groupID);
            return senderID.IsEmptyGuid() && isDefault ? Users.CurrentUserID : senderID;
        }

        /// <summary>
        /// 得到一个流程实例的发起者部门
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public Guid GetFirstSnderDeptID(Guid flowID, Guid groupID)
        {
            if (flowID.IsEmptyGuid() || groupID.IsEmptyGuid())
            {
                return Users.CurrentDeptID; 
            }
            var senderID = dataWorkFlowTask.GetFirstSnderID(flowID, groupID);
            var dept = new Users().GetDeptByUserID(senderID);
            return dept == null ? Guid.Empty : dept.ID;
        }


        /// <summary>
        /// 得到一个流程实例一个步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            return dataWorkFlowTask.GetStepSnderID(flowID, stepID, groupID);
        }

        /// <summary>
        /// 得到一个流程实例一个步骤的处理者字符串
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public string GetStepSnderIDString(Guid flowID, Guid stepID, Guid groupID)
        {
            var list = GetStepSnderID(flowID, stepID, groupID);
            StringBuilder sb = new StringBuilder(list.Count * 43);
            foreach (var li in list)
            {
                sb.Append(YJ.Platform.Users.PREFIX);
                sb.Append(li);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            return dataWorkFlowTask.GetPrevSnderID(flowID, stepID, groupID);
        }

        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public String GetPrevSnderIDString(Guid flowID, Guid stepID, Guid groupID)
        {
            var list = dataWorkFlowTask.GetPrevSnderID(flowID, stepID, groupID);
            StringBuilder sb = new StringBuilder(list.Count * 43);
            foreach (var li in list)
            {
                sb.Append(YJ.Platform.Users.PREFIX);
                sb.Append(li);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 将json字符串转换为执行实体
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        private YJ.Data.Model.WorkFlowExecute.Execute GetExecuteModel(string jsonString)
        {
            YJ.Data.Model.WorkFlowExecute.Execute execute = new YJ.Data.Model.WorkFlowExecute.Execute();
            YJ.Platform.Organize borganize = new Organize();

            LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(jsonString);
            if (jsondata == null) return execute;

            execute.Comment = jsondata["comment"].ToString();
            string op = jsondata["type"].ToString().ToLower();
            switch (op)
            { 
                case "submit":
                    execute.ExecuteType = YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit;
                    break;
                case "save":
                    execute.ExecuteType = YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;
                    break;
                case "back":
                    execute.ExecuteType = YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back;
                    break;
            }
            execute.FlowID = jsondata["flowid"].ToString().ToGuid();
            execute.GroupID = jsondata["groupid"].ToString().ToGuid();
            execute.InstanceID = jsondata["instanceid"].ToString();
            execute.IsSign = jsondata["issign"].ToString().ToInt() == 1;
            execute.StepID = jsondata["stepid"].ToString().ToGuid();
            execute.TaskID = jsondata["taskid"].ToString().ToGuid();
           
            var stepsjson = jsondata["steps"];
            Dictionary<Guid, List<YJ.Data.Model.Users>> steps = new Dictionary<Guid, List<YJ.Data.Model.Users>>();
            if (stepsjson.IsArray)
            {
                foreach (LitJson.JsonData step in stepsjson)
                {
                    var id = step["id"].ToString().ToGuid();
                    var member = step["member"].ToString();
                    if (id == Guid.Empty || member.IsNullOrEmpty())
                    {
                        continue;
                    }
                    steps.Add(id, borganize.GetAllUsers(member));
                }
            }
            execute.Steps = steps;
            return execute;
        }

        /// <summary>
        /// 处理流程
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public YJ.Data.Model.WorkFlowExecute.Result Execute(string jsonString)
        {
            return Execute(GetExecuteModel(jsonString));
        }

        /// <summary>
        /// 发起一个流程
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="users"></param>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        public bool StartFlow(Guid flowID, List<Data.Model.Users> users, string title, string instanceID = "")
        {
            if (users.Count == 0)
            {
                return false;
            }
            try
            {
                foreach (var user in users)
                {
                    YJ.Data.Model.WorkFlowExecute.Execute executeModel = new Data.Model.WorkFlowExecute.Execute();
                    executeModel.ExecuteType = Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;
                    executeModel.FlowID = flowID;
                    executeModel.InstanceID = instanceID;
                    executeModel.Title = title;
                    executeModel.Sender = user;
                    createFirstTask(executeModel);
                }
                return true;
            }
            catch (Exception err)
            {
                Platform.Log.Add(err);
                return false;
            }
        }

        private static readonly object lockobj = new object();
        /// <summary>
        /// 处理流程
        /// </summary>
        /// <param name="executeModel">处理实体</param>
        /// <returns></returns>
        public YJ.Data.Model.WorkFlowExecute.Result Execute(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            result = new YJ.Data.Model.WorkFlowExecute.Result();
            nextTasks = new List<YJ.Data.Model.WorkFlowTask>();
            if (executeModel.FlowID == Guid.Empty)
            {
                result.DebugMessages = "流程ID错误";
                result.IsSuccess = false;
                result.Messages = "执行参数错误";
                return result;
            }
            

            wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
            if (wfInstalled == null)
            {
                result.DebugMessages = "未找到流程运行时实体";
                result.IsSuccess = false;
                result.Messages = "流程运行时为空";
                return result;
            }

            lock (lockobj)
            {
                switch (executeModel.ExecuteType)
                {
                    case YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back:
                        executeBack(executeModel);
                        break;
                    case YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save:
                        executeSave(executeModel);
                        break;
                    case YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit:
                    case YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed:
                        executeSubmit(executeModel);
                        break;
                    case YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Redirect:
                        executeRedirect(executeModel);
                        break;
                    case Data.Model.WorkFlowExecute.EnumType.ExecuteType.AddWrite:
                        executeAddWrite(executeModel);
                        break;
                    case Data.Model.WorkFlowExecute.EnumType.ExecuteType.CopyforCompleted:
                        executeCopyforComplete(executeModel);
                        break;
                    default:
                        result.DebugMessages = "流程处理类型为空";
                        result.IsSuccess = false;
                        result.Messages = "流程处理类型为空";
                        return result;
                }

                result.NextTasks = nextTasks;
                //添加消息
                //保存和抄送完成不发送站内消息
                if (executeModel.ExecuteType != Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save 
                    && executeModel.ExecuteType != Data.Model.WorkFlowExecute.EnumType.ExecuteType.CopyforCompleted)
                {
                    ShortMessage shorMsg = new ShortMessage();
                    Users bUsers = new Users();
                    WeiXin.Agents bAgents = new WeiXin.Agents();
                    shorMsg.Delete(executeModel.TaskID.ToString(), 1);
                    Guid groupID = Guid.NewGuid();
                    foreach (var task in result.NextTasks.Where(p => p.Status == 0))
                    {
                        if (!task.ReceiveID.IsEmptyGuid())
                        {
                            //下一步处理人是自己不发站内消息
                            if (task.ReceiveID == task.SenderID)
                            {
                                continue;
                            }
                            string url = "";
                            if (System.Web.HttpContext.Current.Request.Url != null
                                && System.Web.HttpContext.Current.Request.Url.AbsolutePath.EndsWith(".aspx", StringComparison.CurrentCultureIgnoreCase))
                            {
                                url = "/Platform/WorkFlowRun/Default.aspx";
                            }
                            else
                            {
                                url = "/WorkFlowRun/Index";
                            }
                            Guid msgID = Guid.NewGuid();
                            string msgContents = "您有一个新的待办任务，流程:" + wfInstalled.Name + "，步骤：" + task.StepName + "。";
                            string linkUrl = "javascript:openApp('" + url + "?flowid=" + task.FlowID + "&stepid=" + task.StepID + "&instanceid=" + task.InstanceID + "&taskid=" + task.ID + "&groupid=" + task.GroupID + "',0,'" + task.Title.RemoveHTML().RemovePunctuationOrEmpty() + "','tab_" + task.ID + "');closeMessage('" + msgID + "');";
                            ShortMessage.Send(task.ReceiveID, task.ReceiveName, "流程待办提醒", msgContents, 1, linkUrl, task.ID.ToString(), msgID.ToString(), groupID.ToString());
                            //发送微信消息
                            if (WeiXin.Config.IsUse)
                            {
                                new WeiXin.Message().SendText(msgContents, bUsers.GetAccountByID(task.ReceiveID), agentid: bAgents.GetAgentIDByCode("weixinagents_waittasks"), async: true);
                            }
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeSubmit(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                //如果是第一步提交并且没有实例则先创建实例
                YJ.Data.Model.WorkFlowTask currentTask = null;
                bool isFirst = executeModel.StepID == wfInstalled.FirstStepID && executeModel.TaskID == Guid.Empty && executeModel.GroupID == Guid.Empty;
                if (isFirst)
                {
                    currentTask = createFirstTask(executeModel);
                    executeModel.TaskID = currentTask.ID;
                }
                else
                {
                    currentTask = Get(executeModel.TaskID);
                    if (currentTask == null)
                    {
                        throw new Exception("未找到要提交的任务");
                    }
                    //使用程序创建的第一步任务有时候instanceID没有值，需要在这里更新一下
                    if (currentTask.InstanceID.IsNullOrEmpty() && !executeModel.InstanceID.IsNullOrEmpty())
                    {
                        currentTask.InstanceID = executeModel.InstanceID;
                        Update(currentTask);
                    }
                }
                if (currentTask == null)
                {
                    result.DebugMessages = "未能创建或找到当前任务";
                    result.IsSuccess = false;
                    result.Messages = "未能创建或找到当前任务";
                    return;
                }
                else if (currentTask.Status.In(2, 3, 4, 5, 6, 7))
                {
                    result.DebugMessages = "当前任务已处理";
                    result.IsSuccess = false;
                    result.Messages = "当前任务已处理";
                    return;
                }
                else if (currentTask.ReceiveID != Users.CurrentUserID && currentTask.ReceiveID != WeiXin.Organize.CurrentUserID && currentTask.IsExpiredAutoSubmit == 0)
                {
                    result.DebugMessages = "不能处理当前任务";
                    result.IsSuccess = false;
                    result.Messages = "不能处理当前任务";
                    return;
                }
                var currentSteps = wfInstalled.Steps.Where(p => p.ID == executeModel.StepID);
                var currentStep = currentSteps.Count() > 0 ? currentSteps.First() : null;
                if (currentStep == null)
                {
                    result.DebugMessages = "未找到当前步骤";
                    result.IsSuccess = false;
                    result.Messages = "未找到当前步骤";
                    return;
                }
                

                //如果当前步骤是子流程步骤，并且策略是 子流程完成后才能提交 则要判断子流程是否已完成
                if (currentStep.Type == "subflow" 
                    && currentStep.SubFlowID.IsGuid()
                    && currentStep.Behavior.SubFlowStrategy == 0 
                    && !currentTask.SubFlowGroupID.IsNullOrEmpty() 
                    )
                {
                    foreach (string groupID in currentTask.SubFlowGroupID.Split(','))
                    {
                        if (!GetInstanceIsCompleted(currentStep.SubFlowID.ToGuid(), groupID.ToGuid()))
                        {
                            result.DebugMessages = "当前步骤的子流程实例未完成,子流程：" + currentStep.SubFlowID + ",实例组：" + currentTask.SubFlowGroupID.ToString();
                            result.IsSuccess = false;
                            result.Messages = "当前步骤的子流程未完成,不能提交!";
                            return;
                        }
                    }
                }

                int status = 0;//步骤是否通过，为-1

                //是否是完成任务或者没有后续处理步骤
                bool isCompletedTask = executeModel.ExecuteType == Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed 
                    || executeModel.Steps == null || executeModel.Steps.Count == 0;
                
                #region 处理策略判断
                var tjTaskList = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID);
                if (currentTask.StepID != wfInstalled.FirstStepID)//第一步不判断策略
                {
                    switch (currentStep.Behavior.HanlderModel)
                    {
                        case 0://所有人必须处理
                            var taskList = tjTaskList.FindAll(p => p.Sort == currentTask.Sort && p.Type != 5 && p.Type != 7);
                            if (taskList.Count > 1)
                            {
                                var noCompleted = taskList.Where(p => p.Status != 2);
                                if (noCompleted.Count() - 1 > 0)
                                {
                                    status = -1;
                                }
                            }
                            if (!isCompletedTask)
                            {
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                            }
                            break;
                        case 1://一人同意即可
                            var taskList1 = tjTaskList.FindAll(p => p.Sort == currentTask.Sort && p.Type != 5 && p.Type != 7);
                            foreach (var task in taskList1)
                            {
                                if (task.ID != currentTask.ID)
                                {
                                    if (task.Status.In(0, 1))
                                    {
                                        Completed(task.ID, "", false, 4);
                                    }
                                }
                                else
                                {
                                    if (!isCompletedTask)
                                    {
                                        Completed(task.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                                    }
                                }
                            }
                            break;
                        case 2://依据人数比例
                            var taskList2 = tjTaskList.FindAll(p => p.Sort == currentTask.Sort && p.Type != 5 && p.Type != 7);
                            if (taskList2.Count > 1)
                            {
                                decimal percentage = currentStep.Behavior.Percentage <= 0 ? 100 : currentStep.Behavior.Percentage;//比例
                                if ((((decimal)(taskList2.Where(p => p.Status == 2).Count() + 1) / (decimal)taskList2.Count) * 100).Round() < percentage)
                                {
                                    status = -1;
                                }
                                else
                                {
                                    foreach (var task in taskList2)
                                    {
                                        if (task.ID != currentTask.ID && task.Status.In(0, 1))
                                        {
                                            Completed(task.ID, "", false, 4);
                                        }
                                    }
                                }
                            }
                            if (!isCompletedTask)
                            {
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                            }
                            break;
                        case 3://独立处理
                            if (!isCompletedTask)
                            {
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                            }
                            break;
                        case 4://选择人员顺序审批
                            //激活当前任务的下一个任务
                            var taskList3 = tjTaskList.FindAll(p => p.Sort == currentTask.Sort && p.Type != 5 && p.Type != 7 && p.StepSort == currentTask.StepSort + 1);
                            if (taskList3.Count > 0)
                            {
                                status = -3;
                                foreach (var task in taskList3)
                                {
                                    task.Status = 0;
                                    Update(task);
                                }
                            }
                            if (!isCompletedTask)
                            {
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                            }
                            break;
                    }
                }
                else
                {
                    if (!isCompletedTask)
                    {
                        Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                    }
                }
                #endregion

                //如果是完成任务或者没有后续处理步骤，则完成任务
                if (isCompletedTask)
                {
                    executeComplete(executeModel);
                    #region 如果该任务是子流程任务则要判断是否应该提交主流程步骤
                    var subTask = GetTaskList(Guid.Empty, currentTask.GroupID).Find(p => p.OtherType == 4);
                    if (subTask != null)
                    {
                        var mainTasks = GetBySubFlowGroupID(subTask.GroupID);
                        bool subFlowIsCompleted = true;
                        foreach (var mainTask in mainTasks)
                        {
                            if (!subFlowIsCompleted)
                            {
                                break;
                            }
                            foreach (string subFlowGroupID in mainTask.SubFlowGroupID.Split(','))
                            {
                                if (!GetInstanceIsCompleted(subTask.FlowID, subFlowGroupID.ToGuid()))
                                {
                                    subFlowIsCompleted = false;
                                    break;
                                }
                            }
                        }
                        if (subFlowIsCompleted)
                        {
                            foreach (var mainTask in mainTasks)
                            {
                                var autoResult = AutoSubmit(mainTask);
                                Log.Add("子流程完成后提交主流程步骤-" + mainTask.Title, "是否成功：" + autoResult.IsSuccess.ToString() + " 信息：" + autoResult.DebugMessages, Log.Types.流程相关);
                            }
                        }
                    }
                    #endregion
                    
                    scope.Complete();
                    return;
                }

                #region 判断加签
                var noAddWriteTasks = tjTaskList.FindAll(p => p.Sort == currentTask.Sort && p.Type != 5 && p.Type != 7);
                foreach (var noAddTask in noAddWriteTasks)
                {
                    var addTasks = tjTaskList.FindAll(p => p.PrevID == noAddTask.ID && p.Type == 7);
                    if (noAddTask.ID == currentTask.ID && addTasks.Count>0)
                    {
                        foreach (var addTask in addTasks)
                        {
                            if(!addTask.OtherType.HasValue)
                            {
                                continue;
                            }
                            int addType = addTask.OtherType.Value.ToString().Left(1).ToString().ToInt();
                            int writeType = addTask.OtherType.Value.ToString().Right(1).ToString().ToInt();
                            //如果是后加签本步骤通过了则要将后加签的人的待办状态更新为0
                            if (addType == 2)
                            {
                                if (writeType.In(1, 2) || (writeType == 3 && addTask.ReceiveID == addTasks.FindAll(p => p.Status.In(-1, 0, 1)).OrderBy(p => p.ReceiveTime).FirstOrDefault().ReceiveID))
                                {
                                    addTask.Status = 0;
                                    Update(addTask);
                                }
                            }
                        }
                    }
                    List<YJ.Data.Model.WorkFlowTask> addWriteTasks1 = new List<Data.Model.WorkFlowTask>();
                    if (addTasks.Count > 0 && !isPassingAddWrite(addTasks.FirstOrDefault(), out addWriteTasks1))
                    {
                        status = -2;
                        break;
                    }
                }
                #endregion

                //如果条件不满足则创建一个状态为-1的后续任务，等条件满足后才修改状态，待办人员才能看到任务。
                if (status == -1 || status == -2 || status == -3)
                {
                    var tempTasks = createTempTasks(executeModel, currentTask);
                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in tempTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    nextTasks.AddRange(tempTasks);
                    string stepName = nextStepName.Distinct().ToArray().Join1(",");
                    result.DebugMessages += string.Format("已发送到:{0},{1},不创建后续任务", stepName, status == -2 ? "加签人未处理" : status == -3 ? "顺序处理的下一人处理" : "其他人未处理");
                    result.IsSuccess = true;
                    result.Messages += string.Format("已发送到:{0}{1}!", stepName, status == -2 ? ",等待加签人处理" : status == -3 ? "" : ",等待他人处理");
                    result.NextTasks = nextTasks;
                    scope.Complete();
                    return;
                }
                
                List<YJ.Data.Model.WorkFlowTask> autoSubmitTasks = new List<Data.Model.WorkFlowTask>();//记录需要自动提交的子流程任务
                foreach (var step in executeModel.Steps)
                {
                    Guid subflowGroupOneID = Guid.NewGuid();//如果子流程是多人同一实例，所有任务使用一个组ID
                    StringBuilder subflowGroupID = new StringBuilder();//如果子流程是多人单独实例，保存每个任务的组ID
                    List<Data.Model.WorkFlowTask> tempTasks = new List<Data.Model.WorkFlowTask>();
                    int stepSort = 0;//记录步骤内人员处理顺序
                    foreach (var user in step.Value)
                    {
                        if (wfInstalled == null) //子流程有多个人员时此处会为空，所以判断并重新获取
                        {
                            wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
                        }
                        var nextSteps = wfInstalled.Steps.Where(p => p.ID == step.Key);
                        if (nextSteps.Count() == 0)
                        {
                            continue;
                        }
                        var nextStep = nextSteps.First();

                        bool isPassing = 0 == nextStep.Behavior.Countersignature;

                        #region 如果下一步骤为会签，则要检查当前步骤的平级步骤是否已处理
                        if (0 != nextStep.Behavior.Countersignature)
                        {
                            var prevSteps = bWorkFlow.GetPrevSteps(executeModel.FlowID, nextStep.ID);
                            if (prevSteps.Count > 1)
                            {
                                Guid currentPrevStep = currentTask.PrevStepID;
                                switch (nextStep.Behavior.Countersignature)
                                {
                                    case 1://所有步骤同意
                                        isPassing = true;
                                        foreach (var prevStep in prevSteps)
                                        {
                                            if (!IsPassing(prevStep, executeModel.FlowID, executeModel.GroupID, currentTask, currentPrevStep))
                                            {
                                                isPassing = false;
                                                break;
                                            }
                                        }
                                        break;
                                    case 2://一个步骤同意即可
                                        isPassing = false;
                                        foreach (var prevStep in prevSteps)
                                        {
                                            if (IsPassing(prevStep, executeModel.FlowID, executeModel.GroupID, currentTask, currentPrevStep))
                                            {
                                                isPassing = true;
                                                break;
                                            }
                                        }
                                        break;
                                    case 3://依据比例
                                        int passCount = 0;
                                        if (prevSteps.Count == 0)
                                        {
                                            isPassing = true;
                                        }
                                        else
                                        {
                                            foreach (var prevStep in prevSteps)
                                            {
                                                if (IsPassing(prevStep, executeModel.FlowID, executeModel.GroupID, currentTask, currentPrevStep))
                                                {
                                                    passCount++;
                                                }
                                            }
                                            isPassing = (((decimal)passCount / (decimal)prevSteps.Count) * 100).Round() >= (nextStep.Behavior.CountersignaturePercentage <= 0 ? 100 : nextStep.Behavior.CountersignaturePercentage);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                isPassing = true;
                            }
                            if (isPassing)
                            {
                                var tjTasks = GetTaskList(currentTask.ID, false);
                                foreach (var tjTask in tjTasks)
                                {
                                    if (tjTask.ID == currentTask.ID || tjTask.Type == 5 || tjTask.Status.In(2, 3, 4, 5, 6, 7))
                                    {
                                        continue;
                                    }
                                    Completed(tjTask.ID, "", false, 4);
                                }
                            }
                        }
                        #endregion

                        if (isPassing)
                        {
                            YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
                            //如果指定了具体的完成时间，就写入，否则就加上指定的工时小数 
                            if (executeModel.StepCompletedTimes.Keys.Contains(nextStep.ID))
                            {
                                task.CompletedTime = executeModel.StepCompletedTimes[nextStep.ID];
                            }
                            else if (nextStep.WorkTime > 0)
                            {
                                task.CompletedTime = new WorkCalendar().GetWorkDate((double)nextStep.WorkTime, YJ.Utility.DateTimeNew.Now);  //YJ.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                            }
                            task.IsExpiredAutoSubmit = nextStep.TimeOutModel;

                            task.FlowID = executeModel.FlowID;
                            task.GroupID = currentTask != null ? currentTask.GroupID : executeModel.GroupID;
                            task.ID = Guid.NewGuid();
                            task.Type = 0;
                            task.InstanceID = executeModel.InstanceID;
                            if (!executeModel.Note.IsNullOrEmpty())
                            {
                                task.Note = executeModel.Note;
                            }
                            task.PrevID = currentTask.ID;
                            task.PrevStepID = currentTask.StepID;
                            task.ReceiveID = user.ID;
                            task.ReceiveName = user.Name;
                            task.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                            task.SenderID = executeModel.Sender.ID;
                            task.SenderName = executeModel.Sender.Name;
                            task.SenderTime = task.ReceiveTime;
                            task.StepID = step.Key;
                            task.StepName = nextStep.Name;
                            task.Sort = currentTask.Sort + 1;
                            task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;
                            task.OtherType = executeModel.OtherType;
                            if (4 != nextStep.Behavior.HanlderModel)
                            {
                                task.Status = status;
                            }
                            else //如果是选择人员顺序处理，则要隐藏不是第一个人的任务
                            {
                                task.Status = stepSort == 0 ? 0 : -1;
                            }
                            task.StepSort = stepSort++;
                          

                            #region 如果当前步骤是子流程步骤，则要发起子流程实例
                            if (nextStep.Type == "subflow" && nextStep.SubFlowID.IsGuid())
                            {
                                YJ.Data.Model.WorkFlowExecute.Execute subflowExecuteModel = null;
                                if (!nextStep.Event.SubFlowActivationBefore.IsNullOrEmpty())
                                {
                                    object obj = ExecuteFlowCustomEvent(nextStep.Event.SubFlowActivationBefore.Trim(),
                                        new YJ.Data.Model.WorkFlowCustomEventParams()
                                        {
                                            FlowID = executeModel.FlowID,
                                            GroupID = currentTask.GroupID,
                                            InstanceID = currentTask.InstanceID,
                                            StepID = executeModel.StepID,
                                            TaskID = currentTask.ID
                                        });
                                    if (obj is YJ.Data.Model.WorkFlowExecute.Execute)
                                    {
                                        subflowExecuteModel = (YJ.Data.Model.WorkFlowExecute.Execute)obj;
                                    }
                                }
                                if (subflowExecuteModel == null)
                                {
                                    subflowExecuteModel = new YJ.Data.Model.WorkFlowExecute.Execute();
                                }
                                subflowExecuteModel.ExecuteType = YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Save;
                                subflowExecuteModel.FlowID = nextStep.SubFlowID.ToGuid();
                                subflowExecuteModel.Sender = user;
                                if (subflowExecuteModel.Title.IsNullOrEmpty())
                                {
                                    subflowExecuteModel.Title = bWorkFlow.GetFlowName(subflowExecuteModel.FlowID);
                                }
                                if (subflowExecuteModel.InstanceID.IsNullOrEmpty())
                                {
                                    subflowExecuteModel.InstanceID = "";
                                }
                                if (nextStep.SubFlowTaskType == 0)
                                {
                                    subflowExecuteModel.GroupID = subflowGroupOneID;
                                }
                                else
                                {
                                    subflowExecuteModel.GroupID = Guid.NewGuid();
                                    subflowGroupID.Append(subflowExecuteModel.GroupID.ToString());
                                    subflowGroupID.Append(",");
                                }
                                subflowExecuteModel.OtherType = 4;

                                var subflowTask = createFirstTask(subflowExecuteModel, true);

                                //如果是子流程则任务接收人为任务发起人
                                task.ReceiveID = currentTask.ReceiveID;
                                task.ReceiveName = currentTask.ReceiveName;
                                //将SubFlowGroupID标识为空GUID，以便后面判断任务有子流程，设置SubFlowGroupID
                                task.SubFlowGroupID = Guid.Empty.ToString();
                                //将子流程处理策略临时保存到这个字段，以便后面判断，判断之后改回空
                                task.OtherType = nextStep.Behavior.SubFlowStrategy == 0 ? 2 : 3;                               
                                //将当前任务类型标识为子流程任务
                                task.Type = 6;
                            }
                            
                            #endregion

                            
                            nextTasks.Add(task);
                            tempTasks.Add(task);
                        }
                    }

                    foreach (var nextTask in tempTasks.Distinct())
                    {
                        //2017-6-26添加了条件currentStep.Behavior.HanlderModel == 3目的在于如果是独立处理的任务，下一步骤接收人可以接收重复的任务
                        if (currentStep.Behavior.HanlderModel == 3 || !HasNoCompletedTasks(executeModel.FlowID, step.Key, currentTask.GroupID, nextTask.ReceiveID))
                        {
                            if (nextTask.Type == 6)//如果是子流程任务则要更新SubFlowGroupID值，关联子流程的GroupID
                            {
                                if (subflowGroupID.Length == 0)
                                {
                                    nextTask.SubFlowGroupID = subflowGroupOneID.ToString();
                                }
                                else
                                {
                                    nextTask.SubFlowGroupID = subflowGroupID.ToString().TrimEnd(',');
                                }
                                //nextTask.OtherType = null
                                
                                Add(nextTask);
                                if (nextTask.OtherType == 3)
                                {
                                    
                                    autoSubmitTasks.Add(nextTask);
                                }
                            }
                            else
                            {
                                
                                Add(nextTask);
                            }
                        }
                    }
                }

                if (nextTasks.Count > 0)
                {
                    //激活临时任务
                    var nextWaitSteps = nextTasks.GroupBy(p => p.StepID);
                    if (wfInstalled == null) //子流程有多个人员时此处会为空，所以判断并重新获取
                    {
                        wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
                    }
                    foreach (var nextStep in nextWaitSteps)
                    {
                        //2018-1-27判断，如果接收任务的步骤类型是按人中顺序处理，则不要激活任务。
                        var nextSteps = wfInstalled.Steps.Where(p => p.ID == nextStep.Key);
                        if (nextSteps.Count() == 0)
                        {
                            continue;
                        }
                        var nextStep1 = nextSteps.First();
                        if (nextStep1.Behavior.HanlderModel == 4)
                        {
                            continue;
                        }
                        //2018-1-27判断完
                        
                        dataWorkFlowTask.UpdateTempTasks(nextTasks.FirstOrDefault().FlowID, nextStep.Key, nextTasks.FirstOrDefault().GroupID,
                            nextTasks.FirstOrDefault().CompletedTime, nextTasks.FirstOrDefault().ReceiveTime);
                    }
                   

                    //将当前步骤未处理的任务完成 如果是自由流程任务或者独立处理策略或者是按人员顺序处理，则不完成
                    if (executeModel.OtherType != 1 && currentStep.Behavior.HanlderModel != 3 && currentStep.Behavior.HanlderModel != 4)
                    {
                        var currStepTasks = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID);
                        foreach (var currStepTask in currStepTasks)
                        {
                            if (currStepTask.Status.In(-1, 0, 1) && currStepTask.Type != 5 && currStepTask.OtherType != 1)
                            {
                                Completed(currStepTask.ID, "", false, 4);
                            }
                        }
                    }

                    #region 抄送
                    if (wfInstalled == null)
                    {
                        wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
                    }
                    foreach (var step in executeModel.Steps)
                    {
                        var nextSteps = wfInstalled.Steps.Where(p => p.ID == step.Key);
                        if (nextSteps.Count() > 0)
                        {
                            var nextStep = nextSteps.First();
                            var copyForUsers = GetCopyForUsers(nextStep.CopyFor, currentTask);
                            foreach (var user in copyForUsers)
                            {
                                //如果已有任务则不抄送
                                if (nextTasks.Find(p => p.ReceiveID == user.ID) != null)
                                {
                                    continue;
                                }
                                YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
                                //如果指定了具体的完成时间，就写入，否则就加上指定的工时小数 
                                if (executeModel.StepCompletedTimes.Keys.Contains(nextStep.ID))
                                {
                                    task.CompletedTime = executeModel.StepCompletedTimes[nextStep.ID];
                                }
                                else if (nextStep.WorkTime > 0)
                                {
                                    task.CompletedTime = new WorkCalendar().GetWorkDate((double)nextStep.WorkTime, YJ.Utility.DateTimeNew.Now);  //YJ.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                                }
                                task.FlowID = executeModel.FlowID;
                                task.GroupID = currentTask != null ? currentTask.GroupID : executeModel.GroupID;
                                task.ID = Guid.NewGuid();
                                task.Type = 5;
                                task.InstanceID = executeModel.InstanceID;
                                task.Note = executeModel.Note.IsNullOrEmpty() ? "抄送" : executeModel.Note + "(抄送)";
                                task.PrevID = currentTask.ID;
                                task.PrevStepID = currentTask.StepID;
                                task.ReceiveID = user.ID;
                                task.ReceiveName = user.Name;
                                task.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                                task.SenderID = executeModel.Sender.ID;
                                task.SenderName = executeModel.Sender.Name;
                                task.SenderTime = task.ReceiveTime;
                                task.Status = status;
                                task.StepID = step.Key;
                                task.StepName = nextStep.Name;
                                task.Sort = currentTask.Sort + 1;
                                task.StepSort = 0;
                                task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;
                                if (!HasNoCompletedTasks(executeModel.FlowID, step.Key, currentTask.GroupID, user.ID))
                                {
                                    Add(task);
                                }
                            }
                        }
                    }
                    #endregion

                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in nextTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    string stepName = nextStepName.Distinct().ToArray().Join1(",");
                    result.DebugMessages += string.Format("已发送到:{0}", stepName);
                    result.IsSuccess = true;
                    result.Messages += string.Format("已发送到:{0}", stepName);
                    result.NextTasks = nextTasks;
                }
                else
                {
                    var tempTasks = createTempTasks(executeModel, currentTask);
                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in tempTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    nextTasks.AddRange(tempTasks);
                    string stepName = nextStepName.Distinct().ToArray().Join1(",");
                    result.DebugMessages += string.Format("已发到:{0},等待其它步骤处理", stepName);
                    result.IsSuccess = true;
                    result.Messages += string.Format("已发送:{0},等待其它步骤处理", stepName);
                    result.NextTasks = nextTasks;
                }

                //如果子流程发起即提交，则要立即提交任务
                if (autoSubmitTasks.Count > 0)
                {
                    foreach (var subTask in autoSubmitTasks)
                    {
                        AutoSubmit(subTask);
                    }
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// 退回任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeBack(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            var currentTask = Get(executeModel.TaskID);
            if (currentTask == null)
            {
                result.DebugMessages = "未能找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未能找到当前任务";
                return;
            }
            else if (currentTask.Status.In(2, 3, 4, 5, 6, 7))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            else if (currentTask.ReceiveID != Users.CurrentUserID && currentTask.ReceiveID != WeiXin.Organize.CurrentUserID && currentTask.IsExpiredAutoSubmit == 0)
            {
                result.DebugMessages = "不能处理当前任务";
                result.IsSuccess = false;
                result.Messages = "不能处理当前任务";
                return;
            }

            var currentSteps = wfInstalled.Steps.Where(p => p.ID == currentTask.StepID);
            var currentStep = currentSteps.Count() > 0 ? currentSteps.First() : null;

            if (currentStep == null)
            {
                result.DebugMessages = "未能找到当前步骤";
                result.IsSuccess = false;
                result.Messages = "未能找到当前步骤";
                return;
            }
            if (currentTask.StepID == wfInstalled.FirstStepID)
            {
                result.DebugMessages = "当前任务是流程第一步,不能退回";
                result.IsSuccess = false;
                result.Messages = "当前任务是流程第一步,不能退回";
                return;
            }
            //if (executeModel.Steps.Count == 0)
            //{
            //    result.DebugMessages = "没有选择要退回的步骤";
            //    result.IsSuccess = false;
            //    result.Messages = "没有选择要退回的步骤";
            //    return;
            //}

            #region 按顺序处理时的退回
            if (currentStep.Behavior.HanlderModel == 4)
            {
                var tjtasklist = GetTaskList(currentTask.ID);
                if (currentTask.StepSort == 0)
                {
                    foreach (var t in tjtasklist)
                    {
                        if (t.ID != currentTask.ID && t.Status == -1)
                        {
                            Delete(t.ID);
                        }
                    }
                }
                var ptask = tjtasklist.Find(p => p.StepSort == currentTask.StepSort - 1);
                if (ptask != null)
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                        ptask.Status = 0;
                        ptask.Type = 4;
                        ptask.Comment = "";
                        ptask.CompletedTime1 = null;
                        ptask.IsSign = 0;
                        ptask.Note = "";
                        Update(ptask);
                        scope.Complete();
                        nextTasks.Add(ptask);
                        result.DebugMessages = "已退回到：" + ptask.StepName + "(" + ptask.ReceiveName + ")";
                        result.IsSuccess = true;
                        result.Messages = "已退回到：" + ptask.StepName + "(" + ptask.ReceiveName + ")";
                        result.NextTasks = nextTasks;
                        return;
                    }
                }
            }
            #endregion

            #region 加签退回
            if (currentTask.Type == 7 && currentTask.OtherType.HasValue)
            {
                int addType = currentTask.OtherType.Value.ToString().Left(1).ToInt();
                int writeType = currentTask.OtherType.Value.ToString().Right(1).ToInt();
                var tjTasks = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.PrevID == currentTask.PrevID && p.Type == 7);
                bool isBack = false;
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    switch (writeType)
                    {
                        case 1:
                        case 3:
                            foreach (var t in tjTasks)
                            {
                                if (t.ID == currentTask.ID)
                                {
                                    Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                                }
                                else if (t.Status.In(-1, 0, 1))
                                {
                                    t.Status = 5;
                                    Update(t);
                                }
                            }
                            isBack = true;
                            break;
                        case 2:
                            Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                            if (tjTasks.FindAll(p => p.Status.In(-1, 0, 1) && p.ID != currentTask.ID).Count == 0)
                            {
                                isBack = true;
                            }
                            break;
                    }

                    if (isBack)
                    {
                        var prevTask = Get(currentTask.PrevID);
                        if (prevTask != null)
                        {
                            if (addType == 2)
                            {
                                foreach (var t in GetNextTaskList(prevTask.ID))
                                {
                                    if (t.Status == -1)
                                    {
                                        Delete(t.ID);
                                    }
                                }
                            }
                            prevTask.Status = 0;
                            Update(prevTask);
                            nextTasks.Add(prevTask);
                            result.DebugMessages += "已退回到" + prevTask.ReceiveName;
                            result.IsSuccess = true;
                            result.Messages += result.DebugMessages;
                            result.NextTasks = nextTasks;
                        }
                        else
                        {
                            result.DebugMessages += "未找到前一任务";
                            result.IsSuccess = false;
                            result.Messages += result.DebugMessages;
                            result.NextTasks = nextTasks;
                        }
                    }
                    else
                    {
                        result.DebugMessages += "已退回,等待他人处理";
                        result.IsSuccess = true;
                        result.Messages += result.DebugMessages;
                        result.NextTasks = nextTasks;
                    }

                    scope.Complete();
                }
                return;
            }
            #endregion

            #region 独立退回
            if (4 == currentStep.Behavior.BackModel)
            {
                YJ.Data.Model.WorkFlowTask newTask = Get(currentTask.PrevID);
                if (newTask != null)
                {
                    newTask.ID = Guid.NewGuid();
                    newTask.PrevID = currentTask.ID;
                    newTask.Note = "退回任务";
                    newTask.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                    newTask.SenderID = currentTask.ReceiveID;
                    newTask.SenderName = currentTask.ReceiveName;
                    newTask.SenderTime = newTask.ReceiveTime;
                    newTask.Sort = currentTask.Sort + 1;
                    newTask.Status = 0;
                    newTask.Type = 4;
                    newTask.Comment = "";
                    newTask.OpenTime = null;
                    //newTask.PrevStepID = currentTask.StepID;
                    if (currentStep.WorkTime > 0)
                    {
                        newTask.CompletedTime = new WorkCalendar().GetWorkDate((double)currentStep.WorkTime, YJ.Utility.DateTimeNew.Now);//YJ.Utility.DateTimeNew.Now.AddHours((double)currentStep.WorkTime);
                    }
                    else
                    {
                        newTask.CompletedTime = null;
                    }
                    newTask.CompletedTime1 = null;
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        Add(newTask);
                        nextTasks.Add(newTask);
                        Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, "", executeModel.Files);
                        scope.Complete();
                    }
                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in nextTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    string msg = string.Format("已退回到:{0}", newTask.ReceiveName);
                    result.DebugMessages += msg;
                    result.IsSuccess = true;
                    result.Messages += msg;
                    result.NextTasks = nextTasks;
                    return;
                }
                else
                {
                    string msg = string.Format("未找到发送者");
                    result.DebugMessages += msg;
                    result.IsSuccess = true;
                    result.Messages += msg;
                    result.NextTasks = nextTasks;
                    return;
                }
            }
            #endregion

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                List<YJ.Data.Model.WorkFlowTask> backTasks = new List<YJ.Data.Model.WorkFlowTask>();
                int status = 0;
                int backModel = currentStep.Behavior.BackModel;
                int hanlderModel = currentStep.Behavior.HanlderModel;
                if (backModel == 2)//一人退回全部退回
                {
                    backModel = 1;
                    hanlderModel = 0;
                }
                else if (backModel == 3)//所有人退回才退回
                {
                    backModel = 1;
                    hanlderModel = 1;
                }
                switch (backModel)
                {
                    case 0://不能退回
                        result.DebugMessages = "当前步骤设置为不能退回";
                        result.IsSuccess = false;
                        result.Messages = "当前步骤设置为不能退回";
                        return;
                    #region 根据策略退回
                    case 1:
                        switch (hanlderModel)
                        {
                            case 0://所有人必须同意,如果一人不同意则全部退回
                                var taskList = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                                foreach (var task in taskList)
                                {
                                    if (task.ID != currentTask.ID)
                                    {
                                        if (task.Status.In(0, 1))
                                        {
                                            Completed(task.ID, "", false, 5);
                                            //backTasks.Add(task);
                                        }
                                    }
                                    else
                                    {
                                        Completed(task.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                                    }
                                }
                                break;
                            case 1://一人同意即可
                                var taskList1 = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                                if (taskList1.Count > 1)
                                {
                                    var noCompleted = taskList1.Where(p => p.Status != 3);
                                    if (noCompleted.Count() - 1 > 0)
                                    {
                                        status = -1;
                                    }
                                }
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                                break;
                            case 2://依据人数比例
                                var taskList2 = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort && p.Type != 5);
                                if (taskList2.Count > 1)
                                {
                                    decimal percentage = currentStep.Behavior.Percentage <= 0 ? 100 : currentStep.Behavior.Percentage;//比例
                                    if ((((decimal)(taskList2.Where(p => p.Status == 3).Count() + 1) / (decimal)taskList2.Count) * 100).Round() < 100 - percentage)
                                    {
                                        status = -1;
                                    }
                                    else
                                    {
                                        foreach (var task in taskList2)
                                        {
                                            if (task.ID != currentTask.ID && task.Status.In(0, 1))
                                            {
                                                Completed(task.ID, "", false, 5);
                                                //backTasks.Add(task);
                                            }
                                        }
                                    }
                                }
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                                break;
                            case 3://独立处理
                                Completed(currentTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                                break;
                        }
                        backTasks.Add(currentTask);
                        break;
                    #endregion
                }

                if (status == -1)
                {
                    result.DebugMessages += "已退回,等待他人处理";
                    result.IsSuccess = true;
                    result.Messages += "已退回,等待他人处理!";
                    result.NextTasks = nextTasks;
                    scope.Complete();
                    return;
                }

                foreach (var backTask in backTasks)
                {
                    if (backTask.Status.In(2, 3))//已完成的任务不能退回
                    {
                        continue;
                    }
                    if (backTask.ID == currentTask.ID)
                    {
                        Completed(backTask.ID, executeModel.Comment, executeModel.IsSign, 3, files: executeModel.Files);
                    }
                    else
                    {
                        Completed(backTask.ID, "", false, 5, "他人已退回");
                    }
                }

                List<YJ.Data.Model.WorkFlowTask> tasks = new List<YJ.Data.Model.WorkFlowTask>();
                /* 因为设置为退回到第一步时，会同时退回上一步和第一步两个步骤，所以临时注释掉（2016-4-1）
                if (currentStep.Behavior.HanlderModel.In(0, 1, 2))//退回时要退回其它步骤发来的同级任务。
                {
                    var tjTasks = GetTaskList(currentTask.FlowID, currentTask.StepID, currentTask.GroupID).FindAll(p => p.Sort == currentTask.Sort);
                    foreach (var tjTask in tjTasks)
                    {
                        if (!executeModel.Steps.ContainsKey(tjTask.PrevStepID))
                        {
                            executeModel.Steps.Add(tjTask.PrevStepID, new List<Data.Model.Users>());
                        }
                    }
                }*/
                foreach (var step in executeModel.Steps)
                {
                    var tasks1 = GetTaskList(executeModel.FlowID, step.Key, executeModel.GroupID).FindAll(p => p.Type != 7);
                    if (tasks1.Count > 0)
                    {
                        tasks1 = tasks1.OrderByDescending(p => p.Sort).ToList();
                        int maxSort = tasks1.First().Sort;
                        tasks.AddRange(tasks1.FindAll(p => p.Sort == maxSort));
                    }
                }

                #region 处理会签形式的退回
                //当前步骤是否是会签步骤
                var countersignatureStep = bWorkFlow.GetNextSteps(executeModel.FlowID, executeModel.StepID).Find(p => p.Behavior.Countersignature != 0);
                bool IsCountersignature = countersignatureStep != null;
                bool isBack = true;
                if (IsCountersignature)
                {
                    var steps = bWorkFlow.GetPrevSteps(executeModel.FlowID, countersignatureStep.ID);
                    switch (countersignatureStep.Behavior.Countersignature)
                    {
                        case 1://所有步骤处理，如果一个步骤退回则退回
                            isBack = false;
                            foreach (var step in steps)
                            {
                                if (IsBack(step, executeModel.FlowID, currentTask.GroupID, currentTask.PrevID, currentTask.Sort))
                                {
                                    isBack = true;
                                    break;
                                }
                            }
                            break;
                        case 2://一个步骤退回,如果有一个步骤同意，则不退回
                            isBack = true;
                            foreach (var step in steps)
                            {
                                if (!IsBack(step, executeModel.FlowID, currentTask.GroupID, currentTask.PrevID, currentTask.Sort))
                                {
                                    isBack = false;
                                    break;
                                }
                            }
                            break;
                        case 3://依据比例退回
                            int backCount = 0;
                            foreach (var step in steps)
                            {
                                if (IsBack(step, executeModel.FlowID, currentTask.GroupID, currentTask.PrevID, currentTask.Sort))
                                {
                                    backCount++;
                                }
                            }
                            isBack = (((decimal)backCount / (decimal)steps.Count) * 100).Round() >= (countersignatureStep.Behavior.CountersignaturePercentage <= 0 ? 100 : countersignatureStep.Behavior.CountersignaturePercentage);
                            break;
                    }

                    if (isBack)
                    {
                        var tjTasks = GetTaskList(currentTask.ID, false);
                        foreach (var tjTask in tjTasks)
                        {
                            if (tjTask.ID == currentTask.ID || tjTask.Status.In(2, 3, 4, 5, 6, 7))
                            {
                                continue;
                            }
                            Completed(tjTask.ID, "", false, 5);
                        }
                    }
                }
                #endregion

                //如果退回步骤是子流程步骤，则要作废子流程实例
                if (currentStep.Type == "subflow" && currentStep.SubFlowID.IsGuid() && !currentTask.SubFlowGroupID.IsNullOrEmpty())
                {
                    foreach (string groupID in currentTask.SubFlowGroupID.Split(','))
                    {
                        DeleteInstance(currentStep.SubFlowID.ToGuid(), groupID.ToGuid(), true);
                    }
                }

                if (isBack)
                {
                    var backTaskList = tasks.Distinct(this);
                    if (backTaskList.Count() == 0)
                    {
                        Completed(currentTask.ID, "", false, 0, "");
                        result.DebugMessages += "没有接收人,退回失败!";
                        result.IsSuccess = false;
                        result.Messages += "没有接收人,退回失败!";
                        result.NextTasks = nextTasks;
                        scope.Complete();
                        return;
                    }

                    foreach (var task in backTaskList)
                    {
                        if (task != null)
                        {
                            //删除抄送
                            if (task.Type == 5)
                            {
                                Delete(task.ID);
                                continue;
                            }

                            if (task.OtherType == 1)
                            {
                                var prevtask = Get(task.PrevID);
                                if (prevtask != null)
                                {
                                    prevtask.OpenTime = null;
                                    prevtask.Status = 0;
                                    Update(prevtask);
                                    Delete(task.ID);
                                    nextTasks.Add(prevtask);
                                }
                            }
                            else
                            {
                                YJ.Data.Model.WorkFlowTask newTask = task;
                                newTask.ID = Guid.NewGuid();
                                newTask.PrevID = currentTask.ID;
                                newTask.Note = "退回任务";
                                newTask.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                                newTask.SenderID = currentTask.ReceiveID;
                                newTask.SenderName = currentTask.ReceiveName;
                                newTask.SenderTime = YJ.Utility.DateTimeNew.Now;
                                newTask.Sort = currentTask.Sort + 1;
                                newTask.Status = 0;
                                newTask.Type = 4;
                                newTask.Comment = "";
                                newTask.OpenTime = null;
                                //newTask.PrevStepID = currentTask.StepID;
                                if (currentStep.WorkTime > 0)
                                {
                                    newTask.CompletedTime = new WorkCalendar().GetWorkDate((double)currentStep.WorkTime, YJ.Utility.DateTimeNew.Now);//YJ.Utility.DateTimeNew.Now.AddHours((double)currentStep.WorkTime);
                                }
                                else
                                {
                                    newTask.CompletedTime = null;
                                }
                                newTask.CompletedTime1 = null;
                                Add(newTask);
                                nextTasks.Add(newTask);
                            }
                        }
                    }

                    //删除临时任务
                    var nextSteps = bWorkFlow.GetNextSteps(executeModel.FlowID, executeModel.StepID);
                    foreach(var step in nextSteps)
                    {
                        dataWorkFlowTask.DeleteTempTasks(currentTask.FlowID, step.ID, currentTask.GroupID,
                            IsCountersignature ? Guid.Empty : currentStep.ID
                            );
                    }
                }

                scope.Complete();
            }

            

            if (nextTasks.Count > 0)
            {
                //2018-1-27增加处理如果退回的任务步骤是按顺序处理则只激活最后一个任务
                foreach(var step1 in nextTasks.GroupBy(p=>p.StepID))
                {
                    if (step1.Count() > 0 && step1.First().StepSort != step1.Last().StepSort)
                    {
                        var maxsort = step1.Max(p => p.StepSort);
                        foreach (var task in step1)
                        {
                            if (task.StepSort != maxsort)
                            {
                                task.Status = -1;
                                nextTasks.Remove(task);
                                Update(task);
                            }
                        }
                    }
                }


                List<string> nextStepName = new List<string>();
                foreach (var nstep in nextTasks)
                {
                    nextStepName.Add(nstep.StepName);
                }
                string msg = string.Format("已退回到:{0}", nextStepName.Distinct().ToArray().Join1(","));
                result.DebugMessages += msg;
                result.IsSuccess = true;
                result.Messages += msg;
                result.NextTasks = nextTasks;
            }
            else
            {
                result.DebugMessages += "已退回,等待其它步骤处理";
                result.IsSuccess = true;
                result.Messages += "已退回,等待其它步骤处理";
                result.NextTasks = nextTasks;
            }
            return;
        }

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeSave(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            //如果是第一步提交并且没有实例则先创建实例
            YJ.Data.Model.WorkFlowTask currentTask = null;
            bool isFirst = executeModel.StepID == wfInstalled.FirstStepID && executeModel.TaskID == Guid.Empty && executeModel.GroupID == Guid.Empty;
            if (isFirst)
            {
                currentTask = createFirstTask(executeModel);
            }
            else
            {
                currentTask = Get(executeModel.TaskID);
            }
            if (currentTask == null)
            {
                result.DebugMessages = "未能创建或找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未能创建或找到当前任务";
                return;
            }
            else if (currentTask.Status.In(2, 3, 4, 5, 6, 7))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            else if (currentTask.ReceiveID != Users.CurrentUserID && currentTask.ReceiveID != WeiXin.Organize.CurrentUserID && currentTask.IsExpiredAutoSubmit == 0)
            {
                result.DebugMessages = "不能处理当前任务";
                result.IsSuccess = false;
                result.Messages = "不能处理当前任务";
                return;
            }
            else
            {
                currentTask.InstanceID = executeModel.InstanceID;
                nextTasks.Add(currentTask);
                if (isFirst)
                {
                    //currentTask.Title = executeModel.Title.IsNullOrEmpty() ? "未命名任务" : executeModel.Title;
                    //Update(currentTask);
                }
                else
                {
                    if (!executeModel.Title.IsNullOrEmpty())
                    {
                        currentTask.Title = executeModel.Title;
                        Update(currentTask);
                    }
                }
            }

            result.DebugMessages = "保存成功";
            result.IsSuccess = true;
            result.Messages = "保存成功";
        }

        /// <summary>
        /// 完成抄送任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeCopyforComplete(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            if (executeModel.TaskID == Guid.Empty || executeModel.FlowID == Guid.Empty)
            {
                result.DebugMessages = "完成流程参数错误";
                result.IsSuccess = false;
                result.Messages = "完成流程参数错误";
                return;
            }
            var task = Get(executeModel.TaskID);
            if (task == null)
            {
                result.DebugMessages = "未找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未找到当前任务";
                return;
            }
            else if (task.Status.In(2, 3, 4, 5, 6, 7))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            Completed(task.ID, executeModel.Comment, executeModel.IsSign, note: task.Note, files: executeModel.Files);
            result.DebugMessages += "已完成";
            result.IsSuccess = true;
            result.Messages += "已完成";
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="executeModel"></param>
        /// <param name="isCompleteTask">是否需要调用Completed方法完成当前任务</param>
        private void executeComplete(YJ.Data.Model.WorkFlowExecute.Execute executeModel, bool isCompleteTask = true)
        {
            if (executeModel.TaskID == Guid.Empty || executeModel.FlowID == Guid.Empty)
            {
                result.DebugMessages = "完成流程参数错误";
                result.IsSuccess = false;
                result.Messages = "完成流程参数错误";
                return;
            }
            var task = Get(executeModel.TaskID);
            if (task == null)
            {
                result.DebugMessages = "未找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未找到当前任务";
                return;
            }
            else if (isCompleteTask && task.Status.In(2, 3, 4, 5, 6, 7))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            

            #region 更新业务表标识字段的值为1
            if (wfInstalled.TitleField != null && wfInstalled.TitleField.LinkID != Guid.Empty && !wfInstalled.TitleField.Table.IsNullOrEmpty()
                && !wfInstalled.TitleField.Field.IsNullOrEmpty() && wfInstalled.DataBases.Count() > 0)
            {
                var firstDB = wfInstalled.DataBases.First();
                try
                {
                    string sql = string.Format("UPDATE {0} SET {1}='{2}' WHERE {3}", wfInstalled.TitleField.Table, wfInstalled.TitleField.Field, 
                        "1", string.Format("{0}='{1}'", firstDB.PrimaryKey, task.InstanceID));
                    Data.Factory.Factory.GetDBHelper().Execute(sql);

                    //new DBConnection().GetDataTable(firstDB.LinkID, sql);
                }
                catch (Exception err)
                {
                    Log.Add("更新流程完成标题发生了错误-" + task.Title, err.Message + err.StackTrace, Log.Types.系统错误, executeModel.Serialize());
                }
            }
            #endregion

            if (isCompleteTask)
            {
                Completed(task.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
            }

            #region 执行子流程完成后事件
            var parentTasks = GetBySubFlowGroupID(task.GroupID);
            if (parentTasks.Count > 0)
            {
                var parentTask = parentTasks.First();
                var flowRunModel = bWorkFlow.GetWorkFlowRunModel(parentTask.FlowID);
                if (flowRunModel != null)
                {
                    var steps = flowRunModel.Steps.Where(p => p.ID == parentTask.StepID);
                    if (steps.Count() > 0 && !steps.First().Event.SubFlowCompletedBefore.IsNullOrEmpty())
                    {
                        ExecuteFlowCustomEvent(steps.First().Event.SubFlowCompletedBefore.Trim(), new YJ.Data.Model.WorkFlowCustomEventParams()
                        {
                            FlowID = parentTask.FlowID,
                            GroupID = parentTask.GroupID,
                            InstanceID = parentTask.InstanceID,
                            StepID = parentTask.StepID,
                            TaskID = parentTask.ID
                        });
                    }
                }
            }
            #endregion

            result.DebugMessages += "已完成";
            result.IsSuccess = true;
            result.Messages += "已完成";
        }

        /// <summary>
        /// 转交任务
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeRedirect(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            YJ.Data.Model.WorkFlowTask currentTask = Get(executeModel.TaskID);
            if (currentTask == null)
            {
                result.DebugMessages = "未能创建或找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未能创建或找到当前任务";
                return;
            }
            else if (currentTask.Status.In(2, 3, 4, 5, 6, 7))
            {
                result.DebugMessages = "当前任务已处理";
                result.IsSuccess = false;
                result.Messages = "当前任务已处理";
                return;
            }
            else if (currentTask.ReceiveID != Users.CurrentUserID && currentTask.ReceiveID != WeiXin.Organize.CurrentUserID && currentTask.IsExpiredAutoSubmit == 0)
            {
                result.DebugMessages = "不能处理当前任务";
                result.IsSuccess = false;
                result.Messages = "不能处理当前任务";
                return;
            }
            else if (currentTask.Status == -1)
            {
                result.DebugMessages = "当前任务正在等待他人处理";
                result.IsSuccess = false;
                result.Messages = "当前任务正在等待他人处理";
                return;
            }
            if (executeModel.Steps.First().Value.Count == 0)
            {
                result.DebugMessages = "未设置转交人员";
                result.IsSuccess = false;
                result.Messages = "未设置转交人员";
                return;
            }
            string receiveName = currentTask.ReceiveName;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (var user in executeModel.Steps.First().Value)
                {
                    currentTask.ID = Guid.NewGuid();
                    currentTask.ReceiveID = user.ID;
                    currentTask.ReceiveName = user.Name;
                    currentTask.OpenTime = null;
                    currentTask.Status = 0;
                    currentTask.IsSign = 0;
                    currentTask.Type = 3;
                    currentTask.Note = string.Format("该任务由{0}转交", receiveName);
                    if (!HasNoCompletedTasks(currentTask.FlowID, currentTask.StepID, currentTask.GroupID, user.ID))
                    {
                        Add(currentTask);
                    }
                    nextTasks.Add(currentTask);
                }
                Completed(executeModel.TaskID, executeModel.Comment, executeModel.IsSign, 2, "已转交他人处理");
                scope.Complete();
            }
            List<string> nextStepName = new List<string>();
            foreach (var user in executeModel.Steps.First().Value)
            {
                nextStepName.Add(user.Name);
            }
            string userName = nextStepName.Distinct().ToArray().Join1(",");
            result.DebugMessages = string.Concat("已转交给:", userName);
            result.IsSuccess = true;
            result.Messages = string.Concat("已转交给:", userName);
            return;
        }

        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="executeModel"></param>
        private void executeAddWrite(YJ.Data.Model.WorkFlowExecute.Execute executeModel)
        {
            if (executeModel.TaskID.IsEmptyGuid())
            {
                result.DebugMessages = "未找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未找到当前任务";
                return;
            }
            var task = Get(executeModel.TaskID);
            if (task == null)
            {
                result.DebugMessages = "未找到当前任务";
                result.IsSuccess = false;
                result.Messages = "未找到当前任务";
                return;
            }
            else if (task.ReceiveID != Users.CurrentUserID && task.ReceiveID != WeiXin.Organize.CurrentUserID && task.IsExpiredAutoSubmit == 0)
            {
                result.DebugMessages = "不能处理当前任务";
                result.IsSuccess = false;
                result.Messages = "不能处理当前任务";
                return;
            }
            if (task.OtherType.ToString().Length != 2)
            {
                result.DebugMessages = "未找到加签类型和审批类型!";
                result.IsSuccess = false;
                result.Messages = "加签参数错误";
                return;
            }
            int addType = task.OtherType.ToString().Left(1).ToInt();
            int writeType = task.OtherType.ToString().Right(1).ToInt();
            var stepTasks = GetTaskList(task.FlowID, task.StepID, task.GroupID);
            var tasks = stepTasks.FindAll(p => p.PrevID == task.PrevID && p.Type == 7);
            List<YJ.Data.Model.WorkFlowTask> nextTasks1 = new List<Data.Model.WorkFlowTask>();
            switch (writeType)
            { 
                case 1://所有人同意
                    Completed(task.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                    break;
                case 2://一人同意即可
                    foreach (var t in tasks.FindAll(p => p.Status.In(-1, 0, 1)))
                    {
                        if (t.ID == task.ID)
                        {
                            Completed(task.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                        }
                        else
                        {
                            Completed(t.ID, "", false, 4);
                        }
                    }
                    break;
                case 3://顺序审批
                    Completed(task.ID, executeModel.Comment, executeModel.IsSign, files: executeModel.Files);
                    var nextTasks = tasks.FindAll(p => p.Status.In(-1, 0, 1) && p.ID != task.ID).OrderBy(p => p.ReceiveTime);
                    if (nextTasks.Count() > 0)
                    {
                        var nextTask = nextTasks.FirstOrDefault();
                        nextTask.Status = 0;
                        Update(nextTask);
                        nextTasks1.Add(nextTask);
                    }
                    break;
            }
            List<YJ.Data.Model.WorkFlowTask> noAddWriteTasks = new List<Data.Model.WorkFlowTask>();
            bool isPassing = isPassingAddWrite(task, out noAddWriteTasks);
            
            if (isPassing && noAddWriteTasks.Count > 0)
            {
                switch (addType)
                { 
                    case 1://前加签
                        foreach (var t in noAddWriteTasks)
                        {
                            t.Status = 1;
                            Update(t);
                            nextTasks1.Add(t);
                        }
                        break;
                    case 2://后加签
                    case 3://并签
                        var nextTasks = GetNextTaskList(noAddWriteTasks.FirstOrDefault().ID).FindAll(p => p.Status == -1);
                        foreach (var nextTask in nextTasks)
                        {
                            nextTask.Status = 0;
                            Update(nextTask);
                            nextTasks1.Add(nextTask);
                        }
                        break;
                }
            }
            StringBuilder sendName = new StringBuilder();
            foreach (var nextTask in nextTasks1)
            {
                sendName.Append(nextTask.ReceiveName);
                sendName.Append(",");
            }

            result.DebugMessages = "已发送" + (sendName.Length > 0 ? "到" + sendName.ToString().TrimEnd(',') : "");
            result.IsSuccess = true;
            result.NextTasks = nextTasks1;
            result.Messages = result.DebugMessages;
        }

        /// <summary>
        /// 判断加签是否通过
        /// </summary>
        /// <param name="addWriteTasks"></param>
        /// <param name="isAll">判断当前加签还是步骤所有加签</param>
        /// <returns></returns>
        private bool isPassingAddWrite(YJ.Data.Model.WorkFlowTask addWriteTask, out List<Data.Model.WorkFlowTask> nextTasks)
        {
            nextTasks = new List<Data.Model.WorkFlowTask>();
            if (addWriteTask == null)
            {
                return true;
            }
            var tasks = GetTaskList(addWriteTask.FlowID, addWriteTask.StepID, addWriteTask.GroupID);
            if (tasks.Count == 0)
            {
                return true;
            }
            var task1 = tasks.Find(p => p.ID == addWriteTask.PrevID);
            if (task1 == null)
            {
                return true;
            }
            var tasks1 = tasks.FindAll(p => p.Sort == task1.Sort && p.Type != 7);
            nextTasks = tasks1;
            List<Data.Model.WorkFlowTask> addWriteTasks = new List<Data.Model.WorkFlowTask>();
            foreach (var t in tasks1)
            {
                addWriteTasks.AddRange(tasks.FindAll(p => p.PrevID == t.ID && p.Type == 7));
            }
            
            foreach (var w in addWriteTasks.GroupBy(p => p.PrevID))
            {
                int writeType = w.FirstOrDefault().OtherType.ToString().Right(1).ToInt();
                switch (writeType)
                {
                    case 1:
                    case 3:
                        if (w.Where(p => p.Status.In(0, 1, -1)).Count() > 0)
                        {
                            return false;
                        }
                        break;
                    case 2:
                        if (w.Where(p => p.Status.In(2)).Count() == 0)
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// 创建临时任务（待办人员看不到）
        /// </summary>
        /// <param name="executeModel"></param>
        private List<Data.Model.WorkFlowTask> createTempTasks(YJ.Data.Model.WorkFlowExecute.Execute executeModel, Data.Model.WorkFlowTask currentTask)
        {
            List<Data.Model.WorkFlowTask> tasks = new List<Data.Model.WorkFlowTask>();
            foreach (var step in executeModel.Steps)
            {
                int stecount = 0;
                foreach (var user in step.Value)
                {
                    var nextSteps = wfInstalled.Steps.Where(p => p.ID == step.Key);
                    if (nextSteps.Count() == 0)
                    {
                        continue;
                    }
                    var nextStep = nextSteps.First();
                    YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
                    if (executeModel.StepCompletedTimes.Keys.Contains(nextStep.ID))
                    {
                        task.CompletedTime = executeModel.StepCompletedTimes[nextStep.ID];
                    }
                    else if (nextStep.WorkTime > 0)
                    {
                        task.CompletedTime = new WorkCalendar().GetWorkDate((double)nextStep.WorkTime, YJ.Utility.DateTimeNew.Now);  //YJ.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                    }
                    task.FlowID = executeModel.FlowID;
                    task.GroupID = currentTask != null ? currentTask.GroupID : executeModel.GroupID;
                    task.ID = Guid.NewGuid();
                    task.Type = 0;
                    task.InstanceID = executeModel.InstanceID;
                    if (!executeModel.Note.IsNullOrEmpty())
                    {
                        task.Note = executeModel.Note;
                    }
                    task.PrevID = currentTask.ID;
                    task.PrevStepID = currentTask.StepID;
                    task.ReceiveID = user.ID;
                    task.ReceiveName = user.Name;
                    task.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                    task.SenderID = executeModel.Sender.ID;
                    task.SenderName = executeModel.Sender.Name;
                    task.SenderTime = task.ReceiveTime;
                    task.Status = -1;
                    task.StepID = step.Key;
                    task.StepName = nextStep.Name;
                    task.Sort = currentTask.Sort + 1;
                    task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;
                    task.OtherType = executeModel.OtherType;
                    task.StepSort = stecount++;
                   
                    if (!HasNoCompletedTasks(executeModel.FlowID, step.Key, currentTask.GroupID, user.ID))
                    {
                        Add(task);
                    }
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        /// <summary>
        /// 判断一个步骤是否通过
        /// </summary>
        /// <param name="step"></param>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <param name="currentTask"></param>
        /// <param name="currentPrevStep">会签步取的上一步的上一步ID</param>
        /// <returns></returns>
        private bool IsPassing(YJ.Data.Model.WorkFlowInstalledSub.Step step, Guid flowID, Guid groupID, Data.Model.WorkFlowTask currentTask, Guid currentPrevStep)
        {
            //var prevSteps = bWorkFlow.GetPrevSteps(flowID, step.ID);
            var tasks = GetTaskList(flowID, step.ID, groupID).FindAll(p => p.Type != 7);
            //if (tasks.Count == 0)
            //{
            //    return prevSteps.Count > 0 && prevSteps.Where(p => p.ID == currentPrevStep).Count() > 0;
            //}
            int maxSort = tasks.Count > 0 ? tasks.Max(p => p.Sort) : -1;
            tasks = tasks.FindAll(p => p.Sort == maxSort && p.Type != 5);
            if (tasks.Count == 0)
            {
                //如果步骤没有任务要查询前面的步骤
                var betweenSteps = new WorkFlow().getBetweenSteps(new WorkFlow().GetWorkFlowRunModel(flowID), currentTask.PrevStepID, step.ID);
                foreach (var betweenStep in betweenSteps)
                {
                    var betweenTasks = GetTaskList(flowID, betweenStep.ID, groupID).FindAll(p => p.Type != 7 && p.Type != 5);
                    int maxsort1 = betweenTasks.Count>0 ? betweenTasks.Max(p=>p.Sort) : -1;//tasks.FindAll(p => p.StepID == betweenStep.ID && p.Type != 5).Max(p => p.Sort);
                    if (betweenTasks.Find(p => p.Sort == maxsort1) != null)
                    {
                        return false;
                    }
                }

                return true;
            }
            bool isPassing = true;
            switch (step.Behavior.HanlderModel)
            { 
                case 0://所有人必须处理
                case 3://独立处理
                    isPassing = tasks.Where(p => p.Status != 2).Count() == 0;
                    break;
                case 1://一人同意即可
                    isPassing = tasks.Where(p => p.Status == 2).Count() > 0;
                    break;
                case 2://依据人数比例
                    isPassing = (((decimal)(tasks.Where(p => p.Status == 2).Count() + 1) / (decimal)tasks.Count) * 100).Round() >= (step.Behavior.Percentage <= 0 ? 100 : step.Behavior.Percentage);
                    break;
            }
            if (isPassing)
            {
                List<YJ.Data.Model.WorkFlowTask> addWriteTasks = new List<Data.Model.WorkFlowTask>();
                isPassing = isPassingAddWrite(tasks.FirstOrDefault(), out addWriteTasks);
            }
            return isPassing;
        }

        /// <summary>
        /// 判断一个步骤是否退回
        /// </summary>
        /// <param name="step"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private bool IsBack(YJ.Data.Model.WorkFlowInstalledSub.Step step, Guid flowID, Guid groupID, Guid taskID, int sort)
        {
            var tasks = GetTaskList(flowID, step.ID, groupID).FindAll(p => p.Sort == sort && p.Type != 5);
            if (tasks.Count == 0)
            {
                return false;
            }
            bool isBack = true;
            switch (step.Behavior.HanlderModel)
            {
                case 0://所有人必须处理
                case 3://独立处理
                    isBack = tasks.Where(p => p.Status.In(3,5)).Count() > 0;
                    break;
                case 1://一人同意即可
                    isBack = tasks.Where(p => p.Status.In(2,4)).Count() == 0;
                    break;
                case 2://依据人数比例
                    isBack = (((decimal)(tasks.Where(p => p.Status.In(3, 5)).Count() + 1) / (decimal)tasks.Count) * 100).Round() >= 100 - (step.Behavior.Percentage <= 0 ? 100 : step.Behavior.Percentage);
                    break;
            }
            return isBack;
        }

        /// <summary>
        /// 创建第一个任务
        /// </summary>
        /// <param name="executeModel"></param>
        /// <param name="isSubFlow">是否是创建子流程任务</param>
        /// <returns></returns>
        private YJ.Data.Model.WorkFlowTask createFirstTask(YJ.Data.Model.WorkFlowExecute.Execute executeModel, bool isSubFlow = false)
        {
            if (wfInstalled == null || isSubFlow)
            {
                wfInstalled = bWorkFlow.GetWorkFlowRunModel(executeModel.FlowID);
            }
            
            var nextSteps = wfInstalled.Steps.Where(p => p.ID == wfInstalled.FirstStepID);
            if (nextSteps.Count() == 0)
            {
                return null;
            }
            YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
            if (nextSteps.First().WorkTime > 0)
            {
                task.CompletedTime = new WorkCalendar().GetWorkDate((double)nextSteps.First().WorkTime, YJ.Utility.DateTimeNew.Now);//YJ.Utility.DateTimeNew.Now.AddHours((double)nextSteps.First().WorkTime);
            }
            task.FlowID = executeModel.FlowID;
            task.GroupID = executeModel.GroupID.IsEmptyGuid() ? Guid.NewGuid() : executeModel.GroupID;
            task.ID = Guid.NewGuid();
            task.Type = 0;
            task.InstanceID = executeModel.InstanceID;
            if (!executeModel.Note.IsNullOrEmpty())
            {
                task.Note = executeModel.Note;
            }
            task.PrevID = Guid.Empty;
            task.PrevStepID = Guid.Empty;
            task.ReceiveID = executeModel.Sender.ID;
            task.ReceiveName = executeModel.Sender.Name;
            task.ReceiveTime = YJ.Utility.DateTimeNew.Now;
            task.SenderID = executeModel.Sender.ID;
            task.SenderName = executeModel.Sender.Name;
            task.SenderTime = task.ReceiveTime;
            task.Status = 0;
            task.StepID = wfInstalled.FirstStepID;
            task.StepName = nextSteps.First().Name;
            task.Sort = 1;
            task.StepSort = 0;
            task.OtherType = executeModel.OtherType;
            task.Title = executeModel.Title.IsNullOrEmpty() ? wfInstalled.Name + "-" + task.StepName + '-' + task.SenderName : executeModel.Title;
            Add(task);
            if (isSubFlow)
            {
                wfInstalled = null;
            }
            return task;
        }

        /// <summary>
        /// 查询待办任务
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="type">0待办 1已完成</param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, string query = "", string title = "", string flowid = "", string sender = "", string date1 = "", string date2 = "", int type = 0)
        {
            return dataWorkFlowTask.GetTasks(userID, out pager, query, title, flowid, YJ.Platform.Users.RemovePrefix(sender), date1, date2, type);
        }

        /// <summary>
        /// 查询待办任务
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="type">0待办 1已完成</param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out long count, int size, int number, string title = "", string flowid = "", string sender = "", string date1 = "", string date2 = "", int type = 0, string order = "")
        {
            return dataWorkFlowTask.GetTasks(userID, out count, size, number, title, flowid, YJ.Platform.Users.RemovePrefix(sender), date1, date2, type, order);
        }

        /// <summary>
        /// 得到流程实例列表
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="senderID"></param>
        /// <param name="receiveID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="isCompleted">是否完成 0:全部 1:未完成 2:已完成</param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, string query = "", string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0)
        {
            return dataWorkFlowTask.GetInstances(flowID, senderID, receiveID, out pager, query, title, flowid, date1, date2, status);
        }

        /// <summary>
        /// 得到流程实例列表
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="senderID"></param>
        /// <param name="receiveID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="status">是否完成 0:全部 1:未完成 2:已完成</param>
        /// <returns></returns>
        public System.Data.DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, string query = "", string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0)
        {
            return dataWorkFlowTask.GetInstances1(flowID, senderID, receiveID, out pager, query, title, flowid, date1, date2, status);
        }

        /// <summary>
        /// 得到流程实例列表
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="senderID"></param>
        /// <param name="receiveID"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="title"></param>
        /// <param name="flowid"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="status">是否完成 0:全部 1:未完成 2:已完成</param>
        /// <returns></returns>
        public System.Data.DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out long count, int size, int number, string title = "", string flowid = "", string date1 = "", string date2 = "", int status = 0, string order = "")
        {
            return dataWorkFlowTask.GetInstances1(flowID, senderID, receiveID, out count, size, number, title, flowid, date1, date2, status, order);
        }

        /// <summary>
        /// 执行自定义方法
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public object ExecuteFlowCustomEvent(string eventName, object eventParams, string dllName = "")
        {
            if (dllName.IsNullOrEmpty())
            {
                dllName = eventName.Substring(0, eventName.IndexOf('.'));
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(dllName);
            string typeName = System.IO.Path.GetFileNameWithoutExtension(eventName);
            string methodName = eventName.Substring(typeName.Length + 1);
            Type type = assembly.GetType(typeName, true);

            object obj = System.Activator.CreateInstance(type, false);
            var method = type.GetMethod(methodName);

            if (method != null)
            {
                return method.Invoke(obj, new object[] { eventParams });
            }
            else
            {
                throw new MissingMethodException(typeName, methodName);
            }
        }
        
        /// <summary>
        /// 删除流程实例
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int DeleteInstance(Guid flowID, Guid groupID, bool hasInstanceData = false)
        {
            if (hasInstanceData)
            {
                var tasks = GetTaskList(flowID, groupID);
                if (tasks.Count > 0 && !tasks.First().InstanceID.IsNullOrEmpty())
                {
                    var wfRunModel = bWorkFlow.GetWorkFlowRunModel(flowID);
                    if (wfRunModel != null && wfRunModel.DataBases.Count() > 0)
                    {
                        var dataBase = wfRunModel.DataBases.First();
                        new DBConnection().DeleteData(dataBase.LinkID, dataBase.Table, dataBase.PrimaryKey, tasks.First().InstanceID);
                    }
                }
            }
            return dataWorkFlowTask.Delete(flowID, groupID);
        }

        /// <summary>
        /// 完成一个任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="comment">处理意见</param>
        /// <param name="isSign">是否签章</param>
        /// <param name="status">状态</param>
        /// <param name="note">备注</param>
        /// <param name="files">附件</param>
        /// <returns></returns>
        public int Completed(Guid taskID, string comment = "", bool isSign = false, int status = 2, string note = "", string files = "")
        {
            return dataWorkFlowTask.Completed(taskID, comment, isSign, status, note, files);
        }

        /// <summary>
        /// 得到一个流程实例一个步骤的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            return dataWorkFlowTask.GetTaskList(flowID, stepID, groupID);
        }

        /// <summary>
        /// 得到一个实例的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            return dataWorkFlowTask.GetTaskList(flowID, groupID);
        }

        /// <summary>
        /// 得到和一个任务同级的任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="isStepID">是否区分步骤ID，多步骤会签区分的是上一步骤ID</param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid taskID, bool isStepID = true)
        {
            return dataWorkFlowTask.GetTaskList(taskID, isStepID);
        }

        /// <summary>
        /// 得到和一个任务同级的任务(同一步骤内)
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="stepID">步骤ID</param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetTaskListByStepID(Guid taskID, Guid stepID)
        {
            return dataWorkFlowTask.GetTaskList(taskID).FindAll(p => p.StepID == stepID);
        }

        /// <summary>
        /// 得到一个任务的前一任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        {
            return dataWorkFlowTask.GetPrevTaskList(taskID);
        }

        /// <summary>
        /// 得到一个任务的后续任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            return dataWorkFlowTask.GetNextTaskList(taskID);
        }

        /// <summary>
        /// 得到一个任务可以退回的步骤
        /// </summary>
        /// <param name="taskID">当前任务ID</param>
        /// <param name="backType">退回类型</param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public Dictionary<Guid, string> GetBackSteps(Guid taskID, int backType, Guid stepID, YJ.Data.Model.WorkFlowInstalled wfInstalled)
        {
            Dictionary<Guid, string> dict = new Dictionary<Guid, string>();
            var steps = wfInstalled.Steps.Where(p => p.ID == stepID);
            if (steps.Count() == 0)
            {
                return dict;
            }
            var step = steps.First();
            var task = Get(taskID);

            //按顺序处理时退回给发送人
            if (step.Behavior.HanlderModel == 4 && task.StepSort>0)
            {
                var ptask = GetTaskList(taskID).Find(p => p.StepSort == task.StepSort - 1);
                if (ptask != null)
                {
                    dict.Add(Guid.Empty, step.Name + "(" + ptask.ReceiveName + ")");
                    return dict;
                }
                
            }

            //加签退回给发送人
            if (task != null && task.Type == 7)
            {
                dict.Add(Guid.Empty, task.SenderName);
                return dict;
            }
            //独立退回退回给发送人
            if (task != null && 4 == step.Behavior.BackModel)
            {
                var backStep = wfInstalled.Steps.Where(p => p.ID == task.PrevStepID);
                dict.Add(Guid.Empty, backStep.Count() > 0 ? backStep.First().Name + "(" + task.SenderName + ")" : task.SenderName);
                return dict;
            }
            switch (backType)
            { 
                case 0://退回前一步
                    if (task != null)
                    {
                        if (step.Behavior.Countersignature != 0)//如果是会签步骤，则要退回到前面所有步骤
                        {
                            var backSteps = bWorkFlow.GetPrevSteps(task.FlowID, step.ID);
                            foreach (var backStep in backSteps)
                            {
                                dict.Add(backStep.ID, backStep.Name);
                            }
                        }
                        else
                        {
                            dict.Add(task.PrevStepID, bWorkFlow.GetStepName(task.PrevStepID, wfInstalled));
                        }
                    }
                    break;
                case 1://退回第一步
                    dict.Add(wfInstalled.FirstStepID, bWorkFlow.GetStepName(wfInstalled.FirstStepID, wfInstalled));
                    break;
                case 2://退回某一步
                    if (step.Behavior.BackType == 2 && step.Behavior.BackStepID != Guid.Empty)
                    {
                        dict.Add(step.Behavior.BackStepID, bWorkFlow.GetStepName(step.Behavior.BackStepID, wfInstalled));
                    }
                    else
                    {
                        if (task != null)
                        {
                            var taskList = GetTaskList(task.FlowID, task.GroupID).Where(p => p.Status.In(2, 3, 4)).OrderBy(p => p.Sort);
                            foreach (var task1 in taskList.OrderByDescending(p=>p.CompletedTime1))
                            {
                                if (!dict.Keys.Contains(task1.StepID) && task1.StepID != stepID)
                                {
                                    dict.Add(task1.StepID, bWorkFlow.GetStepName(task1.StepID, wfInstalled));
                                }
                            }
                        }
                    }
                    break;
            }
            return dict;
        }

        /// <summary>
        /// 更新一个任务后续任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="comment"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            int i = 0;
            var taskList = GetTaskList(taskID);

            foreach (var task in taskList)
            {
                i += dataWorkFlowTask.UpdateNextTaskStatus(task.ID, status);
            }
            
            return i;
        }

        /// <summary>
        /// 查询一个流程是否有任务数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasTasks(Guid flowID)
        {
            return dataWorkFlowTask.HasTasks(flowID);
        }

        /// <summary>
        /// 查询一个用户在一个步骤是否有未完成任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            return dataWorkFlowTask.HasNoCompletedTasks(flowID, stepID, groupID, userID);
        }

        /// <summary>
        /// 得到状态显示标题
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetStatusTitle(int status)
        {
            string title = string.Empty;
            switch (status)
            {
                case -1:
                    title = "等待中";
                    break;
                case 0:
                    title = "待处理";
                    break;
                case 1:
                    title = "处理中";
                    break;
                case 2:
                    title = "已完成";
                    break;
                case 3:
                    title = "已退回";
                    break;
                case 4:
                    title = "他人已处理";
                    break;
                case 5:
                    title = "他人已退回";
                    break;
                case 6:
                    title = "终止";
                    break;
                case 7:
                    title = "他人已终止";
                    break;
                default:
                    title = "其它";
                    break;
            }

            return title;
        }

        /// <summary>
        /// 得到一个流程实例一个步骤一个人员的任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            return dataWorkFlowTask.GetUserTaskList(flowID, stepID, groupID, userID);
        }

        /// <summary>
        /// 判断一个任务是否可以收回
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool HasWithdraw(Guid taskID)
        {
            var currentTask = Get(taskID);
            var tjTaskList = GetTaskList(taskID);
            var nextTask = tjTaskList.Find(p => p.StepSort == currentTask.StepSort + 1);
            if (nextTask != null)
            {
                return nextTask.Status == 0;
            }

            var taskList = GetNextTaskList(taskID);
            if (taskList.Count == 0) return false;
            foreach (var task in taskList)
            {
                if (task.Status != 0 && task.Status != -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断一个任务是否可以收回
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="isHasten">是否可以崔办</param>
        /// <returns></returns>
        public bool HasWithdraw(Guid taskID, out bool isHasten)
        {
            isHasten = false;
            var currentTask = Get(taskID);
            var tjTaskList = GetTaskList(taskID);
            var nextTask = tjTaskList.Find(p => p.StepSort == currentTask.StepSort + 1);
            if (nextTask != null)
            {
                if (nextTask.Status.In(0, 1))
                {
                    isHasten = true;
                }
                return nextTask.Status == 0;
            }

            var taskList = GetNextTaskList(taskID);
            if (taskList.Count == 0) return false;
            //判断是否可以催办
            foreach (var task in taskList)
            {
                if(task.Status.In(0,1))
                {
                    isHasten = true;
                    break;
                }
            }
            //判断是否可以收回
            foreach (var task in taskList)
            {
                if (task.Status != 0 && task.Status != -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 收回任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool WithdrawTask(Guid taskID)
        {
            var currentTask = Get(taskID);
            if (currentTask == null)
            {
                return false;
            }
            List<Data.Model.WorkFlowTask> taskList1 = GetTaskList(taskID);
            var nextTask = taskList1.Find(p => p.StepSort == currentTask.StepSort + 1);
            if (nextTask != null)
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    Completed(nextTask.ID, "", false, -1, "");
                    Completed(taskID, "", false, 1, "");
                    scope.Complete();
                    return true;
                }
            }

            ShortMessage SM = new ShortMessage();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (var task in taskList1)
                {
                    var taskList2 = GetNextTaskList(task.ID);
                    foreach (var task2 in taskList2)
                    {
                        if (task2.Status.In(-1,0,1,5))
                        {
                            Delete(task2.ID);
                            //取消消息提醒
                            SM.Delete(task2.ID.ToString(), 1);
                        }
                        if (!task2.SubFlowGroupID.IsNullOrEmpty())
                        {
                            foreach (string groupID in task2.SubFlowGroupID.Split(','))
                            {
                                DeleteInstance(Guid.Empty, groupID.ToGuid());
                            }
                        }
                    }

                    if (task.ID == taskID || task.Status == 4)
                    {
                        Completed(task.ID, "", false, 1, "");
                    }
                }
                scope.Complete();
                return true;
            }
        }

        /// <summary>
        /// 指派任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="user">要指派的人员</param>
        /// <returns></returns>
        public string DesignateTask(Guid taskID, YJ.Data.Model.Users user)
        {
            var task = Get(taskID);
            if (task == null)
            {
                return "未找到任务";
            }
            else if (task.Status.In(2, 3, 4, 5, 6, 7))
            {
                return "该任务已处理";
            }
            string receiveName = task.ReceiveName;
            task.ReceiveID = user.ID;
            task.ReceiveName = user.Name;
            task.OpenTime = null;
            task.Status = 0;
            task.Note = string.Format("该任务由{0}指派", receiveName);
            Update(task);

            return "指派成功";
        }

        /// <summary>
        /// 管理员强制退回任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string BackTask(Guid taskID)
        {
            var task = Get(taskID);
            if (task == null) 
            {
                return "未找到任务";
            }
            else if (task.Status.In(2, 3, 4, 5, 6, 7))
            {
                return "该任务已处理";
            }
            if(wfInstalled==null) 
            {
                wfInstalled = bWorkFlow.GetWorkFlowRunModel(task.FlowID);
            }
            YJ.Data.Model.WorkFlowExecute.Execute executeModel = new YJ.Data.Model.WorkFlowExecute.Execute();
            executeModel.ExecuteType = YJ.Data.Model.WorkFlowExecute.EnumType.ExecuteType.Back;
            executeModel.FlowID = task.FlowID;
            executeModel.GroupID = task.GroupID;
            executeModel.InstanceID = task.InstanceID;
            executeModel.Note = "管理员退回";
            executeModel.Sender = new Users().Get(task.ReceiveID);
            executeModel.StepID = task.StepID;
            executeModel.TaskID = task.ID;
            executeModel.Title = task.Title;
            var steps = wfInstalled.Steps.Where(p => p.ID == task.StepID);
            if(steps.Count()==0) 
            {
                return "未找到步骤";
            }
            else if (steps.First().Behavior.BackType == 2 && steps.First().Behavior.BackStepID == Guid.Empty)
            {
                return "未设置退回步骤";
            }
            Dictionary<Guid, List<YJ.Data.Model.Users>> execSteps = new Dictionary<Guid, List<YJ.Data.Model.Users>>();
            var backsteps = GetBackSteps(taskID, steps.First().Behavior.BackType, task.StepID, wfInstalled);
            foreach (var back in backsteps)
            {
                execSteps.Add(back.Key, new List<YJ.Data.Model.Users>());
            }
            executeModel.Steps = execSteps;
            var result = Execute(executeModel);
            return result.Messages;
        }

        /// <summary>
        /// 排序流程任务列表
        /// </summary>
        /// <param name="tasks"></param>
        public List<YJ.Data.Model.WorkFlowTask> Sort(List<YJ.Data.Model.WorkFlowTask> tasks)
        {
            return tasks.OrderBy(p => p.Sort).ToList();
        }
       
        /// <summary>
        /// 得到一个任务的状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int GetTaskStatus(Guid taskID)
        {
            return dataWorkFlowTask.GetTaskStatus(taskID);
        }

        /// <summary>
        /// 判断一个任务是否可以处理
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool IsExecute(Guid taskID)
        {
            return GetTaskStatus(taskID) <= 1;
        }

        /// <summary>
        /// 判断sql流转条件是否满足
        /// </summary>
        /// <param name="linkID"></param>
        /// <param name="table"></param>
        /// <param name="tablepk"></param>
        /// <param name="instabceID">实例ID</param>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool TestLineSql(Guid linkID, string table, string tablepk, string instabceID, string where)
        {
            if (instabceID.IsNullOrEmpty())
            {
                return false;
            }
            DBConnection dbconn = new DBConnection();
            var conn = dbconn.Get(linkID);
            if (conn == null)
            {
                return false;
            }
            string sql = "SELECT * FROM " + table + " WHERE " + tablepk + "='" + instabceID + "' AND (" + where.FilterWildcard() + ")".ReplaceSelectSql();
            if (!dbconn.TestSql(conn, sql))
            {
                return false;
            }
            System.Data.DataTable dt = dbconn.GetDataTable(conn, sql);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 判断实例是否已完成
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public bool GetInstanceIsCompleted(Guid flowID, Guid groupID)
        {
            var tasks = GetTaskList(Guid.Empty, groupID);
            return tasks.Find(p => p.Type != 5 && p.Status.In(0, 1)) == null;
        }

        /// <summary>
        /// 根据SubFlowID得到一个任务
        /// </summary>
        /// <param name="subflowGroupID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            return dataWorkFlowTask.GetBySubFlowGroupID(subflowGroupID);
        }

        /// <summary>
        /// 得到状态选择字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetStatusListItems(string value)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<int, string> dicts = new Dictionary<int, string>();
            dicts.Add(0, "待处理");
            dicts.Add(1, "处理中");
            dicts.Add(2, "已完成");
            dicts.Add(3, "已退回");
            dicts.Add(4, "他人已处理");
            dicts.Add(5, "他人已退回");
            foreach (var state in dicts)
            {
                sb.AppendFormat("<option value='{0}'{1}>{2}</option>", state.Key, 
                    state.Key.ToString() == value ? "selected='selected'" : "", state.Value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到催办人员复选框字符串
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string GetHastenUsersCheckboxString(Guid taskID, string name, string value="")
        {
            var tasks = GetNextTaskList(taskID);
            List<System.Web.UI.WebControls.ListItem> userItem = new List<System.Web.UI.WebControls.ListItem>();
            foreach (var task in tasks)
            {
                if (task.Status.In(0, 1))
                {
                    userItem.Add(new System.Web.UI.WebControls.ListItem(task.ReceiveName, task.ReceiveID.ToString()) { Selected = true });
                }
            }
            return YJ.Utility.Tools.GetCheckBoxString(userItem.ToArray(), name, (value ?? "").Split(','), "validate='checkbox'");
        }

        /// <summary>
        /// 终止一个任务所在的流程
        /// </summary>
        /// <param name="taskID"></param>
        public string EndTask(Guid taskID)
        {
            var task = Get(taskID);
            if (task == null)
            {
                return "未找到当前任务";
            }
            var tasks = GetTaskList(task.FlowID, task.GroupID);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    foreach (var t in tasks)
                    {
                        if (t.Status < 2)
                        {
                            t.Status = t.ID == task.ID ? 6 : 7;
                            Update(t);
                        }
                    }
                    #region 更新业务表标识字段的值为2
                    if (wfInstalled == null)
                    {
                        wfInstalled = new WorkFlow().GetWorkFlowRunModel(task.FlowID);
                    }
                    if (wfInstalled.TitleField != null && wfInstalled.TitleField.LinkID != Guid.Empty && !wfInstalled.TitleField.Table.IsNullOrEmpty()
                        && !wfInstalled.TitleField.Field.IsNullOrEmpty() && wfInstalled.DataBases.Count() > 0)
                    {
                        var firstDB = wfInstalled.DataBases.First();
                        //new DBConnection().UpdateFieldValue(
                        //    wfInstalled.TitleField.LinkID,
                        //    wfInstalled.TitleField.Table,
                        //    wfInstalled.TitleField.Field,
                        //    "2",
                        //   string.Format("{0}='{1}'", firstDB.PrimaryKey, task.InstanceID));

                        string sql = string.Format("UPDATE {0} SET {1}='{2}' WHERE {3}", wfInstalled.TitleField.Table, wfInstalled.TitleField.Field,
                       "2", string.Format("{0}='{1}'", firstDB.PrimaryKey, task.InstanceID));
                        Data.Factory.Factory.GetDBHelper().Execute(sql);
                    }
                    #endregion
                    scope.Complete();
                }
                catch (Exception err)
                {
                    Platform.Log.Add(err);
                }
            }
            return "1";
        }

        /// <summary>
        /// 跳转任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="stepUser"></param>
        /// <returns></returns>
        public bool GoToTask(Data.Model.WorkFlowTask task, Dictionary<Guid,string> gotoSteps)
        {
            var wfinstance1 = bWorkFlow.GetWorkFlowRunModel(task.FlowID);
            if(wfinstance1==null)
            {
                return false;
            }
            
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    foreach (var dict in gotoSteps)
                    {
                        var steps = wfinstance1.Steps.Where(p => p.ID == dict.Key);
                        if (steps.Count() == 0) continue;
                        var step = steps.First();
                        var users = new Organize().GetAllUsers(dict.Value);
                        foreach (var user in users)
                        {
                            Data.Model.WorkFlowTask newTask = new Data.Model.WorkFlowTask();
                            newTask.Comment = "";
                            newTask.FlowID = task.FlowID;
                            newTask.GroupID = task.GroupID;
                            newTask.ID = Guid.NewGuid();
                            newTask.InstanceID = task.InstanceID;
                            newTask.OtherType = task.OtherType;
                            newTask.PrevID = task.ID;
                            newTask.PrevStepID = task.StepID;
                            newTask.ReceiveID = user.ID;
                            newTask.ReceiveName = user.Name;
                            newTask.ReceiveTime = Utility.DateTimeNew.Now;
                            newTask.SenderID = task.ReceiveID;
                            newTask.SenderName = task.ReceiveName;
                            newTask.SenderTime = newTask.ReceiveTime;
                            newTask.Sort = task.Sort + 1;
                            newTask.Status = 0;
                            newTask.StepID = dict.Key;
                            newTask.StepName = step.Name;
                            newTask.SubFlowGroupID = task.SubFlowGroupID;
                            newTask.Title = task.Title;
                            newTask.Type = task.Type;
                            Add(newTask);
                        }
                    }
                    task.Status = 2;
                    Update(task);
                    scope.Complete();
                    Log.Add("跳转了流程任务", gotoSteps.Serialize(), Log.Types.流程相关, task.Serialize());
                    return true;
                }
                catch (Exception err)
                {
                    Log.Add(err);
                    return false;
                }
            }
        }

        /// <summary>
        /// 得到一个步骤的默认处理人员
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <param name="groupID"></param>
        /// <param name="currentStepID"></param>
        /// <param name="instanceid"></param>
        /// <param name="selectType">选择类型</param>
        /// <param name="selectRange">选择范围</param>
        /// <returns></returns>
        public string GetDefultMember(Guid flowID, Guid stepID, Guid groupID, Guid currentStepID, string instanceid, out string selectType, out string selectRange)
        {
            var defaultMember = string.Empty;//默认处理人员
            selectType = "";
            selectRange = "";
            var wfInstalled1 = bWorkFlow.GetWorkFlowRunModel(flowID);
            if (wfInstalled1 == null)
            {
                return defaultMember;
            }
            var steps = wfInstalled1.Steps.Where(p => p.ID == stepID);
            if (steps.Count() == 0)
            {
                return defaultMember;
            }
            Data.Model.WorkFlowInstalledSub.Step step = steps.First();
            Users busers = new Users();
            //如果是调试模式并且当前登录人员包含在调试人员中 则默认为发起者
            if ((wfInstalled1.Debug == 1 || wfInstalled1.Debug == 2) && wfInstalled1.DebugUsers.Exists(p => p.ID == YJ.Platform.Users.CurrentUserID))
            {
                defaultMember = YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString();
            }
            else
            {
                #region 判断处理者类型
                switch (step.Behavior.HandlerType)
                {
                    case 0:
                        selectType = "unit='1' dept='1' station='1' workgroup='1' user='1'";
                        break;
                    case 1:
                        selectType = "unit='0' dept='1' station='0' workgroup='0' user='0'";
                        break;
                    case 2:
                        selectType = "unit='0' dept='0' station='1' workgroup='0' user='0'";
                        break;
                    case 3:
                        selectType = "unit='0' dept='0' station='0' workgroup='1' user='0'";
                        break;
                    case 4:
                        selectType = "unit='0' dept='0' station='0' workgroup='0' user='1'";
                        break;
                    case 5://发起者
                        Guid userid = GetFirstSnderID(wfInstalled1.ID, groupID);
                        if (userid != Guid.Empty)
                        {
                            defaultMember = YJ.Platform.Users.PREFIX + userid.ToString();
                        }
                        if (defaultMember.IsNullOrEmpty() && currentStepID == wfInstalled1.FirstStepID)
                        {
                            defaultMember = YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString();
                        }
                        break;
                    case 6://前一步骤处理者
                        defaultMember = GetStepSnderIDString(flowID, currentStepID, groupID);
                        if (defaultMember.IsNullOrEmpty() && currentStepID == wfInstalled.FirstStepID)
                        {
                            defaultMember = YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString();
                        }
                        break;
                    case 7://某一步骤处理者
                        defaultMember = GetStepSnderIDString(wfInstalled1.ID, step.Behavior.HandlerStepID, groupID);
                        if (defaultMember.IsNullOrEmpty() && step.Behavior.HandlerStepID == wfInstalled1.FirstStepID)
                        {
                            defaultMember = YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString();
                        }
                        break;
                    case 8://字段值
                        string linkString = step.Behavior.ValueField;
                        if (!linkString.IsNullOrEmpty() && !instanceid.IsNullOrEmpty() && wfInstalled1.DataBases.Count() > 0)
                        {
                            defaultMember = new YJ.Platform.DBConnection().GetFieldValue(linkString, wfInstalled1.DataBases.First().PrimaryKey, instanceid);
                        }
                        break;
                    case 9://发起者主管
                        Guid firstSenderID = GetFirstSnderID(wfInstalled1.ID, groupID);
                        if (firstSenderID.IsEmptyGuid() && currentStepID == wfInstalled1.FirstStepID)//如果是第一步则发起者为当前人员
                        {
                            firstSenderID = YJ.Platform.Users.CurrentUserID;
                        }
                        if (!firstSenderID.IsEmptyGuid())
                        {
                            defaultMember = busers.GetLeader(firstSenderID);
                        }
                        break;
                    case 10://发起者分管领导
                        Guid firstSenderID1 = GetFirstSnderID(wfInstalled1.ID, groupID);
                        if (firstSenderID1.IsEmptyGuid() && currentStepID == wfInstalled1.FirstStepID)//如果是第一步则发起者为当前人员
                        {
                            firstSenderID1 = YJ.Platform.Users.CurrentUserID;
                        }
                        if (!firstSenderID1.IsEmptyGuid())
                        {
                            defaultMember = busers.GetChargeLeader(firstSenderID1);
                        }
                        break;
                    case 11://前一步处理者领导
                        defaultMember = busers.GetLeader(YJ.Platform.Users.CurrentUserID);
                        break;
                    case 12://前一步处理者分管领导
                        defaultMember = busers.GetChargeLeader(YJ.Platform.Users.CurrentUserID);
                        break;
                    case 13://发起者上级部门领导
                        Guid firstSenderID2 = GetFirstSnderID(wfInstalled1.ID, groupID);
                        if (firstSenderID2.IsEmptyGuid() && currentStepID == wfInstalled1.FirstStepID)//如果是第一步则发起者为当前人员
                        {
                            firstSenderID2 = YJ.Platform.Users.CurrentUserID;
                        }
                        if (!firstSenderID2.IsEmptyGuid())
                        {
                            defaultMember = busers.GetParentDeptLeader(firstSenderID2);
                        }
                        break;
                    case 14://前一步处理者上级部门领导
                        defaultMember = busers.GetParentDeptLeader(YJ.Platform.Users.CurrentUserID);
                        break;
                    case 15://前一步处理者部门所有成员
                        string userString = GetStepSnderIDString(wfInstalled1.ID, currentStepID, groupID);
                        if (userString.IsNullOrEmpty() && step.Behavior.HandlerStepID == wfInstalled1.FirstStepID)
                        {
                            userString = YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString();
                        }
                        StringBuilder sb = new StringBuilder();
                        foreach (string user in (userString ?? "").Split(','))
                        {
                            var dept = new Users().GetDeptByUserID(Users.RemovePrefix(user).ToGuid());
                            if (dept != null)
                            {
                                var users1 = new Organize().GetAllUsers(dept.ID);
                                foreach (var u in users1)
                                {
                                    sb.Append("u_");
                                    sb.Append(u.ID);
                                    sb.Append(",");
                                }
                            }
                        }
                        defaultMember = sb.ToString().TrimEnd(',');
                        break;
                    case 16://发起者部门所有成员
                        Guid firstSenderID3 = GetFirstSnderID(wfInstalled1.ID, groupID);
                        if (firstSenderID3.IsEmptyGuid() && currentStepID == wfInstalled1.FirstStepID)//如果是第一步则发起者为当前人员
                        {
                            firstSenderID3 = YJ.Platform.Users.CurrentUserID;
                        }
                        
                        StringBuilder sb1 = new StringBuilder();
                        var dept1 = new Users().GetDeptByUserID(firstSenderID3);
                        if (dept1 != null)
                        {
                            var users1 = new Organize().GetAllUsers(dept1.ID);
                            foreach (var u in users1)
                            {
                                sb1.Append("u_");
                                sb1.Append(u.ID);
                                sb1.Append(",");
                            }
                        }
                        defaultMember = sb1.ToString().TrimEnd(',');
                        break;
                    case 17://发起者所有上级部门领导
                        Guid firstSenderID4 = GetFirstSnderID(wfInstalled1.ID, groupID);
                        if (firstSenderID4.IsEmptyGuid() && currentStepID == wfInstalled1.FirstStepID)//如果是第一步则发起者为当前人员
                        {
                            firstSenderID4 = YJ.Platform.Users.CurrentUserID;
                        }
                        defaultMember = new Users().GetAllParentsDeptLeader(firstSenderID4).ToArray().Join1();
                        break;
                    case 18://前一步处理者所有上级部门领导
                        defaultMember = new Users().GetAllParentsDeptLeader(YJ.Platform.Users.CurrentUserID).ToArray().Join1();
                        break;
                }
                #endregion
            }
            if (!defaultMember.IsNullOrEmpty())
            {
                defaultMember = defaultMember.Split(',').Distinct().ToArray().Join1();
            }
            if (step.Behavior.HandlerType.In(9, 10, 11, 12, 13, 14, 15, 16, 17, 18))
            {
                selectRange = "rootid=\"" + defaultMember + "\"";
            }

            if (defaultMember.IsNullOrEmpty())
            {
                defaultMember = step.Behavior.DefaultHandler;
                #region 如果设置了SQL或方法，则要从SQL或方法中取
                if (!step.Behavior.DefaultHandlerSqlOrMethod.IsNullOrEmpty())
                {
                    string sqlOrMethod = step.Behavior.DefaultHandlerSqlOrMethod.Trim1();
                    StringBuilder defaultHandlerSqlOrMethodMembers = new StringBuilder();
                    //以select开头表示是SQL语句，否则为方法
                    if (sqlOrMethod.StartsWith("select", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (wfInstalled1.DataBases.Count() > 0)
                        {
                            string sql = Wildcard.FilterWildcard(sqlOrMethod, Users.CurrentUserID.ToString()).ReplaceSelectSql();
                            System.Data.DataTable memberDt = new DBConnection().GetDataTable(wfInstalled1.DataBases.First().LinkID, sql);
                            if (memberDt != null && memberDt.Columns.Count > 0)
                            {
                                foreach (System.Data.DataRow dr in memberDt.Rows)
                                {
                                    defaultHandlerSqlOrMethodMembers.Append(dr[0].ToString());
                                    defaultHandlerSqlOrMethodMembers.Append(",");
                                }
                            }
                        }
                    }
                    else
                    {
                        YJ.Data.Model.WorkFlowCustomEventParams eventParams = new YJ.Data.Model.WorkFlowCustomEventParams();
                        eventParams.FlowID = flowID;
                        eventParams.GroupID = groupID;
                        eventParams.StepID = stepID;
                        eventParams.TaskID = Guid.Empty;
                        eventParams.InstanceID = instanceid;
                        object obj = ExecuteFlowCustomEvent(sqlOrMethod, eventParams);
                        if (obj != null)
                        {
                            defaultHandlerSqlOrMethodMembers.Append(obj.ToString());
                        }
                    }
                    defaultMember += "," + defaultHandlerSqlOrMethodMembers.ToString();
                }
                #endregion
            }
            return defaultMember.IsNullOrEmpty() ? "" : defaultMember.TrimStart(',').TrimEnd(',');
        }

        /// <summary>
        /// 根据flowid,groupid得到一个组最新的一条任务
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public YJ.Data.Model.WorkFlowTask GetLastTask(Guid flowID, Guid groupID)
        {
            return dataWorkFlowTask.GetLastTask(flowID, groupID);
        }

        /// <summary>
        /// 自动提交一个任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <returns></returns>
        public Data.Model.WorkFlowExecute.Result AutoSubmit(Guid taskID)
        {
            return AutoSubmit(Get(taskID));
        }

        /// <summary>
        /// 自动提交一个任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="isExpired">是否是超时自动提交</param>
        /// <returns></returns>
        public Data.Model.WorkFlowExecute.Result AutoSubmit(Data.Model.WorkFlowTask task, bool isExpired = false)
        {
            Data.Model.WorkFlowExecute.Result autoSubmitResult = new Data.Model.WorkFlowExecute.Result();
            if (task == null)
            {
                autoSubmitResult.DebugMessages = "未找到任务";
                autoSubmitResult.IsSuccess = false;
                autoSubmitResult.Messages = "未找到任务";
                return autoSubmitResult;
            }
            if (!task.Status.In(-1, 0, 1))
            {
                autoSubmitResult.DebugMessages = "任务已完成";
                autoSubmitResult.IsSuccess = false;
                autoSubmitResult.Messages = "任务已完成";
                return autoSubmitResult;
            }
            var wf = new WorkFlow().GetWorkFlowRunModel(task.FlowID);
            if (null == wf)
            {
                autoSubmitResult.DebugMessages = "未找到流程运行时";
                autoSubmitResult.IsSuccess = false;
                autoSubmitResult.Messages = "未找到流程运行时";
                return autoSubmitResult;
            }
            var currentSteps = wf.Steps.Where(p => p.ID == task.StepID);
            if (currentSteps.Count() == 0)
            {
                autoSubmitResult.DebugMessages = "未找到当前步骤";
                autoSubmitResult.IsSuccess = false;
                autoSubmitResult.Messages = "未找到当前步骤";
                return autoSubmitResult;
            }
            var currentStep = currentSteps.First();

            Data.Model.WorkFlowExecute.Execute subExecute = new Data.Model.WorkFlowExecute.Execute();
            List<Data.Model.WorkFlowInstalledSub.Step> nextSteps = bWorkFlow.GetNextSteps(task.FlowID, task.StepID);

            #region 增加自动提交后续步骤条件判断
            WorkFlowTask btask = new WorkFlowTask();
            Users busers = new Users();
            Organize borg = new Organize();
            //判断流转条件
            if (currentStep.Behavior.FlowType == 0 && nextSteps.Count() > 0 && task.Type != 7)
            {
                List<Guid> removeIDList = new List<Guid>();
                YJ.Data.Model.WorkFlowCustomEventParams eventParams = new YJ.Data.Model.WorkFlowCustomEventParams();
                eventParams.FlowID = task.FlowID;
                eventParams.GroupID = task.GroupID;
                eventParams.StepID = currentStep.ID;
                eventParams.TaskID = task.ID;
                eventParams.InstanceID = task.InstanceID;

                System.Text.StringBuilder nosubmitMsg = new System.Text.StringBuilder();
                foreach (var step in nextSteps)
                {
                    var lines = wfInstalled.Lines.Where(p => p.ToID == step.ID && p.FromID == currentStep.ID);
                    if (lines.Count() > 0)
                    {
                        var line = lines.First();
                        if (!line.SqlWhere.IsNullOrEmpty())
                        {
                            if (wfInstalled.DataBases.Count() == 0)
                            {
                                removeIDList.Add(step.ID);
                                //nosubmitMsg.Append("流程未设置数据连接");
                                //nosubmitMsg.Append("\\n");
                            }
                            else
                            {
                                if (!btask.TestLineSql(wfInstalled.DataBases.First().LinkID, wfInstalled.DataBases.First().Table,
                                        wfInstalled.DataBases.First().PrimaryKey, task.InstanceID, line.SqlWhere))
                                {
                                    removeIDList.Add(step.ID);
                                    //nosubmitMsg.Append(string.Concat("提交条件未满足"));
                                    //nosubmitMsg.Append("\\n");
                                }
                            }
                        }
                        if (!line.CustomMethod.IsNullOrEmpty())
                        {
                            object obj = btask.ExecuteFlowCustomEvent(line.CustomMethod.Trim(), eventParams);
                            var objType = obj.GetType();
                            var boolType = typeof(Boolean);
                            if (objType != boolType && "1" != obj.ToString())
                            {
                                removeIDList.Add(step.ID);
                                nosubmitMsg.Append(obj.ToString());
                                nosubmitMsg.Append("\\n");
                            }
                            else if (objType == boolType && !(bool)obj)
                            {
                                removeIDList.Add(step.ID);
                                nosubmitMsg.Append(obj.ToString());
                                nosubmitMsg.Append("\\n");
                            }
                        }
                        #region 组织机构关系判断
                        Guid SenderID = YJ.Platform.Users.CurrentUserID;
                        Guid sponserID = Guid.Empty;//发起者ID
                        
                        if (currentStep.ID == wfInstalled.FirstStepID)//如果是第一步则发起者就是发送者
                        {
                            sponserID = SenderID;
                        }
                        else
                        {
                            sponserID = btask.GetFirstSnderID(eventParams.FlowID, eventParams.GroupID);
                        }
                        System.Text.StringBuilder orgWheres = new System.Text.StringBuilder();
                        if (!line.Organize.IsNullOrEmpty())
                        {
                            LitJson.JsonData orgJson = LitJson.JsonMapper.ToObject(line.Organize);
                            foreach (LitJson.JsonData json in orgJson)
                            {
                                if (orgJson.Count == 0)
                                {
                                    continue;
                                }
                                string usertype = json["usertype"].ToString();
                                string in1 = json.ContainsKey("in1") ? json["in1"].ToString() : "";
                                string users = json["users"].ToString();
                                string selectorganize = json["selectorganize"].ToString();
                                string tjand = json["tjand"].ToString();
                                string khleft = json["khleft"].ToString();
                                string khright = json["khright"].ToString();
                                Guid userid = "0" == usertype ? SenderID : sponserID;
                                string memberid = "";
                                bool isin = false;
                                if ("0" == users)
                                {
                                    memberid = selectorganize;
                                }
                                else if ("1" == users)
                                {
                                    memberid = busers.GetLeader(userid);
                                }
                                else if ("2" == users)
                                {
                                    memberid = busers.GetChargeLeader(userid);
                                }
                                if ("0" == in1)
                                {
                                    isin = busers.IsContains(userid, memberid);
                                }
                                else if ("1" == in1)
                                {
                                    isin = !busers.IsContains(userid, memberid);
                                }
                                if (!khleft.IsNullOrEmpty())
                                {
                                    orgWheres.Append(khleft);
                                }
                                orgWheres.Append(isin ? " true " : " false ");
                                if (!khright.IsNullOrEmpty())
                                {
                                    orgWheres.Append(khright);
                                }
                                orgWheres.Append(tjand);
                            }
                            string orgCode = string.Concat("bool testbool=", orgWheres.ToString(), ";return testbool;");
                            object rogCodeResult = YJ.Utility.Tools.ExecuteCsharpCode(orgCode);
                            if (rogCodeResult != null && !(bool)rogCodeResult)
                            {
                                removeIDList.Add(step.ID);
                            }
                        }
                        #endregion
                    }
                }
                foreach (Guid rid in removeIDList)
                {
                    nextSteps.RemoveAll(p => p.ID == rid);
                }
                if (nextSteps.Count == 0)
                {
                    autoSubmitResult.DebugMessages = "后续步骤条件均不符合,任务不能提交";
                    autoSubmitResult.IsSuccess = false;
                    autoSubmitResult.Messages = "后续步骤条件均不符合,任务不能提交";
                    return autoSubmitResult;
                }
            }
            #endregion

            if (nextSteps.Count == 0)
            {
                subExecute.ExecuteType = Data.Model.WorkFlowExecute.EnumType.ExecuteType.Completed;
            }
            else
            {
                subExecute.ExecuteType = Data.Model.WorkFlowExecute.EnumType.ExecuteType.Submit;
            }
            Dictionary<Guid, List<Data.Model.Users>> sendSteps = new Dictionary<Guid, List<Data.Model.Users>>();
           
            foreach (var nextStep in nextSteps)
            {
                string selectType, selectRange;
                //如果后面步骤没有设置默认处理者，无法提交
                var defaultMemberString = GetDefultMember(task.FlowID, nextStep.ID, task.GroupID, task.StepID, task.InstanceID, out selectType, out selectRange);
                if (defaultMemberString.IsNullOrEmpty())
                {
                    continue;
                }
                var users = borg.GetAllUsers(defaultMemberString);
                if (users.Count == 0)
                {
                    continue;
                }
                //如果后面步骤需要指定完成时间，无法提交
                if (nextStep.SendSetWorkTime == 1)
                {
                    continue;
                }

                sendSteps.Add(nextStep.ID, users);
            }
            if (sendSteps.Count > 0 || nextSteps.Count == 0)
            {
                subExecute.FlowID = task.FlowID;
                subExecute.GroupID = task.GroupID;
                subExecute.InstanceID = task.InstanceID;
                subExecute.Sender = new Users().Get(task.ReceiveID);
                subExecute.StepID = task.StepID;
                subExecute.Steps = sendSteps;
                subExecute.TaskID = task.ID;
                subExecute.Title = task.Title;
                subExecute.IsSign = false;
                subExecute.OtherType = 0;
                if (isExpired)
                {
                    subExecute.Note = "";
                }
                return Execute(subExecute);
            }
            else
            {
                autoSubmitResult.DebugMessages = "后续步骤未找到接收人";
                autoSubmitResult.IsSuccess = false;
                autoSubmitResult.Messages = "后续步骤未找到接收人";
                return autoSubmitResult;
            }
        }

        /// <summary>
        /// 得到加签的发送步骤
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<Data.Model.WorkFlowInstalledSub.Step> GetAddWriteSteps(Guid taskID)
        {
            List<Data.Model.WorkFlowInstalledSub.Step> steps = new List<Data.Model.WorkFlowInstalledSub.Step>();
            var task = Get(taskID);
            if (task == null || !task.OtherType.HasValue)
            {
                return steps;
            }
            int addType = task.OtherType.Value.ToString().Left(1).ToInt();
            int writeType = task.OtherType.Value.ToString().Right(1).ToInt();
            var wfinstance = new WorkFlow().GetWorkFlowRunModel(task.FlowID);
            if (addType == 2)
            {
                var nextTasks = GetNextTaskList(task.PrevID);
                if (nextTasks.Count > 0)
                {
                    var step = wfinstance.Steps.Where(p => p.ID == nextTasks.FirstOrDefault().StepID);
                    if (step.Count() > 0)
                    {
                        steps.Add(step.FirstOrDefault());
                    }
                }
            }
            else
            {
                var step = wfinstance.Steps.Where(p => p.ID == task.StepID);
                if (step.Count() > 0)
                {
                    steps.Add(step.FirstOrDefault());
                }
            }
            return steps;
        }

        /// <summary>
        /// 得到加签的接收人员
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string GetAddWriteMembers(Guid taskID)
        {
            var task = Get(taskID);
            if (task == null || !task.OtherType.HasValue)
            {
                return "";
            }
            string defaultMember = string.Empty;
            int addType = task.OtherType.Value.ToString().Left(1).ToInt();
            int writeType = task.OtherType.Value.ToString().Right(1).ToInt();
            if (writeType == 3)
            {
                var addTasks = GetTaskList(task.FlowID, task.StepID, task.GroupID).FindAll(p => p.PrevID == task.PrevID && p.Type == 7 && p.ID != task.ID && p.Status.In(-1, 0, 1)).OrderBy(p => p.ReceiveTime);
                if (addTasks.Count() > 0)
                {
                    defaultMember = Users.PREFIX + addTasks.FirstOrDefault().ReceiveID.ToString();
                }
            }
            if (defaultMember.IsNullOrEmpty())
            {
                switch (addType)
                {
                    case 1:
                        defaultMember = Users.PREFIX + task.SenderID.ToString();
                        break;
                    case 2:
                        var tasks = GetNextTaskList(task.PrevID).FindAll(p => p.StepID != task.StepID);
                        StringBuilder sb = new StringBuilder();
                        foreach (var t in tasks)
                        {
                            sb.Append(Users.PREFIX + t.ReceiveID.ToString());
                            sb.Append(",");
                        }
                        defaultMember = sb.ToString().TrimEnd(',');
                        break;
                    case 3:
                        defaultMember = Users.PREFIX + task.SenderID.ToString();
                        break;
                }
            }
            return defaultMember;
        }

        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="addType">加签类型</param>
        /// <param name="writeType">审批类型</param>
        /// <param name="users">加签人员</param>
        /// <param name="note">加签备注</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public bool AddWrite(Guid taskID, int addType, int writeType, string writeUsers, string note, out string msg)
        {
            YJ.Platform.WorkFlowTask btask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.Organize borg = new YJ.Platform.Organize();
            msg = "";
            var task = btask.Get(taskID);
            if (task == null)
            {
                msg = "未找到当前任务,不能加签!";
                return false;
            }
            #region 为选择的加签人员添加待办
            var users = borg.GetAllUsers(writeUsers);
            int i = 0;
            foreach (var user in users)
            {
                YJ.Data.Model.WorkFlowTask addTask = new YJ.Data.Model.WorkFlowTask();
                addTask.FlowID = task.FlowID;
                addTask.GroupID = task.GroupID;
                addTask.ID = Guid.NewGuid();
                addTask.InstanceID = task.InstanceID;
                addTask.Note = note;
                addTask.PrevID = task.ID;
                addTask.PrevStepID = task.PrevStepID;
                addTask.ReceiveID = user.ID;
                addTask.ReceiveName = user.Name;
                addTask.SenderTime = Utility.DateTimeNew.Now;
                addTask.ReceiveTime = addTask.SenderTime.AddSeconds(i++);
                addTask.SenderID = task.ReceiveID;
                addTask.SenderName = task.ReceiveName;
                addTask.Sort = task.Sort + 1;
                addTask.StepID = task.StepID;
                addTask.StepName = task.StepName;
                addTask.Title = task.Title;
                addTask.OtherType = (addType.ToString() + writeType.ToString()).ToInt();
                addTask.Type = 7;
                if ((addType == 1 && writeType == 3 && user.ID != users.FirstOrDefault().ID) || addType == 2)
                {
                    addTask.Status = -1;
                }
                else
                {
                    addTask.Status = 0;
                }
                if (!HasNoCompletedTasks(task.FlowID, task.StepID, task.GroupID, user.ID))
                {
                    btask.Add(addTask);
                }

            }
            #endregion

            #region 将当前任务的同级任务设置为等待中
            if (addType == 1)
            {
                var tjTasks = btask.GetTaskList(taskID);
                foreach (var tjTask in tjTasks)
                {
                    tjTask.Status = -1;
                    btask.Update(tjTask);
                }
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 定时检查超时自动提交的任务
        /// </summary>
        public void ExpiredAutoSubmit()
        {
            var tasks = dataWorkFlowTask.GetExpiredAutoSubmitTasks();
            WorkFlowTask btask = new WorkFlowTask();
            foreach (var task in tasks)
            {
                try
                {
                    var result = btask.AutoSubmit(task, true);
                    //Log.Add("超时自动提交了任务-" + task.Title, task.Serialize(), Log.Types.流程相关, result.IsSuccess + "--" + result.DebugMessages);
                }
                catch (Exception err)
                {
                    Log.Add(err);
                }
            }
        }

        /// <summary>
        /// 根据抄送设置策略得到抄送人员
        /// </summary>
        /// <param name="copyFor"></param>
        /// <param name="currentTask">当前任务</param>
        /// <returns></returns>
        public List<Data.Model.Users> GetCopyForUsers(Data.Model.WorkFlowInstalledSub.StepSet.CopyFor copyFor, Data.Model.WorkFlowTask currentTask)
        {
            List<Data.Model.Users> copyForUsers = new List<Data.Model.Users>();
            if (null == copyFor)
            {
                return copyForUsers;
            }
            Organize borg = new Organize();
            Users busers = new Users();
            if (!copyFor.MemberID.IsNullOrEmpty())
            {
                copyForUsers.AddRange(borg.GetAllUsers(copyFor.MemberID));
            }
            #region sql或方法
            if (!copyFor.MethodOrSql.IsNullOrEmpty())
            {
                var wfInstalled1 = bWorkFlow.GetWorkFlowRunModel(currentTask.FlowID);
                string sqlOrMethod = copyFor.MethodOrSql.Trim1();
                StringBuilder defaultHandlerSqlOrMethodMembers = new StringBuilder();
                //以select开头表示是SQL语句，否则为方法
                if (sqlOrMethod.StartsWith("select", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (wfInstalled1.DataBases.Count() > 0)
                    {
                        string sql = Wildcard.FilterWildcard(sqlOrMethod, Users.CurrentUserID.ToString()).ReplaceSelectSql();
                        System.Data.DataTable memberDt = new DBConnection().GetDataTable(wfInstalled1.DataBases.First().LinkID, sql);
                        if (memberDt != null && memberDt.Columns.Count > 0)
                        {
                            foreach (System.Data.DataRow dr in memberDt.Rows)
                            {
                                defaultHandlerSqlOrMethodMembers.Append(dr[0].ToString());
                                defaultHandlerSqlOrMethodMembers.Append(",");
                            }
                        }
                    }
                }
                else
                {
                    YJ.Data.Model.WorkFlowCustomEventParams eventParams = new YJ.Data.Model.WorkFlowCustomEventParams();
                    eventParams.FlowID = currentTask.FlowID;
                    eventParams.GroupID = currentTask.GroupID;
                    eventParams.StepID = currentTask.StepID;
                    eventParams.TaskID = currentTask.ID;
                    eventParams.InstanceID = currentTask.InstanceID;
                    object obj = ExecuteFlowCustomEvent(sqlOrMethod, eventParams);
                    if (obj != null)
                    {
                        defaultHandlerSqlOrMethodMembers.Append(obj.ToString());
                    }
                }
                copyForUsers.AddRange(borg.GetAllUsers(defaultHandlerSqlOrMethodMembers.ToString()));
            }
            #endregion

            #region 处理者类型
            if (!copyFor.handlerType.IsNullOrEmpty())
            {
                string[] handlerTypes = copyFor.handlerType.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string handler in handlerTypes)
                {
                    switch (handler.ToInt())
                    {
                        case 0://发起者
                            copyForUsers.Add(busers.Get(GetFirstSnderID(currentTask.FlowID, currentTask.GroupID)));
                            break;
                        case 1://前一步骤处理者
                            copyForUsers.AddRange(borg.GetAllUsers(GetStepSnderIDString(currentTask.FlowID, currentTask.PrevStepID, currentTask.GroupID)));
                            break;
                        case 2://发起者部门领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetLeader(GetFirstSnderID(currentTask.FlowID, currentTask.GroupID))));
                            break;
                        case 3://发起者分管领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetChargeLeader(GetFirstSnderID(currentTask.FlowID, currentTask.GroupID))));
                            break;
                        case 4://发起者上级部门领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetParentDeptLeader(GetFirstSnderID(currentTask.FlowID, currentTask.GroupID))));
                            break;
                        case 5://发起者部门所有成员
                            var dept1 = busers.GetDeptByUserID(GetFirstSnderID(currentTask.FlowID, currentTask.GroupID));
                            copyForUsers.AddRange(borg.GetAllUsers(dept1.ID));
                            break;
                        case 6://发起者所有上级部门领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetAllParentsDeptLeader(GetFirstSnderID(currentTask.FlowID, currentTask.GroupID)).ToArray().Join1()));
                            break;
                        case 7://前一步处理者部门领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetLeader(Users.CurrentUserID)));
                            break;
                        case 8://前一步处理者分管领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetChargeLeader(Users.CurrentUserID)));
                            break;
                        case 9://前一步处理者上级部门领导
                            copyForUsers.AddRange(borg.GetAllUsers(busers.GetParentDeptLeader(Users.CurrentUserID)));
                            break;
                        case 10://前一步处理者部门所有成员
                            copyForUsers.AddRange(borg.GetAllUsers(Users.CurrentDeptID));
                            break;

                    }
                }
            }
            #endregion

            #region 步骤处理人员
            if (!copyFor.steps.IsNullOrEmpty())
            {
                string[] stepsArray = copyFor.steps.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string step in stepsArray)
                {
                    copyForUsers.AddRange(borg.GetAllUsers(GetStepSnderIDString(currentTask.FlowID, step.ToGuid(), currentTask.GroupID)));
                }
            }
            #endregion
            copyForUsers.Distinct(new UsersEqualityComparer());
            return copyForUsers;
        }
    }
}