using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // UserFavouriteMovie repository interface inherationg generic repository interface
    public interface IUserFavouriteMovieRepository : IRepository<UserFavouriteMovie>
    {
          
    }
}
