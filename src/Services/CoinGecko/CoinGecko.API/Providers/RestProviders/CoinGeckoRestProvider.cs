using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using MarketPeep.Models.CoinGecko;
using Newtonsoft.Json;

namespace MarketPeep.RestProviders.CoinGecko {

  public class CoinGeckoRestProvider : ICoinGeckoRestProvider {

    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<CoinGeckoRestProvider> _logger;
    private HttpClient _client;
    public CoinGeckoRestProvider(IHttpClientFactory clientFactory, 
      ILogger<CoinGeckoRestProvider> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _client = _clientFactory.CreateClient("CoinGecko");
    }

    public async Task<IList<CoinList>> GetCoinListAsync(bool includePlatform = false) {
      var request = new HttpRequestMessage(HttpMethod.Get, 
        $"/api/v3/coins/list?include_platform={includePlatform}");
      var response = await _client.SendAsync(request);
      if(response.StatusCode == HttpStatusCode.OK) {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IList<CoinList>>(content);
      }
      return null;

    }
    public async Task<CoinDetail> GetCoinDetailAsync(string id = "bitcoin") {
      var request = new HttpRequestMessage(HttpMethod.Get, 
        $"/api/v3/coins/{id}?localization=false&tickers=false&market_data=false&community_data=true&developer_data=true&sparkline=false");
      var response = await _client.SendAsync(request);
      if(response.StatusCode == HttpStatusCode.OK) {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CoinDetail>(content);
      }
      return null;
    }
  }
}