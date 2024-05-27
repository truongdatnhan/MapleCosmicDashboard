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
    public record GetTopFamePlayerQuery() : IRequest<List<CharDto>>;

    public class GetTopFamePlayerHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetTopFamePlayerQuery, List<CharDto>>
    {
        public async Task<List<CharDto>> Handle(GetTopFamePlayerQuery request, CancellationToken cancellationToken)
        {
            var charDictionary = new Dictionary<int, CharDto>();

            var query = @"WITH ranked_characters AS (
                            SELECT
                                ROW_NUMBER() OVER(ORDER BY c.id ASC, c.fame DESC) AS rank,
                                c.id,
                                c.name,
                                c.level,
                                c.job,
                                c.gender,
                                c.skincolor,
                                c.hair,
                                c.face,
                                c.fame
                            FROM
                                `characters` c
	                        LIMIT 5
                        )
                        SELECT
                            rc.rank,
                            rc.name,
                            rc.level,
                            rc.job,
                            rc.gender AS isFemale,
                            rc.skincolor,
                            rc.hair,
                            rc.face,
                            rc.fame,
                            inv.itemid
                        FROM
                            ranked_characters rc
                        LEFT JOIN `inventoryitems` inv ON
                            inv.characterid = rc.id AND inv.inventorytype = -1;";

            await sqlDataAccess.GetDbConnection().QueryAsync<CharDto, CharItemDto, CharDto>(
                query,
                (ch, item) =>
                {
                    CharDto? charEntry;

                    if (!charDictionary.TryGetValue(ch.Rank, out charEntry))
                    {
                        charEntry = ch;
                        charDictionary.Add(charEntry.Rank, charEntry);
                    }

                    if(item != null)
                    {
                        charEntry.ItemsEquipped.Add(item);
                    }
                    return ch;
                },
                splitOn: "itemid"
            );

            return charDictionary.Values.ToList();
        }

    }
}
