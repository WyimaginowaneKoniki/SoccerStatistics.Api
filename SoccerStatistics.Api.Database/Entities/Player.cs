using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Height { get; set; }
        public uint Weight { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public string DominantLeg { get; set; }
        public string Nick { get; set; }
        public uint Number { get; set; }
        public virtual Team Team { get; set; }
        public virtual IEnumerable<Activity> Activities { get; set; }
        [InverseProperty("Player1")]
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionsBetweenPlayer1 { get; set; }
        [InverseProperty("Player2")]
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionsBetweenPlayer2 { get; set; }
    }
    
}
