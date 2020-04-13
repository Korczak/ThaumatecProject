using System;
using System.Collections.Generic;
using System.Text;
using Thaumatec.Core.Configuration;

namespace Thaumatec.Core.Database.Settings
{
    public class DatabaseStartup : IStartupStep
    {
        private readonly Config _config;

        public DatabaseStartup(Config config)
        {
            _config = config;
        }

        public IStartupValidation Configure()
        {
            DatabaseConnection.SetConnection(_config);

            using(var handler = new DatabaseHandler())
            {
                if (handler.db != null)
                    return DatabaseValidation.Successfull();
            }

            return DatabaseValidation.Failure();
        }
    }
}
