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
    public record InsertAccountCommand(string Username, string Password, string Email) : IRequest<(bool IsSuccess, string? ErrorMessage)>;

    public class InsertAccountHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<InsertAccountCommand, (bool IsSuccess, string? ErrorMessage)>
    {
        public async Task<(bool IsSuccess, string? ErrorMessage)> Handle(InsertAccountCommand request, CancellationToken cancellationToken)
        {
            var passHashed = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
            string? error;

            var usernameExist = (await sqlDataAccess.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Accounts WHERE Name = @username",
                new { username = request.Username })) == 1;

            if(usernameExist)
            {
                error = "Username already exists";
                return (false, error);
            }

            var emailExist = (await sqlDataAccess.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Accounts WHERE Email = @email",
                new { email = request.Email })) == 1;

            if (emailExist)
            {
                error = "Email already exists";
                return (false, error);
            }

            var result= await sqlDataAccess.ExecuteAsync("INSERT INTO accounts (name, password, email, gender) VALUES (@username, @password, @email, 0) ",
                new { username = request.Username, password = passHashed, email = request.Email });

            return (true, null);
        }
    }
}
