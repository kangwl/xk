using System;

namespace XK.DataApi.ApiInfo {
    public class ApiException {

        public ApiException() { }
        public ApiException(int code,string message) {
            Code = code;
            Msg = message;
        }

        private int _code = -1;
        private string _message = "系统出错";

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Msg
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
