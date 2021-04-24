﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Web.Entities;

namespace WebAdmin.Repository.Interfaces
{
    public interface IParamSettingRepository: IGenericRepository<ParamSetting, long>
    {
    }
}