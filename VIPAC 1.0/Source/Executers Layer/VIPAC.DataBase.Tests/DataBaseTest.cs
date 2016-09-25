using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPAC.Domain;
using VIPAC.Persistence;
using VIPAC.Persistence.EntityFramework;

namespace VIPAC.DataBase.Tests
{
    [TestClass]
    public class DataBaseTest
    {
        [TestMethod]
        public void CreateDataBaseDesarrollo()
        {
            Database.SetInitializer(new DbContextDropCreateDatabaseAlways());
            PersistenceConfigurator.Configure("VIPAC", typeof(Usuario).Assembly, typeof(ConnectionFactory).Assembly);
            var target = new DbContextBase();
            target.Database.Initialize(true);
        }
    }
}
