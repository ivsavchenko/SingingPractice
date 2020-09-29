using System;
using System.Collections.Generic;
using LinqToDB.Configuration;

namespace SingingPractice.Database.Settings
{
    public class SingingPracticeSqlDbSettings : ILinqToDBSettings
    {
        public string DefaultConfiguration => "SingingPractice";

        public string DefaultDataProvider => "SqlServer.2017";

        public string ConnectionString { get; set; }

        public IEnumerable<IDataProviderSettings> DataProviders
        {
            get { yield break; }
        }

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = DefaultConfiguration,
                        ProviderName = DefaultDataProvider,
                        ConnectionString = ConnectionString
                    };
            }
        }

        public SingingPracticeSqlDbSettings(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
    }
}
