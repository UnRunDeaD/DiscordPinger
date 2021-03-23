using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using Middleware;

namespace Master {
    public class Bot {
        private DiscordClient Client { get; set; }
        private CommandsNextExtension Commands { get; set; }

        public async Task RunAsync(Configuration config) {
            Client = CreateClient(config);
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
        
        private DiscordClient CreateClient (Configuration config) {
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