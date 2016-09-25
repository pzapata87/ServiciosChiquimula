using System.Configuration;

namespace VIPAC.Common
{
    public class ConfigurationSettings
    {
        #region Propiedades

        public static string IdiomaDefault
        {
            get
            {
                return ConfigurationManager.AppSettings[Constantes.IdiomaDefault];
            }
        }

        public static string UrlServicioMigracion
        {
            get
            {
                return ConfigurationManager.AppSettings[Constantes.UrlServicioMigracion];
            }
        }

        #endregion 
    }
}