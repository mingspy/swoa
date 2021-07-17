using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class ProgramBuilderButtons
    {
        private YJ.Data.Interface.IProgramBuilderButtons dataProgramBuilderButtons;
        public ProgramBuilderButtons()
        {
            this.dataProgramBuilderButtons = YJ.Data.Factory.Factory.GetProgramBuilderButtons();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ProgramBuilderButtons model)
        {
            return dataProgramBuilderButtons.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ProgramBuilderButtons model)
        {
            return dataProgramBuilderButtons.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll()
        {
            return dataProgramBuilderButtons.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ProgramBuilderButtons Get(Guid id)
        {
            return dataProgramBuilderButtons.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataProgramBuilderButtons.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataProgramBuilderButtons.GetCount();
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll(Guid programID)
        {
            return dataProgramBuilderButtons.GetAll(programID);
        }
    }
}
