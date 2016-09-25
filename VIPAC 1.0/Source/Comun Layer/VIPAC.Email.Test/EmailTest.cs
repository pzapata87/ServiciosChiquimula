using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VIPAC.Email.Test
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void DeberiaEnviarEmail()
        {
            Email.From("testejemplito@gmail.com")
                 .To("pedroze2009@gmail.com")
                 .Subject("Nuevo Correo")
                 .Body("Felicitaciones!!!")
                 .Send();
        }
    }
}
