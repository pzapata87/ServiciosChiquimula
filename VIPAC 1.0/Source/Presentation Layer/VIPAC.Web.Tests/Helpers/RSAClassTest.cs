using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VIPAC.Web.Tests.Helpers
{
    [TestClass]
    public class RSAClassTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var rsa = new RSACryptoServiceProvider();
            //Save the public key information to an RSAParameters structure.
            string respuesta = rsa.ToXmlString(true);
            string publica = rsa.ToXmlString(false);

            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(publica);
        }
    }
}