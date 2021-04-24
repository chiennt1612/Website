﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Entities
{
    public class AppUser : IdentityUser<long>
    {
        public long OldId { get; set; }
    }
}