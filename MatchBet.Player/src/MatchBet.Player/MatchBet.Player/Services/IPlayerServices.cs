using MatchBet.Player.Contracts;

namespace MatchBet.Player.Services
{
    public interface IPlayerServices
    {
        void ValidateCreatePlayerRequest(CreatePlayerRequest playerRequest);
        Task<Models.Player?> GetPlayerByUsernameAsync(string username);
        Task SavePlayerAsync(CreatePlayerRequest createPlayerRequest);
    }
}
