using Application.Accounts.Command;
using Application.Common;
using Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Coupons.Command
{
    public record InsertCouponCommand(int UserId, int Amount) : IRequest<string?>;

    public class InsertCouponHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<InsertCouponCommand, string?>
    {
        public async Task<string?> Handle(InsertCouponCommand request, CancellationToken cancellationToken)
        {
            var query = @"INSERT INTO coupons (accountid, code, amount) VALUES (@accountId, @code, @amount)";
            var coupon = Util.GetRandomCoupon();
            var result = await sqlDataAccess.ExecuteAsync(query, new { accountId = request.UserId, code = coupon, amount = request.Amount });

            return result == 1 ? coupon : null;
        }
    }
}
