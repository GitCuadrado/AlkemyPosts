using AlkemyPOSTS.Models.Common;
using AlkemyPOSTS.Models.Post.ListarPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.ModificarPost
{
    public class ModificarPostResponse : StatusResponse
    {
        public List<PostItemPreview> Posts { get; set; }
        public ModificarPostResponse() {
            Posts = new List<PostItemPreview>();
        }
    }
}