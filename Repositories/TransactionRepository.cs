using PlayerWallet.Models;
using System.Collections.Concurrent;

namespace PlayerWallet.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        // dict of all transactions
        private readonly ConcurrentDictionary<Guid, Transaction> transactions = new ConcurrentDictionary<Guid, Transaction>();

        // dict of players transactions
        private readonly ConcurrentDictionary<Guid, List<Transaction>> playerTransactions = new ConcurrentDictionary<Guid, List<Transaction>>();

        // get transaction based on transaction id
        public Task<Transaction> GetTransactionAsync(Guid transactionId) =>
            Task.FromResult(transactions.TryGetValue(transactionId, out var transaction) ? transaction : null);

        // add transaction to dict of transactions
        public Task<bool> AddTransactionAsync(Transaction transaction)
        {
            if (transactions.TryAdd(transaction.TransactionId, transaction))
            {
                if (!playerTransactions.ContainsKey(transaction.PlayerId))
                {
                    playerTransactions[transaction.PlayerId] = new List<Transaction>();
                }
                playerTransactions[transaction.PlayerId].Add(transaction);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        // gets list of transactions linked to player id
        public Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid playerId) =>
            Task.FromResult(playerTransactions.TryGetValue(playerId, out var transactionList)
                ? (IEnumerable<Transaction>)transactionList
                : new List<Transaction>());
    }
}
