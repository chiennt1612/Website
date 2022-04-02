using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SSO.Entities
{
    public class AppUser : IdentityUser<long>
    {
        [StringLength(39)]
        public string OldId { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
