namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IWorkFlowTask
    {
        int Add(WorkFlowTask model);
        int Completed(Guid taskID, [Optional, DefaultParameterValue("")] string comment, [Optional, DefaultParameterValue(false)] bool isSign, [Optional, DefaultParameterValue(2)] int status, [Optional, DefaultParameterValue("")] string note, [Optional, DefaultParameterValue("")] string files);
        int Delete(Guid id);
        int Delete(Guid flowID, Guid groupID);
        int DeleteTempTasks(Guid flowID, Guid stepID, Guid groupID, Guid prevStepID);
        WorkFlowTask Get(Guid id);
        List<WorkFlowTask> GetAll();
        List<WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID);
        long GetCount();
        List<WorkFlowTask> GetExpiredAutoSubmitTasks();
        Guid GetFirstSnderID(Guid flowID, Guid groupID);
        List<WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status);
        DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status);
        DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status, [Optional, DefaultParameterValue("")] string order);
        WorkFlowTask GetLastTask(Guid flowID, Guid groupID);
        List<WorkFlowTask> GetNextTaskList(Guid taskID);
        List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID);
        List<WorkFlowTask> GetPrevTaskList(Guid taskID);
        List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID);
        List<WorkFlowTask> GetTaskList(Guid taskID, [Optional, DefaultParameterValue(true)] bool isStepID);
        List<WorkFlowTask> GetTaskList(Guid flowID, Guid groupID);
        List<WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID);
        List<WorkFlowTask> GetTasks(Guid userID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type);
        List<WorkFlowTask> GetTasks(Guid userID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type, [Optional, DefaultParameterValue("")] string order);
        int GetTaskStatus(Guid taskID);
        List<WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID);
        bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID);
        bool HasTasks(Guid flowID);
        int Update(WorkFlowTask model);
        int UpdateNextTaskStatus(Guid taskID, int status);
        void UpdateOpenTime(Guid id, DateTime openTime, [Optional, DefaultParameterValue(false)] bool isStatus);
        int UpdateTempTasks(Guid flowID, Guid stepID, Guid groupID, DateTime? completedTime, DateTime receiveTime);
    }
}

