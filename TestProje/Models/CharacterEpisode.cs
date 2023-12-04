using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProje.Models
{
    public class CharacterEpisode
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        [ForeignKey("Episode")]
        public int EpisodeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Character Character { get; set; }
        public virtual Episodes Episode { get; set; }
    }
}
