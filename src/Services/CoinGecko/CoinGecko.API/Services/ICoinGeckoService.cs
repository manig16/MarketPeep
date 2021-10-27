using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPeep.Models.CoinGecko;


namespace MarketPeep.Services.CoinGecko {

  public interface ICoinGeckoService {
    Task<IList<CoinList>> GetCoinListAsync(bool includePlatform = false);
    Task<CoinDetail> GetCoinDetailAsync(string id = "bitcoin");

  }
}