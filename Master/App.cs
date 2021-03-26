using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Master {
    public class App {
        private readonly ILogger<App> _logger;
        private readonly AppSettings _appSettings;

        public App(IOptions<AppSettings> appSettings, ILogger<App> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task Run() {
            _logger.LogInformation("Starting app...");
            await RunBot(_appSettings);
        }

        //create bot instance and run it
        private async Task RunBot(AppSettings settings) => new Bot().RunAsync(settings).GetAwaiter().GetResult();
    }
}