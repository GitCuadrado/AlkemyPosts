using AlkemyPOSTS.Handler;
using AlkemyPOSTS.Models.Post.CrearPost;
using AlkemyPOSTS.Models.Post.EliminarPost;
using AlkemyPOSTS.Models.Post.FiltrarPosts;
using AlkemyPOSTS.Models.Post.ModificarPost;
using AlkemyPOSTS.Models.Post.ObtenerPost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlkemyPOSTS.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarPosts() {
            return new JsonResult()
            {
                Data = PostHandler.ListarPosts(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        [HttpGet]
        public JsonResult ObtenerPost(ObtenerPostImportModel model)
        {
            return new JsonResult()
            {
                Data = PostHandler.ObtenerPost(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        [HttpGet]
        public JsonResult EliminarPost(EliminarPostImportModel model)
        {
            return new JsonResult()
            {
                Data = PostHandler.EliminarPost(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        [HttpPost]
        public JsonResult ModificarPost(ModificarPostImportModel model)
        {
            return new JsonResult()
            {
                Data = PostHandler.ModificarPost(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        public JsonResult CrearPost(CrearPostImportModel model)
        {
            return new JsonResult()
            {
                Data = PostHandler.CrearPost(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        [HttpGet]
        public JsonResult FiltrarPosts(FiltrarPostsImportModel model)
        {
            return new JsonResult()
            {
                Data = PostHandler.FiltrarPosts(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        public JsonResult UploadImg()
        {
            HttpPostedFileBase file = Request.Files[0];

            try
            {
                
                string path = Server.MapPath("~/resources/imagesPosts/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(path + Path.GetFileName(file.FileName));
            }
            catch (Exception e)
            {

                return new JsonResult()
                {
                    Data = e,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
          

            return new JsonResult()
            {
                Data = file.FileName,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
    }
}