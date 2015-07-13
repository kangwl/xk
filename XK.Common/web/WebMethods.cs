using System.Web.UI.WebControls;

namespace XK.Common.web
{
    /// <summary>
    /// 绑定扩展
    /// </summary>
    public static class WebMethods {
        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dropDownList">DropDownList实例</param>
        /// <param name="tData">数据</param>
        /// <param name="textField">文本字段</param>
        /// <param name="valField">值字段</param>
        public static void BindEXT<T>(this DropDownList dropDownList, T tData, string textField, string valField) {
            dropDownList.DataTextField = textField;
            dropDownList.DataValueField = valField;

            dropDownList.DataSource = tData;
            dropDownList.DataBind();
        }
        /// <summary>
        /// 绑定repeater控件数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repeater"></param>
        /// <param name="tData"></param>
        public static void BindEXT<T>(this Repeater repeater, T tData) {
            if (!tData.Equals(default(T))) {
                repeater.DataSource = tData;
                repeater.DataBind();
            }
        }
    }
}