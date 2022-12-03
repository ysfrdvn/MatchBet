using MatchBet.Player.Contracts;
using MatchBet.Player.Helper;
using MatchBet.Player.Repository;

namespace MatchBet.Player.Services
{
    public class PlayerServices : IPlayerServices
    {
        private readonly IPlayerRepository _playerRepository;
    
        public PlayerServices(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
    
        public void ValidateCreatePlayerRequest(CreatePlayerRequest playerRequest)
        {
            foreach (var property in playerRequest.GetType().GetProperties())
            {
                if (string.IsNullOrEmpty(property.GetValue(playerRequest)?.ToString()))
                {
                    throw new ArgumentException($"{property.Name} boş bırakılamaz");
                }
            }
        }

        public async Task<Models.Player?> GetPlayerByUsernameAsync(string username)
        {
            var player = await _playerRepository.GetPlayerByUserNameAsync(username);
            return player;
        }
        
        public async Task SavePlayerAsync(CreatePlayerRequest createPlayerRequest)
        {
            var playerModel = new Models.Player
            {
                UserName = createPlayerRequest.UserName,
                Password = createPlayerRequest.Password,
                Name = createPlayerRequest.Name,
                Surname = createPlayerRequest.Surname,
                Credit = 3,
                Score = 0
            };
            await _playerRepository.SavePlayerAsync(playerModel);
        }
    }
}

