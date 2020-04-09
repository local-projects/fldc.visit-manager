using System;
using System.Collections.Generic;
using System.Text;

namespace DBManager.Models
{
    public class ResponseResult
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public ResponseResult()
        {
            Status = 200;
            Message = "OK";
        }
    }
}
