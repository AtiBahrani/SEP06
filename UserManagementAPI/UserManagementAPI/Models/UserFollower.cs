using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAPI.Models
{
    // UserFollower entity class
    public class UserFollower
    {
        [ForeignKey(nameof(Followers))]
        public int FollowersId { get; set; }
        public User Followers { get; set; }
        [ForeignKey(nameof(FollowedTo))]
        public int FollowedToId { get; set; }
        public User FollowedTo { get; set; }
    }
}