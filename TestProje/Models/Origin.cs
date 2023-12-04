using System.ComponentModel.DataAnnotations;

namespace TestProje.Models
{
    public class Origin
    {
        [Key]
        public int OriginId { get; set; }
        public string OriginName { get; set; }
        public string OriginUrl { get; set; }
    }
}
