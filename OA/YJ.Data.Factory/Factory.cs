using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YJ.Data.Factory
{
    public class Factory
    {

        #region MSSQL

        #region Areas
        public static Data.Interface.Areas.ICRConferenceSign GetCRConferenceSign()
        {
            return new Data.MSSQL.Areas.CRConferenceSign();
        }


        #endregion

        public static Data.Interface.IDBHelper GetDBHelper()
        {
            return new Data.MSSQL.DBHelper();
        }

        public static Data.Interface.IAppLibrary GetAppLibrary()
        {
            return new Data.MSSQL.AppLibrary();
        }

        public static Data.Interface.IDBConnection GetDBConnection()
        {
            return new Data.MSSQL.DBConnection();
        }

        public static Data.Interface.IDictionary GetDictionary()
        {
            return new Data.MSSQL.Dictionary();
        }

        public static Data.Interface.ILog GetLog()
        {
            return new Data.MSSQL.Log();
        }

        public static Data.Interface.IOrganize GetOrganize()
        {
            return new Data.MSSQL.Organize();
        }

        public static Data.Interface.IUsers GetUsers()
        {
            return new Data.MSSQL.Users();
        }
        /// <summary>
        /// 2020年10月29日 增加登录黑名单支持
        /// </summary>
        /// <returns></returns>
        public static Data.Interface.ILoginBlacklist GetLoginBlacklist()
        {
            return new Data.MSSQL.LoginBlacklist();
        }

        public static Data.Interface.IUsersRelation GetUsersRelation()
        {
            return new Data.MSSQL.UsersRelation();
        }


        public static Data.Interface.IWorkFlow GetWorkFlow()
        {
            return new Data.MSSQL.WorkFlow();
        }

        public static Data.Interface.IWorkFlowArchives GetWorkFlowArchives()
        {
            return new Data.MSSQL.WorkFlowArchives();
        }

        public static Data.Interface.IWorkFlowButtons GetWorkFlowButtons()
        {
            return new Data.MSSQL.WorkFlowButtons();
        }

        public static Data.Interface.IWorkFlowComment GetWorkFlowComment()
        {
            return new Data.MSSQL.WorkFlowComment();
        }

        public static Data.Interface.IWorkFlowData GetWorkFlowData()
        {
            return new Data.MSSQL.WorkFlowData();
        }

        public static Data.Interface.IWorkFlowDelegation GetWorkFlowDelegation()
        {
            return new Data.MSSQL.WorkFlowDelegation();
        }

        public static Data.Interface.IWorkFlowForm GetWorkFlowForm()
        {
            return new Data.MSSQL.WorkFlowForm();
        }

        public static Data.Interface.IWorkFlowTask GetWorkFlowTask()
        {
            return new Data.MSSQL.WorkFlowTask();
        }

        public static Data.Interface.IWorkGroup GetWorkGroup()
        {
            return new Data.MSSQL.WorkGroup();
        }

        public static Data.Interface.IShortMessage GetShortMessage()
        {
            return new Data.MSSQL.ShortMessage();
        }

        public static Data.Interface.ISMSLog GetSMSLog()
        {
            return new Data.MSSQL.SMSLog();
        }

        public static Data.Interface.IHastenLog GetHastenLog()
        {
            return new Data.MSSQL.HastenLog();
        }

        public static Data.Interface.IProgramBuilder GetProgramBuilder()
        {
            return new Data.MSSQL.ProgramBuilder();
        }

        public static Data.Interface.IProgramBuilderFields GetProgramBuilderFields()
        {
            return new Data.MSSQL.ProgramBuilderFields();
        }

        public static Data.Interface.IProgramBuilderQuerys GetProgramBuilderQuerys()
        {
            return new Data.MSSQL.ProgramBuilderQuerys();
        }

        public static Data.Interface.IProgramBuilderButtons GetProgramBuilderButtons()
        {
            return new Data.MSSQL.ProgramBuilderButtons();
        }

        public static Data.Interface.IProgramBuilderValidates GetProgramBuilderValidates()
        {
            return new Data.MSSQL.ProgramBuilderValidates();
        }

        public static Data.Interface.IProgramBuilderExport GetProgramBuilderExport()
        {
            return new Data.MSSQL.ProgramBuilderExport();
        }

        public static Data.Interface.IDocumentDirectory GetDocumentDirectory()
        {
            return new Data.MSSQL.DocumentDirectory();
        }

        public static Data.Interface.IDocuments GetDocuments()
        {
            return new Data.MSSQL.Documents();
        }

        public static Data.Interface.IDocumentsReadUsers GetDocumentsReadUsers()
        {
            return new Data.MSSQL.DocumentsReadUsers();
        }

        public static Data.Interface.IAppLibraryButtons GetAppLibraryButtons()
        {
            return new Data.MSSQL.AppLibraryButtons();
        }


        public static Data.Interface.IAppLibraryButtons1 GetAppLibraryButtons1()
        {
            return new Data.MSSQL.AppLibraryButtons1();
        }

        public static Data.Interface.IAppLibrarySubPages GetAppLibrarySubPages()
        {
            return new Data.MSSQL.AppLibrarySubPages();
        }

        public static Data.Interface.IMenu GetMenu()
        {
            return new Data.MSSQL.Menu();
        }

        public static Data.Interface.IMenuUser GetMenuUser()
        {
            return new Data.MSSQL.MenuUser();
        }

        public static Data.Interface.IWorkCalendar GetWorkCalendar()
        {
            return new Data.MSSQL.WorkCalendar();
        }

        public static Data.Interface.IHomeItems GetHomeItems()
        {
            return new Data.MSSQL.HomeItems();
        }

        public static Data.Interface.IUserShortcut GetUserShortcut()
        {
            return new Data.MSSQL.UserShortcut();
        }

        public static Data.Interface.IWorkTime GetWorkTime()
        {
            return new Data.MSSQL.WorkTime();
        }

        public static Data.Interface.IWeiXinMessage GetWeiXinMessage()
        {
            return new Data.MSSQL.WeiXinMessage();
        }
        
        #endregion

        #region ORACLE
        /*
        public static Data.Interface.IDBHelper GetDBHelper()
        {
            return new Data.ORACLE.DBHelper();
        }
        
        public static Data.Interface.IAppLibrary GetAppLibrary()
        {
            return new Data.ORACLE.AppLibrary();
        }

        public static Data.Interface.IDBConnection GetDBConnection()
        {
            return new Data.ORACLE.DBConnection();
        }

        public static Data.Interface.IDictionary GetDictionary()
        {
            return new Data.ORACLE.Dictionary();
        }

        public static Data.Interface.ILog GetLog()
        {
            return new Data.ORACLE.Log();
        }

        public static Data.Interface.IOrganize GetOrganize()
        {
            return new Data.ORACLE.Organize();
        }

        public static Data.Interface.IUsers GetUsers()
        {
            return new Data.ORACLE.Users();
        }

        public static Data.Interface.IUsersRelation GetUsersRelation()
        {
            return new Data.ORACLE.UsersRelation();
        }
        
        public static Data.Interface.IWorkFlow GetWorkFlow()
        {
            return new Data.ORACLE.WorkFlow();
        }

        public static Data.Interface.IWorkFlowArchives GetWorkFlowArchives()
        {
            return new Data.ORACLE.WorkFlowArchives();
        }

        public static Data.Interface.IWorkFlowButtons GetWorkFlowButtons()
        {
            return new Data.ORACLE.WorkFlowButtons();
        }

        public static Data.Interface.IWorkFlowComment GetWorkFlowComment()
        {
            return new Data.ORACLE.WorkFlowComment();
        }

        public static Data.Interface.IWorkFlowData GetWorkFlowData()
        {
            return new Data.ORACLE.WorkFlowData();
        }

        public static Data.Interface.IWorkFlowDelegation GetWorkFlowDelegation()
        {
            return new Data.ORACLE.WorkFlowDelegation();
        }

        public static Data.Interface.IWorkFlowForm GetWorkFlowForm()
        {
            return new Data.ORACLE.WorkFlowForm();
        }

        public static Data.Interface.IWorkFlowTask GetWorkFlowTask()
        {
            return new Data.ORACLE.WorkFlowTask();
        }

        public static Data.Interface.IWorkGroup GetWorkGroup()
        {
            return new Data.ORACLE.WorkGroup();
        }
         
         public static Data.Interface.IShortMessage GetShortMessage()
        {
            return new Data.ORACLE.ShortMessage();
        }

        public static Data.Interface.ISMSLog GetSMSLog()
        {
            return new Data.ORACLE.SMSLog();
        }

        public static Data.Interface.IHastenLog GetHastenLog()
        {
            return new Data.ORACLE.HastenLog();
        }

        public static Data.Interface.IProgramBuilder GetProgramBuilder()
        {
            return new Data.ORACLE.ProgramBuilder();
        }

        public static Data.Interface.IProgramBuilderFields GetProgramBuilderFields()
        {
            return new Data.ORACLE.ProgramBuilderFields();
        }

        public static Data.Interface.IProgramBuilderQuerys GetProgramBuilderQuerys()
        {
            return new Data.ORACLE.ProgramBuilderQuerys();
        }

        public static Data.Interface.IProgramBuilderButtons GetProgramBuilderButtons()
        {
            return new Data.ORACLE.ProgramBuilderButtons();
        }

        public static Data.Interface.IProgramBuilderValidates GetProgramBuilderValidates()
        {
            return new Data.ORACLE.ProgramBuilderValidates();
        }
        
        public static Data.Interface.IProgramBuilderExport GetProgramBuilderExport()
        {
            return new Data.ORACLE.ProgramBuilderExport();
        } 

        public static Data.Interface.IDocumentDirectory GetDocumentDirectory()
        {
            return new Data.ORACLE.DocumentDirectory();
        }

        public static Data.Interface.IDocuments GetDocuments()
        {
            return new Data.ORACLE.Documents();
        }

        public static Data.Interface.IDocumentsReadUsers GetDocumentsReadUsers()
        {
            return new Data.ORACLE.DocumentsReadUsers();
        }

        public static Data.Interface.IAppLibraryButtons GetAppLibraryButtons()
        {
            return new Data.ORACLE.AppLibraryButtons();
        }


        public static Data.Interface.IAppLibraryButtons1 GetAppLibraryButtons1()
        {
            return new Data.ORACLE.AppLibraryButtons1();
        }

        public static Data.Interface.IAppLibrarySubPages GetAppLibrarySubPages()
        {
            return new Data.ORACLE.AppLibrarySubPages();
        }

        public static Data.Interface.IMenu GetMenu()
        {
            return new Data.ORACLE.Menu();
        }

        public static Data.Interface.IMenuUser GetMenuUser()
        {
            return new Data.ORACLE.MenuUser();
        }

        public static Data.Interface.IWorkCalendar GetWorkCalendar()
        {
            return new Data.ORACLE.WorkCalendar();
        }

        public static Data.Interface.IHomeItems GetHomeItems()
        {
            return new Data.ORACLE.HomeItems();
        }

        public static Data.Interface.IUserShortcut GetUserShortcut()
        {
            return new Data.ORACLE.UserShortcut();
        }
          
        public static Data.Interface.IWorkTime GetWorkTime()
        {
            return new Data.ORACLE.WorkTime();
        }

        public static Data.Interface.IWeiXinMessage GetWeiXinMessage()
        {
            return new Data.ORACLE.WeiXinMessage();
        }
        */
        #endregion

        #region MySql
        /*
        public static Data.Interface.IDBHelper GetDBHelper()
        {
            return new Data.MySql.DBHelper();
        }

        public static Data.Interface.IAppLibrary GetAppLibrary()
        {
            return new Data.MySql.AppLibrary();
        }

        public static Data.Interface.IDBConnection GetDBConnection()
        {
            return new Data.MySql.DBConnection();
        }

        public static Data.Interface.IDictionary GetDictionary()
        {
            return new Data.MySql.Dictionary();
        }

        public static Data.Interface.ILog GetLog()
        {
            return new Data.MySql.Log();
        }

        public static Data.Interface.IOrganize GetOrganize()
        {
            return new Data.MySql.Organize();
        }

        public static Data.Interface.IUsers GetUsers()
        {
            return new Data.MySql.Users();
        }

        public static Data.Interface.IUsersRelation GetUsersRelation()
        {
            return new Data.MySql.UsersRelation();
        }
        
        public static Data.Interface.IWorkFlow GetWorkFlow()
        {
            return new Data.MySql.WorkFlow();
        }

        public static Data.Interface.IWorkFlowArchives GetWorkFlowArchives()
        {
            return new Data.MySql.WorkFlowArchives();
        }

        public static Data.Interface.IWorkFlowButtons GetWorkFlowButtons()
        {
            return new Data.MySql.WorkFlowButtons();
        }

        public static Data.Interface.IWorkFlowComment GetWorkFlowComment()
        {
            return new Data.MySql.WorkFlowComment();
        }

        public static Data.Interface.IWorkFlowData GetWorkFlowData()
        {
            return new Data.MySql.WorkFlowData();
        }

        public static Data.Interface.IWorkFlowDelegation GetWorkFlowDelegation()
        {
            return new Data.MySql.WorkFlowDelegation();
        }

        public static Data.Interface.IWorkFlowForm GetWorkFlowForm()
        {
            return new Data.MySql.WorkFlowForm();
        }

        public static Data.Interface.IWorkFlowTask GetWorkFlowTask()
        {
            return new Data.MySql.WorkFlowTask();
        }

        public static Data.Interface.IWorkGroup GetWorkGroup()
        {
            return new Data.MySql.WorkGroup();
        }
         
         public static Data.Interface.IShortMessage GetShortMessage()
        {
            return new Data.MySql.ShortMessage();
        }

        public static Data.Interface.ISMSLog GetSMSLog()
        {
            return new Data.ORACLE.SMSLog();
        }

        public static Data.Interface.IHastenLog GetHastenLog()
        {
            return new Data.MySql.HastenLog();
        }

        public static Data.Interface.IProgramBuilder GetProgramBuilder()
        {
            return new Data.MySql.ProgramBuilder();
        }

        public static Data.Interface.IProgramBuilderFields GetProgramBuilderFields()
        {
            return new Data.MySql.ProgramBuilderFields();
        }

        public static Data.Interface.IProgramBuilderQuerys GetProgramBuilderQuerys()
        {
            return new Data.MySql.ProgramBuilderQuerys();
        }

        public static Data.Interface.IProgramBuilderButtons GetProgramBuilderButtons()
        {
            return new Data.MySql.ProgramBuilderButtons();
        }

        public static Data.Interface.IProgramBuilderValidates GetProgramBuilderValidates()
        {
            return new Data.MySql.ProgramBuilderValidates();
        }
        
        public static Data.Interface.IProgramBuilderExport GetProgramBuilderExport()
        {
            return new Data.MySql.ProgramBuilderExport();
        } 

        public static Data.Interface.IDocumentDirectory GetDocumentDirectory()
        {
            return new Data.MySql.DocumentDirectory();
        }

        public static Data.Interface.IDocuments GetDocuments()
        {
            return new Data.MySql.Documents();
        }

        public static Data.Interface.IDocumentsReadUsers GetDocumentsReadUsers()
        {
            return new Data.MySql.DocumentsReadUsers();
        }

        public static Data.Interface.IAppLibraryButtons GetAppLibraryButtons()
        {
            return new Data.MySql.AppLibraryButtons();
        }


        public static Data.Interface.IAppLibraryButtons1 GetAppLibraryButtons1()
        {
            return new Data.MySql.AppLibraryButtons1();
        }

        public static Data.Interface.IAppLibrarySubPages GetAppLibrarySubPages()
        {
            return new Data.MySql.AppLibrarySubPages();
        }

        public static Data.Interface.IMenu GetMenu()
        {
            return new Data.MySql.Menu();
        }

        public static Data.Interface.IMenuUser GetMenuUser()
        {
            return new Data.MySql.MenuUser();
        }

        public static Data.Interface.IWorkCalendar GetWorkCalendar()
        {
            return new Data.MySql.WorkCalendar();
        }

        public static Data.Interface.IHomeItems GetHomeItems()
        {
            return new Data.MySql.HomeItems();
        }

        public static Data.Interface.IUserShortcut GetUserShortcut()
        {
            return new Data.MySql.UserShortcut();
        }
         
        public static Data.Interface.IWorkTime GetWorkTime()
        {
            return new Data.MySql.WorkTime();
        }
         
        public static Data.Interface.IWeiXinMessage GetWeiXinMessage()
        {
            return new Data.MySql.WeiXinMessage();
        }
        */
        #endregion
    }
}
