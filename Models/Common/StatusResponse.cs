using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Common
{
    public class StatusResponse
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
    }
}