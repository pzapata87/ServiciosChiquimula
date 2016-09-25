using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPAC.Domain;
using VIPAC.IoC.App_Start;
using VIPAC.Persistence;
using VIPAC.Persistence.EntityFramework;

namespace VIPAC.RepositoryTest.Core
{
    [TestClass]
    public class InitializeTest
    {
        [AssemblyInitialize]
        public static void GenerarSnapShotDB(TestContext testContext)
        {
            Database.SetInitializer(new ContextInitializer());
            Database.SetInitializer<DbContextBase>(null);
            PersistenceConfigurator.Configure("VIPAC", typeof(Usuario).Assembly, typeof(ConnectionFactory).Assembly);

            StructuremapMvc.Start();

            var contextDB = new DbContextBase();
            contextDB.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, @"
                IF DB_ID('VIPACDBTest_Snap') IS NOT NULL 
                    DROP DATABASE VIPACDBTest_Snap;

                CREATE DATABASE VIPACDBTest_Snap ON
                    ( NAME = VIPACBD, FILENAME = 'D:\Temp\VIPACTest_Snapshot.ss' )
                 AS SNAPSHOT OF VIPACBD;
            ");
        }

        [AssemblyCleanup]
        public static void DeleteSnapshot()
        {
            var contextDB = new DbContextBase();
            contextDB.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, @"
                USE master                
                DROP DATABASE VIPACDBTest_Snap;
            ");
        }
    }
}
