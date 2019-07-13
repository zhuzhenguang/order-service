using System.Data.Common;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Engine;
using NHibernate.Tool.hbm2ddl;
using order_service.Controllers;

namespace order_service.Infrastructures.Bootstrap.DatabaseModules
{
    public class InMemoryDatabase : IServiceModule
    {
        private const string ConnectionString = "Data Source=:memory:;Version=3;New=True;DateTimeKind=Utc;";

        private static Configuration inMemDbConfiguration;
        private static ISessionFactory sessionFactory;

        public void Load(IServiceCollection services)
        {
            sessionFactory = BuildSessionFactory();

            DbConnection dbConnection = ((ISessionFactoryImplementor) sessionFactory)
                .ConnectionProvider
                .GetConnection();
            ImportTables(dbConnection, inMemDbConfiguration);

            services.AddSingleton(c => sessionFactory);

            services.AddTransient(c => sessionFactory
                .WithOptions()
                .Connection(dbConnection)
                .FlushMode(FlushMode.Commit)
                .OpenSession());
        }

        private static ISessionFactory BuildSessionFactory()
        {
            return sessionFactory ?? (sessionFactory = Fluently.Configure()
                       .Database(SQLiteConfiguration.Standard.InMemory().ConnectionString(ConnectionString))
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<HealthController>())
                       .CurrentSessionContext<ThreadStaticSessionContext>()
                       .ExposeConfiguration(cfg => { inMemDbConfiguration = cfg; })
                       .BuildSessionFactory());
        }

        private static void ImportTables(DbConnection dbConnection, Configuration dbConfiguration)
        {
            var schemaExport = new SchemaExport(dbConfiguration);
            schemaExport.Execute(false, true, false, dbConnection, null);
        }
    }
}