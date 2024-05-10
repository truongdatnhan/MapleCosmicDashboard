using Domain.Entities;
using Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Query
{
    public record GetAccountListQuery() : IRequest<IEnumerable<Account>>;

    public class GetAccountListHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetAccountListQuery, IEnumerable<Account>>
    {
        public async Task<IEnumerable<Account>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
        {
            return await sqlDataAccess.QueryAsync<Account>("SELECT * FROM Accounts");
        }
    }
}
