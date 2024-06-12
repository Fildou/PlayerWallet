using PlayerWallet.Models;
using System.Collections.Concurrent;

namespace PlayerWallet.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {   
        // private dict for players
        private readonly ConcurrentDictionary<Guid, Player> players = new ConcurrentDictionary<Guid, Player>();

        // task that ansynchronnly gets player from list of players based on id
        public Task<Player> GetPlayerAsync(Guid playerId) =>
            Task.FromResult(players.TryGetValue(playerId, out var player) ? player : null);

        // adding new player to players dict
        public Task<bool> RegisterPlayerAsync(Player player) =>
            Task.FromResult(players.TryAdd(player.PlayerId, player));
    }
}
