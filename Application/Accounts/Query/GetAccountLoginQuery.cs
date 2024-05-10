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
            var bCryptHashed = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
            var shaHashed = HashPasswordWithSHA512(request.Password);

            return await sqlDataAccess.QueryFirstOrDefaultAsync<Account>("SELECT * FROM Accounts WHERE Name = @username AND (Password = @normalPassword OR Password = @bcryptPassword OR Password = @shaPassword)",
                new { username = request.Username, normalPassword = request.Password, bcryptPassword = bCryptHashed, shaPassword = shaHashed });
        }

        public static string HashPasswordWithSHA512(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha512.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashedBytes).Replace("-", string.Empty).ToLower();
            }
        }
    }
}
