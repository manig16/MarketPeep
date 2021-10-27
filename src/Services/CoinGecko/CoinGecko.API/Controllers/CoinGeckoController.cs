using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MarketPeep.Services.CoinGecko;

namespace MarketPeep.Controllers.CoinGecko {

  [ApiController]
  [Route("api/v1/[controller]")]
  public class CoinGeckoController : ControllerBase, ICoinGeckoController {
    private readonly ICoinGeckoService _coinGeckoService;
    private ILogger<CoinGeckoController> _logger;

    public CoinGeckoController(ICoinGeckoService coinGeckoService,
      ILogger<CoinGeckoController> logger ) {
        _coinGeckoService = coinGeckoService;
        _logger = logger;
    }

    [HttpGet("GetCoinList")]
    public async Task<IActionResult> GetCoinList(bool includePlatform = false) {
        var coinList = await _coinGeckoService.GetCoinListAsync(includePlatform);
        if(coinList == null) {
          return NotFound(null);
        }
        return Ok(coinList);
    }

    [HttpGet("GetCoinDetail/{id}")]
    public async Task<IActionResult> GetCoinDetail(string id = "bitcoin") {
      var coinDetail = await _coinGeckoService.GetCoinDetailAsync(id);
      if(coinDetail == null) {
        return NotFound(null);
      }
      return Ok(coinDetail);
    }
  }
}