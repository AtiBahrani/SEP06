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
    public class UsersController : ControllerBase
    {      
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public UsersController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _config = configuration;
        }

        // end point To signup
        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            userCreateDto.Email = userCreateDto.Email.ToLower();
            if (await _unitOfWork.Users.IsUserExist(userCreateDto.Email))           
                return BadRequest("email already exist");

            var userToCreate = new User
            {
                UserName = userCreateDto.UserName,
                Email = userCreateDto.Email.ToLower(),
            };

            var createdUser = await _unitOfWork.Users.Create(userToCreate, userCreateDto.Password);
            await _unitOfWork.Complete();

            return Ok(new { userId = createdUser.Id, email = createdUser.Email });
        }
        // end point To login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userFromRepo = await _unitOfWork.Users.Login(userLoginDto.Email.ToLower(), userLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();
            // add claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", userFromRepo.Id.ToString()),                
                new Claim("UserName", userFromRepo.UserName),
                new Claim("Email", userFromRepo.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.Now.AddDays(1),
            //    SigningCredentials = creds
            //};
            var tokenDescriptor = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.UtcNow.AddDays(1), signingCredentials: creds);


            var tokenHandler = new JwtSecurityTokenHandler();
            // create jwt token
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(tokenDescriptor),
                userId = userFromRepo.Id
            });
        }
        // end point To get all users
        [HttpGet("Userslist")]
        public async Task<IEnumerable<User>> GetUsersList(int pageIndex = 1, int pageSize = 10)
        {
            return await _unitOfWork.Users.UsersListWithAll(pageIndex, pageSize);
        }
        // end point To get user by id
        [HttpGet("Get/{id}")]
        public async Task<User> GetUser(int id)
        {
            return await _unitOfWork.Users.UserSingleWithAll(id);
        }

        //[HttpPost("Update")]
        //public async Task<IActionResult> UpdateUser(User user)
        //{
        //    var userToUpdate = await _unitOfWork.Users.GetById(user.Id);
        //    userToUpdate.FirstName = user.FirstName;
        //    userToUpdate.LastName = user.LastName;   
        //    _unitOfWork.Users.Update(userToUpdate);
        //    await _unitOfWork.Complete();
        //    return Ok("User Updated Successfuly");
        //}

        //[HttpPost("delete")]
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var userToDelete = await _unitOfWork.Users.GetById(Id);
        //    if (userToDelete != null)
        //        _unitOfWork.Users.Remove(userToDelete);
        //    await _unitOfWork.Complete();

        //    return Ok("User Deleted Successfuly");
        //}
    }
}
