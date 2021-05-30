using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagementAPI.Data;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FavouriteMovieController : ControllerBase
    {      
        private readonly IUnitOfWork _unitOfWork;

        public FavouriteMovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // end point To add a movie to favourite list
        [HttpPost("Add")]
        public async Task<IActionResult> Add(FavouriteMovieDto favouriteMovieDto)
        {
            var favouriteMovie = new UserFavouriteMovie
            {
                MovieId = favouriteMovieDto.MovieId,
                UserId = favouriteMovieDto.UserId
            };
            await _unitOfWork.UserFavouriteMovies.Add(favouriteMovie);
            await _unitOfWork.Complete();

            return StatusCode(201);            
        }
        // end point To remove a movie from favourite list
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(FavouriteMovieDto model)
        {
            var data = await _unitOfWork.UserFavouriteMovies.Find(m=>m.UserId == model.UserId && m.MovieId == model.MovieId);
            if (data != null)
            {
                _unitOfWork.UserFavouriteMovies.Remove(data);
                await _unitOfWork.Complete();
                return Ok("Favourite Movie Removed");
            }
            return Ok("Not Found");
        }

        [HttpGet("TopFavouriteMoviesForUser")]
        public async Task<IActionResult> TopFavouriteMoviesForUser(int UserId)
        {
            var data = await _unitOfWork.UserFavouriteMovies.TopFavouriteMoviesForUser(UserId);
            return Ok(data);
        }
    }
}
