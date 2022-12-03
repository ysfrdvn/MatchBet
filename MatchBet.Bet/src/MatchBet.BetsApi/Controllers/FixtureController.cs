using MatchBet.BetsApi.Entities;
using MatchBet.BetsApi.Helper;
using MatchBet.BetsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace MatchBet.BetsApi.Controllers;

[ApiController]
[Route("fixtures")]
public class FixtureController : ControllerBase
{
    private readonly ILogger<FixtureController> _logger;
    private readonly IMatchPrepareService _matchPrepareService;

    public FixtureController(ILogger<FixtureController> logger, IMatchPrepareService matchPrepareService)
    {
        _logger = logger;
        _matchPrepareService = matchPrepareService;
    }

    // [Route("{date}")]
    // [HttpGet]
    // public async Task<IActionResult> GetFixturesByDate(DateTime date)
    // {
    //     var result = _matchPrepareService.GetAllDailyMatches();
    //     return Ok(result);
    // }
    
    // [Route("match-prepare")]
    // [HttpGet]
    // public async Task<IActionResult> PrepareMatch()
    // {
    //     _matchPrepareService.PrepareMatch();
    //     return Ok();
    // }
    
    [Route("live")]
    [HttpGet]
    public async Task<IActionResult> GetFixturesLive()
    {
        var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3");
        var request = new RestRequest("fixtures?live=all");
        request.AddHeader("X-RapidAPI-Key", "fafa894307msh153846cd1ae874ep129895jsna7cdd3f3e836"); 
        request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
        var response = await client.ExecuteAsync(request);
        var result = JsonConvert.DeserializeObject<HttpResponseMessageHelper<MatchResponse>>(response.Content);
        return Ok(result);
    }
}