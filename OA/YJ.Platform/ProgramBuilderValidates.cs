using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class ProgramBuilderValidates
    {
        private YJ.Data.Interface.IProgramBuilderValidates dataProgramBuilderValidates;
        public ProgramBuilderValidates()
        {
            this.dataProgramBuilderValidates = YJ.Data.Factory.Factory.GetProgramBuilderValidates();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ProgramBuilderValidates model)
        {
            return dataProgramBuilderValidates.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ProgramBuilderValidates model)
        {
            return dataProgramBuilderValidates.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll()
        {
            return dataProgramBuilderValidates.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ProgramBuilderValidates Get(Guid id)
        {
            return dataProgramBuilderValidates.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataProgramBuilderValidates.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataProgramBuilderValidates.GetCount();
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll(Guid programID)
        {
            return dataProgramBuilderValidates.GetAll(programID);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByProgramID(Guid id)
        {
            return dataProgramBuilderValidates.DeleteByProgramID(id);
        }

        
    }
}
