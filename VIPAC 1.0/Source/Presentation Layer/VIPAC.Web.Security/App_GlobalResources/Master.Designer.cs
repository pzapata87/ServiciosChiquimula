//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18449
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile el proyecto de Visual Studio.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Master {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Master() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Master", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Invalida la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Restaurar tu contraseña de VIPAC OnSite.
        /// </summary>
        internal static string AsuntoResetClave {
            get {
                return ResourceManager.GetString("AsuntoResetClave", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Por favor ingrese todos los datos requeridos..
        /// </summary>
        internal static string DatosRequeridos {
            get {
                return ResourceManager.GetString("DatosRequeridos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Las credenciales especificadas son incorrectas..
        /// </summary>
        internal static string LoginFalla {
            get {
                return ResourceManager.GetString("LoginFalla", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ocurrió un error, por favor intente de nuevo o mas tarde..
        /// </summary>
        internal static string MensajeFalla {
            get {
                return ResourceManager.GetString("MensajeFalla", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Su petición fue procesada con éxito, en breve recibirá un correo con su contraseña..
        /// </summary>
        internal static string ResetClaveExito {
            get {
                return ResourceManager.GetString("ResetClaveExito", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El usuario no existe en la base de datos..
        /// </summary>
        internal static string UsuarioNoExiste {
            get {
                return ResourceManager.GetString("UsuarioNoExiste", resourceCulture);
            }
        }
    }
}
