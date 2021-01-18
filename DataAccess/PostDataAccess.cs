using AlkemyPOSTS.Models.Post.CrearPost;
using AlkemyPOSTS.Models.Post.EliminarPost;
using AlkemyPOSTS.Models.Post.ListarPosts;
using AlkemyPOSTS.Models.Post.ModificarPost;
using AlkemyPOSTS.Models.Post.ObtenerPost;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AlkemyPOSTS.DataAccess
{
    public static class PostDataAccess
    {
        public static string GetConnectionString(string connectionName = "AlkemyPostsDB")
        {

            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<PostItemPreview> GetPostsPreview()
        {

            string sql = @"SELECT ID,TITLE,CATEGORIA,FECHA_CREACION FROM POST WHERE ESTADO = 1";
            return ConexionDB.GetList<PostItemPreview>(sql);
           
        }
        public static ObtenerPostResponse ObtenerPost(ObtenerPostImportModel model)
        {
            string sql = @"SELECT ID,TITLE,CONTENT,CATEGORIA,FECHA_CREACION,IMAGEN FROM POST WHERE ID = @ID AND ESTADO = 1";
            return ConexionDB.Get<ObtenerPostResponse>(sql, model);
        }
        public static int EliminarPost(EliminarPostImportModel model)
        {
            //string sql = @"DELETE FROM POST WHERE ID = @ID";
            string sql = @"UPDATE POST SET ESTADO = 0 WHERE ID = @ID";
            return ConexionDB.Alternate(sql, model);
        }
        public static int ModificarPost(ModificarPostImportModel model)
        {
            string sql = @"UPDATE POST SET TITLE = @TITLE,CONTENT = @CONTENT, CATEGORIA = @CATEGORIA,IMAGEN = @IMAGEN WHERE ID = @ID";
            return ConexionDB.Alternate(sql, model);
        }
        public static int CrearPost(CrearPostImportModel model)
        {
            string sql = @"INSERT INTO POST(TITLE,CONTENT,CATEGORIA,FECHA_CREACION,IMAGEN,ESTADO) VALUES(@TITLE,@CONTENT,@CATEGORIA,@FECHA_CREACION,@IMAGEN,@ESTADO)";
            return ConexionDB.Alternate(sql, model);
        }

    }


}