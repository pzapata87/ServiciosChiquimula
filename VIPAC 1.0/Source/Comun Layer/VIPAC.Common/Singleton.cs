using System;
using System.Threading;

namespace VIPAC.Common
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instancia;
        // ReSharper disable once StaticFieldInGenericType
        private static readonly Mutex Mutex = new Mutex();

        public static T Instancia
        {
            get
            {
                try
                {
                    Mutex.WaitOne();
                    if (_instancia == null)
                        _instancia = new T();
                    Mutex.ReleaseMutex();
                }
                catch (Exception)
                {
                    _instancia = new T();
                }

                return _instancia;
            }
        }
    }
}