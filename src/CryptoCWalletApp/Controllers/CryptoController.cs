using System.Runtime.InteropServices;
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
        private readonly AppDbContext _appDbContext;

        public CryptoController(CryptoService cryptoService, AppDbContext appDbContext)
        {
            _cryptoService = cryptoService;
            _appDbContext = appDbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCryptoData()
        {
            var jsonResponse = await _cryptoService.FetchCoins();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return StatusCode(500, new { message = "Failed to fetch data from CoinGecko." });
            }

            var coins = JsonConvert.DeserializeObject<List<Coin>>(jsonResponse);

            if (coins == null || coins.Count == 0)
            {
                return NotFound(new { message = "No coins found." });
            }

            List<string> coinIds = coins
                .Where(coin => coin.Id != null)
                .Select(coin => coin.Id!)
                .ToList();

            jsonResponse = await _cryptoService.FetchCryptosData(coinIds);

            var cryptoCurrencies = JsonConvert.DeserializeObject<Dictionary<string, CryptoCurrencyData>>(jsonResponse);

            if (cryptoCurrencies == null || cryptoCurrencies.Count == 0)
            {
                return NotFound(new { message = "No cryptocurrency data found." });
            }

            List<CryptoCurrencyEntity> cryptoCurrencyEntities = new List<CryptoCurrencyEntity>();

            foreach (var kvp in cryptoCurrencies)
            {
                var cryptoCurrencyEntity = new CryptoCurrencyEntity
                {
                    Id = kvp.Key,
                    Usd = kvp.Value.Usd,
                    UsdMarketCap = kvp.Value.UsdMarketCap,
                    Usd24HVol = kvp.Value.Usd24HVol,
                    Usd24HChange = kvp.Value.Usd24HChange,
                    LastUpdatedAt = kvp.Value.LastUpdatedAt
                };

                cryptoCurrencyEntities.Add(cryptoCurrencyEntity);
            }

            _appDbContext.AddRange(cryptoCurrencyEntities);
            await _appDbContext.SaveChangesAsync();

            return Ok(cryptoCurrencyEntities);
        }
    }
}