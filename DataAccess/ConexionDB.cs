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
    public static class ConexionDB
    {
        //get connectionString, AlkemyPostsDB default
        public static string GetConnectionString(string connectionName = "AlkemyPostsDB")
        {

            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
        //get object
        public static T Get<T>(string sql, object model) {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.QuerySingle<T>(sql, model);

            }
        }
        //Add,edit,delete
        public static int Alternate<T>(string sql, T model) {
            using (IDbConnection cnn = new SqlConnection(PostDataAccess.GetConnectionString()))
            {
                return cnn.Execute(sql,model);

            }
        }
        //get list
        public static List<T> GetList<T>(string sql) {
            using (IDbConnection cnn = new SqlConnection(PostDataAccess.GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();

            }
        }
    }
}