using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using RazorEngine;

namespace VIPAC.Common
{
    public static class UtilsComun
    {
        /// <summary>
        ///     Metodo para obtener una expresion Lambda para un OrderBy en base al nombre de la propiedad
        /// </summary>
        /// <typeparam name="T">El tipo la clase que contiene la propiedad</typeparam>
        /// <param name="propiedad">El nombre de la propiedad que se usara en el OrderBy</param>
        /// <returns>Una expresion del tipo dynamic</returns>
        public static dynamic LambdaPropertyOrderBy<T>(string propiedad) where T : class
        {
            string[] listaPropiedades = propiedad.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (string prop in listaPropiedades)
            {
                PropertyInfo propertyInfo = type.GetProperty(prop);
                expr = Expression.MakeMemberAccess(expr, propertyInfo);
                type = propertyInfo.PropertyType;
            }

            return Expression.Lambda(expr, arg);
        }

        public static Expression GetMemberAccessLambda<T>(ParameterExpression arg, string itemField) where T : class
        {
            var listaPropiedades = itemField.Split('.');
            Expression expression = arg;

            var tipoActual = typeof (T);

            foreach (string propiedad in listaPropiedades)
            {
                PropertyInfo propertyInfo = tipoActual.GetProperty(propiedad);
                expression = Expression.MakeMemberAccess(expression, propertyInfo);
                tipoActual = propertyInfo.PropertyType;
            }

            return expression;
        }

        /// <summary>
        /// Permite obtener una expresion lambda
        /// </summary>
        /// <typeparam name="T">Clase de la cual se avalua las propiedades usadas para el filtro</typeparam>
        /// <typeparam name="TQ">Clase que contiene el valor de las propiedades para el filtro</typeparam>
        /// <param name="data">Representa el valor de las propiedades para el filtro</param>
        /// <param name="filterRules">Representa la lista de Field-Value, donde "Field" es el nombre de la propiedad de T, y "Value" es el nombre de la propiedad de TQ</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ConvertToLambda<T, TQ>(TQ data, Rule[] filterRules)
            where T : class
        {
            Expression<Func<T, bool>> expresionsLambdaSet = null;

            foreach (var item in filterRules)
            {
                PropertyInfo propertyKey = null;
                var tipoActual = typeof (T);

                if (item.Field.Contains("."))
                {
                    #region Seccion para obtener la ultima propiedad de la composicion

                    var properties = item.Field.Split('.');

                    foreach (var propiedad in properties)
                    {
                        propertyKey = tipoActual.GetProperty(propiedad);
                        tipoActual = propertyKey.PropertyType;
                    }

                    #endregion
                }
                else
                {
                    propertyKey = typeof (T).GetProperty(item.Field);
                }

                PropertyInfo propertyValue;
                object value = null;

                if (item.Value.Contains("."))
                {
                    #region Seccion para obtener la ultima propiedad de la composicion

                    tipoActual = typeof (TQ);
                    var properties = item.Value.Split('.');
                    object valueTemp = data;

                    foreach (var propiedad in properties)
                    {
                        propertyValue = tipoActual.GetProperty(propiedad);
                        tipoActual = propertyValue.PropertyType;
                        value = propertyValue.GetValue(valueTemp);
                        valueTemp = value;
                    }

                    #endregion
                }
                else
                {
                    propertyValue = typeof (TQ).GetProperty(item.Value);
                    value = propertyValue.GetValue(data);
                }

                if (propertyKey != null)
                {
                    var valorEvaluar = value == null
                        ? (Expression) Expression.Constant(null)
                        : Expression.Convert(Expression.Constant(Convert.ChangeType(value,
                            Nullable.GetUnderlyingType(propertyKey.PropertyType) ?? propertyKey.PropertyType)),
                            propertyKey.PropertyType);

                    var arg = Expression.Parameter(typeof (T), "p");
                    var comparison = Expression.Equal(GetMemberAccessLambda<T>(arg, item.Field), valorEvaluar);

                    expresionsLambdaSet = expresionsLambdaSet != null
                        ? expresionsLambdaSet.And(Expression.Lambda<Func<T, bool>>(comparison, arg))
                        : Expression.Lambda<Func<T, bool>>(comparison, arg);
                }
            }

            return expresionsLambdaSet ?? PredicateBuilder.True<T>();
        }

