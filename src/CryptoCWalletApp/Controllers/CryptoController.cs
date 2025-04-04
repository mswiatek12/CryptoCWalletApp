using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CryptoCWalletApp.Services;
using CryptoCWalletApp.Models;

namespace CryptoCWalletApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoService _cryptoService;

        public CryptoController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCryptoData()
        {
            var jsonResponse = await _cryptoService.FetchCoins();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return StatusCode(500, new { message = "Failed to fetch data from CoinGecko." });
            }
            var coins =  JsonConvert.DeserializeObject<List<Coin>>(jsonResponse);
            if (coins == null || coins.Count == 0)
            {
                return NotFound(new { message = "No coins found." });
            }

            List<string> coinIds = coins.Select(coin => coin.Id).ToList();
            
            jsonResponse = await _cryptoService.FetchCryptosData(coinIds);
            
            var cryptoCurrencies = JsonConvert.DeserializeObject<Dictionary<string, CryptoCurrencyData>>(jsonResponse);
            
            return Ok(cryptoCurrencies);
        }
    }
}