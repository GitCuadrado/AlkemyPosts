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

            string sql = @"SELECT ID,TITLE,CATEGORIA,FECHA_CREACION FROM POST";
            List<PostItemPreview> response = new List<PostItemPreview>();

            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                response = cnn.Query<PostItemPreview>(sql).ToList();

            }
            return response;
        }
        public static ObtenerPostResponse ObtenerPost(ObtenerPostImportModel model)
        {
            string sql = @"SELECT ID,TITLE,CONTENT,CATEGORIA,FECHA_CREACION FROM POST WHERE ID = @ID";
            ObtenerPostResponse response = new ObtenerPostResponse();

            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                response = cnn.QuerySingle<ObtenerPostResponse>(sql,model);

            }
            return response;
        }
        public static int EliminarPost(EliminarPostImportModel model)
        {
            string sql = @"DELETE FROM POST WHERE ID = @ID";
            int response = 0;
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                response = cnn.Execute(sql,model);

            }
            return response;
        }
        public static int ModificarPost(ModificarPostImportModel model)
        {
            string sql = @"UPDATE POST SET TITLE = @TITLE,CONTENT = @CONTENT, CATEGORIA = @CATEGORIA WHERE ID = @ID";
            int response = 0;
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                response = cnn.Execute(sql,model);

            }
            return response;
        }
        public static int CrearPost(CrearPostImportModel model)
        {
            string sql = @"INSERT INTO POST(TITLE,CONTENT,CATEGORIA,FECHA_CREACION) VALUES(@TITLE,@CONTENT,@CATEGORIA,@FECHA_CREACION)";
            int response = 0;
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                response = cnn.Execute(sql,model);

            }
            return response;
        }

    }


}