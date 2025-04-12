using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CryptoCWalletApp.Services;
using CryptoCWalletApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoCWalletApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CryptoController(CryptoService cryptoService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCryptoData()
        {
            var cryptoCurrencyEntities = await _appDbContext.CryptoCurrency.ToListAsync();

            return Ok(cryptoCurrencyEntities);
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetCryptoDataBySymbol(string symbol)
        {
            var cryptoCurrencyEntities =
                await _appDbContext.CryptoCurrency.Where(c => c.Symbol == symbol).ToListAsync();
            return Ok(cryptoCurrencyEntities);
        }

    }
}