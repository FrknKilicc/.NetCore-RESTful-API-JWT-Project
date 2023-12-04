using System.ComponentModel.DataAnnotations;

namespace TestProje.Models
{
    public class Episodes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AirDate { get; set; }
        public string EpisodeCode { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
    }
}
