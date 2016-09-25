using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using VIPAC.Domain;
using VIPAC.Persistence;
using VIPAC.Persistence.EntityFramework;
using VIPAC.Repository;

namespace VIPAC.RepositoryTest
{
    [TestClass]
    public class UsuarioRepositoryTest
    {
        [TestCleanup]
        public void TestCleanUp()
        {
            var contextDB = new DbContextBase();
            contextDB.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, @"
                USE master                
                ALTER DATABASE VIPACBDTest SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                RESTORE DATABASE VIPACBDTest FROM DATABASE_SNAPSHOT = 'VIPACDBTest_Snap'
            ");
        }

        [TestMethod]
        public void CrearUsuario_DebeRetornar_Id_2()
        {
            #region Arrange

            var repository = ObjectFactory.GetInstance<IUsuarioRepository>();

            #endregion

            #region Act

            var user = new Usuario();

            try
            {
                var userSave = new Usuario
                {
                    UserName = "Admin2",
                    Password = "1234",
                    Email = "admin@sigcomt.com",
                    Estado = 1
                };

                var msgDispatcher = new MessageDispatcher();
                msgDispatcher.HandleCommand(() => user = repository.AddGet(userSave));
            }
            catch
            {
                user = null;
            }

            #endregion

            #region Assert

            Assert.IsNotNull(user);
            Assert.AreEqual(2, user.Id);

            #endregion
        }
    }
}
