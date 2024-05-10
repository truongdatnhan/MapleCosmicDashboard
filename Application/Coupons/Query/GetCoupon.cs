using Application.Common;
using Application.Coupons.Command;
using Domain.Entities;
using Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Coupons.Query
{
    public record GetCouponQuery(int UserId, string Coupon) : IRequest<Coupon?>;

    public class GetCouponHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetCouponQuery, Coupon?>
    {
        public async Task<Coupon?> Handle(GetCouponQuery request, CancellationToken cancellationToken)
        {
            var query = @"SELECT * FROM coupons WHERE accountid = @accountId AND code = @code";
            var result = await sqlDataAccess.QueryFirstOrDefaultAsync<Coupon>(query, new { accountId = request.UserId, code = request.Coupon});

            return result;
        }
    }
}
