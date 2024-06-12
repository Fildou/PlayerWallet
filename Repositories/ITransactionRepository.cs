using PlayerWallet.Models;

namespace PlayerWallet.Repositories
{
    // interface for transaction repo
    public interface ITransactionRepository
    {
        // gets transaction by id
        Task<Transaction> GetTransactionAsync(Guid transactionId);
        // adds new transaction
        Task<bool> AddTransactionAsync(Transaction transaction);
        // gets a list of transactions for player by id
        Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid playerId);
    }
}
