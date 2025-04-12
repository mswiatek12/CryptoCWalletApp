using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CryptoCWalletApp.Models
{
    public class CryptoCurrencyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Symbol { get; set; }
        public decimal? Usd { get; set; }
        public decimal? UsdMarketCap { get; set; }
        public decimal? Usd24HVol { get; set; }
        public decimal? Usd24HChange { get; set; }
        public decimal? LastUpdatedAt { get; set; }
    }
}