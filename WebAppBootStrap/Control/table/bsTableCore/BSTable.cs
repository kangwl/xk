using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebAppBS.Control.table.bsTableCore {
    /// <summary>
    ///  bs table 模型
    /// </summary>
    public class BSTable {

        private const string tableTPL = "<table id=\"{0}\"  data-show-columns=\"false\">{1}</table>";
        private static string theadTPL = "<thead><tr>{0}</tr></thead>";//普通的
        /// <summary>
        /// 带checkbox的
        /// </summary>
        private const string theadCheckTPL = "<thead><tr><th data-field=\"state\" data-checkbox=\"true\"></th>{0}</tr></thead>";
        private static string thead_th_tpl = "<th data-field=\"{0}\">{1}</th>";

        private static string thead_th_sort_tpl = "  <th data-field=\"{0}\" data-sortable=\"true\">{1}</th>";

        /// <summary>
        /// 创建 bs table 模型
        /// </summary>
        /// <param name="bsTableID">table的ID</param>
        /// <param name="bsTableFields"></param>
        /// <param name="hasCheck">是否出现checkbox</param>
        /// <returns></returns>
        private static string Create(string bsTableID, List<BSTableField> bsTableFields, bool hasCheck) {
            if (string.IsNullOrEmpty(bsTableID)) {
                bsTableID = Guid.NewGuid().ToString("n");
            }
            if (bsTableFields.Count < 1) return "";

            StringBuilder thBuilder = new StringBuilder();
            foreach (BSTableField field in bsTableFields) {
                string th = "";
                if (field.SortEnable) {
                    th = string.Format(thead_th_sort_tpl, field.DataField, field.DataText);
                }
                else {
                    th = string.Format(thead_th_tpl, field.DataField, field.DataText);
                }
                thBuilder.Append(th);
            }
            string thead = string.Format(hasCheck ? theadCheckTPL : theadTPL, thBuilder.ToString());
            string table = string.Format(tableTPL, bsTableID, thead);

            return table;
        }

        /// <summary>
        /// 创建 bs table 模型
        /// </summary>
        /// <param name="bsTableID"></param>
        /// <param name="bsTableFields"> id:#,name:名称,price:价格</param>
        /// <param name="hasCheck">是否出现checkbox</param>
        /// <param name="sortFieldList"></param>
        /// <returns></returns>
        private static string Create(string bsTableID, string bsTableFields,bool hasCheck,List<string> sortFieldList) {
            List<BSTableField> fieldList = new List<BSTableField>();
            var arrFieldW = bsTableFields.Split(',');
            foreach (string s in arrFieldW) {
                string sTrim = s.Trim();
                if (!string.IsNullOrEmpty(sTrim)) {
                    var arrFieldO = sTrim.Split(':');
                    if (arrFieldO.Length > 1) {
                        BSTableField oneField = new BSTableField {
                            DataField = arrFieldO[0].Trim(),
                            DataText = arrFieldO[1].Trim(),
                            SortEnable = sortFieldList.Contains(arrFieldO[0].Trim())
                        };
                        fieldList.Add(oneField);
                    }
                }
            }
            return Create(bsTableID, fieldList,hasCheck);
        }

        /// <summary>
        /// 创建 bs table 模型
        /// </summary>
        /// <param name="bsTableID">table的ID</param>
        /// <param name="bsTableFields">由数据库字段和显示名组成</param>
        /// <param name="hasCheck">是否出现checkbox</param>
        /// <param name="sortFilds">需要排序的字段</param>
        /// <returns></returns>
        public static string Create(string bsTableID, string bsTableFields, bool hasCheck, string sortFilds) {
            string table = "";
            List<string> sortFieldList = new List<string>();
            if (!string.IsNullOrEmpty(sortFilds)) {
                var sortArr = sortFilds.Split(',');
                sortFieldList.AddRange(sortArr);
            }
 

            table = Create(bsTableID, bsTableFields,hasCheck, sortFieldList);

            return table;
        }

 

    }
}