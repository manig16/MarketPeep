using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPeep.Controllers;
using MarketPeep.Services.CoinGecko;
using MarketPeep.Models.CoinGecko;
using MarketPeep.Controllers.CoinGecko;
using Xunit;
using Moq;

namespace CoinGecko.UnitTests.ControllerTests
{
    public class CoinGeckoControllerTest
    {
        CoinGeckoController _controller;
        Mock<ICoinGeckoService> _coinGeckoServiceMock;
        Mock<ILogger<CoinGeckoController>> _loggerMock;

        public CoinGeckoControllerTest() {
        SetupBeforeEachTest();
        }

        private void SetupBeforeEachTest() {
        _coinGeckoServiceMock = new Mock<ICoinGeckoService>();
        _loggerMock = new Mock<ILogger<CoinGeckoController>>();
        _controller = new CoinGeckoController(_coinGeckoServiceMock.Object,
            _loggerMock.Object);
        }

        [Fact]
        public async Task GetCoinList_OK() {
            _coinGeckoServiceMock.Setup(svc => 
                svc.GetCoinListAsync(false)).Returns( async () => 
            {
                var coinList = new List<CoinList>() {
                    new CoinList() {
                        Id = "btc",
                        Symbol = "btc",
                        Name = "Bitcoin"
                    },
                    new CoinList() {
                    Id = "dot",
                    Symbol = "dot",
                    Name = "Polkadot"
                    },      
                };
                await Task.Yield();
                return coinList;
            });

            var result = await _controller.GetCoinList(false);
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as IList<CoinList>;

            Assert.NotNull(actualResult);
            Assert.Equal(2, actualResult.Count);
            Assert.Equal("btc", actualResult[0].Id);
            Assert.Equal("dot", actualResult[1].Id);
        }

        [Fact]
        public async Task GetCoinList_Error() {
            _coinGeckoServiceMock.Setup(svc => 
                svc.GetCoinListAsync(false)).Returns( async () => 
            {
                List<CoinList> coinList = null;
                await Task.Yield();
                return coinList;
            });

            var result = await _controller.GetCoinList(false);
            var okResult = result as NotFoundObjectResult;
            var actualResult = okResult.Value;

            Assert.Null(actualResult);
        }
    }
}
