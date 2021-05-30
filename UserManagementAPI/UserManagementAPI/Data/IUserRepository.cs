using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // User repository interface inherating generic repository interface
    public interface IUserRepository : IRepository<User>
    {
        // extra methods just for user repository which can not be handeled in generic repository
        Task<User> Create(User user, string password);
        Task<User> Login(string email, string password);
        Task<bool> IsUserExist(string username);
        Task<List<User>> UsersListWithAll(int pageIndex, int pageSize);
        Task<User> UserSingleWithAll(int Id);
        Task<List<User>> SearchUsers(string searchTerm, int pageIndex, int pageSize);

    }
}
