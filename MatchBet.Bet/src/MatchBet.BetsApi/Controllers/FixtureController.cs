using System.Runtime.InteropServices;
using MatchBet.BetsApi.Entities;
using MatchBet.BetsApi.Entities.Bet;
using MatchBet.BetsApi.Helper;
using MatchBet.BetsApi.Options;
using MatchBet.BetsApi.Repositories;
using MatchBet.BetsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace MatchBet.BetsApi.Controllers;

[ApiController]
[Route("fixtures")]
public class FixtureController : ControllerBase
{
    private readonly ILogger<FixtureController> _logger;
    private readonly IMatchPrepareService _matchPrepareService;
    private readonly IMatchRepository _matchRepository;
    private readonly IOptions<MatchConfiguration> _matchConfiguration;
    

    public FixtureController(ILogger<FixtureController> logger, IMatchPrepareService matchPrepareService, IMatchRepository matchRepository, IOptions<MatchConfiguration> matchConfiguration)
    {
        _logger = logger;
        _matchPrepareService = matchPrepareService;
        _matchRepository = matchRepository;
        _matchConfiguration = matchConfiguration;
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> GetFixturesByDate()
    {
        var dailyMatches = _matchRepository.GetAsync();
        var result = JsonConvert.DeserializeObject<List<MatchResponse>>(dailyMatches.Matches);
        return Ok(result);
    }
    

    // [Route("")]
    // [HttpDelete]
    // public async Task<IActionResult> DeleteAllAsync()
    // {
    //     await _matchRepository.DeleteAllAsync();
    //     return Ok("Silme başarılı");
    // }
    //
    
    [Route("live")]
    [HttpGet]
    public async Task<IActionResult> GetFixturesLive()
    {
        var key = _matchConfiguration.Value.Key;
        var leagues = new List<Leagues>()
        {
            Leagues.WorldCup, Leagues.ChampionsLeague, Leagues.UefaEuropeLeague,
            Leagues.PremierLeague,  Leagues.BundesLiga,
            Leagues.France, Leagues.LaLiga,  Leagues.SuperLig, Leagues.ItalySerieA
        };
        var matchResponses = new List<MatchResponse>();

        foreach (var league in leagues)
        {
            var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3");
            var request = new RestRequest("fixtures?live=all&league="+(int)league);
            request.AddHeader("X-RapidAPI-Key", key); 
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful is false)
            {
                return Ok(matchResponses);
            }
            var result = JsonConvert.DeserializeObject<HttpResponseMessageHelper<MatchResponse>>(response.Content);
            matchResponses.AddRange(result.Response);
        }
        
        
        return Ok(matchResponses);
    }
}