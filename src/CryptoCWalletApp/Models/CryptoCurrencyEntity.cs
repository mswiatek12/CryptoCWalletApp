using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CryptoCWalletApp.Models
{
    public class CryptoCurrencyEntity
    {
        [Key]
        public string Id { get; set; }

        public decimal? Usd { get; set; }
        public decimal? UsdMarketCap { get; set; }
        public decimal? Usd24HVol { get; set; }
        public decimal? Usd24HChange { get; set; }
        public decimal? LastUpdatedAt { get; set; }
    }
}