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
    public record GetTopMesoPlayerQuery() : IRequest<List<CharDto>>;

    public class GetTopMesoPlayerHandler(ISqlDataAccess sqlDataAccess) : IRequestHandler<GetTopMesoPlayerQuery, List<CharDto>>
    {
        public async Task<List<CharDto>> Handle(GetTopMesoPlayerQuery request, CancellationToken cancellationToken)
        {
            var charDictionary = new Dictionary<int, CharDto>();

            var query = @"WITH ranked_characters AS (
                        SELECT
                            c.id,
                            c.name,
                            c.level,
                            c.job,
                            c.gender,
                            c.skincolor,
                            c.hair,
                            c.face,
                            (c.meso + s.meso) AS total_meso,
                            ROW_NUMBER() OVER (PARTITION BY acc.id ORDER BY c.id ASC, (c.meso + s.meso) DESC) AS rank_within_account
                        FROM
                            `accounts` acc
                        JOIN `characters` c ON
                            c.accountid = acc.id
                        JOIN `storages` s ON
                            acc.id = s.accountid
                    ),
                    top_characters AS (
                        SELECT
                            rc.*
                        FROM
                            ranked_characters rc
                        WHERE
                            rc.rank_within_account = 1
                        LIMIT 5
                    )
                    SELECT
                        DENSE_RANK() OVER (ORDER BY tc.id ASC,tc.total_meso DESC) AS rank,
                        tc.name,
                        tc.level,
                        tc.job,
                        tc.gender AS isFemale,
                        tc.skincolor,
                        tc.hair,
                        tc.face,
                        tc.total_meso AS meso,
                        inv.itemid,
                        inv.position
                    FROM
                        top_characters tc
                    LEFT JOIN `inventoryitems` inv ON
                        inv.characterid = tc.id AND inv.inventorytype = -1;";

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

                    if (item != null)
                    {
                        charEntry.ItemsEquipped.Add(item);
                    }
                    return ch;
                },
                splitOn: "Itemid"
            );

            return charDictionary.Values.ToList();
        }

    }
}
