using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations;
using User = SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities.User;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
   public class AuthServices(ApplicationDbContext context, IConfiguration configuration) : IAuthServices 
    {
       

      public async Task<TokenResponseDto?> RefreshTokenAsync( RefreshTokenDto request)
        {
            var user = await ValidateRefreshToken(request.UserId, request.RefreshToken);
            if (user is null)
            {
                return null;
            }
            return  await CreateTokenResponse(user);
        }
        public async Task<User?> RegisterAsync( UserDtos request )
        {
            if(await context.users.AnyAsync(s => s.Username == request.UserName))
            {
                return null;
                
            }
            if(string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }

            var user = new User();
            var passwordhasher = new PasswordHasher<User>()
                .HashPassword(user, request.Password);
            user.Id = Guid.NewGuid();
            user.Username = request.UserName;
            user.Password = passwordhasher;         
            user.Role = request.Role;
            context.users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<TokenResponseDto?> LoginAsync(LoginDto request)
        {
            var user = await context.users.FirstOrDefaultAsync(s => s.Username == request.UserName);
            if(user is null)
            {
                return null;
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user,user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return await CharacterTypeAsync(user.Id);
        }
        public async Task<bool> LogoutAsync(Guid user)
        {
            var users = await context.users.FindAsync(user);
            if (users is null)
            {
                return false;
            }
            users.RefreshToken = null;
            users.ExpiredRefreshToken = DateTime.UtcNow.AddDays(-1);
            await context.SaveChangesAsync();
            return true;
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Character!)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var TokenDescriptor = new JwtSecurityToken(

                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims : claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials : creds

                );
            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);


        }
        public string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }

        public async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshtoken = GenerateRefreshToken();
            user.RefreshToken = refreshtoken;
            user.ExpiredRefreshToken = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshtoken;
        }
        public async Task<User?> ValidateRefreshToken(Guid userid, string refreshtoken)
        {
            var user = await context.users.FindAsync(userid);
            if ( user is null || user.RefreshToken != refreshtoken || user.ExpiredRefreshToken <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
        public async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken =  await GenerateAndSaveRefreshToken(user)
            };
        }
        public async Task<TokenResponseDto?> CharacterTypeAsync(Guid Id)
        {
            var request = await context.users.FindAsync(Id);
            if (request is null) return null;

            request.Character = request.Role switch
            {
                UserRole.Facilitator => "Admin",
                UserRole.Student => "Student",
                _ => request.Character
            };
            await context.SaveChangesAsync();
            return await CreateTokenResponse(request);
        }
      
    }
}
