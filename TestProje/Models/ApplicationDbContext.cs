using Microsoft.EntityFrameworkCore;

namespace TestProje.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episodes> Episodess { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<Origin> Pages { get; set; }
        
    }
}
