using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonDomain
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string ClientName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int MasterId { get; set; }

        [ForeignKey("MasterId")]
        public Master Master { get; set; } = null!;

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<ProcedureItem> ProcedureItems { get; set; } = null!;
    }
}
