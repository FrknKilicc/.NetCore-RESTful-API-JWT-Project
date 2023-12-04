using System.ComponentModel.DataAnnotations;

namespace TestProje.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocType { get; set; }
        public string Dimension { get; set; }
        public List<Character> Residents { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
}
