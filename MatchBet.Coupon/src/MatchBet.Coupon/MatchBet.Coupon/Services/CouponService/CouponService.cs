using MatchBet.Coupon.Models;
using MatchBet.Coupon.Repositories.CouponRepository;

namespace MatchBet.Coupon.Services.CouponService
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task CreateCouponAsync(Helper.Contarct.CreateCouponRequest coupon)
        {
            var matchPredictList = new List<MatchPredict>();

            if(coupon.MatchPredicts is not null)
            {
                foreach (var item in coupon.MatchPredicts)
                {
                    matchPredictList.Add(new MatchPredict
                    {
                        IsActive = item.IsActive,
                        MatchId = item.MatchId,
                        Prediction = item.Prediction,
                        Rate = item.Rate,
                        Result = item.Result,
                    });
                }
            }
            Models.Coupon couponModel = new Models.Coupon{
                MatchPredicts = matchPredictList,
                IsActive = coupon.IsActive,
                OwnerId = coupon.OwnerId,
                Result = coupon.Result,
                TotalRate = coupon.TotalRate,
            };
            foreach(var data in couponModel.MatchPredicts)
            {
                data.CouponId = couponModel.Id;
            }
            await _couponRepository.CreateCouponAsync(couponModel);
        }

        public async Task<Models.Coupon> GetCouponByIdAsync(int id)
        {
            return await _couponRepository.GetCouponByIdAsync(id);
        }

        public async Task<List<Models.Coupon>> RefreshCouponsByUserId(int userId)
        {
            var coupons = await _couponRepository.GetCouponsByUserIdAsync(userId);
            foreach(var coupon in coupons)
            {
                await UpdateCouponStatus(coupon);
            }
            return coupons;
        }

        public async Task<Models.Coupon> UpdateCouponStatus(Models.Coupon coupon)
        {
            foreach(var matchPredict in coupon.MatchPredicts)
            {
                //maç servisinden bilgileri alıp maçları kontrol edicez.
            }
            _couponRepository.UpdateCouponAsync(coupon);
            return coupon;
        }
    }
}
