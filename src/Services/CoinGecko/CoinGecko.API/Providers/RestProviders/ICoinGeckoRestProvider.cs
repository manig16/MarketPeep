using System.Threading.Tasks;
using System.Collections.Generic;
using MarketPeep.Models.CoinGecko;

namespace MarketPeep.RestProviders.CoinGecko {
  public interface ICoinGeckoRestProvider {
    Task<IList<CoinList>> GetCoinListAsync(bool includePlatform = false);
    Task<CoinDetail> GetCoinDetailAsync(string id = "bitcoin");
  }
}