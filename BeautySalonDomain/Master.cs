using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonDomain
{
    public class Master
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public int ProfessionId { get; set; }

        [ForeignKey("ProfessionId")]
        public Profession Profession { get; set; } = null!;

        public int OfficeId { get; set; }

        [ForeignKey("OfficeId")]
        public Office Office { get; set; } = null!;

        public List<Procedure> Procedures { get; set; } = null!;
    }
}
