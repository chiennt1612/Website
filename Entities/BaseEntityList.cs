﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Entities
{
    public class BaseEntityList <T>
    {
        public IEnumerable<T> list { get; set; }
        public int totalRecords { get; set; }
    }
}
