using AlkemyPOSTS.Models.Post.ListarPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Models.Post.FiltrarPosts
{
    public class FiltrarPostsResponse
    {
        public List<PostItemPreview> Posts { get; set; }
    }
}