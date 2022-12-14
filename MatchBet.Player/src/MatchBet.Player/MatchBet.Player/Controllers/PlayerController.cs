using System.Net;
using MatchBet.Player.Contracts;
using MatchBet.Player.Data;
using MatchBet.Player.Helper;
using MatchBet.Player.Repository;
using MatchBet.Player.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchBet.Player.Controllers
{
    [Route("players")]
    public class PlayerController: ControllerBase
    {
        private readonly IPlayerServices _playerServices;
        public PlayerController(IPlayerServices playerServices)
        {
            _playerServices = playerServices;
        }

        [Route("{username}")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns player by username.", typeof(Models.Player))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Returns user notfount.", typeof(NotFoundResult))]    
        public async Task<IActionResult> GetPlayer(string username)
        {
            var player = await _playerServices.GetPlayerByUsernameAsync(username);
            if (player is null)
            {
                return NotFound(Constants.PlayerIsNotFound);
            }
            return Ok(player);
        }
        
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Save new player", typeof(Models.Player))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "BadRequest.", typeof(BadRequest))]    
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequest request)
        {
            _playerServices.ValidateCreatePlayerRequest(request);
            var existingPlayer = await _playerServices.GetPlayerByUsernameAsync(request.UserName);
            if (existingPlayer is not null)
            {
                return BadRequest(Constants.PlayerIsAlreadyExist);
            }

            await _playerServices.SavePlayerAsync(request);
            return Ok(Constants.Success);
        }
    }
}

    