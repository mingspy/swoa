using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace YJ.Data.Model
{
    [Serializable]
    public class ProgramBuilder
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [DisplayName("应用名称")]
        public string Name { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [DisplayName("分类")]
        public Guid Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [DisplayName("发布时间")]
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public Guid CreateUser { get; set; }

        /// <summary>
        /// 查询SQL
        /// </summary>
        [DisplayName("查询SQL")]
        public string SQL { get; set; }

        /// <summary>
        /// 是否显示新增按钮
        /// </summary>
        [DisplayName("是否显示新增按钮")]
        public int IsAdd { get; set; }

        /// <summary>
        /// 数据连接ID
        /// </summary>
        [DisplayName("数据连接ID")]
        public Guid DBConnID { get; set; }

        /// <summary>
        /// 状态 0设计中 1已发布 2已作废
        /// </summary>
        [DisplayName("状态 0设计中 1已发布 2已作废")]
        public int Status { get; set; }

        /// <summary>
        /// 表单ID
        /// </summary>
        [DisplayName("表单ID")]
        public string FormID { get; set; }

        /// <summary>
        /// 编辑模式 0，当前窗口 1，弹出层
        /// </summary>
        [DisplayName("编辑模式 0，当前窗口 1，弹出层")]
        public int? EditModel { get; set; }

        /// <summary>
        /// 弹出层宽度
        /// </summary>
        [DisplayName("弹出层宽度")]
        public string Width { get; set; }

        /// <summary>
        /// 弹出层高度
        /// </summary>
        [DisplayName("弹出层高度")]
        public string Height { get; set; }

        /// <summary>
        /// 按钮显示位置
        /// </summary>
        [DisplayName("按钮显示位置")]
        public int? ButtonLocation { get; set; }

        /// <summary>
        /// 是否分页
        /// </summary>
        [DisplayName("是否分页")]
        public int? IsPager { get; set; }

        /// <summary>
        /// 页面脚本
        /// </summary>
        [DisplayName("页面脚本")]
        public string ClientScript { get; set; }

        /// <summary>
        /// 导出EXCEL模板
        /// </summary>
        [DisplayName("导出EXCEL模板")]
        public string ExportTemplate { get; set; }

        /// <summary>
        /// 导出Excel表头
        /// </summary>
        [DisplayName("导出Excel表头")]
        public string ExportHeaderText { get; set; }

        /// <summary>
        /// 导出EXCLE的文件名
        /// </summary>
        [DisplayName("导出EXCLE的文件名")]
        public string ExportFileName { get; set; }

        /// <summary>
        /// 列表样式
        /// </summary>
        [DisplayName("导出EXCLE的文件名")]
        public string TableStyle { get; set; }

        /// <summary>
        /// 列表表头HTML
        /// </summary>
        [DisplayName("列表表头HTML")]
        public string TableHead { get; set; }

        /// <summary>
        /// 程序设计对应的表(用于数据导入时)
        /// </summary>
        [DisplayName("程序设计对应的表(用于数据导入时)")]
        public string TableName { get; set; }

        /// <summary>
        /// 导入EXCEL数据时的标识字段，每次导入生成一个编号区分
        /// </summary>
        [DisplayName("导入EXCEL数据时的标识字段，每次导入生成一个编号区分")]
        public string InDataNumberFiledName { get; set; }
        /// <summary>
        /// 导入EXCEL数据时的字段列表
        /// </summary>
        [DisplayName("导入EXCEL数据时的字段列表")]
        public string InDataFiledName { get; set; }
        
    }

    /// <summary>
    /// 程序缓存类
    /// </summary>
    [Serializable]
    public class ProgramBuilderCache
    {
        public ProgramBuilder Program { get; set; }

        public List<ProgramBuilderFields> Fields { get; set; }

        public List<ProgramBuilderQuerys> Querys { get; set; }

        public List<ProgramBuilderButtons> Buttons { get; set; }

        public List<ProgramBuilderValidates> Validates { get; set; }

        public List<ProgramBuilderExport> Export { get; set; }
    }
}
