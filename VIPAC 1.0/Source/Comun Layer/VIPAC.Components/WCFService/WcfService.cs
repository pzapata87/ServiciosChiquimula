using System.Linq;

namespace VIPAC.Components.WCFService
{
    public class WcfService
    {
        public static T InvokeServiceWcf<T>(string uri, string method, object[] parameters)
        {
            var factory = new DynamicProxyFactory(uri);
            var proxy = factory.CreateProxy(factory.Endpoints.First());
            var resp = (T) proxy.ProxyType.GetMethod(method).Invoke(proxy.Proxy, parameters);

            return resp;
        }
    }
}
