using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain
{
    public class Profession
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Master> Masters { get; set; }

        public List<Service> Services { get; set; }
    }
}
