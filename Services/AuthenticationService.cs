using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,
                                        IOptions<JWTOptions> _jwtOptions) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);

            return new UserResponse
            {
                Id = user.Id!,
                Email = user.Email!,
                Username = user.UserName!,
                Token = await GenerateToken(user)
            };
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new UserNotFoundException(request.Email);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (isPasswordValid)
                return new UserResponse
                {
                    Id = user.Id!,
                    Email = user.Email!,
                    Username = user.UserName!,
                    Token = await GenerateToken(user)
                };

            throw new UnAuthorizedException();
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = (Domain.Models.Identity.Gender)request.Gender,
                UserType = (Domain.Models.Identity.UserType)request.Role
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            var roleName = request.Role.ToString();

            await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded) return new UserResponse()
            {
                Id = user.Id!,
                Username = user.UserName,
                Email = user.Email,
                Token = await GenerateToken(user)
            };

            var Errors = result.Errors.Select(e => e.Description).ToList();

            throw new BadRequestException(Errors);
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var jwtOptions = _jwtOptions.Value;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string secretKey = jwtOptions.SecretKey;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

    }
}
