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
    public record GetAccountLoginQuery(string Username, string Password) : IRequest<Account?>;

    public class GetAccountLoginHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetAccountLoginQuery, Account?>
    {
        public async Task<Account?> Handle(GetAccountLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await sqlDataAccess.QueryFirstOrDefaultAsync<Account>("SELECT * FROM Accounts WHERE Name = @username",
                new { username = request.Username });

            if(user == null)
            {
                return null;
            }

            bool passMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            return passMatch ? user : null;
        }
    }
}
