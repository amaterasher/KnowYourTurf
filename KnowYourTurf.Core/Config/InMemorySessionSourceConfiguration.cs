using System;
using FluentNHibernate.Cfg;
using NHibernate;

namespace KnowYourTurf.Core.Domain
{
    public class InMemorySessionSourceConfiguration : ISessionFactoryConfiguration
    {
        private readonly INHSetupConfig _config;
        private readonly string _connectionStr;

        public InMemorySessionSourceConfiguration(INHSetupConfig config,string connectionStr)
        {
            _config = config;
            _connectionStr = connectionStr;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
               .Database(_config.DBConfiguration(_connectionStr))
               .Mappings(_config.MappingConfiguration())
               .BuildSessionFactory();
        }

        public ISessionFactory CreateSessionFactoryAndGenerateSchema()
        {
            return Fluently.Configure()
                         .Database(_config.DBConfiguration(_connectionStr))
                         .Mappings(_config.MappingConfiguration())
                         .ExposeConfiguration(_config.GenerateSchema)
                         .BuildSessionFactory();
        }

    }
}