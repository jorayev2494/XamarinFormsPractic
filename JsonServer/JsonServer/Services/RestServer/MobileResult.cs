using System;
using System.Collections.Generic;
using System.Text;

namespace JsonServer.Services.RestServer
{
    public class MobileResult
    {
        public int Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
