using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.BackGroundHelper
{public class InvoiceMonitorService : BackgroundService
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public InvoiceMonitorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var respondCommand = scope.ServiceProvider.GetRequiredService<IRespondCommand>();
                await respondCommand.CheckUnpaidInvoicesAsync();
               
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
    
