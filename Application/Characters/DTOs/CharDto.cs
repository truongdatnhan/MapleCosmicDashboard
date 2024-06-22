using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Characters.DTOs
{
    public class CharDto
    {
        public int Rank { get; set; }
        public int Level { get; set; }
        public string Name { get; set; } = string.Empty;
        public Job Job { get; set; }
        public bool IsFemale { get; set; }
        public byte SkinColor { get; set; }
        public int Hair { get; set; }
        public int Face { get; set; }
        public int Fame { get; set; }
        public decimal Meso { get; set; }
        public HashSet<CharItemDto> ItemsEquipped { get; set; } = new();
        public string CharacterAvatar()
        {
            //add default if character not wearing anything
            if (!ItemsEquipped.Any(x => x.Position == -5)) // top
            {
                if (IsFemale)
                {
                    ItemsEquipped.Add(new(1041046)); // top
                }
                else
                {
                    ItemsEquipped.Add(new(1042162)); // top
                }
            }

            if (!ItemsEquipped.Any(x => x.Position == -6)) // bottom
            {
                if (IsFemale)
                {
                    ItemsEquipped.Add(new(1061039)); // bottom
                }
                else
                {
                    ItemsEquipped.Add(new(1062112)); // bottom
                }
            }

            //add body, head (crucial)
            ItemsEquipped.Add(new(2000 + SkinColor));
            ItemsEquipped.Add(new(12000 + SkinColor));

            ItemsEquipped.Add(new(Hair));
            ItemsEquipped.Add(new(Face));

            string json = JsonSerializer.Serialize(ItemsEquipped, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            string imageUrl = $"https://maplestory.io/api/character/{json.Substring(1, json.Length - 2)}/jump";
            return imageUrl;
        }

        public string MesoFormatted()
        {
            char? letter;
            decimal meso = 0;
            if (Meso < 1000000) //1m
            {
                meso = Meso / 1000;
                letter = 'K';
            }
            else if (Meso < 100000000) //100m
            {
                meso = Meso / 1000000;
                letter = 'M';
            }
            else // 1b
            {
                meso = Meso / 1000000000;
                letter = 'B';
            }
            return meso.ToString("#,0.#") + letter;
        }
    }

    public class CharItemDto
    {
        public int ItemId { get; set; }
        [JsonIgnore]
        public int Position { get; set; }
        public string Region { set; get; } = "GMS";
        public string Version { set; get; } = "83";

        public CharItemDto(int itemId)
        {
            ItemId = itemId;
        }

        public CharItemDto(int itemId, int position)
        {
            ItemId = itemId;
            Position = position;
        }


    }

}
