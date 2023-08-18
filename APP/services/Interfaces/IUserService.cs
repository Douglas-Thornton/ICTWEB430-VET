﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APP.Data.models.models;

namespace APP.Data.services
{
    public interface IUserService
    {

        Task<List<User>> GetAllUsersList(); 
        Task<User> LoginUser(LoginRequest loginRequest);
    }
}
