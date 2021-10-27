using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarketPeep.RestProviders.CoinGecko;
using MarketPeep.Models.CoinGecko;

namespace MarketPeep.Services.CoinGecko {

  public class CoinGeckoService : ICoinGeckoService {

    private readonly ICoinGeckoRestProvider _coinGeckoRestProvider;
    private readonly ILogger<ICoinGeckoRestProvider> _logger;

    public CoinGeckoService(ICoinGeckoRestProvider coinGeckoRestProvider,
      ILogger<ICoinGeckoRestProvider> logger) {
        _coinGeckoRestProvider = coinGeckoRestProvider;
        _logger = logger;
    }

    public async Task<IList<CoinList>> GetCoinListAsync(bool includePlatform = false) {
      return await _coinGeckoRestProvider.GetCoinListAsync(includePlatform);
    }

    public async Task<CoinDetail> GetCoinDetailAsync(string id = "bitcoin") {
      return await _coinGeckoRestProvider.GetCoinDetailAsync(id);
    }
  }
}