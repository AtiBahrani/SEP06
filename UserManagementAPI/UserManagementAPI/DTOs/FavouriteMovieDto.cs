using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.DTOs
{
    // data transfer object for FavouriteMovieDto
    public class FavouriteMovieDto
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
