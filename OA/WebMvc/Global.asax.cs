using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using YJ.Utility;

namespace WebMvc
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        Thread server_thread;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

      

            //每一分钟检查需要自动提交的任务
            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.Elapsed += Common.Tools.ExpiredAutoSubmit;
            timer.AutoReset = true;
            timer.Start();
            //每天检查车辆年检时间是否到期

            System.Timers.Timer timers = new System.Timers.Timer(43100000);
            timers.Elapsed += Common.Tools.ExpiredSelectCarYrar;
            timers.AutoReset = true;
            timers.Start();
            //检查当前时间是否为23:59从微信端数据库抓取考勤数据
            System.Timers.Timer timerss = new System.Timers.Timer(2100000);
            timerss.Elapsed += Common.Tools.ExpiredSelectWorkAttendance;
            timerss.AutoReset = true;
            timerss.Start();

            //每10分钟导入样品检验结果
            System.Timers.Timer timerI = new System.Timers.Timer(600000);
            timerI.Elapsed += Common.Tools.ImportSampleResult;
            timerI.AutoReset = true;
            timerI.Start();

 

            //BundleTable.EnableOptimizations = true;

            //server_thread = new System.Threading.Thread(
            //    new System.Threading.ThreadStart(new TemperatureServer().start_server)
            //    );//开辟一个新线程
            //server_thread.Start();
        }
        protected void Application_end()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

    
            //每一分钟检查需要自动提交的任务
            System.Timers.Timer timer = new System.Timers.Timer(600000);
            timer.Elapsed += Common.Tools.ExpiredAutoSubmit;
            timer.AutoReset = true;
            timer.Start();
            //每天检查车辆年检时间是否到期


            System.Timers.Timer timers = new System.Timers.Timer(43100000);
            timers.Elapsed += Common.Tools.ExpiredSelectCarYrar;
            timers.AutoReset = true;
            timers.Start();
            //检查当前时间是否为23:59从微信端数据库抓取考勤数据
            System.Timers.Timer timerss = new System.Timers.Timer(2100000);
            timerss.Elapsed += Common.Tools.ExpiredSelectWorkAttendance;
            timerss.AutoReset = true;
            timerss.Start();

            //每10分钟导入样品检验结果
            System.Timers.Timer timerI = new System.Timers.Timer(600000);
            timerI.Elapsed += Common.Tools.ImportSampleResult;
            timerI.AutoReset = true;
            timerI.Start();

            //server_thread.Abort();
        }

    }
}