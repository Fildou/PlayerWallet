using PlayerWallet.Models;

namespace PlayerWallet.Services
{
    // interface for wallet service
    public interface IWalletService
    {
        Task<bool> RegisterPlayerAsync(Guid playerId);
        Task<decimal?> GetPlayerBalanceAsync(Guid playerId);
        Task<IEnumerable<Transaction>> GetPlayerTransactionsAsync(Guid playerId);
        Task<string> CreditTransactionAsync(Guid playerId, Transaction transaction);
    }
}
