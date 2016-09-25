using System.Data.Entity;
using StructureMap;
using StructureMap.Web;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Persistence.EntityFramework;
using VIPAC.Repository;
using VIPAC.Repository.SqlServer;

namespace VIPAC.IoC.DependencyResolution
{
    public static class StructureMapConfigurator
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.AssemblyContainingType<IUsuarioRepository>();
                    scan.AssemblyContainingType<UsuarioRepository>();
                    scan.AssemblyContainingType<IUsuarioBL>();
                    scan.WithDefaultConventions();
                });

                x.For<DbContext>().HybridHttpOrThreadLocalScoped().Use<DbContextBase>();
            });

            return ObjectFactory.Container;
        }
    }
}