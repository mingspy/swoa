using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YJ.Platform
{
    public class WorkFlow
    {
        private YJ.Data.Interface.IWorkFlow dataWorkFlow;
        public WorkFlow()
        {
            this.dataWorkFlow = Data.Factory.Factory.GetWorkFlow();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.WorkFlow model)
        {
            return dataWorkFlow.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.WorkFlow model)
        {
            return dataWorkFlow.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkFlow> GetAll()
        {
            return dataWorkFlow.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.WorkFlow Get(Guid id)
        {
            return dataWorkFlow.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataWorkFlow.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataWorkFlow.GetCount();
        }
        /// <summary>
        /// 查询所有类型
        /// </summary>
        public List<string> GetAllTypes()
        {
            return dataWorkFlow.GetAllTypes();
        }
        
        /// <summary>
        /// 得到所有类型的下拉选择项
        /// </summary>
        /// <returns></returns>
        public string GetAllTypesOptions(string value="")
        {
            var types = GetAllTypes();
            StringBuilder options = new StringBuilder();
            foreach (var type in types)
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{0}</option>", type, type == value ? "selected=\"selected\"" : "");
            }
            return options.ToString();
        }
        /// <summary>
        /// 得到流程状态显示
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetStatusTitle(int status)
        {
            string title = string.Empty;
            switch (status)
            { 
                case 1:
                    title = "设计中";
                    break;
                case 2:
                    title = "已安装";
                    break;
                case 3:
                    title = "已卸载";
                    break;
                case 4:
                    title = "已删除";
                    break;
                case 5:
                    title = "等待他人处理";
                    break;
            }
            return title;
        }

        /// <summary>
        /// 保存一个流程
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>返回1 表示成功 其它为具体错误信息</returns>
        public string SaveFlow(string jsonString)
        {
            var jsonData = LitJson.JsonMapper.ToObject(jsonString);
            string id = jsonData["id"].ToString();
            string name = jsonData["name"].ToString();
            string type = jsonData["type"].ToString();
            Guid flowID;
            if (!id.IsGuid(out flowID))
            {
                return "请先新建或打开流程!";
            }
            else if (name.IsNullOrEmpty())
            {
                return "流程名称不能为空!";
            }
            else
            {
                YJ.Platform.WorkFlow bwf = new YJ.Platform.WorkFlow();
                YJ.Data.Model.WorkFlow wf = bwf.Get(flowID);
                bool isAdd = false;
                if (wf == null)
                {
                    wf = new YJ.Data.Model.WorkFlow();
                    isAdd = true;
                    wf.ID = flowID;
                    wf.CreateDate = YJ.Utility.DateTimeNew.Now;
                    wf.CreateUserID = YJ.Platform.Users.CurrentUserID;
                    wf.Status = 1;
                }
                wf.DesignJSON = jsonString;
                wf.InstanceManager = jsonData["instanceManager"].ToString();
                wf.Manager = jsonData["manager"].ToString();
                wf.Name = name.Trim();
                wf.Type = type.IsGuid() ? type.ToGuid() : new Dictionary().GetIDByCode("FlowTypes");
                try
                {
                    if (isAdd)
                    {
                        bwf.Add(wf);
                    }
                    else
                    {
                        bwf.Update(wf);
                    }
                    ClaearCache();
                    return "1";
                }
                catch (Exception err)
                {
                    ClaearCache();
                    return err.Message;
                }
            }
        }

        /// <summary>
        /// 安装一个流程
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="isMvc">是否是mvc程序，用于区分应用程序库连接</param>
        /// <returns>返回1 表示成功 其它为具体错误信息</returns>
        public string InstallFlow(string jsonString, bool isMvc = true)
        {
            string saveInfo = SaveFlow(jsonString);
            if ("1" != saveInfo)
            {
                return saveInfo;
            }
            string errMsg;

            YJ.Data.Model.WorkFlowInstalled wfInstalled = GetWorkFlowRunModel(jsonString, out errMsg);
            if (wfInstalled == null)
            {
                return errMsg;
            }
            else
            {
                YJ.Data.Model.WorkFlow wf = dataWorkFlow.Get(wfInstalled.ID);
                if (wf == null)
                {
                    return "流程实体为空";
                }
                else
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        wf.InstallDate = wfInstalled.InstallTime;
                        wf.InstallUserID = wfInstalled.InstallUser.ToGuid();
                        wf.RunJSON = wfInstalled.RunJSON;
                        wf.Status = 2;
                        dataWorkFlow.Update(wf);

                        wfInstalled.Status = 2;

                        #region 添加到应用程序库
                        YJ.Platform.AppLibrary bappLibrary = new AppLibrary();
                        YJ.Data.Model.AppLibrary app = bappLibrary.GetByCode(wfInstalled.ID.ToString());
                        bool isAdd = false;
                        if (app == null)
                        {
                            isAdd = true;
                            app = new YJ.Data.Model.AppLibrary();
                            app.ID = Guid.NewGuid();
                        }
                        app.Address = isMvc ? "/WorkFlowRun/Index" : "/Platform/WorkFlowRun/Default.aspx";
                        app.Code = wfInstalled.ID.ToString();
                        app.Note = "流程应用";
                        app.OpenMode = 0;
                        app.Params = "flowid=" + wfInstalled.ID.ToString();
                        app.Title = wfInstalled.Name;
                        app.Type = wfInstalled.Type.IsGuid() ? wfInstalled.Type.ToGuid() : new Dictionary().GetIDByCode("FlowTypes");
                        if (isAdd)
                        {
                            bappLibrary.Add(app);
                        }
                        else
                        {
                            bappLibrary.Update(app);
                        }
                        bappLibrary.ClearCache();
                        
                        #endregion
                        YJ.Cache.IO.Opation.Set(getCacheKey(wfInstalled.ID), wfInstalled);
                        ClearStartFlowsCache();
                        ClaearCache();
                        scope.Complete();
                        return "1";
                    }
                }
            }
            
        }

        /// <summary>
        /// 流程另存为
        /// </summary>
        /// <param name="flowID">流程ID</param>
        /// <param name="newName">新流程名称</param>
        /// <returns>返回另存后的流程实体</returns>
        public YJ.Data.Model.WorkFlow SaveAs(Guid flowID, string newName)
        {
            YJ.Data.Model.WorkFlow wf = dataWorkFlow.Get(flowID);
            if (wf == null || newName.IsNullOrEmpty())
            {
                return wf;
            }
            else
            {
                wf.ID = Guid.NewGuid();
                wf.Name = newName.Trim();
                wf.CreateDate = YJ.Utility.DateTimeNew.Now;
                wf.CreateUserID = Platform.Users.CurrentUserID;
                wf.InstallDate = null;
                wf.InstallUserID = null;
                wf.RunJSON = null;
                wf.Status = 1;

                if (!wf.DesignJSON.IsNullOrEmpty())
                {
                    LitJson.JsonData json = LitJson.JsonMapper.ToObject(wf.DesignJSON);
                    json["id"] = wf.ID.ToString();
                    json["name"] = wf.Name;

                    LitJson.JsonData steps = json["steps"];
                    LitJson.JsonData lines = json["lines"];
                    foreach (LitJson.JsonData step in steps)
                    {
                        string oldStepid = step["id"].ToString();
                        string stepid = Guid.NewGuid().ToString();
                        step["id"] = stepid;
                        foreach (LitJson.JsonData line in lines)
                        {
                            if (line["from"].ToString() == oldStepid)
                            {
                                line["from"] = stepid;
                            }
                            if (line["to"].ToString() == oldStepid)
                            {
                                line["to"] = stepid;
                            }
                        }
                        
                    }
                    foreach (LitJson.JsonData line in lines)
                    {
                        line["id"] = Guid.NewGuid().ToString();
                    }
                    wf.DesignJSON = json.ToJson();
                }

                dataWorkFlow.Add(wf);
            }
            ClaearCache();
            return wf;
        }

        /// <summary>
        /// 得到一个流程的缓存键
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        private string getCacheKey(Guid flowID)
        {
            return string.Concat(YJ.Utility.Keys.CacheKeys.WorkFlowInstalled_.ToString(), flowID.ToString("N"));
        }

        /// <summary>
        /// 得到流程运行时实体
        /// </summary>
        /// <param name="flowID">流程ID</param>
        /// <returns></returns>
        public YJ.Data.Model.WorkFlowInstalled GetWorkFlowRunModel(string flowID, bool cache = true)
        {
            Guid fid;
            return flowID.IsGuid(out fid) ? GetWorkFlowRunModel(fid, cache) : null;
        }

        /// <summary>
        /// 得到流程运行时实体
        /// </summary>
        /// <param name="flowID">流程ID</param>
        /// <returns></returns>
        public YJ.Data.Model.WorkFlowInstalled GetWorkFlowRunModel(Guid flowID, bool cache = true)
        {
            if (!cache)
            {
                return getWorkFlowRunFromDesign(flowID);
            }
            else
            {
                YJ.Data.Model.WorkFlowInstalled wfi = null;
                string key = getCacheKey(flowID);
                try
                {
                    object obj = YJ.Cache.IO.Opation.Get(key);
                    if (obj != null)
                    {

                        wfi = (YJ.Data.Model.WorkFlowInstalled)obj;

                    }
                }
                catch(Exception err)
                {
                    wfi = null;
                    Log.Add(err);
                }
                if (wfi == null)
                {
                    wfi = getWorkFlowRunFromDesign(flowID);
                    YJ.Cache.IO.Opation.Set(key, wfi);
                }
                return wfi;
            }
        }

        private YJ.Data.Model.WorkFlowInstalled getWorkFlowRunFromDesign(Guid flowID)
        {
            var wf = GetFromCache(flowID);
            if (wf == null)
            {
                wf = Get(flowID);
            }
            if (wf == null || wf.RunJSON.IsNullOrEmpty())
            {
                return null;
            }
            string msg;
            var wfi = GetWorkFlowRunModel(wf.RunJSON, out msg);
            return wfi;
        }

        /// <summary>
        /// 清除一个流程的运行时实体缓存
        /// </summary>
        /// <param name="flowID"></param>
        public void ClearWorkFlowCache(Guid flowID)
        {
            string key = getCacheKey(flowID);
            YJ.Cache.IO.Opation.Remove(key);
        }

        /// <summary>
        /// 刷新一个流程运行时实体
        /// </summary>
        /// <param name="flowID"></param>
        public void RefreshWrokFlowCache(Guid flowID)
        {
            string key = getCacheKey(flowID);
            YJ.Cache.IO.Opation.Set(key, GetWorkFlowRunModel(flowID, false));
        }

        /// <summary>
        /// 从缓存获取一个流程设置
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public Data.Model.WorkFlow GetFromCache(Guid flowID)
        {
            return GetAllFromCache().Find(p => p.ID == flowID);
        }

        /// <summary>
        /// 从缓存获取所有流程设置
        /// </summary>
        /// <returns></returns>
        public List<Data.Model.WorkFlow> GetAllFromCache()
        {
            List<Data.Model.WorkFlow> list = null;
            string cacheKey = Utility.Keys.CacheKeys.WorkFlowInstalled_.ToString();
            object obj = Cache.IO.Opation.Get(cacheKey);
            if (obj != null)
            {
                try
                {
                    list = (List<Data.Model.WorkFlow>)obj;
                }
                catch
                {
                    list = null;
                }
            }
            if (list == null)
            {
                list = GetAll();
                Cache.IO.Opation.Set(cacheKey, list);
            }
            return list;
        }

        /// <summary>
        /// 清除所有流程列表-List-Data.Model.WorkFlow
        /// </summary>
        public void ClaearCache()
        {
            string cacheKey = Utility.Keys.CacheKeys.WorkFlowInstalled_.ToString();
            Cache.IO.Opation.Remove(cacheKey);
        }

        /// <summary>
        /// 得到一个流程运行时实体
        /// </summary>
        /// <param name="jsonString">流程设计json字符串</param>
        /// <returns>流程已安装实体类(如果返回为空则表示验证失败,流程设计不完整)</returns>
        public YJ.Data.Model.WorkFlowInstalled GetWorkFlowRunModel(string jsonString, out string errMsg)
        {
            errMsg = "";
            YJ.Data.Model.WorkFlowInstalled wfInstalled = new YJ.Data.Model.WorkFlowInstalled();
            var json = LitJson.JsonMapper.ToObject(jsonString);
            
            #region 载入基本信息
            string id = json["id"].ToString();
            if (!id.IsGuid())
            {
                errMsg = "流程ID错误";
                return null;
            }
            else
            {
                wfInstalled.ID = id.ToGuid();
            }

            string name = json["name"].ToString();
            if (name.IsNullOrEmpty())
            {
                errMsg = "流程名称为空";
                return null;
            }
            else
            {
                wfInstalled.Name = name.Trim();
            }

            string type = json["type"].ToString();
            wfInstalled.Type = type.IsNullOrEmpty() ? new Dictionary().GetIDByCode("FlowTypes").ToString() : type.Trim();
            
            string manager = json["manager"].ToString();
            if (manager.IsNullOrEmpty())
            {
                errMsg = "流程管理者为空";
                return null;
            }
            else
            {
                wfInstalled.Manager = manager;
            }

            string instanceManager = json["instanceManager"].ToString();
            if (instanceManager.IsNullOrEmpty())
            {
                errMsg = "流程实例管理者为空";
                return null;
            }
            else
            {
                wfInstalled.InstanceManager = instanceManager;
            }

            wfInstalled.RemoveCompleted = json["removeCompleted"].ToString().ToInt();
            wfInstalled.Debug = json["debug"].ToString().ToInt();
            wfInstalled.DebugUsers = new YJ.Platform.Organize().GetAllUsers(json["debugUsers"].ToString());
            wfInstalled.Note = json["note"].ToString();
            wfInstalled.FlowType = json.ContainsKey("flowType") ? json["flowType"].ToString().ToInt() : 0;

            List<YJ.Data.Model.WorkFlowInstalledSub.DataBases> dataBases = new List<YJ.Data.Model.WorkFlowInstalledSub.DataBases>();
            var dbs = json["databases"];
            if (dbs.IsArray)
            {
                foreach (LitJson.JsonData db in dbs)
                {
                    dataBases.Add(new YJ.Data.Model.WorkFlowInstalledSub.DataBases()
                    {
                        LinkID = db["link"].ToString().ToGuid(),
                        LinkName = db["linkName"].ToString(),
                        Table = db["table"].ToString(),
                        PrimaryKey = db["primaryKey"].ToString()
                    });
                }
            }
            wfInstalled.DataBases = dataBases;

            var titleField = json["titleField"];
            if (titleField.IsObject)
            {
                wfInstalled.TitleField = new YJ.Data.Model.WorkFlowInstalledSub.TitleField()
                {
                    Field = titleField["field"].ToString(),
                    LinkID = titleField["link"].ToString().ToGuid(),
                    LinkName = "",
                    Table = titleField["table"].ToString()
                };
            }
            #endregion

            #region 载入步骤信息
            List<YJ.Data.Model.WorkFlowInstalledSub.Step> stepsList = new List<YJ.Data.Model.WorkFlowInstalledSub.Step>();
            LitJson.JsonData steps = json["steps"];
            if (steps.IsArray)
            {
                foreach (LitJson.JsonData step in steps)
                {
                    #region 行为
                    LitJson.JsonData behavior = step["behavior"];
                    YJ.Data.Model.WorkFlowInstalledSub.StepSet.Behavior behavior1 = new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Behavior();
                    if (behavior.IsObject)
                    {
                        behavior1.BackModel = behavior["backModel"].ToString().ToInt();
                        behavior1.BackStepID = behavior["backStep"].ToString().ToGuid();
                        behavior1.BackType = behavior["backType"].ToString().ToInt();
                        behavior1.DefaultHandler = behavior["defaultHandler"].ToString();
                        behavior1.FlowType = behavior["flowType"].ToString().ToInt();
                        behavior1.HandlerStepID = behavior["handlerStep"].ToString().ToGuid();
                        behavior1.HandlerType = behavior["handlerType"].ToString().ToInt();
                        behavior1.HanlderModel = behavior["hanlderModel"].ToString().ToInt(3);
                        behavior1.Percentage = behavior["percentage"].ToString().IsDecimal() ? behavior["percentage"].ToString().ToDecimal() : decimal.MinusOne;
                        behavior1.RunSelect = behavior["runSelect"].ToString().ToInt();
                        behavior1.SelectRange = behavior["selectRange"].ToString();
                        behavior1.ValueField = behavior["valueField"].ToString();
                        behavior1.Countersignature = behavior.ContainsKey("countersignature") ? behavior["countersignature"].ToString().ToInt() : 0;
                        behavior1.CountersignaturePercentage = behavior.ContainsKey("countersignaturePercentage") ? behavior["countersignaturePercentage"].ToString().ToDecimal() : decimal.MinusOne;
                        behavior1.SubFlowStrategy = behavior.ContainsKey("subflowstrategy") ? behavior["subflowstrategy"].ToString().ToInt() : int.MinValue;
                        behavior1.CopyFor = behavior.ContainsKey("copyFor") ? behavior["copyFor"].ToString() : "";
                        behavior1.ConcurrentModel = behavior.ContainsKey("concurrentModel") ? behavior["concurrentModel"].ToString().ToInt(0) : 0;
                        behavior1.DefaultHandlerSqlOrMethod = behavior.ContainsKey("defaultHandlerSqlOrMethod") ? behavior["defaultHandlerSqlOrMethod"].ToString() : "";
                    }
                    #endregion
                    #region 按钮
                    LitJson.JsonData buttons = step["buttons"];
                    List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button> buttionList = new List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button>();
                    if (buttons.IsArray)
                    {
                        foreach (LitJson.JsonData button in buttons)
                        {
                            string butID = button["id"].ToString();
                            if (butID.IsGuid())
                            {
                                var buttonModel = new WorkFlowButtons().Get(butID.ToGuid(), true);
                                if (buttonModel == null)
                                {
                                    continue;
                                }
                                buttionList.Add(new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                                {
                                    ID = butID,
                                    Note = buttonModel.Note.IsNullOrEmpty() ? "" : buttonModel.Note.Replace("\"", "'"),
                                    Sort = button["sort"].ToString().ToInt(),
                                    ShowTitle = button.ContainsKey("showTitle") ? button["showTitle"].ToString() : ""
                                });
                            }
                            else
                            {
                                buttionList.Add(new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                                {
                                    ID = butID,
                                    Note = "",
                                    Sort = button["sort"].ToString().ToInt(),
                                    ShowTitle = button.ContainsKey("showTitle") ? button["showTitle"].ToString() : ""
                                });
                            }
                        }
                    }
                    if (buttionList.Count == 0)
                    {
                        //errMsg = string.Format("步骤[{0}]未设置按钮", step["name"].ToString());
                        //return null;
                    }
                    #endregion
                    #region 抄送
                    Data.Model.WorkFlowInstalledSub.StepSet.CopyFor copyFor1 = new Data.Model.WorkFlowInstalledSub.StepSet.CopyFor();
                    if (step.ContainsKey("copyFor"))
                    {
                        LitJson.JsonData copyFor = step["copyFor"];
                        if (copyFor.IsObject)
                        {
                            copyFor1.handlerType = copyFor["handlerType"].ToString();
                            copyFor1.MemberID = copyFor["memberId"].ToString();
                            copyFor1.MethodOrSql = copyFor["methodOrSql"].ToString();
                            copyFor1.steps = copyFor["steps"].ToString();
                        }
                    }
                    #endregion
                    #region 事件
                    LitJson.JsonData event1 = step["event"];
                    YJ.Data.Model.WorkFlowInstalledSub.StepSet.Event event2 = new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Event();
                    if (event1.IsObject)
                    {
                        event2.BackAfter = event1["backAfter"].ToString();
                        event2.BackBefore = event1["backBefore"].ToString();
                        event2.SubmitAfter = event1["submitAfter"].ToString();
                        event2.SubmitBefore = event1["submitBefore"].ToString();
                        event2.SubFlowActivationBefore = event1.ContainsKey("subflowActivationBefore") ? event1["subflowActivationBefore"].ToString() : "";
                        event2.SubFlowCompletedBefore = event1.ContainsKey("subflowCompletedBefore") ? event1["subflowCompletedBefore"].ToString() : "";
                    }
                    #endregion
                    #region 表单
                    LitJson.JsonData forms = step["forms"];
                    List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.Form> formList = new List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.Form>();
                    if (forms.IsArray)
                    {
                        foreach (LitJson.JsonData form in forms)
                        {
                            formList.Add(new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Form()
                            {
                                ID = form["id"].ToString().ToGuid(),
                                Name = form["name"].ToString(),
                                IDApp = form.ContainsKey("idApp") && form["idApp"].ToString().IsGuid() ? form["idApp"].ToString().ToGuid() : Guid.Empty,
                                NameApp = form.ContainsKey("nameApp") ? form["nameApp"].ToString() : "",
                                Sort = form["srot"].ToString().ToInt()
                            });
                        }
                    }
                    if (formList.Count == 0)
                    {
                        //errMsg = string.Format("步骤[{0}]未设置表单", step["name"].ToString());
                        //return null;
                    }
                    #endregion
                    #region 字段状态
                    LitJson.JsonData fieldStatus = step["fieldStatus"];
                    List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.FieldStatus> fieldStatusList = new List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.FieldStatus>();
                    if (fieldStatus.IsArray)
                    {
                        foreach (LitJson.JsonData field in fieldStatus)
                        {
                            fieldStatusList.Add(new YJ.Data.Model.WorkFlowInstalledSub.StepSet.FieldStatus()
                            {
                                Check = field["check"].ToString().ToInt(),
                                Field = field["field"].ToString(),
                                Status1 = field["status"].ToString().ToInt(1)
                            });
                        }
                    }
                    #endregion
                    #region 坐标/基本信息
                    LitJson.JsonData position = step["position"];
                    decimal x = 0, y = 0;
                    if (position.IsObject)
                    {
                        x = position["x"].ToString().ToDecimal();
                        y = position["y"].ToString().ToDecimal();
                    }

                    stepsList.Add(new YJ.Data.Model.WorkFlowInstalledSub.Step()
                    {
                        Archives = step["archives"].ToString().ToInt(),
                        ArchivesParams = step["archivesParams"].ToString(),
                        Behavior = behavior1,
                        Buttons = buttionList,
                        DataSaveType = step.ContainsKey("dataSaveType") ? step["dataSaveType"].ToString().ToInt(0) : 0,
                        DataSaveTypeWhere = step.ContainsKey("dataSaveTypeWhere") ? step["dataSaveTypeWhere"].ToString() : "",
                        Event = event2,
                        CopyFor = copyFor1,
                        ExpiredPrompt = step["expiredPrompt"].ToString().ToInt(),
                        Forms = formList,
                        FieldStatus = fieldStatusList,
                        ID = step["id"].ToString().ToGuid(),
                        Type = step.ContainsKey("type") ? step["type"].ToString() : "normal",
                        LimitTime = step["limitTime"].ToString().ToDecimal(),
                        Name = step["name"].ToString(),
                        Note = step["note"].ToString(),
                        OpinionDisplay = step["opinionDisplay"].ToString().ToInt(),
                        OtherTime = step["otherTime"].ToString().ToDecimal(),
                        SignatureType = step["signatureType"].ToString().ToInt(),
                        WorkTime = step["workTime"].ToString().ToDecimal(),
                        SubFlowID = step.ContainsKey("subflow") ? step["subflow"].ToString() : "",
                        SubFlowTaskType = step.ContainsKey("subflowTaskType") ? step["subflowTaskType"].ToString().ToInt(0) : 0,
                        Position_x = x,
                        Position_y = y,
                        SendShowMsg = step.ContainsKey("sendShowMsg") ? step["sendShowMsg"].ToString() : "",
                        BackShowMsg = step.ContainsKey("backShowMsg") ? step["backShowMsg"].ToString() : "",
                        SendSetWorkTime = step.ContainsKey("sendSetWorkTime") ? step["sendSetWorkTime"].ToString().ToInt() : 0,
                        TimeOutModel = step.ContainsKey("timeOutModel") ? step["timeOutModel"].ToString().ToInt() : 0
                    });
                    #endregion

                }
            }

            if (1 == wfInstalled.FlowType)
            { 
                
            }

            wfInstalled.Steps = stepsList;
            if (stepsList.Count == 0)
            {
                errMsg = "流程至少需要一个步骤";
                return null;
            }
            #endregion

            #region 载入连线信息

            List<YJ.Data.Model.WorkFlowInstalledSub.Line> linesList = new List<YJ.Data.Model.WorkFlowInstalledSub.Line>();
            LitJson.JsonData lines = json.ContainsKey("lines") ? json["lines"] : null;
            if (lines != null && lines.IsArray)
            {
                foreach (LitJson.JsonData line in lines)
                {
                    linesList.Add(new YJ.Data.Model.WorkFlowInstalledSub.Line()
                    {
                        ID = line["id"].ToString().ToGuid(),
                        FromID = line["from"].ToString().ToGuid(),
                        ToID = line["to"].ToString().ToGuid(),
                        CustomMethod = line["customMethod"].ToString(),
                        SqlWhere = line["sql"].ToString(),
                        NoAccordMsg = line.ContainsKey("noaccordMsg") ? line["noaccordMsg"].ToString() : "",
                        Organize = line.ContainsKey("organize") ? line["organize"].ToJson() : ""
                        /*
                        Organize_SenderIn = line.ContainsKey("organize_senderin") ? line["organize_senderin"].ToString() : "",
                        Organize_SenderNotIn = line.ContainsKey("organize_sendernotin") ? line["organize_sendernotin"].ToString() : "",
                        Organize_SponsorIn = line.ContainsKey("organize_sponsorin") ? line["organize_sponsorin"].ToString() : "",
                        Organize_SponsorNotIn = line.ContainsKey("organize_sponsornotin") ? line["organize_sponsornotin"].ToString() : "",
                        Organize_SenderLeader = line.ContainsKey("organize_senderleader") ? line["organize_senderleader"].ToString() : "",
                        Organize_SenderChargeLeader = line.ContainsKey("organize_senderchargeleader") ? line["organize_senderchargeleader"].ToString() : "",
                        Organize_SponsorLeader = line.ContainsKey("organize_sponsorleader") ? line["organize_sponsorleader"].ToString() : "",
                        Organize_SponsorChargeLeader = line.ContainsKey("organize_sponsorchargeleader") ? line["organize_sponsorchargeleader"].ToString() : "",
                        Organize_NotSenderLeader = line.ContainsKey("organize_notsenderleader") ? line["organize_notsenderleader"].ToString() : "",
                        Organize_NotSenderChargeLeader = line.ContainsKey("organize_notsenderchargeleader") ? line["organize_notsenderchargeleader"].ToString() : "",
                        Organize_NotSponsorLeader = line.ContainsKey("organize_notsponsorleader") ? line["organize_notsponsorleader"].ToString() : "",
                        Organize_NotSponsorChargeLeader = line.ContainsKey("organize_notsponsorchargeleader") ? line["organize_notsponsorchargeleader"].ToString() : ""
                         */
                    });
                }
            }

            wfInstalled.Lines = linesList;

            #endregion

            #region 载入其它信息
            //得到第一步
            List<Guid> firstStepIDList = new List<Guid>();
            foreach (var step in wfInstalled.Steps)
            {
                if (wfInstalled.Lines.Where(p => p.ToID == step.ID).Count() == 0)
                {
                    firstStepIDList.Add(step.ID);
                }
            }
            if (firstStepIDList.Count==0)
            {
                errMsg = "流程没有开始步骤";
                return null;
            }
            /*
            else if (firstStepIDList.Count > 1)
            {
                errMsg = "流程有多个开始步骤";
                return null;
            }

            Guid lastStepID = Guid.Empty;
            foreach (var step in wfInstalled.Steps)
            {
                if (wfInstalled.Lines.Where(p => p.FromID == step.ID).Count() == 0)
                {
                    lastStepID = step.ID;
                    break;
                }
            }
            if (lastStepID == Guid.Empty)
            {
                errMsg = "流程没有结束步骤";
                return null;
            }
            */
            var wf = dataWorkFlow.Get(wfInstalled.ID);
            if (wf != null)
            {
                wfInstalled.CreateTime = wf.CreateDate;
                wfInstalled.CreateUser = wf.CreateUserID.ToString();
                wfInstalled.DesignJSON = wf.DesignJSON;
                wfInstalled.FirstStepID = firstStepIDList.First();
                wfInstalled.InstallTime = YJ.Utility.DateTimeNew.Now;
                wfInstalled.InstallUser = Platform.Users.CurrentUserID.ToString();
                wfInstalled.RunJSON = jsonString;
                wfInstalled.Status = wf.Status;
            }
            #endregion

            return wfInstalled;
        }

        /// <summary>
        /// 得到一个流程步骤的前面所有步骤集合
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowInstalledSub.Step> GetAllPrevSteps(Guid flowID, Guid stepID)
        {
            List<YJ.Data.Model.WorkFlowInstalledSub.Step> stepList = new List<YJ.Data.Model.WorkFlowInstalledSub.Step>();
            var wfInstalled = GetWorkFlowRunModel(flowID);
            if (wfInstalled == null)
            {
                return stepList;
            }
            addPrevSteps(stepList, wfInstalled, stepID);
            return stepList.Distinct().ToList();
        }

        private void addPrevSteps(List<YJ.Data.Model.WorkFlowInstalledSub.Step> list, YJ.Data.Model.WorkFlowInstalled wfInstalled, Guid stepID)
        {
            if (wfInstalled == null) return;
            var lines = wfInstalled.Lines.Where(p => p.ToID == stepID);
            foreach (var line in lines)
            {
                var step = wfInstalled.Steps.Where(p => p.ID == line.FromID);
                if (step.Count() > 0)
                {
                    list.Add(step.First());
                    addPrevSteps(list, wfInstalled, step.First().ID);
                }
            }
        }

        /// <summary>
        /// 得到一个流程步骤的前面步骤集合
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowInstalledSub.Step> GetPrevSteps(Guid flowID, Guid stepID)
        {
            List<YJ.Data.Model.WorkFlowInstalledSub.Step> stepList = new List<YJ.Data.Model.WorkFlowInstalledSub.Step>();
            var wfInstalled = GetWorkFlowRunModel(flowID);
            if (wfInstalled == null)
            {
                return stepList;
            }
            var lines = wfInstalled.Lines.Where(p => p.ToID == stepID);
            foreach (var line in lines)
            {
                var step = wfInstalled.Steps.Where(p => p.ID == line.FromID);
                if (step.Count() > 0)
                {
                    stepList.Add(step.First());
                }
            }
            return stepList;
        }


        /// <summary>
        /// 得到一个流程当前步骤的后续步骤集合
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkFlowInstalledSub.Step> GetNextSteps(Guid flowID, Guid stepID)
        {
            List<YJ.Data.Model.WorkFlowInstalledSub.Step> stepList = new List<YJ.Data.Model.WorkFlowInstalledSub.Step>();
            var wfInstalled = GetWorkFlowRunModel(flowID);
            if (wfInstalled == null)
            {
                return stepList;
            }
            var lines = wfInstalled.Lines.Where(p => p.FromID == stepID);
            foreach (var line in lines)
            {
                var step = wfInstalled.Steps.Where(p => p.ID == line.ToID);
                if (step.Count() > 0)
                {
                    stepList.Add(step.First());
                }
            }
            return stepList;
        }


        /// <summary>
        /// 根据步骤ID得到步骤名称
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="flowID"></param>
        /// <param name="flowName"></param>
        /// <param name="defaultFirstStepName">如果步骤为空是否返回第一步的名称</param>
        /// <returns></returns>
        public string GetStepName(Guid stepID, Guid flowID, out string flowName, bool defaultFirstStepName = false)
        {
            flowName = "";
            var wfInstalled = GetWorkFlowRunModel(flowID);
            if (wfInstalled == null) return "";
            if (stepID == Guid.Empty && defaultFirstStepName)
            {
                stepID = wfInstalled.FirstStepID;
            }
            flowName = wfInstalled.Name;
            var steps = wfInstalled.Steps.Where(p => p.ID == stepID);
            return steps.Count() > 0 ? steps.First().Name : "";
        }

        /// <summary>
        /// 根据步骤ID得到步骤名称
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="flowID"></param>
        /// <param name="defautFirstStepName">如果步骤ID为空是否默认为第一步</param>
        /// <returns></returns>
        public string GetStepName(Guid stepID, Guid flowID, bool defautFirstStepName=false)
        {
            string temp;
            return GetStepName(stepID, flowID, out temp, defautFirstStepName);
        }
        /// <summary>
        /// 根据步骤ID得到步骤名称
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="flowID"></param>
        /// <param name="defautFirstStepName">如果步骤ID为空是否默认为第一步</param>
        /// <returns></returns>
        public string GetStepName(Guid stepID, YJ.Data.Model.WorkFlowInstalled wfinstalled, bool defautFirstStepName = false)
        {
            if (wfinstalled == null) return "";
            if (stepID == Guid.Empty && defautFirstStepName)
            {
                stepID = wfinstalled.FirstStepID;
            }
            var steps = wfinstalled.Steps.Where(p => p.ID == stepID);
            return steps.Count() > 0 ? steps.First().Name : "";
        }

        /// <summary>
        /// 得到流程名称
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public string GetFlowName(Guid flowID)
        {
            var flow = GetFromCache(flowID);
            return flow != null ? flow.Name : "";
        }

        /// <summary>
        /// 查询所有ID和名称
        /// </summary>
        public Dictionary<Guid,string> GetAllIDAndName()
        {
            return dataWorkFlow.GetAllIDAndName();
        }

        /// <summary>
        /// 得到所有流程选择项
        /// </summary>
        /// <returns></returns>
        public string GetOptions(string value = "")
        {
            var dicts = GetAllIDAndName();
            StringBuilder options = new StringBuilder();
            foreach (var dict in dicts.OrderBy(p=>p.Value))
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", dict.Key,
                    ("," + value + ",").Contains("," + dict.Key.ToString() + ",") ? "selected=\"selected\"" : "", dict.Value);
            }
            return options.ToString();
        }

        /// <summary>
        /// 得到一个人员可管理实例的所有流程选择项
        /// </summary>
        /// <returns></returns>
        public string GetOptions(Dictionary<Guid,string> flows, string typeid, string value = "")
        {
            var dicts = flows;
            StringBuilder options = new StringBuilder();
            foreach (var dict in dicts)
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", dict.Key,
                    dict.Key.ToString() == value ? "selected=\"selected\"" : "", dict.Value);
            }
            return options.ToString();
        }

        /// <summary>
        /// 得到一个人员可管理实例的流程ID和名称列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="typeID">分类ID</param>
        /// <returns></returns>
        public Dictionary<Guid,string> GetInstanceManageFlowIDList(Guid userID, string typeID="")
        {
            var flows = this.GetAllFromCache();
            Organize borg = new Organize();
            Dictionary<Guid, string> flowids = new Dictionary<Guid, string>();
            foreach (var flow in flows)
            {
                if (typeID.IsGuid() && !GetAllChildsIDString(typeID.ToGuid()).Contains(flow.Type.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                //2017-2-10更改，为了提升速度，流程设置实例管理者时只能选择人员，这里就只判断人员
                if (flow.InstanceManager.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    flowids.Add(flow.ID, flow.Name);
                }
                //if (borg.GetAllUsers(flow.InstanceManager).Exists(p => p.ID == userID))
                //{
                //    flowids.Add(flow.ID, flow.Name);
                //}
            }
            return flowids;
        }

        /// <summary>
        /// 生成印章图片
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Bitmap CreateSignImage(string UserName)
        {
            if (UserName.IsNullOrEmpty())
            {
                return null;
            }
            System.Random rand = new Random(UserName.GetHashCode());
            Size ImageSize = Size.Empty;
            Font myFont = new Font("隶书", 16);

            // 计算图片大小 
            using (Bitmap bmp1 = new Bitmap(5, 5))
            {
                using (Graphics g = Graphics.FromImage(bmp1))
                {
                    SizeF size = g.MeasureString(UserName, myFont, 10000);
                    ImageSize.Width = (int)size.Width + 4;
                    ImageSize.Height = (int)size.Height;
                }
            }

            // 创建图片 
            Bitmap bmp = new Bitmap(ImageSize.Width, ImageSize.Height);

            // 绘制文本 
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (StringFormat f = new StringFormat())
                {
                    f.Alignment = StringAlignment.Center;
                    f.LineAlignment = StringAlignment.Center;
                    f.FormatFlags = StringFormatFlags.NoWrap;
                    g.DrawString(
                        UserName,
                        myFont,
                        Brushes.Red,
                        new RectangleF(
                        0,
                        0,
                        ImageSize.Width,
                        ImageSize.Height),
                        f);
                }
            }

            // 随机制造噪点 (用户名绑定)
            Color c = Color.Red;
            int x, y;
            int num = ImageSize.Width * ImageSize.Height * 8 / 100;
            for (int iCount = 0; iCount < num; iCount++)
            {
                x = rand.Next(0, 4);
                y = rand.Next(ImageSize.Height);
                bmp.SetPixel(x, y, c);

                x = rand.Next(ImageSize.Width - 4, ImageSize.Width);
                y = rand.Next(ImageSize.Height);
                bmp.SetPixel(x, y, c);

            }

            int num1 = ImageSize.Width * ImageSize.Height * 20 / 100;
            for (int iCount = 0; iCount < num1; iCount++)
            {
                x = rand.Next(ImageSize.Width);
                y = rand.Next(0, 4);
                bmp.SetPixel(x, y, c);

                x = rand.Next(ImageSize.Width);
                y = rand.Next(ImageSize.Height - 4, ImageSize.Height);
                bmp.SetPixel(x, y, c);
            }

            int num2 = ImageSize.Width * ImageSize.Height / 150;
            for (int iCount = 0; iCount < num2; iCount++)
            {
                x = rand.Next(ImageSize.Width);
                y = rand.Next(ImageSize.Height);
                bmp.SetPixel(x, y, c);
            }

            myFont.Dispose();

            return bmp;
        }

        /// <summary>
        /// 得到流程运行时自动标题
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public string GetAutoTitle(string flowID, string stepID)
        { 
            string flowName;
            string stepName = GetStepName(stepID.ToGuid(), flowID.ToGuid(), out flowName, true);
            return string.Format("<div class='flowautotitle'>{0} - {1}</div>", flowName, stepName);
        }

        /// <summary>
        /// 得到默认任务标题
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public string GetAutoTaskTitle(string flowID, string stepID, string groupID = "")
        {
            var wfrun = GetWorkFlowRunModel(flowID);
            if (wfrun == null) return "";
            string flowName = wfrun.Name;
            string senderName = "";
            Guid gid;
            if (groupID.IsGuid(out gid) || gid==Guid.Empty)
            {
                var fqz = new WorkFlowTask().GetFirstSnderID(flowID.ToGuid(), gid);
                senderName = new Users().GetName(fqz);
            }
            if (senderName.IsNullOrEmpty())
            {
                senderName = Users.CurrentUserName;
            }

            return string.Concat(flowName, "(", senderName, ")");
        }

        /// <summary>
        /// 保存表单数据
        /// </summary>
        public string SaveFromData(string instanceid, YJ.Data.Model.WorkFlowCustomEventParams eventParams)
        {
            //保存自定义表单内容
            string form_CustomSaveMethod = System.Web.HttpContext.Current.Request.Form["Form_CustomSaveMethod"];
            if (!form_CustomSaveMethod.IsNullOrEmpty())
            {
                return new WorkFlowTask().ExecuteFlowCustomEvent(form_CustomSaveMethod, eventParams).ToString();
            }

            if ("1" != System.Web.HttpContext.Current.Request.Form["Form_AutoSaveData"])
            {
                return instanceid;
            }
            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();
            string dbconnid = System.Web.HttpContext.Current.Request.Form["Form_DBConnID"];
            string dbtable = System.Web.HttpContext.Current.Request.Form["Form_DBTable"];
            string dbtablepk = System.Web.HttpContext.Current.Request.Form["Form_DBTablePk"];
            string dbtabletitle = System.Web.HttpContext.Current.Request.Form["Form_DBTableTitle"];
            string HasSerialNumber = System.Web.HttpContext.Current.Request.Form["HasSerialNumber"];//是否有序列号
            if (!dbconnid.IsGuid())
            {
                return instanceid;
            }

            //读取当前步骤的字段状态设置，为了解决CHECKBOX,RADIO控件选择后不能设置为空的判断。
            List<Data.Model.WorkFlowInstalledSub.StepSet.FieldStatus> fieldStatusList = new List<Data.Model.WorkFlowInstalledSub.StepSet.FieldStatus>();
            int dataSaveType = 0;
            string dataSaveTypeWhere = string.Empty;
            var wfInstance = GetWorkFlowRunModel(eventParams.FlowID);
            if (wfInstance != null)
            {
                var steps = wfInstance.Steps.Where(p => p.ID == eventParams.StepID);
                if (steps.Count() > 0)
                {
                    fieldStatusList = steps.First().FieldStatus.ToList();
                    dataSaveType = steps.First().DataSaveType;
                    dataSaveTypeWhere = steps.First().DataSaveTypeWhere;
                }
            }

            YJ.Data.Model.DBConnection dbconn = bdbconn.Get(dbconnid.ToGuid());
            if (dbconn == null)
            {
                return instanceid;
            }
            
            using (System.Data.IDbConnection conn = bdbconn.GetConnection(dbconn))
            {
                if (conn == null)
                {
                    return instanceid;
                }
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write("连接数据库出错：" + ex.Message);
                    YJ.Platform.Log.Add(ex);
                }
                
                string sql = string.Empty;
                List<System.Data.IDataParameter> parList = new List<System.Data.IDataParameter>();
                if(instanceid.IsNullOrEmpty() || 1 == dataSaveType)
                {
                    sql = string.Format("SELECT * FROM {0} WHERE {1}", dbtable, dataSaveTypeWhere.IsNullOrEmpty() ? "1=0" : dataSaveTypeWhere.FilterWildcard());
                }
                else
                {
                    switch (dbconn.Type)
                    { 
                        case "SqlServer":
                            sql = string.Format("SELECT * FROM {0} WHERE {1}=@pk", dbtable, dbtablepk);
                            parList.Add(new System.Data.SqlClient.SqlParameter("@pk", instanceid));
                            break;
                        case "Oracle":
                            sql = string.Format("SELECT * FROM {0} WHERE {1}=:pk", dbtable, dbtablepk);
                            parList.Add(new  OracleParameter(":pk", instanceid));
                            break;
                        case "MySql":
                            sql = string.Format("SELECT * FROM {0} WHERE {1}=@pk", dbtable, dbtablepk);
                            parList.Add(new MySqlParameter("@pk", instanceid));
                            break;
                    }
                }
                System.Data.IDbDataAdapter dataAdapter = bdbconn.GetDataAdapter(conn, dbconn.Type, sql, parList.ToArray());
                System.Data.DataSet ds = new System.Data.DataSet();
                dataAdapter.Fill(ds);
                System.Data.DataTable schemaDt = bdbconn.GetTableSchema(conn, dbtable, dbconn.Type);
                System.Data.DataTable dt = ds.Tables[0];
                bool isNew = dt.Rows.Count == 0;
                if (isNew)
                {
                    dt.Rows.Add(dt.NewRow());
                }

                //设置主键值(应用于参数中有instanceid而对应业务表中没有数据时保存会出错)
                if (!instanceid.IsNullOrEmpty())
                {
                    dt.Rows[0][dbtablepk] = instanceid;
                }

                #region 保存主表数据
                string maxfiled = string.Empty;//保存当前流水号的字段
                int maxNumber = 0;//当前流水号值
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colnumName = dt.Columns[i].ColumnName;
                    string name = string.Concat(dbtable, ".", colnumName);
                    string value = System.Web.HttpContext.Current.Request.Form[name];
                    if (string.Compare(colnumName, dbtablepk, true) == 0)
                    {
                        if (value.IsNullOrEmpty())
                        {
                            continue;
                        }
                        else
                        {
                            instanceid = value;
                        }
                    }

                    #region 流水号
                    if (value.IsNullOrEmpty() && !HasSerialNumber.IsNullOrEmpty()
                        && ("," + HasSerialNumber + ",").Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (dt.Rows[0][colnumName].ToString().IsNullOrEmpty())
                        {
                            string serialNumberConfig = System.Web.HttpContext.Current.Request.Form["HasSerialNumberConfig_" + name];
                            if (!serialNumberConfig.IsNullOrEmpty())
                            {
                                LitJson.JsonData serialNumberJson = LitJson.JsonMapper.ToObject(serialNumberConfig);
                                value = bdbconn.GetSerialNumber(conn, dbconn.Type, dbtable, colnumName, serialNumberJson, out maxNumber);
                                maxfiled = serialNumberJson.ContainsKey("maxfiled") ? serialNumberJson["maxfiled"].ToString() : "";
                            }
                        }
                        else 
                        {
                            value = dt.Rows[0][colnumName].ToString();
                        }
                    }
                    #endregion

                    if (value == null && !isNew)
                    {
                        var fieldStatus = fieldStatusList.Find(p => p.Field.Equals(dbconnid + "." + name, StringComparison.CurrentCultureIgnoreCase));
                        //这里判断是为了角色CHECKBOX,RADIO控件选择之后设置为空值的问题。
                        if (fieldStatus == null || fieldStatus.Status1.In(1, 2))
                        {
                            continue;
                        }
                    }
                    var colnum = dt.Columns[i];
                    string colnumDataType = colnum.DataType.FullName;
                    object defaultValue;

                    System.Data.DataRow[] schemaDrs = schemaDt.Select(string.Format("f_name='{0}'", colnumName));
                    bool hasDefault = false;//列是否有默认值
                    bool hasNull = false;//列是否可以为空
                    bool isSet = getColnumIsValue(colnumDataType, value, out defaultValue);
                    object dbDefaultValue = string.Empty;//数据库表默认值
                    
                    switch (dbconn.Type)
                    { 
                        case "SqlServer":
                            hasDefault = schemaDrs.Length > 0 && schemaDrs[0]["cdefault"].ToString() != "0";
                            hasNull = schemaDrs.Length > 0 && schemaDrs[0]["is_null"].ToString() != "0";
                            if (hasDefault && !schemaDrs[0]["defaultvalue"].ToString().IsNullOrEmpty())
                            {
                                dbDefaultValue = bdbconn.GetDbDefaultValue_SqlServer(schemaDrs[0]["defaultvalue"].ToString().Trim1());
                            }
                            break;
                        case "Oracle":
                            hasDefault = schemaDrs.Length > 0 && !schemaDrs[0]["cdefault"].ToString().IsNullOrEmpty();
                            hasNull = schemaDrs.Length > 0 && schemaDrs[0]["is_null"].ToString() != "0";
                            if (hasDefault && !schemaDrs[0]["defaultvalue"].ToString().IsNullOrEmpty())
                            {
                                dbDefaultValue = bdbconn.GetDbDefaultValue_Oracle(schemaDrs[0]["defaultvalue"].ToString().Trim1());
                            }
                            break;
                        case "MySql":
                            hasDefault = schemaDrs.Length > 0 && !schemaDrs[0]["cdefault"].ToString().IsNullOrEmpty();
                            hasNull = schemaDrs.Length > 0 && schemaDrs[0]["is_null"].ToString() != "0";
                            if (hasDefault && !schemaDrs[0]["defaultvalue"].ToString().IsNullOrEmpty())
                            {
                                dbDefaultValue = bdbconn.GetDbDefaultValue_MySql(schemaDrs[0]["defaultvalue"].ToString().Trim1());
                            }
                            break;
                    }
                    if (maxfiled.IsNullOrEmpty() || !colnumName.Equals(maxfiled, StringComparison.CurrentCultureIgnoreCase))//这里做判断主要是为了二次保存时不将存储编号的字段更新为空
                    {
                        if (isSet)
                        {
                            dt.Rows[0][colnumName] = value;
                        }
                        else
                        {
                            if (!hasDefault)
                            {
                                if (hasNull)
                                {
                                    dt.Rows[0][colnumName] = DBNull.Value;
                                }
                                else
                                {
                                    dt.Rows[0][colnumName] = defaultValue;
                                }
                            }
                            else
                            {
                                if (dbDefaultValue != null)
                                {
                                    try
                                    {
                                        dt.Rows[0][colnumName] = dbDefaultValue;
                                    }
                                    catch
                                    { }
                                }
                            }
                        }
                    }
                }
                //如果设置了最大流水号字段，则这里要将当前流水号值保存到这个字段
                if (!maxfiled.IsNullOrEmpty())
                {
                    dt.Rows[0][maxfiled] = maxNumber;
                }
                #endregion

                #region 设置主键值
                bool isIdentity = false;
                if (isNew)
                {
                    if (instanceid.IsNullOrEmpty())
                    {
                        var pkColnum = dt.Columns[dbtablepk];
                        System.Data.DataRow[] schemaDrs = schemaDt.Select(string.Format("f_name='{0}'", dbtablepk));
                        if (schemaDrs.Length > 0)
                        {
                            isIdentity = false;
                            bool isDefault = false;
                            bool isGuid = false;
                            switch (dbconn.Type)
                            { 
                                case "SqlServer":
                                    isIdentity = schemaDrs[0]["isidentity"].ToString() == "1";
                                    isDefault = schemaDrs[0]["cdefault"].ToString() != "0";
                                    isGuid = pkColnum.DataType.FullName == "System.Guid";
                                    break;
                                case "Oracle":
                                    isIdentity = schemaDrs[0]["t_name"].ToString().Equals("NUMBER", StringComparison.CurrentCultureIgnoreCase);
                                    isDefault = !schemaDrs[0]["cdefault"].ToString().IsNullOrEmpty();
                                    isGuid = pkColnum.DataType.FullName == "System.String";
                                    break;
                                case "MySql":
                                    isIdentity = schemaDrs[0]["isidentity"].ToString() == "1";
                                    isDefault = !schemaDrs[0]["cdefault"].ToString().IsNullOrEmpty();
                                    isGuid = pkColnum.DataType.FullName == "System.Guid" || pkColnum.DataType.FullName == "System.String";
                                    break;
                            }
                            if (!isIdentity && isGuid)
                            {
                                instanceid = Guid.NewGuid().ToString();
                                dt.Rows[0][dbtablepk] = instanceid;
                            }
                        }
                    }
                    else
                    {
                        //dt.Rows[0][dbtablepk] = instanceid;
                    }
                }
                #endregion

                #region 执行保存
                switch (dbconn.Type)
                {
                    case "SqlServer":
                        System.Data.SqlClient.SqlCommandBuilder scb = new System.Data.SqlClient.SqlCommandBuilder((System.Data.SqlClient.SqlDataAdapter)dataAdapter);
                        dataAdapter.Update(ds);
                        scb.Dispose();
                        break;
                    case "Oracle":
                        OracleCommandBuilder ocb = new OracleCommandBuilder((OracleDataAdapter)dataAdapter);
                        dataAdapter.Update(ds);
                        ocb.Dispose();
                        break;
                    case "MySql":
                        MySqlCommandBuilder mcb = new MySqlCommandBuilder((MySqlDataAdapter)dataAdapter);
                        dataAdapter.Update(ds);
                        mcb.Dispose();
                        break;
                }
                
                
                #endregion

                #region 如果是新增，又是自增列则查询刚插入的自增列值
                if (isNew && isIdentity)
                {
                    switch (dbconn.Type)
                    {
                        case "SqlServer":
                            string identitysql = "SELECT @@IDENTITY";
                            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(identitysql, (System.Data.SqlClient.SqlConnection)conn))
                            {
                                object obj = cmd.ExecuteScalar();
                                if (obj != null)
                                {
                                    instanceid = obj.ToString();
                                    dt.Rows[0][dbtablepk] = instanceid;
                                }
                            }
                            break;
                        case "Oracle":
                            string identitysql1 = string.Format("SELECT {0}_{1}_SEQ.currval FROM dual", dbtable, dbtablepk);
                            using (OracleCommand cmd = new OracleCommand(identitysql1, (OracleConnection)conn))
                            {
                                object obj = cmd.ExecuteScalar();
                                if (obj != null)
                                {
                                    instanceid = obj.ToString();
                                    dt.Rows[0][dbtablepk] = instanceid;
                                }
                            }
                            break;
                        case "MySql":
                            string identitysql2 = string.Format("select @@IDENTITY");
                            using (MySqlCommand cmd = new MySqlCommand(identitysql2, (MySqlConnection)conn))
                            {
                                object obj = cmd.ExecuteScalar();
                                if (obj != null)
                                {
                                    instanceid = obj.ToString();
                                    dt.Rows[0][dbtablepk] = instanceid;
                                }
                            }
                            break;
                    }
                }
                #endregion

                #region 保存从表数据
                string flowSubTableIDString = System.Web.HttpContext.Current.Request.Form["flowsubtable_id"] ?? "";
                string[] flowSubTableIDArray = flowSubTableIDString.Split(',');
                foreach (string flowSubTableID in flowSubTableIDArray)
                {
                    string secondtable = System.Web.HttpContext.Current.Request.Form["flowsubtable_" + flowSubTableID + "_secondtable"];
                    string primarytablefiled = System.Web.HttpContext.Current.Request.Form["flowsubtable_" + flowSubTableID + "_primarytablefiled"];
                    string secondtableprimarykey = System.Web.HttpContext.Current.Request.Form["flowsubtable_" + flowSubTableID + "_secondtableprimarykey"];
                    string secondtablerelationfield = System.Web.HttpContext.Current.Request.Form["flowsubtable_" + flowSubTableID + "_secondtablerelationfield"];
                    if (secondtable.IsNullOrEmpty() || primarytablefiled.IsNullOrEmpty() || secondtableprimarykey.IsNullOrEmpty() || secondtablerelationfield.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string primyarTableFeldValue = dt.Rows[0][primarytablefiled].ToString();
                    if (primyarTableFeldValue.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string subSql = string.Empty;
                    List<System.Data.IDataParameter> parList1 = new List<System.Data.IDataParameter>();
                    switch (dbconn.Type)
                    { 
                        case "SqlServer":
                            subSql = string.Format("SELECT * FROM {0} WHERE {1}=@pk", secondtable, secondtablerelationfield);
                            parList1.Add(new System.Data.SqlClient.SqlParameter("@pk", primyarTableFeldValue));
                            break;
                        case "Oracle":
                            subSql = string.Format("SELECT * FROM {0} WHERE {1}=:pk", secondtable, secondtablerelationfield);
                            parList1.Add(new OracleParameter(":pk", primyarTableFeldValue));
                            break;
                        case "MySql":
                            subSql = string.Format("SELECT * FROM {0} WHERE {1}=@pk", secondtable, secondtablerelationfield);
                            parList1.Add(new MySqlParameter("@pk", primyarTableFeldValue));
                            break;
                    }
                    string[] colGuidArray = (System.Web.HttpContext.Current.Request.Form["hidden_guid_" + flowSubTableID] ?? "").Split(',');
                    System.Data.IDbDataAdapter dataAdapter1 = bdbconn.GetDataAdapter(conn, dbconn.Type, subSql, parList1.ToArray());
                    System.Data.DataSet ds1 = new System.Data.DataSet();
                    dataAdapter1.Fill(ds1);
                    System.Data.DataTable schemaDt1 = bdbconn.GetTableSchema(conn, secondtable, dbconn.Type);
                    System.Data.DataTable dt1 = ds1.Tables[0];
                    bool isInitNew = dt1.Rows.Count == 0;
                    foreach (string colGuid in colGuidArray)
                    {
                        bool isNew1 = true;
                        System.Data.DataRow dr1 = null;
                        foreach (System.Data.DataRow dr in dt1.Rows)
                        {
                            if (string.Compare(dr[secondtableprimarykey].ToString(), colGuid, 0) == 0)
                            {
                                dr1 = dr;
                                isNew1 = false;
                                break;
                            }
                        }
                       
                        if (isNew1)
                        {
                            dr1 = dt1.NewRow();
                            dr1[secondtablerelationfield] = primyarTableFeldValue;
                            dt1.Rows.Add(dr1);
                            isNew1 = true;
                        }
                    
                        #region 循环保存列数据
                        for (int i = 0; i < dt1.Columns.Count; i++)
                        {
                            string colnumName1 = dt1.Columns[i].ColumnName;
                            if (string.Compare(colnumName1, secondtableprimarykey, true) == 0
                                 || string.Compare(colnumName1, secondtablerelationfield,0) ==0 )
                            {
                                continue;
                            }

                            string value1 = System.Web.HttpContext.Current.Request.Form[flowSubTableID + "_" + colGuid + "_" + secondtable + "_" + colnumName1];
                            if (value1 == null && !isNew1)
                            {
                                var fieldStatus = fieldStatusList.Find(p => p.Field.Equals(dbconnid + "." + secondtable + "." + colnumName1, StringComparison.CurrentCultureIgnoreCase));
                                //这里判断是为了角色CHECKBOX,RADIO控件选择之后设置为空值的问题。
                                if (fieldStatus == null || fieldStatus.Status1.In(1, 2))
                                {
                                    continue;
                                }
                            }
                            var colnum1 = dt1.Columns[i];
                            string colnumDataType1 = colnum1.DataType.FullName;
                            object defaultValue1 = string.Empty;
                            object dbDefaultValue1 = null;

                            System.Data.DataRow[] schemaDrs1 = schemaDt1.Select(string.Format("f_name='{0}'", colnumName1));
                            bool hasDefault1 = schemaDrs1.Length > 0 && schemaDrs1[0]["cdefault"].ToString() != "0";//列是否有默认值
                            bool hasNull1 = schemaDrs1.Length > 0 && schemaDrs1[0]["is_null"].ToString() != "0";//列是否可以为空
                            bool isSet1 = getColnumIsValue(colnumDataType1, value1, out defaultValue1);
                            if (hasDefault1 && !schemaDrs1[0]["defaultvalue"].ToString().IsNullOrEmpty())
                            {
                                switch (dbconn.Type)
                                {
                                    case "SqlServer":
                                        dbDefaultValue1 = bdbconn.GetDbDefaultValue_SqlServer(schemaDrs1[0]["defaultvalue"].ToString().Trim1());
                                        break;
                                    case "Oracle":
                                        dbDefaultValue1 = bdbconn.GetDbDefaultValue_Oracle(schemaDrs1[0]["defaultvalue"].ToString().Trim1());
                                        break;
                                    case "MySql":
                                        dbDefaultValue1 = bdbconn.GetDbDefaultValue_MySql(schemaDrs1[0]["defaultvalue"].ToString().Trim1());
                                        break;
                                }
                                
                            }
                            if (isSet1)
                            {
                                dr1[colnumName1] = value1;
                            }
                            else
                            {
                                if (!hasDefault1)
                                {
                                    if (hasNull1)
                                    {
                                        dr1[colnumName1] = DBNull.Value;
                                    }
                                    else
                                    {
                                        dr1[colnumName1] = defaultValue1;
                                    }
                                }
                                else
                                {
                                    if (dbDefaultValue1 != null)
                                    {
                                        try
                                        {
                                            dr1[colnumName1] = dbDefaultValue1;
                                        }
                                        catch
                                        { }
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    #region 删除多余行
                    if (!isInitNew)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            bool isIn = false;
                            foreach (string colGuid in colGuidArray)
                            {
                                if (dt1.Rows[i][secondtableprimarykey].ToString().IsNullOrEmpty() || string.Compare(dt1.Rows[i][secondtableprimarykey].ToString(), colGuid, 0) == 0)
                                {
                                    isIn = true;
                                    break;
                                }
                            }
                            if (!isIn)
                            {
                                dt1.Rows[i].Delete();
                            }
                        }
                    }
                    #endregion

                    #region 检查从表如果数据库类型是Oracle或MySql并且主键是VARCHAR2，则要设置默认值(Oracle不能自动生成guid)
                    if ("Oracle".Equals(dbconn.Type) || "MySql".Equals(dbconn.Type))
                    {
                        System.Data.DataRow[] schemaDrs2 = schemaDt1.Select(string.Format("f_name='{0}'", secondtableprimarykey));
                        bool isIdentity1 = schemaDrs2.Length>0 && schemaDrs2[0]["isidentity"].ToString() == "1";
                        if ("Oracle".Equals(dbconn.Type))
                        {
                            isIdentity1 = schemaDrs2[0]["t_name"].ToString().Equals("NUMBER", StringComparison.CurrentCultureIgnoreCase);
                        }
                        if (!isIdentity1)
                        {
                            foreach (System.Data.DataRow dr in dt1.Rows)
                            {
                                if (dr.RowState == System.Data.DataRowState.Added)
                                {
                                    dr[secondtableprimarykey] = Guid.NewGuid();
                                }
                            }
                        }
                    }
                    
                    #endregion

                    #region 执行保存
                    switch (dbconn.Type)
                    {
                        case "SqlServer":
                            System.Data.SqlClient.SqlCommandBuilder scb1 = new System.Data.SqlClient.SqlCommandBuilder((System.Data.SqlClient.SqlDataAdapter)dataAdapter1);
                            dataAdapter1.Update(ds1);
                            scb1.Dispose();
                            break;
                        case "Oracle":
                            OracleCommandBuilder ocb1 = new OracleCommandBuilder((OracleDataAdapter)dataAdapter1);
                            dataAdapter1.Update(ds1);
                            ocb1.Dispose();
                            break;
                        case "MySql":
                            MySqlCommandBuilder mcb1 = new MySqlCommandBuilder((MySqlDataAdapter)dataAdapter1);
                            dataAdapter1.Update(ds1);
                            mcb1.Dispose();
                            break;
                    }
                    #endregion

                }
                
                #endregion

                return instanceid;
            }
        }

        /// <summary>
        /// 判断列是否有值
        /// </summary>
        /// <param name="colnumDataType"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private bool getColnumIsValue(string colnumDataType, string value, out object defaultValue)
        {
            bool isSet = false;
            defaultValue = null;
            switch (colnumDataType)
            {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                    isSet = value.IsInt();
                    defaultValue = int.MinValue;
                    break;
                case "System.String":
                    isSet = value != null;
                    defaultValue = "";
                    break;
                case "System.Guid":
                    isSet = value.IsGuid();
                    defaultValue = Guid.Empty;
                    break;
                case "System.Decimal":
                    isSet = value.IsDecimal();
                    defaultValue = decimal.MinValue;
                    break;
                case "System.Double":
                case "System.Single":
                    isSet = value.IsDouble();
                    defaultValue = double.MinValue;
                    break;
                case "System.DateTime":
                    isSet = value.IsDateTime();
                    defaultValue = Utility.DateTimeNew.Now;
                    break;
                case "System.Object":
                    isSet = value != null;
                    defaultValue = "";
                    break;
                case "System.Boolean":
                    isSet = value != null && (value.ToString().ToLower() == "false" 
                        || value.ToString().ToLower() == "true");
                    defaultValue = 0;
                    break;
            }
            return isSet;
        }

        /// <summary>
        /// 得到实例数据
        /// </summary>
        /// <param name="connid"></param>
        /// <param name="table"></param>
        /// <param name="pk"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public LitJson.JsonData GetFormData(string connid, string table, string pk, string instanceid, string filedStatus = "", string formats = "", string taskID="")
        {
            LitJson.JsonData jsonData = new LitJson.JsonData();
            if (instanceid.IsNullOrEmpty())
            {
                return jsonData;
            }
            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();
            YJ.Data.Model.DBConnection dbconn = bdbconn.Get(connid.ToGuid());
            if (dbconn == null)
            {
                return "";
            }
            LitJson.JsonData formatJSON = null;
            if (!formats.IsNullOrEmpty())
            {
                formatJSON = LitJson.JsonMapper.ToObject(formats);
            }
            using (System.Data.IDbConnection conn = bdbconn.GetConnection(dbconn))
            {
                if (conn == null)
                {
                    return "";
                }
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write("连接数据库出错：" + ex.Message);
                    YJ.Platform.Log.Add(ex);
                }
                string sql = string.Empty;
                string sqlWhere = string.Empty;
                int dataSaveType = 0;
                List<System.Data.IDataParameter> parList = new List<System.Data.IDataParameter>();
                if (taskID.IsGuid())
                {
                    var task = new WorkFlowTask().Get(taskID.ToGuid());
                    if (task != null)
                    {
                        var wfinstance = new WorkFlow().GetWorkFlowRunModel(task.FlowID);
                        if (wfinstance != null)
                        {
                            var steps = wfinstance.Steps.Where(p => p.ID == task.StepID);
                            if (steps.Count() > 0)
                            {
                                dataSaveType = steps.First().DataSaveType;
                                sqlWhere = steps.First().DataSaveTypeWhere;
                            }
                        }
                    }
                }
                if (dataSaveType == 1)
                {
                    sql = string.Format("SELECT * FROM {0} WHERE {1}", table, sqlWhere.FilterWildcard());
                }
                else
                {
                    switch (dbconn.Type)
                    {
                        case "SqlServer":
                            sql = string.Format("SELECT * FROM {0} WHERE {1}=@pk", table, pk);
                            parList.Add(new System.Data.SqlClient.SqlParameter("@pk", instanceid));
                            break;
                        case "Oracle":
                            sql = string.Format("SELECT * FROM {0} WHERE {1}=:pk", table, pk);
                            parList.Add(new OracleParameter(":pk", instanceid));
                            break;
                        case "MySql":
                            sql = string.Format("SELECT * FROM {0} WHERE {1}=@pk", table, pk);
                            parList.Add(new MySqlParameter("@pk", instanceid));
                            break;
                    }
                }

                System.Data.IDbDataAdapter dataAdapter = bdbconn.GetDataAdapter(conn, dbconn.Type, sql, parList.ToArray());
                System.Data.DataSet ds = new System.Data.DataSet();
                dataAdapter.Fill(ds);
                if (dataAdapter.SelectCommand != null)
                {
                    dataAdapter.SelectCommand.Dispose();
                }

                System.Data.DataTable dt = ds.Tables[0];
                LitJson.JsonData json = null;
                if (!filedStatus.IsNullOrEmpty())
                {
                    json = LitJson.JsonMapper.ToObject(filedStatus);
                }
                if (dt.Rows.Count > 0)
                {
                    System.Data.DataRow dr = dt.Rows[0];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        bool isShow = true;
                        string fieldName = (table + "_" + dt.Columns[i].ColumnName).ToLower();
                        if (json != null && json.ContainsKey(fieldName))
                        {
                            string status = json[fieldName].ToString();
                            if (!status.IsNullOrEmpty())
                            {
                                string[] statusArray = status.Split('_');
                                if (statusArray.Length == 2 && "2" == statusArray[0])
                                {
                                    isShow = false;
                                }
                            }
                        }
                        string value = dr[dt.Columns[i].ColumnName].ToString();
                        string jsonkey = (table + "." + dt.Columns[i].ColumnName).ToLower();
                        if (formatJSON != null && formatJSON.ContainsKey(jsonkey))
                        {
                            string format = formatJSON[jsonkey].ToString();
                            if (value.IsDecimal())
                            {
                                value = value.ToDecimal().ToString(format);
                            }
                            else if (value.IsDateTime())
                            {
                                value = value.ToDateTime().ToString(format);
                            }
                        }
                        if (isShow)
                        {
                            jsonData[fieldName] = value;
                        }
                        else
                        {
                            jsonData[fieldName] = "";
                        }
                    }
                }

            }
            return jsonData;
        }

        /// <summary>
        /// 得到实例数据
        /// </summary>
        /// <param name="connid"></param>
        /// <param name="table"></param>
        /// <param name="pk"></param>
        /// <param name="instanceid"></param>
        /// <returns>json字符串</returns>
        public string GetFormDataJsonString(string connid, string table, string pk, string instanceid)
        {
            LitJson.JsonData jsonData = GetFormData(connid, table, pk, instanceid);
            return GetFormDataJsonString(jsonData);
        }

        
        /// <summary>
        /// 得到实例数据
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns>json字符串</returns>
        public string GetFormDataJsonString(LitJson.JsonData jsonData)
        {
            string json = jsonData.ToJson();
            return json.IsNullOrEmpty() ? "{}" : json;
        }

        /// <summary>
        /// 得到从表数据
        /// </summary>
        /// <param name="connID">连接ID</param>
        /// <param name="secondTable">从表名称</param>
        /// <param name="relationField">关联字段</param>
        /// <param name="fieldValue">关联字段值</param>
        /// <param name="sortField">排序字段</param>
        /// <returns></returns>
        public LitJson.JsonData GetSubTableData(string connID, string secondTable, string relationField, string fieldValue, string sortField = "", string fieldFormat = "")
        {
            LitJson.JsonData jsonData = new LitJson.JsonData();
            if (fieldValue.IsNullOrEmpty())
            {
                return jsonData;
            }
            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();
            YJ.Data.Model.DBConnection dbconn = bdbconn.Get(connID.ToGuid());
            if (dbconn == null)
            {
                return "";
            }

            using (System.Data.IDbConnection conn = bdbconn.GetConnection(dbconn))
            {
                if (conn == null)
                {
                    return "";
                }
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write("连接数据库出错：" + ex.Message);
                    YJ.Platform.Log.Add(ex);
                }
                string sql = string.Empty;
                List<System.Data.IDataParameter> parList = new List<System.Data.IDataParameter>();
                switch (dbconn.Type)
                {
                    case "SqlServer":
                       sql = string.Format("SELECT * FROM {0} WHERE {1}=@fieldvalue {2}", secondTable, relationField,
                                (sortField.IsNullOrEmpty() ? "" : string.Concat("ORDER BY ", sortField)));
                        parList.Add(new System.Data.SqlClient.SqlParameter("@fieldvalue", fieldValue));
                        break;
                    case "Oracle":
                        sql = string.Format("SELECT * FROM {0} WHERE {1}=:fieldvalue {2}", secondTable, relationField,
                                (sortField.IsNullOrEmpty() ? "" : string.Concat("ORDER BY ", sortField)));
                        parList.Add(new OracleParameter(":fieldvalue", fieldValue));
                        break;
                    case "MySql":
                        sql = string.Format("SELECT * FROM {0} WHERE {1}=@fieldvalue {2}", secondTable, relationField,
                                (sortField.IsNullOrEmpty() ? "" : string.Concat("ORDER BY ", sortField)));
                        parList.Add(new MySqlParameter("@fieldvalue", fieldValue));
                        break;
                }
                
                System.Data.IDbDataAdapter dataAdapter = bdbconn.GetDataAdapter(conn, dbconn.Type, sql, parList.ToArray());
                System.Data.DataSet ds = new System.Data.DataSet();
                dataAdapter.Fill(ds);
                if (dataAdapter.SelectCommand != null)
                {
                    dataAdapter.SelectCommand.Dispose();
                }
                System.Data.DataTable dt = ds.Tables[0];
                //jsonData.SetJsonType(LitJson.JsonType.Array);
                LitJson.JsonData formatJSON = null;
                if (!fieldFormat.IsNullOrEmpty())
                {
                    formatJSON = LitJson.JsonMapper.ToObject(fieldFormat);
                }
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    LitJson.JsonData data = new LitJson.JsonData();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string fieldname = secondTable.ToLower() + "_" + dt.Columns[i].ColumnName.ToLower();
                        string fieldformat = "";
                        if (formatJSON != null && formatJSON.IsArray)
                        {
                            foreach (LitJson.JsonData jd in formatJSON)
                            {
                                if (jd.ContainsKey("fieldname") && jd["fieldname"].ToString().Equals(fieldname, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    fieldformat = jd["format"].ToString();
                                    break;
                                }
                            }
                        }
                        string value = dr[dt.Columns[i].ColumnName].ToString();
                        if (!fieldformat.IsNullOrEmpty())
                        {
                            if (value.IsDecimal())
                            {
                                value = value.ToDecimal().ToString(fieldformat);
                            }
                            else if (value.IsDateTime())
                            {
                                value = value.ToDateTime().ToString(fieldformat);
                            }
                        }
                        data[fieldname] = value;
                    }
                    jsonData.Add(data);
                }
            }
            return jsonData;
        }

        /// <summary>
        /// 得到实例某个字段的值
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetFromFieldData(LitJson.JsonData jsonData, string table, string field)
        {
            string value = string.Empty;
            if (jsonData == null || table.IsNullOrEmpty() || field.IsNullOrEmpty())
            {
                return value;
            }
            var key = string.Concat(table, "_", field).ToLower();
            if (!jsonData.ContainsKey(key))
            {
                return value;
            }
            value = jsonData[key].ToString();
            return value;
        }

        /// <summary>
        ///  得到一个流程一个步骤的字段状态设置
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public string GetFieldStatus(string flowID, string stepID)
        { 
            Guid fid, sid;
            if(!flowID.IsGuid(out fid))
            {
                string programid = System.Web.HttpContext.Current.Request.QueryString["programid"];
                if (programid.IsGuid())
                {
                    return new ProgramBuilder().GetJsonString(programid.ToGuid());
                }
                else
                {
                    return "{}";
                }
            }
            var wfinstance = GetWorkFlowRunModel(fid);
            if (wfinstance == null)
            {
                return "{}";
            }
            if (!stepID.IsGuid(out sid))
            { 
                sid=wfinstance.FirstStepID;
            }
            var steps = wfinstance.Steps.Where(p => p.ID == sid);
            if (steps.Count() == 0)
            {
                return "{}";
            }
            var step = steps.First();
            var fieldStatus = step.FieldStatus;
            StringBuilder sb = new StringBuilder("{");
            int count = fieldStatus.Count();
            int i = 0;
            foreach (var fs in fieldStatus)
            {
                var fields = fs.Field.Split('.');
                if (fields.Length != 3)
                {
                    continue;
                }
                var fieldName = fields[1] + "_" + fields[2];
                sb.AppendFormat("\"{0}\":\"{1}\"", fieldName.ToLower(), string.Concat(fs.Status1, "_", fs.Check));
                if (i++ < count - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString() + "}";
        }

        /// <summary>
        /// 得到下级ID字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetAllChildsIDString(Guid id, bool isSelf = true)
        {
            return new Dictionary().GetAllChildsIDString(id, isSelf);
        }

        /// <summary>
        /// 得到类型选择项
        /// </summary>
        /// <returns></returns>
        public string GetTypeOptions(string value = "")
        {
            return new Dictionary().GetOptionsByCode("FlowTypes", Dictionary.OptionValueField.ID, value);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkFlow> GetByTypes(string typeString)
        {
            return dataWorkFlow.GetByTypes(typeString);
        }

        /// <summary>
        /// 得到一个类型下的流程ID
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public string GetFlowIDFromType(Guid typeID)
        {
            var flows = GetByTypes(GetAllChildsIDString(typeID));
            StringBuilder sb = new StringBuilder();
            foreach (var flow in flows)
            {
                sb.Append(flow.ID);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 得到流程类型选项
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetFlowTypeOptions(string type)
        {
            List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>() { 
                new System.Web.UI.WebControls.ListItem("常规流程","0"){Selected="0"==type},
                new System.Web.UI.WebControls.ListItem("自由流程","1"){Selected="1"==type}
            };
            return Utility.Tools.GetOptionsString(items.ToArray());
        }



        /// <summary>
        /// 得到一个自由流程步骤实体
        /// </summary>
        /// <returns></returns>
        public YJ.Data.Model.WorkFlowInstalledSub.Step GetFreeFlowStep(YJ.Data.Model.WorkFlowInstalled wfInstalled)
        {
            YJ.Data.Model.WorkFlowInstalledSub.Step step = new Data.Model.WorkFlowInstalledSub.Step();
            var firstSteps = wfInstalled.Steps.Where(p => p.ID == wfInstalled.FirstStepID);
            if (firstSteps.Count() == 0)
            {
                return step;
            }
            step = firstSteps.First();
            step.ID = Guid.NewGuid();
            step.Name = wfInstalled.Name + "-审核";
            step.Buttons = new List<YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button>() { 
                new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                    {
                        ID = "3B271F67-0433-4082-AD1A-8DF1B967B879",Note = "保存",Sort = 0
                    },
                    new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                    {
                        ID = "86B7FA6C-891F-4565-9309-81672D3BA80A",Note = "退回",Sort = 1
                    },
                    new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                    {
                        ID = "8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8",Note = "发送",Sort = 2
                    },
                    new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                    {
                        ID = "other_splitline",Note = "",Sort = 3
                    },
                    new YJ.Data.Model.WorkFlowInstalledSub.StepSet.Button()
                    {
                        ID = "954EFFA8-03B8-461A-AAA8-8727D090DCB9",Note = "结束流程",Sort = 4
                    }
            };

            return step;
        }

        /// <summary>
        /// 查询一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Data.Model.WorkFlow> GetPagerData(out string pager, string query = "", string userid = "", string typeid = "", string name = "", int pagesize = 15)
        {
            return dataWorkFlow.GetPagerData(out pager, query, userid, typeid, name, pagesize);
        }

        /// <summary>
        /// 查询一页数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <param name="name"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<Data.Model.WorkFlow> GetPagerData(out long count, int pageSize, int pageNumber, string userid = "", string typeid = "", string name = "", string order = "")
        {
            return dataWorkFlow.GetPagerData(out count, pageSize, pageNumber, userid, typeid, name, order);
        }

        /// <summary>
        /// 得到一个用户可以发起的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Data.Model.WorkFlowStart> GetUserStartFlows(Guid userID)
        {
            List<Data.Model.WorkFlowStart> list = GetAllStartFlows();
            return list.FindAll(p => p.StartUsers.IsNullOrEmpty() || p.StartUsers.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
        }
        /// <summary>
        /// 得到所有流程发起者实体(保存第一步的发起人，用于登录人员查看所有可以发起的流程)
        /// </summary>
        /// <returns></returns>
        public List<Data.Model.WorkFlowStart> GetAllStartFlows()
        {
            string cacheKey = Utility.Keys.CacheKeys.WorkFlowInstalled_.ToString() + "_StartFlows";
            var obj = Cache.IO.Opation.Get(cacheKey);
            if (obj != null && obj is List<Data.Model.WorkFlowStart>)
            {
                List<Data.Model.WorkFlowStart> flows = (List<Data.Model.WorkFlowStart>)obj;
                return flows;
            }
            else
            {
                var flowList = GetAllFromCache().FindAll(p => p.Status == 2);
                Organize borg = new Organize();
                Users busers = new Users();
                Dictionary bdict = new Dictionary();
                AppLibrary bapp = new AppLibrary();
                List<Data.Model.WorkFlowStart> flows = new List<Data.Model.WorkFlowStart>();
                foreach (var flow in flowList)
                {
                    var wfRunInstance = GetWorkFlowRunModel(flow.ID);
                    if (wfRunInstance == null)
                    {
                        continue;
                    }
                    var firstSteps = wfRunInstance.Steps.Where(p => p.ID == wfRunInstance.FirstStepID);
                    if (firstSteps.Count() == 0)
                    {
                        continue;
                    }
                    var firstStep = firstSteps.First();
                    //if (firstStep.Behavior.DefaultHandler.IsNullOrEmpty())
                    //{
                    //    continue;
                    //}
                    var app = bapp.GetByCode(flow.ID.ToString());
                    Data.Model.WorkFlowStart wf = new Data.Model.WorkFlowStart();
                    wf.ID = flow.ID;
                    wf.Name = flow.Name;
                    wf.StartUsers = borg.GetAllUsersIdString(firstStep.Behavior.DefaultHandler);//这里用来存放发起人员ID
                    wf.Type = bdict.GetTitle(flow.Type);//这里用来存放类别字符串
                    wf.InstallDate = flow.InstallDate.HasValue ? flow.InstallDate.Value.ToShortDateString() : "";
                    wf.InstallUserName = flow.InstallUserID.HasValue ? busers.GetName(flow.InstallUserID.Value) : "";
                    if (app != null)
                    {
                        wf.OpenMode = app.OpenMode;
                        wf.WindowWidth = app.Width.HasValue ? app.Width.Value : 0;
                        wf.WindowHeight = app.Height.HasValue ? app.Height.Value : 0;
                        wf.Params = app.Params;
                    }
                    flows.Add(wf);
                }
                Cache.IO.Opation.Set(cacheKey, flows);
                return flows;
            }
        }
        /// <summary>
        /// 清除流程发起人列表缓存
        /// </summary>
        public void ClearStartFlowsCache()
        {
            string cacheKey = Utility.Keys.CacheKeys.WorkFlowInstalled_.ToString() + "_StartFlows";
            Cache.IO.Opation.Remove(cacheKey);
        }

        /// <summary>
        /// 得到流程XML
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public string GetWorkFlowXml(Guid flowID)
        {
            var wf = Get(flowID);
            if (wf == null)
            {
                return "";
            }

            StringBuilder wfXml = new StringBuilder();
            wfXml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            wfXml.Append("<WorkFlow>");
            wfXml.Append("<ID>" + wf.ID.ToString() + "</ID>");
            wfXml.Append("<Name><![CDATA[" + wf.Name + "]]></Name>");
            wfXml.Append("<Type>" + wf.Type.ToString() + "</Type>");
            wfXml.Append("<Manager>" + wf.Manager + "</Manager>");
            wfXml.Append("<InstanceManager>" + wf.Manager + "</InstanceManager>");
            wfXml.Append("<CreateDate>" + wf.CreateDate.ToDateTimeStringS() + "</CreateDate>");
            wfXml.Append("<CreateUserID>" + wf.CreateUserID.ToString() + "</CreateUserID>");
            wfXml.Append("<DesignJSON><![CDATA[" + wf.DesignJSON + "]]></DesignJSON>");
            wfXml.Append("<InstallDate>" + (wf.InstallDate.HasValue ? wf.InstallDate.Value.ToDateTimeStringS() : "") + "</InstallDate>");
            wfXml.Append("<InstallUserID>" + wf.InstallUserID.ToString() + "</InstallUserID>");
            wfXml.Append("<RunJSON><![CDATA[" + wf.RunJSON + "]]></RunJSON>");
            wfXml.Append("<Status>" + wf.Status.ToString() + "</Status>");
            wfXml.Append("</WorkFlow>");

            return wfXml.ToString();
        }

        /// <summary>
        /// 导出流程
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="type">0 webform 1 mvc</param>
        public string Export(Guid flowID, int type = 0)
        {
            var wfrun = GetWorkFlowRunModel(flowID);
            if (wfrun == null)
            {
                return "";
            }
            string flowName = wfrun.Name;

            string path = Utility.Config.FilePath + "WorkFlowExportFiles\\" + Guid.NewGuid().ToString("N") + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<FileInfo> files = new List<FileInfo>();
            string flowXmlFile = path + "FlowDesigner_" + flowID.ToString() + ".xml";
            if (File.Exists(flowXmlFile))
            {
                File.Delete(flowXmlFile);
            }
            string flowXml = GetWorkFlowXml(flowID);
            FileStream fileStream = new FileStream(flowXmlFile, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
            streamWriter.Write(flowXml);
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            files.Add(new FileInfo(flowXmlFile));
            
            List<Data.Model.WorkFlowInstalledSub.StepSet.Form> forms = new List<Data.Model.WorkFlowInstalledSub.StepSet.Form>();
            foreach(var step in wfrun.Steps)
            {
                foreach (var form in step.Forms)
                {
                    if (forms.Find(p => p.ID == form.ID) != null)
                    {
                        continue;
                    }
                    forms.Add(form);
                }
            }

            WorkFlowForm WFF = new WorkFlowForm();
            AppLibrary bApp = new AppLibrary();
            foreach (var form in forms)
            {
                var applibary = bApp.Get(form.ID);
                if (applibary == null)
                {
                    continue;
                }
                var mform = WFF.Get(applibary.Code.ToGuid());
                if (mform == null)
                {
                    continue;
                }
                string flowFormXmlFile = path + "FlowFormDesigner_" + form.ID.ToString() + ".xml";
                if (File.Exists(flowFormXmlFile))
                {
                    File.Delete(flowFormXmlFile);
                }
                string formXml = WFF.GetWorkFlowFormXml(mform.ID, applibary.ID.ToString());
                FileStream fileStream1 = new FileStream(flowFormXmlFile, FileMode.Append);
                StreamWriter streamWriter1 = new StreamWriter(fileStream1, Encoding.UTF8);
                streamWriter1.Write(formXml);
                streamWriter1.Flush();
                streamWriter1.Close();
                fileStream1.Close();
                files.Add(new FileInfo(flowFormXmlFile));

                //如果表单已发布，则要加载已发布的文件
                if (mform.Status == 1)
                {
                    string formfile = type == 0 ?
                        System.Web.HttpContext.Current.Server.MapPath("/Platform/WorkFlowFormDesigner/Forms/") + "\\" + mform.ID.ToString() + ".aspx" :
                        System.Web.HttpContext.Current.Server.MapPath("/Views/WorkFlowFormDesigner/Forms/") + "\\" + mform.ID.ToString() + ".cshtml";
                    if (File.Exists(formfile))
                    {
                        files.Add(new FileInfo(formfile));
                    }
                }
            }
            string zipFile = path + flowName + "_" + flowID + ".zip";
            if (Utility.FileCompression.CompressFile(files, zipFile, 0, false))
            {
                return zipFile;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 导入流程
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="type">0 webform 1 mvc</param>
        /// <returns>返回1表示成功，其它为错误信息</returns>
        public string Import(string zipFile, int type = 0)
        {
            string path = Path.GetDirectoryName(zipFile) + "\\";
            string decompressMsg = Utility.FileCompression.Decompress(zipFile, path);
            if ("1" != decompressMsg)
            {
                Log.Add("解压文件出错-" + zipFile, decompressMsg);
                return "解压文件出错!";
            }
            var files = Directory.GetFiles(path);
            string flowName = string.Empty;
            #region 导流程
            var flowFile = files.Where(p => Path.GetFileName(p).StartsWith("FlowDesigner_"));
            if (flowFile.Count() > 0)
            {
                XDocument xDoc = XDocument.Load(flowFile.First());
                var xRoot = xDoc.Root;
                Guid ID = xRoot.Element("ID") != null ? xRoot.Element("ID").Value.ToGuid() : Guid.Empty;
                flowName = xRoot.Element("Name") != null ? xRoot.Element("Name").Value : "";
                Guid Type = xRoot.Element("Type") != null ? xRoot.Element("Type").Value.ToGuid() : Guid.Empty;
                string Manager = xRoot.Element("Manager") != null ? xRoot.Element("Manager").Value : "";
                string InstanceManager = xRoot.Element("InstanceManager") != null ? xRoot.Element("InstanceManager").Value : "";
                string CreateDate = xRoot.Element("CreateDate") != null ? xRoot.Element("CreateDate").Value : "";
                string CreateUserID = xRoot.Element("CreateUserID") != null ? xRoot.Element("CreateUserID").Value : "";
                string DesignJSON = xRoot.Element("DesignJSON") != null ? xRoot.Element("DesignJSON").Value : "";
                string InstallDate = xRoot.Element("InstallDate") != null ? xRoot.Element("InstallDate").Value : "";
                string InstallUserID = xRoot.Element("InstallUserID") != null ? xRoot.Element("InstallUserID").Value : "";
                string RunJSON = xRoot.Element("RunJSON") != null ? xRoot.Element("RunJSON").Value : "";
                int Status = xRoot.Element("Status") != null ? xRoot.Element("Status").Value.ToInt() : 1;

                if (!ID.IsEmptyGuid() || !flowName.IsNullOrEmpty())
                {
                    var wf = Get(ID);
                    bool isAdd = false;
                    if (wf == null)
                    {
                        wf = new Data.Model.WorkFlow();
                        isAdd = true;
                    }
                    wf.CreateDate = CreateDate.IsDateTime() ? CreateDate.ToDateTime() : Utility.DateTimeNew.Now;
                    wf.CreateUserID = CreateUserID.IsGuid() ? CreateUserID.ToGuid() : Users.CurrentUserID;
                    wf.DesignJSON = DesignJSON;
                    wf.ID = ID;
                    wf.InstallDate = InstallDate.IsDateTime() ? InstallDate.ToDateTime() : Utility.DateTimeNew.Now;
                    if (InstallUserID.IsGuid())
                    {
                        wf.InstallUserID = InstallUserID.ToGuid();
                    }
                    wf.InstanceManager = InstanceManager;
                    wf.Manager = Manager;
                    wf.Name = flowName;
                    wf.RunJSON = RunJSON;
                    wf.Status = Status;
                    if (isAdd)
                    {
                        Add(wf);
                    }
                    else
                    {
                        Update(wf);
                    }
                }
            }
            #endregion

            #region 导表单
            var formFiles = files.Where(p => Path.GetFileName(p).StartsWith("FlowFormDesigner_"));
            WorkFlowForm WFF = new WorkFlowForm();
            foreach (var xmlFile in formFiles)
            {
                string[] fileName = Path.GetFileNameWithoutExtension(xmlFile).Split('_');
                string id = string.Empty;
                if (fileName.Length > 1)
                {
                    id = fileName[1];
                }
                if (id.IsNullOrEmpty())
                {
                    continue;
                }
                if (WFF.AddFromXmlFile(xmlFile, type))
                {
                    string formFile = string.Empty;
                    string formFile1 = string.Empty;
                    if (0 == type)
                    {
                        formFile = Path.GetDirectoryName(xmlFile) + "\\" + id + ".aspx";
                        formFile1 = System.Web.HttpContext.Current.Server.MapPath("/Platform/WorkFlowFormDesigner/Forms/") + "\\" + id + ".aspx";
                    }
                    else
                    {
                        formFile = Path.GetDirectoryName(xmlFile) + "\\" + id + ".cshtml";
                        formFile1 = System.Web.HttpContext.Current.Server.MapPath("/Views/WorkFlowFormDesigner/Forms/") + "\\" + id + ".cshtml";
                    }
                    if (File.Exists(formFile))
                    {
                        File.Copy(formFile, formFile1, true);
                    }
                }
            }
            #endregion
            YJ.Platform.Log.Add("导入了流程-" + flowName, zipFile, Log.Types.流程相关);
            return "1";
        }

        /// <summary>
        /// 得到两个步骤之间的步骤
        /// </summary>
        /// <param name="runModel"></param>
        /// <param name="stepID1"></param>
        /// <param name="stepID2"></param>
        /// <returns></returns>
        public List<Data.Model.WorkFlowInstalledSub.Step> getBetweenSteps(Data.Model.WorkFlowInstalled runModel, Guid stepID1, Guid stepID2)
        {
            List<Data.Model.WorkFlowInstalledSub.Step> steps = new List<Data.Model.WorkFlowInstalledSub.Step>();
            addBetweenSteps(steps, runModel, stepID1, stepID2);
            return steps;
        }
        private void addBetweenSteps(List<Data.Model.WorkFlowInstalledSub.Step> steps, Data.Model.WorkFlowInstalled runModel, Guid stepID1, Guid stepID2)
        {
            var flowSteps = runModel.Steps.ToList();
            foreach (var line in runModel.Lines)
            {
                if (line.ToID == stepID2)
                {
                    var step = flowSteps.Find(p => p.ID == line.FromID);
                    if (step != null && step.ID != stepID1)
                    {
                        steps.Add(step);
                        addBetweenSteps(steps, runModel, stepID1, step.ID);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

    }
    
}
