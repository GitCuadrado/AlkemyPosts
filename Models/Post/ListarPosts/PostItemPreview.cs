using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.ListarPosts
{
    public class PostItemPreview
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string CATEGORIA { get; set; }
        public string FECHA_CREACION { get; set; }
    }
}