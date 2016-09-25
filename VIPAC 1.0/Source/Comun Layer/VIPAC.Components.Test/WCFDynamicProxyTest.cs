using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPAC.Components.WCFService;

namespace VIPAC.Components.Test
{
    [TestClass]
    public class WcfDynamicProxyTest
    {
        [TestMethod]
        public void ServicioWCF_VerificarPagosFile_DebeRetornar_True()
        {
            #region Arrange

            const string urlWebService = "http://190.81.53.181:2082/MigracionVipac.svc";

            #endregion

            #region Act

            var resp = WcfService.InvokeServiceWcf<bool>(urlWebService, "VerificarPagosFile",
                new object[] {"2009", "0000008045", "RC"});

            #endregion

            #region Assert

            Assert.IsTrue(resp);

            #endregion              
        }
    }
}
