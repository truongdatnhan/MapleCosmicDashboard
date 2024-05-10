using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using System.Security.Cryptography;

namespace Application.Accounts.Query
{
    public record GetAccountQuery(int Id) : IRequest<Account?>;

    public class GetAccountHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetAccountQuery, Account?>
    {
        public async Task<Account?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await sqlDataAccess.QueryFirstOrDefaultAsync<Account>("SELECT * FROM Accounts WHERE Id = @id ", new { id = request.Id });
        }

    }
}
