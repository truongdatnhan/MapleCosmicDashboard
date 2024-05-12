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
    public record UpdateProfileCommand(int Id, string Email, DateTime Birthday, int Gender) : IRequest<bool>;

    public class UpdateProfileHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<UpdateProfileCommand, bool>
    {
        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var query = @"UPDATE accounts SET email = @email, birthday = @birthday, gender = @gender WHERE id = @id";
            return await sqlDataAccess.ExecuteAsync(query, new { email = request.Email, birthday = request.Birthday,
                gender = request.Gender, id = request.Id }) == 1;
        }
    }
}
