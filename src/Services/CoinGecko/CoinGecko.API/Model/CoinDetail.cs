using Newtonsoft.Json;

namespace MarketPeep.Models.CoinGecko {

  public class CoinDetail {

  [JsonProperty("id")]
  public string Id { get; set; }

  [JsonProperty("symbol")]
  public string Symbol { get; set; }

  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("block_time_in_minutes")]
  public string BlockTimeInMinutes { get; set; }

  [JsonProperty("hashing_algorithm")]
  public string HashingAlgotithm { get; set; }

  [JsonProperty("genesis_date")]
  public string GenesisDate { get; set; }

  [JsonProperty("repos_url")]
  public Repos ReposURL { get; set; }

  }

  public class Repos {
    [JsonProperty("github")]
    public string[] Github { get; set; }

  [JsonProperty("bitbucket")]
    public string[] BitBucket { get; set; }
  }
}