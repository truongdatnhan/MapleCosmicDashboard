using Application.Accounts.Query;
using Domain.Entities;
using MediatR;

namespace Web.Services
{
    public class AccountService(IMediator mediator)
    {
        public Account? Account { get; private set; }

        public async Task<Account> FetchAccountAsync(int id = 0)
        {
            if (Account is null && id == 0)
            {
                throw new InvalidOperationException("AccountService has not been initial");
            }

            if (id != 0)
            {
                Account = await mediator.Send(new GetAccountQuery(id));
            }
            else
            {
                Account = await mediator.Send(new GetAccountQuery(Account!.Id));
            }

            if (Account == null)
            {
                throw new Exception("Can not fetch account");
            }

            return Account;
        }
    }
}