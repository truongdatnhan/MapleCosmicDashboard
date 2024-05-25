using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using System.Security.Cryptography;
using Application.Characters.DTOs;
using System.Collections;
using Dapper;

namespace Application.Dashboard.Query
{
    public record GetTopNxPlayerQuery() : IRequest<IList<CharDto>>;

    public class GetTopNxPlayerHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetTopNxPlayerQuery, IList<CharDto>>
    {
        public async Task<IList<CharDto>> Handle(GetTopNxPlayerQuery request, CancellationToken cancellationToken)
        {
            var charDictionary = new Dictionary<int, CharDto>();

            var query = @"";

            await sqlDataAccess.GetDbConnection().QueryAsync<CharDto, CharItemDto, CharDto>(
                query,
                (ch, item) =>
                {
                    CharDto? charEntry;

                    if (!charDictionary.TryGetValue(ch.Id, out charEntry))
                    {
                        charEntry = ch;
                        charDictionary.Add(charEntry.Id, charEntry);
                    }

                    charEntry.ItemsEquipped.Add(item);
                    return charEntry;
                },
                new { orderId = "" },
                splitOn: "ItemId"
            );

            return charDictionary.Values.ToList();
        }

    }
}
