using AlkemyPOSTS.Models.Common;
using AlkemyPOSTS.Models.Post.ListarPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.CrearPost
{
    public class CrearPostResponse : StatusResponse
    {
        public List<PostItemPreview> Posts { get; set; }
        public CrearPostResponse() {
            Posts = new List<PostItemPreview>();
        }
    }
}