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
    public record UpdatePasswordCommand(int Id, string Password) : IRequest<int>;

    public class UpdatePasswordHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<UpdatePasswordCommand, int>
    {
        public async Task<int> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var query = @"UPDATE accounts SET password = @password WHERE id = @id";
            return await sqlDataAccess.ExecuteAsync(query, new { password = request.Password, id = request.Id });
        }
    }
}
