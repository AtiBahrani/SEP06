using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Data
{
    // unit of work repository implementation
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
            Users = new UserRepository(_dataContext);
            UserFavouriteMovies = new UserFavouriteMovieRepository(_dataContext);
            UserFollowers = new UserFollowerRepository(_dataContext);
        }
        public IUserRepository Users { get; private set; }
        public IUserFavouriteMovieRepository UserFavouriteMovies { get; private set; }
        public IUserFollowerRepository UserFollowers { get; private set; }


        public async Task<int> Complete()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
