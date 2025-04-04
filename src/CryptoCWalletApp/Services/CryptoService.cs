namespace CryptoCWalletApp.Services
{
    public class CryptoService
    {
        private readonly HttpClient _httpClient;

        public CryptoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoCWalletApp");
        }

        public async Task<String> FetchCoins()
        {
            string url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd";
            return await _httpClient.GetStringAsync(url);
        }

        public async Task<String> FetchCryptosData(List<string> coinIds)
        {
            var idsParam = string.Join(",", coinIds);
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={idsParam}&vs_currencies=usd&include_market_cap=true&include_24hr_vol=true&include_24hr_change=true&include_last_updated_at=true";
            return await _httpClient.GetStringAsync(url);
        }
    }
}