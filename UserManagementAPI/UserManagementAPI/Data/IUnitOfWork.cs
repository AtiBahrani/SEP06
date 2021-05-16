using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Data
{
    // unit of work interface for all other interfaces
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMovieRepository Movies { get; }
        IUserFavouriteMovieRepository UserFavouriteMovies { get; }
        IUserFollowerRepository UserFollowers { get; }
        Task<int> Complete();
    }
}
