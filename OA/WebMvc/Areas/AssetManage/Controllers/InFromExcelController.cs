using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJ.Platform;

namespace WebMvc.Areas.AssetManage.Controllers
{
    public class InFromExcelController : Controller
    {
        //
        // GET: /AssetManage/InFromExcel/
        [MyAttribute(CheckApp = false)]
        public ActionResult Index()
        {
            return Index(null);
        }

        [HttpPost]
        [MyAttribute(CheckApp = false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection coll)
        {
            string programid = Request.QueryString["programid"];
            int type = (Request.QueryString["type"] ?? "0").ToInt32();
            var program = new YJ.Platform.ProgramBuilder().Get(programid.ToGuid());
            YJ.Platform.DBConnection dbConn = new YJ.Platform.DBConnection();
            ViewBag.TableOptions = dbConn.GetAllTableOptions(program.DBConnID, "");
            ViewBag.ConnID = program.DBConnID.ToString();
            ViewBag.TableName = program.TableName;
            ViewBag.NumberFiled = program.InDataNumberFiledName;
            if (coll != null)
            {
                HttpPostedFileBase file = Request.Files["excel"];
                if (file != null && !file.FileName.IsNullOrEmpty())
                {
                    string numberFiled = Request.Form["NumberFiled"];
                    string tableName = Request.Form["TableName"];
                    string filePath = Server.MapPath(Url.Content("~/Content/UploadFiles/ProgramInExcel/" + programid + "/" + Guid.NewGuid().ToString()));
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filePath1 = System.IO.Path.Combine(filePath, file.FileName);
                    file.SaveAs(filePath1);
                    string msg;
                    int count = 0;
                    switch (type)
                    {
                        //自定义字段导入
                        case 0:
                            count =InDataFormExcel1(programid.ToGuid(), tableName, filePath1, out msg, numberFiled);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                        case 2:
                            count = new Areas.AssetManage.Data.Business.AmSample().InExcelData(filePath1, out msg);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                    }


                }
                else
                {
                    ViewBag.script = "alert('要导入的Excel文件为空!');";
                }
            }
            return View();
        }
        /// <summary>
        /// 从EXCEL文件导入数据（指定列）
        /// </summary>
        /// <param name="programID"></param>
        /// <param name="file">EXCEL文件路径</param>
        /// <param name="table">表名</param>
        /// <param name="numberFiled">标识字段，每次导入的标识相同</param>
        /// <returns></returns>
        public int InDataFormExcel1(Guid programID, string table, string file, out string msg, string numberFiled = "")
        {
            int count = 0;
            msg = "";
            if (table.IsNullOrEmpty())
            {
                msg = "没有选择表";
                return count;
            }

            DBConnection dbConn = new DBConnection();
            var program =new  ProgramBuilder().Get(programID);
            if (program == null)
            {
                msg = "未找到应用程序设计";
                return count;
            }

            YJ.Data.Model.DBConnection conn = dbConn.Get(program.DBConnID);
            if (conn == null)
            {
                msg = "未找到相应的数据库连接";
                return count;
            }

            var pb = new ProgramBuilder().Get(programID);
            if (pb == null)
            {
                msg = "未找到应用程序";
                return count;
            }
            try
            {
                DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(file);
                if (dt.Rows.Count == 0)
                {
                    msg = "未发现要导入的数据";
                    return count;
                }
                var tableFileds = dbConn.GetFieldsBySQL(program.DBConnID, "select * from " + table + " where 1=0");
                DataTable dt1 = new DataTable(table);
                string number = YJ.Utility.DateTimeNew.Now.ToString("yyyyMMddHHmmssfffff");
                var filedList = pb.InDataFiledName.Split(',').ToList();
                foreach (var filed in filedList)
                {
                    if (filed.IsNullOrEmpty() || tableFileds.Find(p => p.Equals(filed.Split('-')[1], StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        continue;
                    }
                    dt1.Columns.Add(filed.Split('-')[1]);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dt1.NewRow();
                    foreach (DataColumn col in dt1.Columns)
                    {
                        var dtFiled = filedList.Find(p => p.Split('-')[1].Equals(col.ColumnName));
                        if (dtFiled == null)
                        {
                            continue;
                        }
                        dr1[col.ColumnName] = dr[dtFiled.Split('-')[0].ToInt32()];
                    }
                    dt1.Rows.Add(dr1);
                }
                count = dbConn.DataTableToDB(conn, dt1);
                YJ.Platform.Log.Add("通过应用程序导入了数据-表(" + table + ")标识(" + number + ")", file, Log.Types.其它分类);
                return count;
            }
            catch (Exception err)
            {
                msg = err.Message;
                return count;
            }
        }
    }
}
