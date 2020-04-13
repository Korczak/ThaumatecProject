using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Thaumatec.Web.Configuration
{
    public class Worker : BackgroundService
    {
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //Log.Information($"Service stopped");
            return base.StopAsync(cancellationToken);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //Log.Information($"Service started");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
