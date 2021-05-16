﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // UserFollower repository interface inherationg generic repository interface
    public interface IUserFollowerRepository : IRepository<UserFollower>
    {
          
    }
}
