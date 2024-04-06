using System.ComponentModel.DataAnnotations;
using System.Security;

namespace BeautySalonDomain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public SecureString Password { get; set; } = null!;

        public string Role { get; set; } = string.Empty;

        public List<Procedure> Procedures { get; set; } = null!;
    }
}
