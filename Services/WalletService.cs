using PlayerWallet.Models;
using PlayerWallet.Repositories;

namespace PlayerWallet.Services
{
    // service for handling wallet operations
    public class WalletService : IWalletService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        // constructor for injecting dependencies
        public WalletService(IPlayerRepository playerRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _playerRepository = playerRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        // register new player with initial wallet balance of 0
        public async Task<bool> RegisterPlayerAsync(Guid playerId)
        {
            if (await _playerRepository.GetPlayerAsync(playerId) != null)
                return false;

            var player = new Player { PlayerId = playerId };
            var wallet = new Wallet { PlayerId = playerId, Balance = 0 };

            return await _playerRepository.RegisterPlayerAsync(player) && await _walletRepository.UpdateWalletAsync(wallet);
        }

        // gets balance of players wallet based on player id
        public async Task<decimal?> GetPlayerBalanceAsync(Guid playerId)
        {
            var wallet = await _walletRepository.GetWalletAsync(playerId);
            return wallet?.Balance;
        }

        // gets list of transactions based on player id
        public async Task<IEnumerable<Transaction>> GetPlayerTransactionsAsync(Guid playerId)
        {
            return await _transactionRepository.GetTransactionsAsync(playerId);
        }

        // credits transaction to players wallet, checks if player has enough funds or if wallet exists
        public async Task<string> CreditTransactionAsync(Guid playerId, Transaction transaction)
        {
            var existingTransaction = await _transactionRepository.GetTransactionAsync(transaction.TransactionId);
            if (existingTransaction != null)
                return "Transaction already processed.";

            var wallet = await _walletRepository.GetWalletAsync(playerId);
            if (wallet == null)
                return "Wallet not found.";

            decimal newBalance = wallet.Balance;
            switch (transaction.Type)
            {
                case TransactionType.Deposit:
                case TransactionType.Win:
                    newBalance += transaction.Amount;
                    break;
                case TransactionType.Stake:
                    newBalance -= transaction.Amount;
                    if (newBalance < 0)
                        return "Insufficient funds.";
                    break;
            }

            wallet.Balance = newBalance;
            await _walletRepository.UpdateWalletAsync(wallet);
            await _transactionRepository.AddTransactionAsync(transaction);
            return "Transaction accepted.";
        }
    }
}
