using MatchBet.Player.Data;
using Microsoft.EntityFrameworkCore;

namespace MatchBet.Player.Repository
{
    public class PlayerRepository: IPlayerRepository
    {
        private readonly DataContext _dataContext;
        public PlayerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Models.Player?> GetPlayerByUserNameAsync(string username)
        {
            return await _dataContext.Players.FirstOrDefaultAsync(q => q.UserName == username);
;       }
        
        public async Task SavePlayerAsync(Models.Player player)
        {
            await _dataContext.Players.AddAsync(player);
            await _dataContext.SaveChangesAsync();
        }
    }
}

