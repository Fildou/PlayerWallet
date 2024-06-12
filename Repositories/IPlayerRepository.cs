using PlayerWallet.Models;

namespace PlayerWallet.Repositories
{
    // interface for player repo
    public interface IPlayerRepository
    {
        // gets player by id
        Task<Player> GetPlayerAsync(Guid playerId);
        // registers new player
        Task<bool> RegisterPlayerAsync(Player player);
    }
}
