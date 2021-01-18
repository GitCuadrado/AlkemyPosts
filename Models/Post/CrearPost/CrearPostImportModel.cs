using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.CrearPost
{
    public class CrearPostImportModel
    {
        public string CONTENT { get; set; }
        public string TITLE { get; set; }
        public string CATEGORIA { get; set; }
        public string FECHA_CREACION { get; set; } 
        public string IMAGEN { get; set; }
        public int ESTADO { get; set; }
    }
}