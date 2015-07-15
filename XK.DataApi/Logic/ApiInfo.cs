namespace XK.DataApi.Logic {
    /// <summary>
    /// 处理结果
    /// </summary>
    public class ApiInfo {

        public ApiInfo() { }
        public ApiInfo(int _code,string _message) {
            code = _code;
            msg = _message;
        }

        private int _code = -1;
        private string _message = "系统出错";

        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string msg
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
