using AlkemyPOSTS.Models.Common;
using AlkemyPOSTS.Models.Post.ListarPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.EliminarPost
{
    public class EliminarPostResponse : StatusResponse
    {
        public List<PostItemPreview> Posts { get; set; }
        public EliminarPostResponse() {
            Posts = new List<PostItemPreview>();
        }
    }
}