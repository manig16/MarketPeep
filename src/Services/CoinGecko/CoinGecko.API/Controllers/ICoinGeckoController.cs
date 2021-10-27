using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketPeep.Controllers.CoinGecko {

  public interface ICoinGeckoController {
    Task<IActionResult> GetCoinList(bool includePlatform = false);
    Task<IActionResult> GetCoinDetail(string id = "bitcoin");
  }
}