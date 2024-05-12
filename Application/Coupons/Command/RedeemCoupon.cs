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
    public record RedeemCouponCommand(int UserId, string Coupon, int Amount, bool IsDaily = true) : IRequest<bool>;


    public class RedeemCouponHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<RedeemCouponCommand, bool>
    {
        public async Task<bool> Handle(RedeemCouponCommand request, CancellationToken cancellationToken)
        {
            sqlDataAccess.GetDbConnection().Open();
            using (var transaction = sqlDataAccess.GetDbConnection().BeginTransaction())
            {
                try
                {
                    var updateCouponQuery = @"UPDATE coupons SET used = 1, redeemDate = NOW() WHERE code = @code ";
                    var updateCouponResult = await sqlDataAccess.ExecuteAsync(updateCouponQuery, new { code = request.Coupon }, transaction: transaction);

                    if (updateCouponResult == 0)
                    {
                        throw new Exception("Unknown coupon code");
                    }

                    int streakDay = 0;

                    if(request.IsDaily)
                    {
                        streakDay = 1;
                    }

                    var updateNxQuery = @"UPDATE accounts SET nxCredit = nxCredit + @amount, lastRedeem = NOW(), streak = streak + @streak WHERE id = @accountId ";
                    var updateNxResult = await sqlDataAccess.ExecuteAsync(updateNxQuery, new { amount = request.Amount, streak = streakDay, accountId = request.UserId }, transaction: transaction);

                    if (updateNxResult == 0)
                    {
                        throw new Exception("Unknown error when redeem coupon");
                    }

                    transaction.Commit();

                    return updateNxResult == 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
