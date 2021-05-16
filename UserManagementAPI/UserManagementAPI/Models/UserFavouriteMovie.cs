using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Models
{
    // UserFavouriteMovie entity class
    public class UserFavouriteMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
    }
}
