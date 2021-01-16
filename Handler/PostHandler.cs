using AlkemyPOSTS.DataAccess;
using AlkemyPOSTS.Models.Post.CrearPost;
using AlkemyPOSTS.Models.Post.EliminarPost;
using AlkemyPOSTS.Models.Post.FiltrarPosts;
using AlkemyPOSTS.Models.Post.ListarPosts;
using AlkemyPOSTS.Models.Post.ModificarPost;
using AlkemyPOSTS.Models.Post.ObtenerPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.Handler
{
    public static class PostHandler
    {
        public static ListarPostsResponse ListarPosts() {
            ListarPostsResponse response = new ListarPostsResponse();
            response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();

            return response;

        }
        public static ObtenerPostResponse ObtenerPost(ObtenerPostImportModel model) {
            return PostDataAccess.ObtenerPost(model); 
        }

        public static EliminarPostResponse EliminarPost(EliminarPostImportModel model) {
            EliminarPostResponse response = new EliminarPostResponse();
            if (PostDataAccess.EliminarPost(model) == 1)
            {
                response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();
            }
            else
            {
                //error
            }

            return response; 
        }
        public static ModificarPostResponse ModificarPost(ModificarPostImportModel model) {
            ModificarPostResponse response = new ModificarPostResponse();
            if (PostDataAccess.ModificarPost(model) == 1)
            {
                response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();

            }
            else
            {
                //error
            }

            return response; 
        }
        public static CrearPostResponse CrearPost(CrearPostImportModel model) {
            CrearPostResponse response = new CrearPostResponse();
            model.FECHA_CREACION = DateTime.Now.ToString();
            if (PostDataAccess.CrearPost(model) == 1)
            {
                response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();
            }
            else
            {
                //error
            }

            return response; 
        }
        public static FiltrarPostsResponse FiltrarPosts(FiltrarPostsImportModel model) {
            FiltrarPostsResponse response = new FiltrarPostsResponse();
            if (model.palabra != null)
            {
                response.Posts = PostDataAccess.GetPostsPreview().Where(x => x.TITLE.ToLower().Contains(model.palabra.ToLower())).OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();

            }
            else
            {
                response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();
            }
        
            return response; 
        }
    }
}