using System;
using System.Collections.Generic;
using System.Text;

namespace Thaumatec.Core.Logging
{

    public class LoggingPaths
    {
        public string Workspace { get; }
        public string LoggerPath { get; }

        public LoggingPaths()
        {
            Workspace = AppDomain.CurrentDomain.BaseDirectory;
            LoggerPath = Workspace;
        }
    }
}
