using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SSO.DBContext.SeekData
{
    public  static partial class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address ()
            };
    }
}
