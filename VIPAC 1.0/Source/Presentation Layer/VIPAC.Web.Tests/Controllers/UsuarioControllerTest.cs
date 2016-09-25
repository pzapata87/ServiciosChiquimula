using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VIPAC.Web.Tests.Controllers
{
    [TestClass]
    public class UsuarioControllerTest
    {
        /* Este test prueba que se cambia correctamente la contraseña e invia un mensaje al correo,         
         * se debe revisar directamente en el correo, para verificar la llegada del correo.
         */

        [TestMethod]
        public void ResetPassword_Usuario_Admin_DebeRetornar_JsonResponse_Success_True()
        {
            //#region Arrange

            //var mockUsuarioBL = new Mock<IUsuarioBL>();
            //var mockContactoBL = new Mock<IContactoBL>();
            //var mockRolBL = new Mock<IRolBL>();

            //mockUsuarioBL.Setup(p => p.Get(It.IsAny<Expression<Func<Usuario, bool>>>()))
            //    .Returns(new Usuario {Id = 1, UserName = "Admin", Email = "pedroze2009@gmail.com"});

            //var userController = new UsuarioController(mockUsuarioBL.Object, mockContactoBL.Object, mockRolBL.Object);

            //#endregion

            //#region Act

            //var resultado = userController.ObtenerUsuario(1);

            //#endregion

            //#region Assert

            //Assert.AreEqual(true, resultado.Success);

            //#endregion
        }
    }
}