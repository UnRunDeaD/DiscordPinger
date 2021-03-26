using System;
using System.Threading.Tasks;
using Master.DiscordBot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Master {
    public class App {
        private readonly ILogger<App> _applogger;
        private readonly ILogger<Bot> _botLogger;
        private readonly AppSettings _appSettings;

        public App(IOptions<AppSettings> appSettings, ILogger<App> appLogger, ILogger<Bot> botLogger) {
            _applogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
            _botLogger = botLogger ?? throw new ArgumentNullException(nameof(botLogger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task Run() {
            _applogger.LogInformation("Starting app...");
            await RunBot(_appSettings);
        }

        //create bot instance and run it
        private async Task RunBot(AppSettings settings) => await new Bot(_botLogger).RunAsync(settings);
    }
}