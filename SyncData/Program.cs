//using Dotmim.Sync.MySql;
//using Dotmim.Sync.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dotmim.Sync; 
using System.Runtime.InteropServices;
using System.Diagnostics;
using Devart.Data;
using Devart.Data.SQLite;
//using SQLiteSync; 
namespace SyncData
{
     
    public class Program
    {
        static string connServer = ConfigurationManager.ConnectionStrings["dbISECServer"].ConnectionString;
        static string connLocal = ConfigurationManager.ConnectionStrings["dbIsecLocal"].ConnectionString;
        static  void Main(string[] args)
        {


            Sync();
            Console.WriteLine("bienn mijoo");

        }
        public static  void Sync()
        {
             

        }

    
        //public static async Task SyncAll()
        //{
        //    var serverProvider = new MySqlSyncProvider(ConfigurationManager.ConnectionStrings["dbISECServer"].ConnectionString);

        //    var cadenasqlite = ConfigurationManager.ConnectionStrings["dbIsecLocal"].ConnectionString;
        //    //string path = @"C:\isec\dbisecSync.sqlite.db";
        //    var clientProvider = new SqliteSyncProvider(cadenasqlite);
        //    //clientProvider.SupportsMultipleActiveResultSets = true;
        //    var agent = new SyncAgent(clientProvider, serverProvider,
        //        new string[] { "usuarios" });

        //    var syncContext = await agent.SynchronizeAsync();
        //    Console.WriteLine(syncContext);
        //}
    }
}
