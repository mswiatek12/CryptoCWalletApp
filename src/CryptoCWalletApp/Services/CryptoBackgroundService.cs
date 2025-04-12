using CryptoCWalletApp.Models;
using Newtonsoft.Json;

namespace CryptoCWalletApp.Services
{
    public class CryptoBackgroundService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CryptoBackgroundService> _logger;
        private readonly CryptoService _cryptoService;
        
        public CryptoBackgroundService(HttpClient httpClient, ILogger<CryptoBackgroundService> logger,CryptoService cryptoService, AppDbContext appDbContext)
        {
            _httpClient = httpClient;
            _appDbContext = appDbContext;
            _cryptoService = cryptoService;
            _logger = logger;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoCWalletApp");
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Background Crypto Fetching Service Is Working.");
                    var jsonResponse = await _cryptoService.FetchCoins();

                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        _logger.LogWarning("Failed to fetch data from CoinGecko.");
                        await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                        continue;
                    }


                    var coins = JsonConvert.DeserializeObject<List<Coin>>(jsonResponse);

                    if (coins == null || coins.Count == 0)
                    {
                        _logger.LogWarning("No coins found.");
                        await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                        continue;                    
                    }
                    
                    List<string> coinIds = coins
                        .Where(coin => coin.Id != null)
                        .Select(coin => coin.Id!)
                        .ToList();

                    jsonResponse = await _cryptoService.FetchCryptosData(coinIds);
                    var cryptoCurrencies = JsonConvert.DeserializeObject<Dictionary<string, CryptoCurrencyData>>(jsonResponse);
                    
                    if (cryptoCurrencies == null || cryptoCurrencies.Count == 0)
                    {
                        _logger.LogWarning("No cryptocurrency data found.");
                        await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                        continue;
                    }

                    List<CryptoCurrencyEntity> cryptoCurrencyEntities = new List<CryptoCurrencyEntity>();

                    foreach (var kvp in cryptoCurrencies)
                    {
                        var cryptoCurrencyEntity = new CryptoCurrencyEntity
                        {
                            Symbol = kvp.Key,
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
                }
                catch(OperationCanceledException)
                {
                    return ;
                }
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}