        public static Expression<Func<T, bool>> ConvertToLambda<T>(FilterRule parametro) where T : class
        {
            Expression<Func<T, bool>> expresionsLambdaSet = MergeRules<T>(parametro);

            return expresionsLambdaSet ?? PredicateBuilder.True<T>();
        }

        private static Expression<Func<T, bool>> MergeRules<T>(FilterRule parametro) where T : class
        {
            Expression<Func<T, bool>> expresionsLambdaSet = null;

            Expression comparison = null;

            foreach (Rule item in parametro.Rules)
            {
                var arg = Expression.Parameter(typeof (T), "p");
                PropertyInfo property;

                if (item.Field.Contains("."))
                {
                    #region Seccion para obtener la ultima propiedad de la composicion

                    var properties = item.Field.Split('.');
                    var tipoActual = typeof (T);
                    PropertyInfo propertyInfo = null;

                    foreach (var propiedad in properties)
                    {
                        propertyInfo = tipoActual.GetProperty(propiedad);
                        tipoActual = propertyInfo.PropertyType;
                    }

                    property = propertyInfo;

                    #endregion
                }
                else
                    property = typeof (T).GetProperty(item.Field);

                if (property != null)
                {
                    var valorEvaluar = item.Value == null
                        ? (Expression) Expression.Constant(item.Value)
                        : Expression.Convert(Expression.Constant(Convert.ChangeType(item.Value,
                            Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType)),
                            property.PropertyType);

                    switch (item.Operator)
                    {
                            #region Lista de Expresiones Comparativas

                        case "bw":
                            MethodInfo miBeginWith = typeof (string).GetMethod("StartsWith", new[] {typeof (string)});
                            comparison = Expression.Call(GetMemberAccessLambda<T>(arg, item.Field), miBeginWith, valorEvaluar);
                            break;
                        case "gt":
                            comparison = Expression.GreaterThanOrEqual(GetMemberAccessLambda<T>(arg, item.Field),
                                valorEvaluar);
                            break;
                        case "lt":
                            comparison = Expression.LessThanOrEqual(GetMemberAccessLambda<T>(arg, item.Field),
                                valorEvaluar);
                            break;
                        case "eq":
                            comparison = Expression.Equal(GetMemberAccessLambda<T>(arg, item.Field), valorEvaluar);
                            break;
                        case "ne":
                            comparison = Expression.NotEqual(GetMemberAccessLambda<T>(arg, item.Field), valorEvaluar);
                            break;
                        case "ew":
                            MethodInfo miEndsWith = typeof (string).GetMethod("EndsWith", new[] {typeof (string)});
                            comparison = Expression.Call(GetMemberAccessLambda<T>(arg, item.Field), miEndsWith, valorEvaluar);
                            break;
                        case "cn":
                            MethodInfo miContains = typeof (string).GetMethod("Contains", new[] {typeof (string)});
                            comparison = Expression.Call(GetMemberAccessLambda<T>(arg, item.Field), miContains, valorEvaluar);
                            break;
                        case "fe":
                            break;

                            #endregion
                    }
                }

                #region Concatenacion de las Expresiones de los rules actuales

                if (parametro.GroupOp.ToUpper() == "AND")
                {
                    expresionsLambdaSet = expresionsLambdaSet != null
                        ? expresionsLambdaSet.And(Expression.Lambda<Func<T, bool>>(comparison, arg))
                        : Expression.Lambda<Func<T, bool>>(comparison, arg);
                }
                else if (parametro.GroupOp.ToUpper() == "OR")
                {
                    expresionsLambdaSet = expresionsLambdaSet != null
                        ? expresionsLambdaSet.Or(Expression.Lambda<Func<T, bool>>(comparison, arg))
                        : Expression.Lambda<Func<T, bool>>(comparison, arg);
                }
                else
                    throw new ArgumentException("Argumento GroupOp invalido");

                #endregion
            }

            if (parametro.Groups == null)
                return expresionsLambdaSet;

            #region Manejo de Expresiones hijas de esta expresion

            foreach (var parametroHijo in parametro.Groups)
            {
                var expressionHijo = MergeRules<T>(parametroHijo);
                if (expressionHijo == null) continue;

                if (expresionsLambdaSet == null)
                {
                    expresionsLambdaSet = expressionHijo;
                    continue;
                }

                if (parametro.GroupOp.ToUpper() == "AND")
                {
                    expresionsLambdaSet = expresionsLambdaSet.And(expressionHijo);
                }
                else if (parametro.GroupOp.ToUpper() == "OR")
                {
                    expresionsLambdaSet = expresionsLambdaSet.Or(MergeRules<T>(parametroHijo));
                }
                else
                    throw new ArgumentException("Argumento GroupOp invalido");
            }

            #endregion

            return expresionsLambdaSet;
        }

