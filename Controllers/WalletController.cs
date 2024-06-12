using Microsoft.AspNetCore.Mvc;
using PlayerWallet.Models;
using PlayerWallet.Services;

namespace PlayerWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        // constructor for injecting WalletService
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        // registers new player in system
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPlayer([FromBody] Guid playerId)
        {
            if (await _walletService.RegisterPlayerAsync(playerId))
                return Ok();
            return BadRequest("Player already registered.");
        }

        // retreives balance of players wallet
        [HttpGet("{playerId}/balance")]
        public async Task<IActionResult> GetBalance(Guid playerId)
        {
            var balance = await _walletService.GetPlayerBalanceAsync(playerId);
            if (balance.HasValue)
                return Ok(balance.Value);
            return NotFound("Player or wallet not found.");
        }

        // adds transaction to players wallet
        [HttpPost("{playerId}/transaction")]
        public async Task<IActionResult> CreditTransaction(Guid playerId, [FromBody] Transaction transaction)
        {
            transaction.PlayerId = playerId;
            var result = await _walletService.CreditTransactionAsync(playerId, transaction);
            if (result == "Transaction accepted.")
                return Ok(result);
            return BadRequest(result);
        }

        // gets list of transactions based on player
        [HttpGet("{playerId}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid playerId)
        {
            var transactions = await _walletService.GetPlayerTransactionsAsync(playerId);
            return Ok(transactions);
        }
    }
}
