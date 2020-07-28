using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Player
    {
        public uint Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        public uint Height { get; set; }
        [Required]
        public uint Weight { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(10)]
        public string DominantLeg { get; set; }
        [StringLength(50)]
        public string Nick { get; set; }
        public uint Number { get; set; }
        public virtual Team Team { get; set; }
        [NotMapped]
        public virtual IEnumerable<Activity> Activities { get; set; }
        [NotMapped]
        [InverseProperty("Player1")]
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionsBetweenPlayer1 { get; set; }
        [NotMapped]
        [InverseProperty("Player2")]
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionsBetweenPlayer2 { get; set; }
    }
}
