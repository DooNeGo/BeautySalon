using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public List<Master> Masters { get; set; } = null!;
    }
}