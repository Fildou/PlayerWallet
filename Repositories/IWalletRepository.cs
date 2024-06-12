using PlayerWallet.Models;

namespace PlayerWallet.Repositories
{
    // interface for wallet repo
    public interface IWalletRepository
    {
        // gets wallet by player id
        Task<Wallet> GetWalletAsync(Guid playerId);

        // updates wallets balance
        Task<bool> UpdateWalletAsync(Wallet wallet);
    }
}
