using System.Configuration;
using System.Data.Entity;
using VIPAC.Common;
using VIPAC.Domain;
using VIPAC.Persistence.EntityFramework;

namespace VIPAC.DataBase.Tests
{
    public class DbContextDropCreateDatabaseAlways : DropCreateDatabaseAlways<DbContextBase>
    {
        #region Métodos

        protected override void Seed(DbContextBase context)
        {
            #region Variables

            string schema = ConfigurationManager.AppSettings["SchemaDB"] ?? "dbo";

            #endregion

            #region Agregar Registros

            AgregarRegistrosUsuario(context);

            #endregion
        }

        private static void AgregarRegistrosUsuario(DbContext context)
        {
            var usuario = new Usuario
            {
                Id = 1,
                UserName = "Admin",
                Password = Encriptador.Encriptar("1234"),
                Email = "admin@sigcomt.com",
            };

            context.Set<Usuario>().Add(usuario);
        }

        #endregion
    }
}