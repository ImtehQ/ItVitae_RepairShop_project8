using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItVitae_RepairShop_project8.RepairShopData.Access
{ 
    public static class SqlDataAccess
{
    public static string GetConnectionString(string connectionName = "SQLRepairShop")
    {
        return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
    }

    public static List<T> LoadData<T>(string sql)
    {
        using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        {
            return cnn.Query<T>(sql).ToList();
        }
    }

    public static int SaveData<T>(string sql, T data)
    {
        using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        {
            return cnn.Execute(sql, data);
        }
    }

    public static int UpdateData<T>(string sql, T data)
    {
        using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        {
            return cnn.Execute(sql, data);
        }
    }

    public static int DeleteData<T>(string sql, T data)
    {
        using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        {
            return cnn.Execute(sql, data);
        }
    }


    public static int ExecuteDataString(string sql)
    {
        using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        {
            return cnn.Execute(sql);
        }
    }
}
}
