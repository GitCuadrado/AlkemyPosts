using AlkemyPOSTS.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.ObtenerPost
{
    public class ObtenerPostResponse : StatusResponse
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
        public string CATEGORIA { get; set; }
        public string FECHA_CREACION { get; set; }
    }
}