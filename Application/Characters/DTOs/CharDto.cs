using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Characters.DTOs
{
    public class CharDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CharItemDto> ItemsEquipped { get; set; }

        public CharDto(int id, string name)
        {
            Id = id;
            Name = name;
            ItemsEquipped = new List<CharItemDto>();
        }

    }

    public class CharItemDto
    {
        public int ItemId { get; set; }
        public int ItemStatus { get; set; }
    }

}
