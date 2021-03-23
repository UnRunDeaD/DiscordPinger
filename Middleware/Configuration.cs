using Newtonsoft.Json;

namespace Middleware {
    public struct Configuration {
        [JsonProperty("token")] public string Token { get; private set; }
        [JsonProperty("prefix")] public string Prefix { get; private set; }
        [JsonProperty("mongoDbConString")] public string MongoDbConString { get; private set; }
    }
}