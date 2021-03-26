using System;
using System.Threading.Tasks;
using Master.DiscordBot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Master {
    public class App {
        private readonly ILogger<App> _applogger;
        private readonly AppSettings _appSettings;
        private readonly IBot _bot;

        public App(IOptions<AppSettings> appSettings, ILogger<App> appLogger, IBot bot) {
            _applogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        public async Task Run() {
            _applogger.LogInformation("Starting app...");
            await _bot.RunAsync(_appSettings);
        }
    }
}