using PlayerWallet.Models;
using System.Collections.Concurrent;

namespace PlayerWallet.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        // dict of wallets
        private readonly ConcurrentDictionary<Guid, Wallet> wallets = new ConcurrentDictionary<Guid, Wallet>();

        // get players wallet based on player id
        public Task<Wallet> GetWalletAsync(Guid playerId) =>
            Task.FromResult(wallets.TryGetValue(playerId, out var wallet) ? wallet : null);

        // update balance on players wallet
        public Task<bool> UpdateWalletAsync(Wallet wallet)
        {
            wallets[wallet.PlayerId] = wallet;
            return Task.FromResult(true);
        }
    }
}
