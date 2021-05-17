using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // movie repository implementation
    public class UserRepository : Repository<User>, IUserRepository
    {
        // constructor call to inject datacontext class into base class
        public UserRepository(DataContext dataContext):base(dataContext)
        {

        }
        public DataContext DataContext
        {
            get { return _dataContext as DataContext; }
        }
       
        public async Task<User> Create(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); // method call to create password hash
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        // method to create hash value for simple password using cryptography
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        // method to check and return true if same email is already exist in db
        public async Task<bool> IsUserExist(string email)
        {
            if (await _dataContext.Users.AnyAsync(m => m.Email == email))
                return true;

            return false;
        }
        // login method which verify password hash value
        public async Task<User> Login(string email, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(m => m.Email == email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))// method call for verify password hash
                return null;

            return user;
        }
        // method to verify password and its hash value for existing passwords
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {               
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)// hash computing using for loop and compare each value
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;                   
                }
            }
            return true;
        }
        // return a list of users with there favourite movies list and forllowers list
        public async Task<List<User>> UsersListWithAll(int pageIndex, int pageSize)
        {
            var usersList = await _dataContext.Users
                .Select(x => new User
                {
                    Email = x.Email,
                    Id = x.Id,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt,
                    UserName = x.UserName,
                    UserFavouriteMovies = x.UserFavouriteMovies.Select(y => new UserFavouriteMovie
                    {
                        Id = y.Id,
                        UserId = y.UserId,
                        MovieId = y.MovieId
                     
                    }).ToList(),
                    Followers = x.Followers.Select(z => new UserFollower
                    {
                        FollowedToId = z.FollowedToId,
                        FollowersId = z.FollowersId
                    }).ToList(),
                    FollowedTo = x.FollowedTo.Select(z => new UserFollower
                    {
                        FollowedToId = z.FollowedToId,
                        FollowersId = z.FollowersId
                    }).ToList(),
                }).Skip((pageIndex-1) * pageSize).Take(pageSize).ToListAsync();
            return usersList;
        }
        // return a single user with there favourite movies list and forllowers list
        public async Task<User> UserSingleWithAll(int Id)
        {
            var user = await _dataContext.Users
                .Select(x => new User
                {
                    Email = x.Email,
                    Id = x.Id,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt,
                    UserName = x.UserName,
                    UserFavouriteMovies = x.UserFavouriteMovies.Select(y => new UserFavouriteMovie
                    {
                        Id = y.Id,
                        UserId = y.UserId,
                        MovieId = y.MovieId
                    }).ToList(),
                    Followers = x.Followers.Select(z => new UserFollower
                    {
                        FollowedToId = z.FollowedToId,
                        FollowersId = z.FollowersId
                    }).ToList(),
                    FollowedTo = x.FollowedTo.Select(z => new UserFollower
                    {
                        FollowedToId = z.FollowedToId,
                        FollowersId = z.FollowersId
                    }).ToList(),
                }).FirstOrDefaultAsync(m=>m.Id == Id);
            return user;
        }
    }
}
