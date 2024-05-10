using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public string Pic { get; set; } = string.Empty;
        public byte LoggedIn { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Birthday { get; set; }
        public byte Banned { get; set; }
        public string BanReason { get; set; } = string.Empty;
        public string Macs { get; set; } = string.Empty;
        public int? NxCredit { get; set; }
        public int? MaplePoint { get; set; }
        public int? NxPrepaid { get; set; }
        public byte CharacterSlots { get; set; }
        public byte Gender { get; set; }
        public DateTime TempBan { get; set; }
        public byte GReason { get; set; }
        public byte Tos { get; set; }
        public string SiteLogged { get; set; } = string.Empty;
        public int? WebAdmin { get; set; }
        public string Nick { get; set; } = string.Empty;
        public int? Mute { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
        public int RewardPoints { get; set; }
        public int VotePoints { get; set; }
        public string Hwid { get; set; } = string.Empty;
        public int Language { get; set; }
        public int Streak { get; set; }
        public DateTime? LastRedeem { get; set; }
    }
}