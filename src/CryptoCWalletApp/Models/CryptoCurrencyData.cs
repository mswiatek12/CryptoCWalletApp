using Newtonsoft.Json;

namespace CryptoCWalletApp.Models
{
    public class CryptoCurrencyData
    {
        [JsonProperty("usd")]
        public decimal? Usd { get; set; }
        
        [JsonProperty("usd_market_cap")]
        public decimal? UsdMarketCap { get; set; }
        
        [JsonProperty("usd_24h_vol")]
        public decimal? Usd24HVol { get; set; }
        
        [JsonProperty("usd_24h_change")]
        public decimal? Usd24HChange { get; set; }
        
        [JsonProperty("last_updated_at")]
        public decimal? LastUpdatedAt { get; set; }
    }
}