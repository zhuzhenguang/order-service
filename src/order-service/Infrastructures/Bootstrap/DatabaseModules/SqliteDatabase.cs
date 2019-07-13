using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Context;
using order_service.Controllers;

namespace order_service.Infrastructures.Bootstrap.DatabaseModules
{
    public class SqliteDatabase : IServiceModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton(c => BuildSessionFactory());
            services.AddTransient(c => CreateSession(c.GetService<ISessionFactory>()));
        }

        private static ISessionFactory BuildSessionFactory()
        {
            IPersistenceConfigurer dbConfiguration = SQLiteConfiguration
                .Standard.UsingFile("order.db");

            FluentConfiguration fluentConfiguration = Fluently.Configure()
                .Database(dbConfiguration)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<HealthController>())
                .CurrentSessionContext<ThreadStaticSessionContext>();

            return fluentConfiguration.BuildSessionFactory();
        }

        private static ISession CreateSession(ISessionFactory sessionFactory)
        {
            ISession session = sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            return session;
        }
    }
}