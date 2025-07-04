using WFServer.Core;
using WFServer.Game;
using WFServer.Game.Clans;
using WFServer.Game.Shops;
using System;
using System.Threading;

namespace WFServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Log.Info("Starting...");

            SQL.Init();
            CommandHandler.Init();
            QueryBinder.Init();
            QueryCache.Init();
            GameData.Init();
            Shop.Init();
            Server.Init();
            Clan.GenerateClanList();
            Thread.Sleep(-1);
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Log.Error(ex.ToString());
            Environment.Exit(1);
        }
    }
}
