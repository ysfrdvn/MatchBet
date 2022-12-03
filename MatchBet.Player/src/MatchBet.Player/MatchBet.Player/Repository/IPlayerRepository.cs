namespace MatchBet.Player.Repository
{
    public interface IPlayerRepository
    {
        Task<Models.Player?> GetPlayerByUserNameAsync(string username);
        Task SavePlayerAsync(Models.Player player);
    }
}

