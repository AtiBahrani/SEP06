using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Models
{
    // User entity class
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<UserFavouriteMovie> UserFavouriteMovies { get; set; }
        public ICollection<UserFollower> Followers { get; set; }
        public ICollection<UserFollower> FollowedTo { get; set; }
    }
}
