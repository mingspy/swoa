using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Data.Model
{
    public class Files
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 如果是目录包含目录完整路径
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 类型 0目录 1文件
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public decimal? Length { get; set; }

        /// <summary>
        /// 目录包含文件数量
        /// </summary>
        public int? FileLength { get; set; }
    }
}
