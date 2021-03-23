using Middleware;

namespace Master {
    class Program {
        static void Main(string[] args) {
            var config = Util.GetConfiguration();
            RunBot(config);
        }

        static void RunBot(Configuration config) => new Bot().RunAsync(config).GetAwaiter().GetResult();
    }
}