using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.DTOs
{
    public class UserFollowerDto
    {
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }
    }
}
