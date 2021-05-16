using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // user follower repository implementation
    public class UserFollowerRepository : Repository<UserFollower>, IUserFollowerRepository
    {
        public UserFollowerRepository(DataContext dataContext):base(dataContext)
        {

        }
        public DataContext DataContext
        {
            get { return _dataContext as DataContext; }
        }   
    }
}
