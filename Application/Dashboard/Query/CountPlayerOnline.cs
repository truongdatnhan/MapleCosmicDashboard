using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using System.Security.Cryptography;

namespace Application.Dashboard.Query
{
    public record CountPlayerOnlineQuery() : IRequest<int>;

    public class CountPlayerOnlineHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<CountPlayerOnlineQuery, int>
    {
        public async Task<int> Handle(CountPlayerOnlineQuery request, CancellationToken cancellationToken)
        {
            return await sqlDataAccess.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Accounts WHERE loggedin = 2 ");
        }

    }
}