        /// <summary>
        /// Permite verificar si una clase hereda de una clase base.
        /// </summary>
        /// <param name="type">Tipo a comparar.</param>
        /// <param name="typeBase">Tipo Base a la cual se va a comparar.</param>
        /// <returns></returns>
        public static bool VerificarBaseType(Type type, Type typeBase)
        {
            bool esValido = false;
            if (type != null)
            {
                if (type.BaseType != null)
                    esValido = (type.BaseType == typeBase ||
                                (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeBase)) ||
                               VerificarBaseType(type.BaseType, typeBase);
            }
            return esValido;
        }

        public static string GetExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            var errorMessage = string.Format("{0}\n{1}", ex.Message, GetExceptionMessage(ex.InnerException));
            return errorMessage;
        }

        public static string BodyEmail<T>(string contenido, T model, bool isHtml = true)
        {
            dynamic obj2 = new ExpandoObject();
            obj2.Dummy = "";
            string body = Razor.Parse(contenido, model, null);
            return body;
        }

        #region Métodos adicionales y de extensión para fechas

        /// <summary>
        /// Convierte una fecha en una cadena con formato: dd/mm/yyyy.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>dd/mm/yyyy</returns>
        public static string GetDate(this DateTime fecha)
        {
            return string.Format("{0:dd/MM/yyyy}", fecha);
        }

        /// <summary>
        /// Extrae la hora de una fecha en formato: hh:mm.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>hh:mm</returns>
        public static string ConvertToHour(this DateTime fecha)
        {
            return string.Format("{0:HH:mm}", fecha);
        }

        /// <summary>
        /// Convierte una fecha en una cadena con formato: dd/mm/yyyy hh:mm:ss(includeSeconds = true), dd/mm/yyyy hh:mm
        /// (includeSeconds = false).
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="includeSeconds"></param>
        /// <returns>dd/mm/yyyy hh:mm:ss => (includeSeconds = true), dd/mm/yyyy hh:mm => (includeSeconds = false)</returns>
        public static string GetDateTime(this DateTime dateTime, bool includeSeconds = true)
        {
            return includeSeconds
                ? string.Format("{0:dd/MM/yyyy HH:mm:ss}", dateTime)
                : string.Format("{0:dd/MM/yyyy HH:mm}", dateTime);
        }

