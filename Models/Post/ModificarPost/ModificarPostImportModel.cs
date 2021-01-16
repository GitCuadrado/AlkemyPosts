using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.ModificarPost
{
    public class ModificarPostImportModel
    {
        public int ID { get; set; }
        public string CONTENT { get; set; }
        public string TITLE { get; set; }
        public string CATEGORIA { get; set; }
    }
}