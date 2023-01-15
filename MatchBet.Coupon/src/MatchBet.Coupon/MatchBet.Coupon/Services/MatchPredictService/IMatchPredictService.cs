namespace MatchBet.Coupon.Services.MatchPredictService
{
    public interface IMatchPredictService
    {
        Task SaveMatchPredictAsync(Models.MatchPredict matchPredict);
        Task<Models.MatchPredict> GetMatchPredictByIdAsync(int id);

    }
}
