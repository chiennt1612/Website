namespace SSO.Models.UserRoles
{
    public class UserRoleList
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
