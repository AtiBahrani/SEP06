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
    //[Authorize]
    [ApiController]
    public class MoviesController : ControllerBase
    {      
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // end point To add a new movie
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Movie movie)
        {
            await _unitOfWork.Movies.Add(movie);
            await _unitOfWork.Complete();

            return StatusCode(201);            
        }
        // end point To get movie by Id
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var data = await _unitOfWork.Movies.GetById(Id);
            return Ok(data);
        }
        // end point To get all movies result paginated
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            var data = await _unitOfWork.Movies.GetAll(pageIndex,pageSize);
            return Ok(data);
        }
        // end point To update an existing movie
        [HttpPost("Update")]
        public IActionResult Update(Movie movie)
        {
            _unitOfWork.Movies.Update(movie);
            return Ok();
        }
    }
}
