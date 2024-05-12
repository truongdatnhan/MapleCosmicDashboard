using Domain.Entities;
using Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Command
{
    public record InsertAccountCommand(string Username, string Password, string Email) : IRequest<int>;

    public class InsertAccountHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<InsertAccountCommand, int>
    {
        public async Task<int> Handle(InsertAccountCommand request, CancellationToken cancellationToken)
        {
            var passHashed = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
            return await sqlDataAccess.ExecuteAsync("INSERT INTO accounts (name, password, email) VALUES (@username, @password, @email) ",
                new { username = request.Username, password = passHashed, email = request.Email });
        }
    }
}
