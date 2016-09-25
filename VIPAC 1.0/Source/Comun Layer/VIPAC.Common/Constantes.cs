namespace VIPAC.Common
{
    public static class Constantes
    {
        #region Key Settings

        public const string IdiomaDefault = "IdiomaDefault";
        public const string ExpresionCronMonitoreoPagos = "ExpresionCronMonitoreoPagos";
        public const string UrlServicioMigracion = "UrlServicioMigracion";

        #endregion

        #region Email

        public const string PathEmailTemplateNuevoUsuario = "~/EmailTemplates/NuevoUsuario.html";
        public const string PathEmailTemplateResetPassword = "~/EmailTemplates/ResetPassword.html";
        public const string PathEmailTemplateAvisoPago = "~/EmailTemplates/AvisoPago.html";
        public const string PathEmailTemplateAvisoAnulacion = "~/EmailTemplates/AvisoAnulacion.html";
        public const string PathEmailNuevoFile = "~/EmailTemplates/AvisoNuevoFile.html";

        #endregion

        #region Azure

        public const string QueueName = "QueueName";
        public const string ConnectionStorage = "ConnectionStorage";
        public const string ServicioWebMigracionOracleToAzure = "VIPAC.ServicioWebOracleToAzure";
        public const string MetodoVerificarPagosFile = "VIPAC.VerificarPagosFile";

        #endregion

        #region Quartz

        public const string QuartzGrupoConcepto = "Concepto";
        public const string QuartzGrupoPago = "Pago";

        #endregion

        #region Datos Generales

        public const string ConectorColumna = "-";
        public const string PlazoMinimoReserva = "PlazoMinimoReserva";
        public const string DuracionCotizacion = "DuracionCotizacion";
        public const string TipoTarifarioDefault = "RC";
        public const string CorrelativoCotizacionId = "CW";
        public const string CorrelativoFileId = "FW";

        #endregion

        #region Servicios Migración

        public const string VerificarPagosFile = "VerificarPagosFile";
        public const string ObtenerAllomentsHotel = "ObtenerAllomentsHotel";
        public const string ObtenerAllomentsHoteles = "ObtenerAllomentsHoteles";
        public const string ActualizarAllomentsHoteles = "ActualizarAllomentsHoteles";

        #endregion
    }
}
