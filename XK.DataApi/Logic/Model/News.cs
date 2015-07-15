using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.DataApi.Logic.Model {
    public class News  {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string AddDateTime { get; set; }
    }
}
