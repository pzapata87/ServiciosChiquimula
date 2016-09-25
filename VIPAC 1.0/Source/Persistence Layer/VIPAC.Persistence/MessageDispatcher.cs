using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data.Entity;
using StructureMap;

namespace VIPAC.Persistence
{
    public class MessageDispatcher
    {
        public static BlockingCollection<string> QueriesQueue;

        public MessageDispatcher()
        {
            QueriesQueue = new BlockingCollection<string>(new ConcurrentQueue<string>());
        }

        public void HandleCommand(Action action)
        {
            var instance = ObjectFactory.GetInstance<DbContext>();
            bool isWebApp = Convert.ToBoolean(ConfigurationManager.AppSettings["IsWebApp"] ?? "true");

            if (isWebApp)
            {
                using (instance)
                {
                    ActionSaveChanges(action, instance);
                }
            }
            else
            {
                ActionSaveChanges(action, instance);
            }
        }

        public T HandleQuery<T>(Func<T> action)
        {
            T local2 = action();
            return local2;
        }

        private void ActionSaveChanges(Action action, DbContext instance)
        {
            action();
            instance.SaveChanges();
        }
    }
}