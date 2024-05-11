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
    public record UpdatePasswordCommand(int Id, string OldPassword, string NewPassword) : IRequest<bool>;

    public class UpdatePasswordHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<UpdatePasswordCommand, bool>
    {
        public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {

            var currentPass = await sqlDataAccess.QueryFirstOrDefaultAsync<string>("SELECT password FROM Accounts WHERE id = @id",
                new { id = request.Id });

            bool passMatch = BCrypt.Net.BCrypt.Verify(request.OldPassword, currentPass);

            if(!passMatch)
            {
                return false;
            }

            var newPassHashed = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, workFactor: 12);

            var query = @"UPDATE accounts SET password = @newPass WHERE id = @id";
            return await sqlDataAccess.ExecuteAsync(query, new { newPass = newPassHashed, id = request.Id }) == 1;
        }
    }
}
