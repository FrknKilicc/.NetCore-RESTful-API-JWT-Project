using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProje.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Statuss Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public int OriginId { get; set; }
        public int LocationId { get; set; }
        public DateTime CreatedAt { get; set; }
       
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
        public  Origin Origin { get; set; }  
        public Location location{ get; set; }

    }
}
