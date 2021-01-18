using AlkemyPOSTS.DataAccess;
using AlkemyPOSTS.Models.Post.CrearPost;
using AlkemyPOSTS.Models.Post.EliminarPost;
using AlkemyPOSTS.Models.Post.FiltrarPosts;
using AlkemyPOSTS.Models.Post.ListarPosts;
using AlkemyPOSTS.Models.Post.ModificarPost;
using AlkemyPOSTS.Models.Post.ObtenerPost;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                response.code = 004;
                response.desc = "Exito";
                response.name = "Post eliminado con exito";
                response.status = true;
            }
            else
            {
                response.code = 005;
                response.desc = "Error";
                response.name = "Error al realizar la eliminacion";
                response.status = false;
            }

            return response; 
        }
        public static ModificarPostResponse ModificarPost(ModificarPostImportModel model) {
            ModificarPostResponse response = new ModificarPostResponse();

            bool existenciaImagen = false;
            if (model.IMAGEN != null)
            {
                string path = HttpContext.Current.Server.MapPath("~/resources/imagesPosts/" + model.IMAGEN);
                if (System.IO.File.Exists(path))
                {
                    existenciaImagen = true;
                }
                else
                {
                    response.code = 001;
                    response.desc = "La imagen solicitada no fue encontrada en el servidor, por favor intente subirla nuevamente";
                    response.name = "Imagen no encontrada";
                    response.status = false;
                }
            }

            if (model.IMAGEN == null || existenciaImagen)
            {
                if (PostDataAccess.ModificarPost(model) == 1)
                {
                    response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();
                    response.code = 006;
                    response.desc = "Exito";
                    response.name = "Post modificado con exito";
                    response.status = true;
                }
                else
                {
                    response.code = 005;
                    response.desc = "Error";
                    response.name = "Error al modificar el post";
                    response.status = true;
                }

            }
            return response; 
        }
        public static CrearPostResponse CrearPost(CrearPostImportModel model) {
            CrearPostResponse response = new CrearPostResponse();

            try
            {
                //string path = "~/resources/imagesPosts/" + model.IMAGEN;
                bool existenciaImagen = false;
                if (model.IMAGEN != null)
                {
                    string path = HttpContext.Current.Server.MapPath("~/resources/imagesPosts/" + model.IMAGEN);
                    if (System.IO.File.Exists(path))
                    {
                        existenciaImagen = true;
                    }
                    else
                    {
                        response.code = 001;
                        response.desc = "La imagen solicitada no fue encontrada en el servidor, por favor intente subirla nuevamente";
                        response.name = "Imagen no encontrada";
                        response.status = false;
                    }
                }

                if (model.IMAGEN == null ||  existenciaImagen)
                {
                    model.FECHA_CREACION = DateTime.Now.ToString();
                    model.ESTADO = 1;
                    if (PostDataAccess.CrearPost(model) == 1)
                    {
                        response.Posts = PostDataAccess.GetPostsPreview().OrderByDescending(x => Convert.ToDateTime(x.FECHA_CREACION)).ToList();
                        response.code = 003;
                        response.desc = "Exito";
                        response.name = "Post creado con exito";
                        response.status = true;
                    }
                    else
                    {
                        response.code = 002;
                        response.desc = "Hubo un error al crear el post, por favor reinicie la pagina y vuelva a intentarlo";
                        response.name = "Error al crear el post";
                        response.status = false;
                    }
                }
            }
            catch (Exception e)
            {
               
                response.desc = e.Message;
                response.name = "error";
                response.status = false;

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