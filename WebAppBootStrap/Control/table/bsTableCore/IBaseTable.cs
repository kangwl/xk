using System.Collections.Generic;

namespace WebAppBS.Control.table.bsTableCore {
    public interface IBaseTable {
        /// <summary>
        /// 生成table的ID
        /// </summary>
        string BSTableID { get; set; }

        /// <summary>
        /// 格式 id:#,name:名称,price:价格
        /// 说明 字段和页面显示的文字组成，中间用逗号分开
        /// </summary>
        string BSTableFields { get; set; }

        /// <summary>
        /// 数据请求地址 bootstrapTable url
        /// 例如：url:/Content/bootstrap/table/data/data.aspx?act=testsimple
        /// </summary>
        string BSUrl { get; set; }

        /// <summary>
        /// table高度 可以为auto 或者 500 ...
        /// </summary>
        string BSTableHeight { get; set; }

        /// <summary>
        /// table 样式
        /// table table-hover table-no-bordered
        /// </summary>
        string BSTableClass { get; set; }

        /// <summary>
        /// 是否出现checkbox
        /// </summary>
        bool HasCheck { get; set; }
        /// <summary>
        /// 要排序的字段
        /// </summary>
        string SortFields { get; set; }
        /// <summary>
        /// 创建 bs table 模型
        /// </summary>
        string Create();
    }
}