using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // user facourite movie repository implementation
    public class UserFavouriteMovieRepository : Repository<UserFavouriteMovie>, IUserFavouriteMovieRepository
    {
        public UserFavouriteMovieRepository(DataContext dataContext):base(dataContext)
        {

        }
        public DataContext DataContext
        {
            get { return _dataContext as DataContext; }
        }   
    }
}
