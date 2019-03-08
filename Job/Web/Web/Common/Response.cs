using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Common
{
    public class Response<T>
    {
        public int code { get; set; }
        public T data { get; set; }
        public string msg { get; set; }
    }
}
