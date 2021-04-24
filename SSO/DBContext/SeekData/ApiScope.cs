using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SSO.DBContext.SeekData
{
    public static partial class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
               new List<ApiScope>
               {
                new ApiScope("api1", "My API"),
                new ApiScope("api2", "New My API")
               };
    }
}
