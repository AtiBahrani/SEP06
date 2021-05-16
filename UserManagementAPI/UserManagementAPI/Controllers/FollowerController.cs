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
using UserManagementAPI.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class FollowerController : ControllerBase
    {      
        private readonly IUnitOfWork _unitOfWork;

        public FollowerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // end point To add a user to followers list
        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserFollowerDto follower)
        {
            var userFollower = new UserFollower
            {
                FollowedToId = follower.FolloweeId,
                FollowersId = follower.FollowerId,                
            };
            await _unitOfWork.UserFollowers.Add(userFollower);
            await _unitOfWork.Complete();

            return StatusCode(201);            
        }
        // end point To remove a user from followers list
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(UserFollowerDto model)
        {
            var follower = await _unitOfWork.UserFollowers.Find(m=>m.FollowersId == model.FollowerId && m.FollowedToId == model.FolloweeId);
            if (follower != null)
            {
                _unitOfWork.UserFollowers.Remove(follower);
                await _unitOfWork.Complete();
                return Ok("Follower Removed");
            }
            return Ok("Not Found");
        }
    }
}
