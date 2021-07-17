using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Text.RegularExpressions;


namespace WebMvc.PlatformApiControllers
{
    public class AmHcManageController : ApiController
    {
        /// <summary>
        /// 查询结余
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public HttpResponseMessage AmHcCompare(string  amHcId,int type=0)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            try
            {

                Guid aid;
                double num = 0;
                string sql = string.Empty;
                switch (type)
                {
                    case 0:
                        if (!amHcId.IsInt())
                        {
                            return new HttpResponseMessage()
                            {
                                Content = new StringContent("{\"status\":0,\"msg\":\"参数错误\"}", Encoding.UTF8, "application/json")
                            };
                        }
                        sql = "select (select SUM(Num) from AmHcinStorage  where CONVERT(nvarchar(50),id)='{0}')-isnull((select SUM(Num) from AmHcOutStorage  where AmHcInCode='{0}'),0)";
                        num = db.GetFieldValue(string.Format(sql, amHcId)).ToDouble();
                        break;
                    case 1:
                        if (!amHcId.IsInt())
                        {
                            return new HttpResponseMessage()
                            {
                                Content = new StringContent("{\"status\":0,\"msg\":\"参数错误\"}", Encoding.UTF8, "application/json")
                            };
                        }
                        sql = "select (select SUM(Num1) from AmHcinStorage  where CONVERT(nvarchar(50),id)='{0}')-isnull((select SUM(Num1) from AmHcOutStorage  where AmHcInCode='{0}'),0)";
                        num = db.GetFieldValue(string.Format(sql, amHcId)).ToDouble();
                        break;
                    case 2:
                        if (!amHcId.IsGuid(out aid))
                        {
                            return new HttpResponseMessage()
                            {
                                Content = new StringContent("{\"status\":0,\"msg\":\"参数错误\"}", Encoding.UTF8, "application/json")
                            };
                        }
                        sql = "select (select SUM(Num) from AmHcinStorage  where AmHcId='{0}')-isnull((select SUM(Num) from AmHcOutStorage  where AmHcId='{0}'),0)";
                        num = db.GetFieldValue(string.Format(sql, aid)).ToDouble();
                        break;
                    default:
                        break;
                }
                
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":"+num+",\"status\":1,\"msg\":\"成功\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\""+e.Message+"\"}", Encoding.UTF8, "application/json")
                };
            }
        }
        /// <summary>
        /// 查询耗材信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public HttpResponseMessage GetAmHcInfo(string amHcId)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            try
            {

                db.ExecuteScalar(string.Format("select b.FetchSpecs from AmHcInStorage a left join AmHcBioMedium b on a.amhcid=b.id where a.id={0}", amHcId));
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":" + db.ExecuteScalar(string.Format("select b.FetchSpecs from AmHcInStorage a left join AmHcBioMedium b on a.amhcid=b.id where a.id={0}", amHcId)) + ",\"status\":1,\"msg\":\"成功\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\"" + e.Message + "\"}", Encoding.UTF8, "application/json")
                };
            }
        }

        #region 办公用品、化学试剂、检验耗材、标准物质 出库数量Ajax自动验证 BY YSJ 2020-05-07。

        /// <summary>
        /// 查询办公用品结余
        /// </summary>
        /// <param name="supid">办公用品种类ID</param>
        /// <param name="outnum">申领数量</param>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public HttpResponseMessage OfficeSupCompare(string supid,string outnum) {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            int num = 0;
            string sql = string.Empty;
            Guid aid;
            if (supid.IsGuid(out aid))
            {
                if (Regex.IsMatch(outnum, @"^\+?[1-9][0-9]*$"))//判断是否是正整数
                {
                    try
                    {
                        sql = "SELECT 结余 FROM View_办公用品台账 WHERE ID = '{0}'";
                        num = db.GetFieldValue(string.Format(sql, aid)).ToInt32();

                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":" + num + ",\"status\":1,\"msg\":\"成功\"}", Encoding.UTF8, "application/json")
                        };
                    }

                    catch (Exception e)
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"status\":0,\"msg\":\"" + e.Message + "\"}", Encoding.UTF8, "application/json")
                        };
                    }
                }
                else {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"" + "领用数量格式不正确，请重新填写！" + "\"}", Encoding.UTF8, "application/json")
                    };
                }
               
            }

            else {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\"" + "办公用品ID参数错误，请选择要领取的办公用品" + "\"}", Encoding.UTF8, "application/json")
                };
            }
        }

        /// <summary>
        /// 查询化学试剂结余
        /// </summary>
        /// <param name="supid">化学试剂种类ID</param>
        /// <param name="outnum">申领数量</param>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public HttpResponseMessage ReagentCompare(string supid, string outnum)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            int num = 0;
            string sql = string.Empty;
            Guid aid;
            if (supid.IsGuid(out aid))
            {
                if (Regex.IsMatch(outnum, @"^\+?[1-9][0-9]*$"))//判断是否是正整数
                {
                    try
                    {
                        sql = "SELECT 结余 FROM View_化学试剂台账 WHERE ID = '{0}'";
                        num = db.GetFieldValue(string.Format(sql, aid)).ToInt32();

                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":" + num + ",\"status\":1,\"msg\":\"成功\"}", Encoding.UTF8, "application/json")
                        };
                    }

                    catch (Exception e)
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"status\":0,\"msg\":\"" + e.Message + "\"}", Encoding.UTF8, "application/json")
                        };
                    }
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"" + "领用数量格式不正确，请重新填写！" + "\"}", Encoding.UTF8, "application/json")
                    };
                }

            }

            else
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\"" + "化学试剂ID参数错误，请选择要领取的化学试剂" + "\"}", Encoding.UTF8, "application/json")
                };
            }
        }

        /// <summary>
        /// 查询标准物质结余
        /// </summary>
        /// <param name="supid">标准物质种类ID</param>
        /// <param name="outnum">申领数量</param>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public HttpResponseMessage RefMaterialCompare(string supid, string outnum)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            int num = 0;
            string sql = string.Empty;
            Guid aid;
            if (supid.IsGuid(out aid))
            {
                if (Regex.IsMatch(outnum, @"^\+?[1-9][0-9]*$"))//判断是否是正整数
                {
                    try
                    {
                        sql = "SELECT 结余 FROM View_标准物质台账 WHERE ID = '{0}'";
                        num = db.GetFieldValue(string.Format(sql, aid)).ToInt32();

                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":" + num + ",\"status\":1,\"msg\":\"成功\"}", Encoding.UTF8, "application/json")
                        };
                    }

                    catch (Exception e)
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"status\":0,\"msg\":\"" + e.Message + "\"}", Encoding.UTF8, "application/json")
                        };
                    }
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"" + "领用数量格式不正确，请重新填写！" + "\"}", Encoding.UTF8, "application/json")
                    };
                }

            }

            else
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\"" + "标准物质ID参数错误，请选择要领取的标准物质" + "\"}", Encoding.UTF8, "application/json")
                };
            }
        }


        /// <summary>
        /// 检验耗材用品结余
        /// </summary>
        /// <param name="supid">检验耗材种类ID</param>
        /// <param name="outnum">申领数量</param>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public HttpResponseMessage ConsumableCompare(string supid, string outnum)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            int num = 0;
            string sql = string.Empty;
            Guid aid;
            if (supid.IsGuid(out aid))
            {
                if (Regex.IsMatch(outnum, @"^\+?[1-9][0-9]*$"))//判断是否是正整数
                {
                    try
                    {
                        sql = "SELECT 结余 FROM View_检验耗材台账 WHERE ID = '{0}'";
                        num = db.GetFieldValue(string.Format(sql, aid)).ToInt32();

                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":" + num + ",\"status\":1,\"msg\":\"成功\"}", Encoding.UTF8, "application/json")
                        };
                    }

                    catch (Exception e)
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"status\":0,\"msg\":\"" + e.Message + "\"}", Encoding.UTF8, "application/json")
                        };
                    }
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"" + "领用数量格式不正确，请重新填写！" + "\"}", Encoding.UTF8, "application/json")
                    };
                }

            }

            else
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":0,\"msg\":\"" + "检验耗材ID参数错误，请选择要领取的检验耗材" + "\"}", Encoding.UTF8, "application/json")
                };
            }
        }

        #endregion

    }
}
