using AlkemyPOSTS.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.ListarPosts
{
    public class ListarPostsResponse : StatusResponse
    {
        public List<PostItemPreview> Posts { get; set; }

        public ListarPostsResponse() {

            Posts = new List<PostItemPreview>();
        }
    }
}