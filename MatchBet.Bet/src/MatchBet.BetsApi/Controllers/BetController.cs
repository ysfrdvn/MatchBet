using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using MatchBet.BetsApi.Entities;
using MatchBet.BetsApi.Entities.Bet;
using MatchBet.BetsApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MatchBet.BetsApi.Controllers;

[ApiController]
[Route("bets")]
public class BetController : ControllerBase
{
    private readonly ILogger<BetController> _logger;

    public BetController(ILogger<BetController> logger)
    {
        _logger = logger;
    }

    [Route("matches/{page}")]
    [HttpGet]
    public async Task<IActionResult> GetMatchesBet(DateTime date, short page)
    {
        var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3");
        var request = new RestRequest("odds?date=" +date.Year + "-" + date.Month + "-" +date.Day + "&page="+page+"&bookmaker=8&bet=1");
        request.AddHeader("X-RapidAPI-Key", "fafa894307msh153846cd1ae874ep129895jsna7cdd3f3e836");
        request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
        var response = await client.ExecuteAsync(request);

        var result = JsonConvert.DeserializeObject<HttpResponseMessageHelper<MatchOddResponse>>(response.Content);
        return Ok(result);
    }
    
    
}