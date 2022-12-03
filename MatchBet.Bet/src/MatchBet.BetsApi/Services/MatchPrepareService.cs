using MatchBet.BetsApi.Entities;
using MatchBet.BetsApi.Entities.Bet;
using MatchBet.BetsApi.Helper;
using Newtonsoft.Json;
using RestSharp;

namespace MatchBet.BetsApi.Services;

public interface IMatchPrepareService
{
    void PrepareMatch();
    Task<List<MatchResponse>> GetAllDailyMatches();
}

public class MatchPrepareService : IMatchPrepareService
{
    public MatchPrepareService()
    {
        
    }

    public async void PrepareMatch() // Job günde 1 kere çalıştırıcak
    {

        var matchOddResponses = await GetMatchOddsData();
        var dailyMatches = await GetAllDailyMatches();
        foreach (var dailyMatch in dailyMatches)
        {
            var matchRate = matchOddResponses.FirstOrDefault(q => q.Id == dailyMatch.Id);
            if (matchRate is null)
            {
                continue;
            }
            dailyMatch.Bets = matchRate.Bookmakers.First().Bets;
        }
        
        // dailyMatches ==> To Mongo DB
    }

    public async Task<List<MatchResponse>> GetFromMongoDb()
    {
        var dataFromMongoDb = "asdjqwe";
        var dailyAllMatches = JsonConvert.DeserializeObject<List<MatchResponse>>(dataFromMongoDb);
        return dailyAllMatches;
    }
    private static async Task<List<MatchOddResponse>> GetMatchOddsData()
    {
        var date = DateTime.Now;
        var matchOddResponses = new List<MatchOddResponse>();
        int page = 1;
        while (true)
        {
            var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3");
            var request = new RestRequest("odds?date=" +date.Year + "-" + date.Month + "-" +date.Day + "&page="+page+"&bookmaker=8&bet=1");
            request.AddHeader("X-RapidAPI-Key", "fafa894307msh153846cd1ae874ep129895jsna7cdd3f3e836");
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            var response = await client.ExecuteAsync(request);

            var result = JsonConvert.DeserializeObject<HttpResponseMessageHelper<MatchOddResponse>>(response.Content);
            if (result.Response.Count == 0)
            {
                break;
            }
            matchOddResponses.AddRange(result.Response);
            page++;
        }

        return matchOddResponses;
    }

    public async Task<List<MatchResponse>> GetAllDailyMatches()
    {
        var date = DateTime.Now;
        var client = new RestClient("https://api-football-v1.p.rapidapi.com/v3");
        var dateQueryParams = "fixtures?date=" +date.Year + "-" + date.Month + "-" +date.Day;
        var request = new RestRequest($"{dateQueryParams}");
        request.AddHeader("X-RapidAPI-Key", "fafa894307msh153846cd1ae874ep129895jsna7cdd3f3e836");
        request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
        var response = await client.ExecuteAsync(request);

        var result = JsonConvert.DeserializeObject<HttpResponseMessageHelper<MatchResponse>>(response.Content);
        return result.Response;
    }
}