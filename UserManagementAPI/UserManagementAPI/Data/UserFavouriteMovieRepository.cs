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
        private readonly DataContext _dataContext;

        public UserFavouriteMovieRepository(DataContext dataContext):base(dataContext)
        {
            _dataContext = dataContext;
        }
        public DataContext DataContext
        {
            get { return _dataContext as DataContext; }
        }

        public async Task<object> TopFavouriteMoviesForUser(int UserId)
        {
            var userFavouritMovies = await _dataContext.UserFavouriteMovies.Where(m => m.UserId == UserId).Select(x => x.MovieId).ToListAsync();
            var userFollowedToList = await _dataContext.UserFollowers.Where(m => m.FollowersId == UserId).Select(x => x.FollowedToId).ToListAsync();
            var data = await _dataContext.UserFavouriteMovies.Where(m => userFollowedToList.Contains(m.UserId))
                .Select(x => new UserFavouriteMovie
                {
                    MovieId = x.MovieId,
                    UserId = x.UserId
                }).ToListAsync();
            if (userFavouritMovies != null && userFavouritMovies.Count > 0)
            {
                data = data.Where(m => userFavouritMovies.Contains(m.MovieId) == false).ToList();
            }
            var groupby = data.GroupBy(g => g.MovieId)
                    .Select(g => new { g.Key, Count = g.Count() }).OrderByDescending(o => o.Count).Take(10).ToList();
            return groupby;
        }
    }
}
