using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonDomain
{
    public class ProcedureItem
    {
        [Key]
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int ProcedureId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        
        [ForeignKey("ProcedureId")]
        public Procedure Procedure { get; set; }
    }
}
