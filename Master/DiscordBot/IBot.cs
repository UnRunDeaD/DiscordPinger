using System.Threading.Tasks;

namespace Master.DiscordBot {
    public interface IBot {
        public Task RunAsync(AppSettings settings);
    }
}