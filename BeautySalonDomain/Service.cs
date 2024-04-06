using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        public int ProfessionId { get; set; }

        public int Duration { get; set; }

        [ForeignKey("ProfessionId")]
        public Profession Profession { get; set; } = null!;

        public List<ProcedureItem> ProcedureItems { get; set; } = null!;
    }
}
