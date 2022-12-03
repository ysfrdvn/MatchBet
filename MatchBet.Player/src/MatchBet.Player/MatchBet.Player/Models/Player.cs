using System.ComponentModel.DataAnnotations.Schema;

namespace MatchBet.Player.Models
{
    [Table("players")]
    public class Player 
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short Credit { get; set; }
        public double Score { get; set; }
    }
}

