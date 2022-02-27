using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UdemyLearning.Data;
using UdemyLearning.DTOs;
using UdemyLearning.Entities;
using UdemyLearning.Interfaces;

namespace UdemyLearning.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext context;

        public ITokenService TokenService { get; }

        public AccountController(DataContext context,ITokenService tokenService)
        {
            this.context = context;
            this.TokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            return new UserDto { Token = TokenService.CreateToken(user),Username=user.UserName};
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await context.Users.
                FirstOrDefaultAsync(c => c.UserName == loginDto.Username);

            if(user == null)
            {
                return Unauthorized("Invalid Username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0;i<computedHash.Length; i++)
            {
                if(computedHash[i] !=  user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }

            return new UserDto { Username=user.UserName,Token = TokenService.CreateToken(user)};

        }

        private async Task<bool> UserExists (string username)
        {
            return await context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
    
}
