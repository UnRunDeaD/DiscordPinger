using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;

namespace Master.DiscordBot {
    public class Bot: IBot {
        private DiscordClient _client { get; set; }
        private CommandsNextExtension _commands { get; set; }
        private readonly ILogger<Bot> _logger;

        public Bot(ILogger<Bot> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync(AppSettings settings) {
            _client = CreateClient(settings);
            await _client.ConnectAsync();
            _logger.LogInformation("Bot connected");
            await Task.Delay(-1);
        }
        
        private DiscordClient CreateClient (AppSettings config) {
            try {
                var discordConfig = new DiscordConfiguration {
                    Token = config.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = LogLevel.Information
                };
                
                var client = new DiscordClient(discordConfig);
                client.Ready += OnReadyAsync;

                var commandsConfig = new CommandsNextConfiguration {
                    StringPrefixes = new[] {config.Prefix},
                    EnableMentionPrefix = false,
                    EnableDms = true,
                    CaseSensitive = false,
                    DmHelp = true
                };

                var commands = client.UseCommandsNext(commandsConfig);

                client.UseInteractivity(new InteractivityConfiguration {
                    Timeout = TimeSpan.FromMinutes(30)
                });
                
                return client;
            }
            catch (Exception exc) {
                Console.WriteLine(exc.Demystify());
                throw;
            }
        }
        
        private async Task OnReadyAsync(DiscordClient sender, ReadyEventArgs e) {
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}