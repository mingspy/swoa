using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class ProgramBuilderExport
    {
        private YJ.Data.Interface.IProgramBuilderExport dataProgramBuilderExport;
        public ProgramBuilderExport()
        {
            this.dataProgramBuilderExport = YJ.Data.Factory.Factory.GetProgramBuilderExport();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ProgramBuilderExport model)
        {
            return dataProgramBuilderExport.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ProgramBuilderExport model)
        {
            return dataProgramBuilderExport.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderExport> GetAll()
        {
            return dataProgramBuilderExport.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ProgramBuilderExport Get(Guid id)
        {
            return dataProgramBuilderExport.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataProgramBuilderExport.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataProgramBuilderExport.GetCount();
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderExport> GetAll(Guid programID)
        {
            return dataProgramBuilderExport.GetAll(programID);
        }


        /// <summary>
        /// 得到单元格格式选项
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetDataTypeOptions(string value)
        {
            Dictionary<int, string> dicts = new Dictionary<int, string>();
            dicts.Add(0, "常规");
            dicts.Add(1, "文本");
            dicts.Add(2, "数字");
            dicts.Add(3, "日期时间");
            StringBuilder sb = new StringBuilder();
            foreach (var dict in dicts)
            {
                sb.Append("<option value=\"" + dicts.Keys.ToString() + "\"" + (dicts.Keys.ToString() == value ? " selected=\"selected\"" : "") + ">" + dict.Value + "</option>");
            }
            return sb.ToString();
        }
    }
}
