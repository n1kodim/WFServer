﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;

namespace WFServer.Core
{
    public static class SQL
    {
        //private static MySqlConnection sqlConnection = new MySqlConnection(constr);

        /*private static readonly string constr = new MySqlConnectionStringBuilder()
        {
            Server              = EmuConfig.Sql.Server,
            UserID              = EmuConfig.Sql.User,
            Password            = EmuConfig.Sql.Password,
            Database            = EmuConfig.Sql.Database,
            CharacterSet        = EmuConfig.Sql.CharacterSet,
            Port                = EmuConfig.Sql.Port,
            //Pooling             = true,
            ConvertZeroDateTime = true
        }.ToString();*/
        private static readonly string constr = $"server={Config.Sql.Server};user id={Config.Sql.User};password={Config.Sql.Password};database={Config.Sql.Database};characterset={Config.Sql.CharacterSet};port={Config.Sql.Port};convertzerodatetime=True";

        public static void Init()
        {
            try
            {
                CreateDatabase();

                using (MySqlConnection connection = new MySqlConnection(constr))
                {
                    connection.Open();
                    //GetConnection().GetAwaiter().GetResult();
                }
            }
            catch(Exception e)
            {
                Log.Error("[SQL] Failed to connect to '{0}' database", Config.Sql.Database);
                throw e;
            }
            finally
            {
                Log.Info("[SQL] Connected to '{0}' database", Config.Sql.Database);
            }
        }

        private static void CreateDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection($"server={Config.Sql.Server};user id={Config.Sql.User};password={Config.Sql.Password};characterset={Config.Sql.CharacterSet};port={Config.Sql.Port};convertzerodatetime=True"))
            {
                connection.Open();

                try
                {
                    MySqlScript script = new MySqlScript(connection, File.ReadAllText("wfserver.sql"));
                    //script.Delimiter = "$$";
                    script.Execute();
                }
                catch { }
            }
        }

        public static void Query(string command)
        {
            using (MySqlCommand cmd = new MySqlCommand(command))
            {
                Query(cmd);
            }
        }

        public static void Query(MySqlCommand command)
        {
            try
            {
                /*MySqlConnection connection = GetConnection().GetAwaiter().GetResult();
                lock (sqlConnection)
                {
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }*/
                using (MySqlConnection connection = new MySqlConnection(constr))
                {
                    connection.Open();

                    command.Connection = connection;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                //TODO
                Log.Error(e.ToString());
            }
        }

        public static DataTable QueryRead(string command)
        {
            
            using (MySqlCommand cmd = new MySqlCommand(command))
            {
                return QueryRead(cmd);
            }
        }

        public static DataTable QueryRead(MySqlCommand command)
        {
            try
            {
                /*DataTable result;
                MySqlConnection connection = GetConnection().GetAwaiter().GetResult();
                lock (sqlConnection)
                {
                    command.Connection = connection;
                    DbDataReader reader = command.ExecuteReader();

                    result = new DataTable();
                    result.Load(reader);

                    reader.Close();
                }
                return result;*/
                using (MySqlConnection connection = new MySqlConnection(constr))
                {
                    connection.Open();

                    command.Connection = connection;
                    MySqlDataReader reader = command.ExecuteReader();
                    DataTable result = new DataTable();
                    result.Load(reader);

                    return result;
                }
            }
            catch (Exception e)
            {
                //TODO
                Log.Error(e.ToString());
                throw e;
            }
        }

        /*private static async Task<MySqlConnection> GetConnection()
        {
            if (sqlConnection.State.HasFlag(System.Data.ConnectionState.Open))
                return sqlConnection;

            //MySqlConnection sqlConnection = new MySqlConnection(constr);
            sqlConnection = new MySqlConnection(constr);
            Task<MySqlConnection> ConnectTask = new Task<MySqlConnection>(delegate
            {
                sqlConnection.Open();
                return sqlConnection;
            });
            ConnectTask.Start();
            return await ConnectTask;
        }*/
    }
}
