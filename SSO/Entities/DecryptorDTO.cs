using System.ComponentModel.DataAnnotations;

namespace SSO.Entities
{
    public class DecryptorDTO
    {
        [Required]
        public string InputValue { get; set; }
        public string OutputValue { get; set; }
        public string Type { get; set; }
    }
}
