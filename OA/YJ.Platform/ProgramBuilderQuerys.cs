using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class ProgramBuilderQuerys
    {
        private YJ.Data.Interface.IProgramBuilderQuerys dataProgramBuilderQuerys;
        public ProgramBuilderQuerys()
        {
            this.dataProgramBuilderQuerys = YJ.Data.Factory.Factory.GetProgramBuilderQuerys();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            return dataProgramBuilderQuerys.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            return dataProgramBuilderQuerys.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll()
        {
            return dataProgramBuilderQuerys.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ProgramBuilderQuerys Get(Guid id)
        {
            return dataProgramBuilderQuerys.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataProgramBuilderQuerys.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataProgramBuilderQuerys.GetCount();
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll(Guid programID)
        {
            return dataProgramBuilderQuerys.GetAll(programID);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByProgramID(Guid id)
        {
            return dataProgramBuilderQuerys.DeleteByProgramID(id);
        }

       
        /// <summary>
        /// 得到查询条件显示html
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        public string GetQueryShowHtml(List<Data.Model.ProgramBuilderQuerys> querys)
        {
            StringBuilder html = new StringBuilder();
            foreach (var query in querys)
            {
                html.Append("<span style=\"margin-right:8px;\">");
                html.AppendFormat("<span>{0}：</span>", query.ShowTitle);
                switch (query.InputType)
                { 
                    case 1://文本
                        html.AppendFormat("<span><input type=\"text\" class=\"mytext\" id=\"{0}\" name=\"{0}\" style=\"{1}\" value=\"{2}\"/></span>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            query.Value
                            );
                        break;
                    case 2://日期
                        html.AppendFormat("<span><input type=\"text\" class=\"mycalendar\" id=\"{0}\" name=\"{0}\" style=\"{1}\" value=\"{2}\"/></span>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            query.Value.IsDateTime() ? query.Value.ToDateString() : ""
                            );
                        break;
                    case 3://日期范围
                        string[] val = query.Value.Split(',');
                        html.Append("<span>");
                        html.AppendFormat("<input type=\"text\" class=\"mycalendar\" id=\"{0}_start\" name=\"{0}_start\" style=\"{1}\" value=\"{2}\"/>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            val[0].IsDateTime() ? val[0].ToDateString() : ""
                            );
                        html.Append("<span style=\"margin:0 3px;\">至</span>");
                        html.AppendFormat("<input type=\"text\" class=\"mycalendar\" id=\"{0}_end\" name=\"{0}_end\" style=\"{1}\" value=\"{2}\"/>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            val[1].IsDateTime() ? val[1].ToDateString() : ""
                            );
                        html.Append("</span>");
                        break;
                    case 4://日期时间
                        html.AppendFormat("<span><input type=\"text\" class=\"mycalendar\" istime=\"1\" id=\"{0}\" name=\"{0}\" style=\"{1}\" value=\"{2}\"/></span>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            query.Value.IsDateTime() ? query.Value.ToDateTimeString() : ""
                            );
                        break;
                    case 5://日期时间范围
                        string[] val1 = query.Value.Split(',');
                        html.Append("<span>");
                        html.AppendFormat("<input type=\"text\" class=\"mycalendar\" istime=\"1\" id=\"{0}_start\" name=\"{0}_start\" style=\"{1}\" value=\"{2}\"/>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            val1[0].IsDateTime() ? val1[0].ToDateTimeString() : ""
                            );
                        html.Append("<span style=\"margin:0 3px;\">至</span>");
                        html.AppendFormat("<input type=\"text\" class=\"mycalendar\" istime=\"1\" id=\"{0}_end\" name=\"{0}_end\" style=\"{1}\" value=\"{2}\"/>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            val1[1].IsDateTime() ? val1[1].ToDateTimeString() : ""
                            );
                        html.Append("</span>");
                        break;
                    case 6://下拉选择
                        html.AppendFormat("<span><select class=\"myselect\" id=\"{0}\" name=\"{0}\" style=\"max-width:300px;{1}\">{2}</select></span>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            GetSelectOptions(query.DataSource.HasValue ? query.DataSource.Value : 0, query.DataSourceString, query.Value, query.DataLinkID)
                            );
                        break;
                    case 7://组织机构
                        html.AppendFormat("<span><input type=\"text\" class=\"mymember\" id=\"{0}\" name=\"{0}\" style=\"{1}\"{2} value=\"{3}\"/></span>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            GetOrganizeAttrString(query.DataSourceString),
                            query.Value
                            );
                        break;
                    case 8://数据字典
                        html.AppendFormat("<span><input type=\"text\" class=\"mydict\" id=\"{0}\" name=\"{0}\" style=\"{1}\"{2} value=\"{3}\"/></span>",
                            query.ControlName,
                            query.Width.IsNullOrEmpty() ? "" : query.Width,
                            "rootid=\"" + query.DataSourceString + "\"",
                            query.Value
                            );
                        break;
                }
                html.Append("</span>");
            }
            return html.ToString();
        }

        private string GetSelectOptions(int dataSource, string soureString, string value, string linkID = "")
        {
            if (dataSource == 1)
            {
                return new WorkFlowForm().GetOptionsFromString(soureString, value);
            }
            else if (dataSource == 2)
            {
                return "<option value=\"\"></option>" + 
                    new Dictionary().GetOptionsByID(soureString.ToGuid(), Dictionary.OptionValueField.ID, value);
            }
            else if (dataSource == 3)
            {
                return "<option value=\"\"></option>" + 
                    new WorkFlowForm().GetOptionsFromSql(linkID, soureString, value);
            }
            return "";
        }

        private string GetOrganizeAttrString(string attrString)
        {
            if (attrString.IsNullOrEmpty()) return "";
            string[] dstring = attrString.Split('|');
            StringBuilder sb = new StringBuilder();
            
            if (dstring.Length > 0)
            {
                sb.AppendFormat(" rootid=\"{0}\"", dstring[0]);
            }
            if (dstring.Length > 1)
            {
                sb.AppendFormat(" unit=\"{0}\"", "1" == dstring[1] ? "1" : "0");
            }
            if (dstring.Length > 2)
            {
                sb.AppendFormat(" dept=\"{0}\"", "1" == dstring[2] ? "1" : "0");
            }
            if (dstring.Length > 3)
            {
                sb.AppendFormat(" station=\"{0}\"", "1" == dstring[3] ? "1" : "0");
            }
            if (dstring.Length > 4)
            {
                sb.AppendFormat(" group=\"{0}\"", "1" == dstring[4] ? "1" : "0");
            }
            if (dstring.Length > 5)
            {
                sb.AppendFormat(" role=\"{0}\"", "1" == dstring[5] ? "1" : "0");
            }
            if (dstring.Length > 6)
            {
                sb.AppendFormat(" user=\"{0}\"", "1" == dstring[6] ? "1" : "0");
            }
            if (dstring.Length > 7)
            {
                sb.AppendFormat(" more=\"{0}\"", "1" == dstring[7] ? "1" : "0");
            }
            return sb.ToString();
        }

        public string GetQueryButtonHtml(Data.Model.ProgramBuilder pb)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<input type=\"submit\" name=\"searchbutton\" value=\" 查 询 \" class=\"mybutton\"/>");
            //if (1 == pb.IsAdd && pb.FormID.IsGuid())
            //{
            //    html.Append("<input style=\"margin-left:8px;\" onclick=\"adddata();\" type=\"button\" name=\"addbutton\" value=\" 新 增 \" class=\"mybutton\"/>");
            //}
            return html.ToString();
        }

        public string GetButtonHtml(List<Data.Model.ProgramBuilderButtons> buttons)
        {
            StringBuilder html = new StringBuilder();
            StringBuilder scripts = new StringBuilder();
            foreach(var button in buttons)
            {
                string funName = "fun_" + Guid.NewGuid().ToString("N") + "()";
                scripts.Append("function ");
                scripts.Append(funName);
                scripts.Append("\r\n{\r\n");
                scripts.Append(button.ClientScript.FilterWildcard(YJ.Platform.Users.CurrentUserID.ToString()));
                scripts.Append("\r\n}\r\n");
                html.AppendFormat("<input style=\"margin-left:8px;\" onclick=\"{0};\" type=\"button\" name=\"addbutton\" value=\"{1}\" class=\"mybutton\"/>",
                    funName, button.ButtonName
                    );
            }
            html.Append("<script type=\"text/javascript\">");
            html.Append(scripts);
            html.Append("</script>");
            return html.ToString();
        }
    }
}