        /// <summary>
        /// Retorna la fecha con la última hora del dia: dd/mm/yy 23:59:59.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime GetDateEnd(this DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);
        }

        /// <summary>
        /// Retorna la fecha con la hora inicial del dia: dd/mm/yy 0:0:0.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime GetDateStar(this DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
        }

        /// <summary>
        /// Retorna la fecha equivalente en base al timezone origen hacia el timezone local
        /// </summary>
        /// <param name="dateAnotherTimeZone">fecha en otro timezone distinto al local</param>
        /// <param name="timeZoneSourceId">Id del timezone en el que viene la fecha</param>
        /// <returns>Fecha equivalente en el timezone local</returns>
        public static DateTime ConvertDateTimeToLocalTimeZone(DateTime dateAnotherTimeZone, string timeZoneSourceId)
        {
            DateTime fechaConvertida = TimeZoneInfo.ConvertTime(dateAnotherTimeZone,
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneSourceId),
                TimeZoneInfo.Local);
            return fechaConvertida;
        }

        #endregion Métodos adicionales y de extensión para fechas

        #region Extensiones enumeración

        public static string GetStringValue(this System.Enum value)
        {
            return Convert.ToString(Convert.ChangeType(value, value.GetTypeCode()));
        }

        public static int GetNumberValue(this System.Enum value)
        {
            return Convert.ToInt32(Convert.ChangeType(value, value.GetTypeCode()));
        }

        #endregion

        #region Archivos

        public static void EliminarArchivo(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void VerificarExistenciaArchivo(string path)
        {
            if (!File.Exists(path))
            {
                using (var sw = new StreamWriter(path, true))
                {
                    sw.Dispose();
                }
            }
        }

        public static string ObtenerGetType(string nombreArchivo)
        {
            #region DiccionarioContentType
            var contentTypes = new Dictionary<string, string>
                               {
                                   {"3dm", "x-world/x-3dmf"},
                                   {"3dmf", "x-world/x-3dmf"},
                                   {"a", "application/octet-stream"},
                                   {"aab", "application/x-authorware-bin"},
                                   {"aam", "application/x-authorware-map"},
                                   {"aas", "application/x-authorware-seg"},
                                   {"abc", "text/vnd.abc"},
                                   {"acgi", "text/html"},
                                   {"afl", "video/animaflex"},
                                   {"ai", "application/postscript"},
                                   {"aif", "audio/aiff"},
                                   {"aifc", "audio/aiff"},
                                   {"aiff", "audio/aiff"},
                                   {"aim", "application/x-aim"},
                                   {"aip", "text/x-audiosoft-intra"},
                                   {"ani", "application/x-navi-animation"},
                                   {"aos", "application/x-nokia-9000-communicator-add-on-software"},
                                   {"aps", "application/mime"},
                                   {"arc", "application/octet-stream"},
                                   {"arj", "application/arj"},
                                   {"art", "image/x-jg"},
                                   {"asf", "video/x-ms-asf"},
                                   {"asm", "text/x-asm"},
                                   {"asp", "text/asp"},
                                   {"asx", "application/x-mplayer2"},
                                   {"au", "audio/basic"},
                                   {"avi", "video/avi"},
                                   {"avs", "video/avs-video"},
                                   {"bcpio", "application/x-bcpio"},
                                   {"bin", "application/octet-stream"},
                                   {"bm", "image/bmp"},
                                   {"bmp", "image/bmp"},
                                   {"boo", "application/book"},
                                   {"book", "application/book"},
                                   {"boz", "application/x-bzip2"},
                                   {"bsh", "application/x-bsh"},
                                   {"bz", "application/x-bzip"},
                                   {"bz2", "application/x-bzip2"},
                                   {"c", "text/plain"},
                                   {"c++", "text/plain"},
                                   {"cat", "application/vnd.ms-pki.seccat"},
                                   {"cc", "text/plain"},
                                   {"ccad", "application/clariscad"},
                                   {"cco", "application/x-cocoa"},
                                   {"cdf", "application/cdf"},
                                   {"cer", "application/pkix-cert"},
                                   {"cha", "application/x-chat"},
                                   {"chat", "application/x-chat"},
                                   {"class", "application/java"},
                                   {"com", "application/octet-stream"},
                                   {"conf", "text/plain"},
                                   {"cpio", "application/x-cpio"},
                                   {"cpp", "text/x-c"},
                                   {"cpt", "application/x-cpt"},
                                   {"crl", "application/pkcs-crl"},
                                   {"css", "text/css"},
                                   {"def", "text/plain"},
                                   {"der", "application/x-x509-ca-cert"},
                                   {"dif", "video/x-dv"},
                                   {"dir", "application/x-director"},
                                   {"dl", "video/dl"},
                                   {"doc", "application/msword"},
                                   {"dot", "application/msword"},
                                   {"dp", "application/commonground"},
                                   {"drw", "application/drafting"},
                                   {"dump", "application/octet-stream"},
                                   {"dv", "video/x-dv"},
                                   {"dvi", "application/x-dvi"},
                                   {"dwf", "drawing/x-dwf (old)"},
                                   {"dwg", "application/acad"},
                                   {"dxf", "application/dxf"},
                                   {"eps", "application/postscript"},
                                   {"es", "application/x-esrehber"},
                                   {"etx", "text/x-setext"},
                                   {"evy", "application/envoy"},
                                   {"exe", "application/octet-stream"},
                                   {"f", "text/plain"},
                                   {"f90", "text/x-fortran"},
                                   {"fdf", "application/vnd.fdf"},
                                   {"fif", "image/fif"},
                                   {"fli", "video/fli"},
                                   {"for", "text/x-fortran"},
                                   {"fpx", "image/vnd.fpx"},
                                   {"g", "text/plain"},
                                   {"g3", "image/g3fax"},
                                   {"gif", "image/gif"},
                                   {"gl", "video/gl"},
                                   {"gsd", "audio/x-gsm"},
                                   {"gtar", "application/x-gtar"},
                                   {"gz", "application/x-compressed"},
                                   {"h", "text/plain"},
                                   {"help", "application/x-helpfile"},
                                   {"hgl", "application/vnd.hp-hpgl"},
                                   {"hh", "text/plain"},
                                   {"hlp", "application/x-winhelp"},
                                   {"htc", "text/x-component"},
                                   {"htm", "text/html"},
                                   {"html", "text/html"},
                                   {"htmls", "text/html"},
                                   {"htt", "text/webviewhtml"},
                                   {"htx", "text/html"},
                                   {"ice", "x-conference/x-cooltalk"},
                                   {"ico", "image/x-icon"},
                                   {"idc", "text/plain"},
                                   {"ief", "image/ief"},
                                   {"iefs", "image/ief"},
                                   {"iges", "application/iges"},
                                   {"igs", "application/iges"},
                                   {"ima", "application/x-ima"},
                                   {"imap", "application/x-httpd-imap"},
                                   {"inf", "application/inf"},
                                   {"ins", "application/x-internett-signup"},
                                   {"ip", "application/x-ip2"},
                                   {"isu", "video/x-isvideo"},
                                   {"it", "audio/it"},
                                   {"iv", "application/x-inventor"},
                                   {"ivr", "i-world/i-vrml"},
                                   {"ivy", "application/x-livescreen"},
                                   {"jam", "audio/x-jam"},
                                   {"jav", "text/plain"},
                                   {"java", "text/plain"},
                                   {"jcm", "application/x-java-commerce"},
                                   {"jfif", "image/jpeg"},
                                   {"jfif-tbnl", "image/jpeg"},
                                   {"jpe", "image/jpeg"},
                                   {"jpeg", "image/jpeg"},
                                   {"jpg", "image/jpeg"},
                                   {"jps", "image/x-jps"},
                                   {"js", "application/x-javascript"},
                                   {"jut", "image/jutvision"},
                                   {"kar", "audio/midi"},
                                   {"ksh", "application/x-ksh"},
                                   {"la", "audio/nspaudio"},
                                   {"lam", "audio/x-liveaudio"},
                                   {"latex", "application/x-latex"},
                                   {"lha", "application/lha"},
                                   {"lhx", "application/octet-stream"},
                                   {"list", "text/plain"},
                                   {"lma", "audio/nspaudio"},
                                   {"log", "text/plain"},
                                   {"lsp", "application/x-lisp"},
                                   {"lst", "text/plain"},
                                   {"lsx", "text/x-la-asf"},
                                   {"ltx", "application/x-latex"},
                                   {"lzh", "application/octet-stream"},
                                   {"lzx", "application/lzx"},
                                   {"m", "text/plain"},
                                   {"m1v", "video/mpeg"},
                                   {"m2a", "audio/mpeg"},
                                   {"m2v", "video/mpeg"},
                                   {"m3u", "audio/x-mpequrl"},
                                   {"man", "application/x-troff-man"},
                                   {"map", "application/x-navimap"},
                                   {"mar", "text/plain"},
                                   {"mbd", "application/mbedlet"},
                                   {"mc$", "application/x-magic-cap-package-1.0"},
                                   {"mcd", "application/mcad"},
                                   {"mcf", "image/vasa"},
                                   {"mcp", "application/netmc"},
                                   {"me", "application/x-troff-me"},
                                   {"mht", "message/rfc822"},
                                   {"mhtml", "message/rfc822"},
                                   {"mid", "audio/midi"},
                                   {"midi", "audio/midi"},
                                   {"mif", "application/x-frame"},
                                   {"mime", "message/rfc822"},
                                   {"mjf", "audio/x-vnd.audioexplosion.mjuicemediafile"},
                                   {"mjpg", "video/x-motion-jpeg"},
                                   {"mm", "application/base64"},
                                   {"mme", "application/base64"},
                                   {"mod", "audio/mod"},
                                   {"moov", "video/quicktime"},
                                   {"mov", "video/quicktime"},
                                   {"movie", "video/x-sgi-movie"},
                                   {"mp2", "audio/mpeg"},
                                   {"mp3", "audio/mpeg3"},
                                   {"mpa", "audio/mpeg"},
                                   {"mpc", "application/x-project"},
                                   {"mpe", "video/mpeg"},
                                   {"mpeg", "video/mpeg"},
                                   {"mpg", "video/mpeg"},
                                   {"mpga", "audio/mpeg"},
                                   {"mpp", "application/vnd.ms-project"},
                                   {"mpt", "application/x-project"},
                                   {"mpv", "application/x-project"},
                                   {"mpx", "application/x-project"},
                                   {"mrc", "application/marc"},
                                   {"ms", "application/x-troff-ms"},
                                   {"mv", "video/x-sgi-movie"},
                                   {"my", "audio/make"},
                                   {"mzz", "application/x-vnd.audioexplosion.mzz"},
                                   {"nap", "image/naplps"},
                                   {"naplps", "image/naplps"},
                                   {"nc", "application/x-netcdf"},
                                   {"ncm", "application/vnd.nokia.configuration-message"},
                                   {"nif", "image/x-niff"},
                                   {"niff", "image/x-niff"},
                                   {"nix", "application/x-mix-transfer"},
                                   {"nsc", "application/x-conference"},
                                   {"nvd", "application/x-navidoc"},
                                   {"o", "application/octet-stream"},
                                   {"oda", "application/oda"},
                                   {"omc", "application/x-omc"},
                                   {"omcd", "application/x-omcdatamaker"},
                                   {"omcr", "application/x-omcregerator"},
                                   {"p", "text/x-pascal"},
                                   {"p10", "application/pkcs10"},
                                   {"p12", "application/pkcs-12"},
                                   {"p7a", "application/x-pkcs7-signature"},
                                   {"p7c", "application/pkcs7-mime"},
                                   {"pas", "text/pascal"},
                                   {"pbm", "image/x-portable-bitmap"},
                                   {"pcl", "application/vnd.hp-pcl"},
                                   {"pct", "image/x-pict"},
                                   {"pcx", "image/x-pcx"},
                                   {"pdf", "application/pdf"},
                                   {"pfunk", "audio/make"},
                                   {"pgm", "image/x-portable-graymap"},
                                   {"pic", "image/pict"},
                                   {"pict", "image/pict"},
                                   {"pkg", "application/x-newton-compatible-pkg"},
                                   {"pko", "application/vnd.ms-pki.pko"},
                                   {"pl", "text/plain"},
                                   {"plx", "application/x-pixclscript"},
                                   {"pm", "image/x-xpixmap"},
                                   {"png", "image/png"},
                                   {"pnm", "application/x-portable-anymap"},
                                   {"pot", "application/mspowerpoint"},
                                   {"pov", "model/x-pov"},
                                   {"ppa", "application/vnd.ms-powerpoint"},
                                   {"ppm", "image/x-portable-pixmap"},
                                   {"pps", "application/mspowerpoint"},
                                   {"ppt", "application/mspowerpoint"},
                                   {"ppz", "application/mspowerpoint"},
                                   {"pre", "application/x-freelance"},
                                   {"prt", "application/pro_eng"},
                                   {"ps", "application/postscript"},
                                   {"psd", "application/octet-stream"},
                                   {"pvu", "paleovu/x-pv"},
                                   {"pwz", "application/vnd.ms-powerpoint"},
                                   {"py", "text/x-script.phyton"},
                                   {"pyc", "applicaiton/x-bytecode.python"},
                                   {"qcp", "audio/vnd.qcelp"},
                                   {"qd3", "x-world/x-3dmf"},
                                   {"qd3d", "x-world/x-3dmf"},
                                   {"qif", "image/x-quicktime"},
                                   {"qt", "video/quicktime"},
                                   {"qtc", "video/x-qtc"},
                                   {"qti", "image/x-quicktime"},
                                   {"qtif", "image/x-quicktime"},
                                   {"ra", "audio/x-pn-realaudio"},
                                   {"ram", "audio/x-pn-realaudio"},
                                   {"ras", "application/x-cmu-raster"},
                                   {"rast", "image/cmu-raster"},
                                   {"rexx", "text/x-script.rexx"},
                                   {"rf", "image/vnd.rn-realflash"},
                                   {"rgb", "image/x-rgb"},
                                   {"rm", "application/vnd.rn-realmedia"},
                                   {"rmi", "audio/mid"},
                                   {"rmm", "audio/x-pn-realaudio"},
                                   {"rmp", "audio/x-pn-realaudio"},
                                   {"rng", "application/ringing-tones"},
                                   {"rnx", "application/vnd.rn-realplayer"},
                                   {"roff", "application/x-troff"},
                                   {"rp", "image/vnd.rn-realpix"},
                                   {"rpm", "audio/x-pn-realaudio-plugin"},
                                   {"rt", "text/richtext"},
                                   {"rtf", "text/richtext"},
                                   {"rtx", "application/rtf"},
                                   {"rv", "video/vnd.rn-realvideo"},
                                   {"s", "text/x-asm"},
                                   {"s3m", "audio/s3m"},
                                   {"saveme", "application/octet-stream"},
                                   {"sbk", "application/x-tbook"},
                                   {"scm", "application/x-lotusscreencam"},
                                   {"sdml", "text/plain"},
                                   {"sdp", "application/sdp"},
                                   {"sdr", "application/sounder"},
                                   {"sea", "application/sea"},
                                   {"set", "application/set"},
                                   {"sgm", "text/sgml"},
                                   {"sgml", "text/sgml"},
                                   {"sh", "application/x-bsh"},
                                   {"shtml", "text/html"},
                                   {"sid", "audio/x-psid"},
                                   {"sit", "application/x-sit"},
                                   {"skd", "application/x-koan"},
                                   {"skm", "application/x-koan"},
                                   {"skp", "application/x-koan"},
                                   {"skt", "application/x-koan"},
                                   {"sl", "application/x-seelogo"},
                                   {"smi", "application/smil"},
                                   {"smil", "application/smil"},
                                   {"snd", "audio/basic"},
                                   {"sol", "application/solids"},
                                   {"spc", "application/x-pkcs7-certificates"},
                                   {"spl", "application/futuresplash"},
                                   {"spr", "application/x-sprite"},
                                   {"sprite", "application/x-sprite"},
                                   {"src", "application/x-wais-source"},
                                   {"ssi", "text/x-server-parsed-html"},
                                   {"ssm", "application/streamingmedia"},
                                   {"sst", "application/vnd.ms-pki.certstore"},
                                   {"step", "application/step"},
                                   {"stl", "application/sla"},
                                   {"stp", "application/step"},
                                   {"sv4cpio", "application/x-sv4cpio"},
                                   {"sv4crc", "application/x-sv4crc"},
                                   {"svf", "image/vnd.dwg"},
                                   {"svr", "application/x-world"},
                                   {"swf", "application/x-shockwave-flash"},
                                   {"t", "application/x-troff"},
                                   {"talk", "text/x-speech"},
                                   {"tar", "application/x-tar"},
                                   {"tbk", "application/toolbook"},
                                   {"tcl", "application/x-tcl"},
                                   {"tcsh", "text/x-script.tcsh"},
                                   {"tex", "application/x-tex"},
                                   {"texi", "application/x-texinfo"},
                                   {"texinfo", "application/x-texinfo"},
                                   {"text", "text/plain"},
                                   {"tgz", "application/x-compressed"},
                                   {"tif", "image/tiff"},
                                   {"tr", "application/x-troff"},
                                   {"tsi", "audio/tsp-audio"},
                                   {"tsp", "audio/tsplayer"},
                                   {"tsv", "text/tab-separated-values"},
                                   {"turbot", "image/florian"},
                                   {"txt", "text/plain"},
                                   {"uil", "text/x-uil"},
                                   {"uni", "text/uri-list"},
                                   {"unis", "text/uri-list"},
                                   {"unv", "application/i-deas"},
                                   {"uri", "text/uri-list"},
                                   {"uris", "text/uri-list"},
                                   {"ustar", "application/x-ustar"},
                                   {"uu", "application/octet-stream"},
                                   {"vcd", "application/x-cdlink"},
                                   {"vcs", "text/x-vcalendar"},
                                   {"vda", "application/vda"},
                                   {"vdo", "video/vdo"},
                                   {"vew", "application/groupwise"},
                                   {"viv", "video/vivo"},
                                   {"vivo", "video/vivo"},
                                   {"vmd", "application/vocaltec-media-desc"},
                                   {"vmf", "application/vocaltec-media-file"},
                                   {"voc", "audio/voc"},
                                   {"vos", "video/vosaic"},
                                   {"vox", "audio/voxware"},
                                   {"vqe", "audio/x-twinvq-plugin"},
                                   {"vqf", "audio/x-twinvq"},
                                   {"vql", "audio/x-twinvq-plugin"},
                                   {"vrml", "application/x-vrml"},
                                   {"vrt", "x-world/x-vrt"},
                                   {"vsd", "application/x-visio"},
                                   {"vst", "application/x-visio"},
                                   {"vsw", "application/x-visio"},
                                   {"w60", "application/wordperfect6.0"},
                                   {"w61", "application/wordperfect6.1"},
                                   {"w6w", "application/msword"},
                                   {"wav", "audio/wav"},
                                   {"wb1", "application/x-qpro"},
                                   {"wbmp", "image/vnd.wap.wbmp"},
                                   {"web", "application/vnd.xara"},
                                   {"wiz", "application/msword"},
                                   {"wk1", "application/x-123"},
                                   {"wmf", "windows/metafile"},
                                   {"wml", "text/vnd.wap.wml"},
                                   {"wmlc", "application/vnd.wap.wmlc"},
                                   {"wmls", "text/vnd.wap.wmlscript"},
                                   {"wmlsc", "application/vnd.wap.wmlscriptc"},
                                   {"word", "application/msword"},
                                   {"wp", "application/wordperfect"},
                                   {"wp5", "application/wordperfect"},
                                   {"wp6", "application/wordperfect"},
                                   {"wpd", "application/wordperfect"},
                                   {"wq1", "application/x-lotus"},
                                   {"wri", "application/mswrite"},
                                   {"wrl", "application/x-world"},
                                   {"wrz", "model/vrml"},
                                   {"wsc", "text/scriplet"},
                                   {"wsrc", "application/x-wais-source"},
                                   {"wtk", "application/x-wintalk"},
                                   {"xbm", "image/x-xbitmap"},
                                   {"xdr", "video/x-amt-demorun"},
                                   {"xgz", "xgl/drawing"},
                                   {"xif", "image/vnd.xiff"},
                                   {"xl", "application/excel"},
                                   {"xla", "application/excel"},
                                   {"xlb", "application/excel"},
                                   {"xlc", "application/excel"},
                                   {"xld", "application/excel"},
                                   {"xlk", "application/excel"},
                                   {"xll", "application/excel"},
                                   {"xlm", "application/excel"},
                                   {"xls", "application/excel"},
                                   {"xlt", "application/excel"},
                                   {"xlv", "application/excel"},
                                   {"xlw", "application/excel"},
                                   {"xm", "audio/xm"},
                                   {"xml", "text/xml"},
                                   {"xmz", "xgl/movie"},
                                   {"xpix", "application/x-vnd.ls-xpix"},
                                   {"xpm", "image/x-xpixmap"},
                                   {"x-png", "image/png"},
                                   {"xsr", "video/x-amt-showrun"},
                                   {"xwd", "image/x-xwd"},
                                   {"xyz", "chemical/x-pdb"},
                                   {"z", "application/x-compress"},
                                   {"zip", "application/x-compressed"},
                                   {"zoo", "application/octet-stream"},
                                   {"zsh", "text/x-script.zsh"}
                               };
            #endregion DiccionarioContentType

            var extension = Path.GetExtension(nombreArchivo).ToLower().TrimStart('.');

            string contentType;

            contentTypes.TryGetValue(extension.ToLower(), out contentType);

            if (String.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        #endregion Archivos
    }
}