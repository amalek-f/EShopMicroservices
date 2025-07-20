using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(
      DiscountContext discountContext,
      ILogger<DiscountService> logger)
      : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountContext _discountContext = discountContext;
        private readonly ILogger<DiscountService> _logger = logger;

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            coupon ??= new Coupon
            {
                ProductName = request.ProductName,
                Amount = 0,
                Description = "No Discount"
            };

            _logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            _discountContext.Coupons.Add(coupon);
            await _discountContext.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully created for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            _discountContext.Coupons.Update(coupon);
            await _discountContext.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully updated for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for ProductName: {request.ProductName}"));
        
            _discountContext.Coupons.Remove(coupon);
            await _discountContext.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully deleted for ProductName : {ProductName}", request.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
