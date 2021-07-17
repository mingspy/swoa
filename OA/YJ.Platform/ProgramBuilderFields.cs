using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class ProgramBuilderFields
    {
        private YJ.Data.Interface.IProgramBuilderFields dataProgramBuilderFields;
        public ProgramBuilderFields()
        {
            this.dataProgramBuilderFields = Data.Factory.Factory.GetProgramBuilderFields();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ProgramBuilderFields model)
        {
            return dataProgramBuilderFields.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ProgramBuilderFields model)
        {
            return dataProgramBuilderFields.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderFields> GetAll()
        {
            return dataProgramBuilderFields.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ProgramBuilderFields Get(Guid id)
        {
            return dataProgramBuilderFields.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataProgramBuilderFields.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataProgramBuilderFields.GetCount();
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderFields> GetAll(Guid programID)
        {
            return dataProgramBuilderFields.GetAll(programID);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByProgramID(Guid id)
        {
            return dataProgramBuilderFields.DeleteByProgramID(id);
        }
    }
}
