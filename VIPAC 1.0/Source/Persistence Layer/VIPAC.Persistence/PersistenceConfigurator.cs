﻿using System.Reflection;

namespace VIPAC.Persistence
{
    public class PersistenceConfigurator
    {
        public static string ConnectionStringKey;
        public static Assembly EntititesAssembly;
        public static Assembly MappingsAssembly;

        public static void Configure(string connectionStringKey, Assembly entitiesAssembly, Assembly mappingAssembly)
        {
            ConnectionStringKey = connectionStringKey;
            EntititesAssembly = entitiesAssembly;
            MappingsAssembly = mappingAssembly;
        }
    }
}