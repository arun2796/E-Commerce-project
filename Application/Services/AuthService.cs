using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.AuthAccount;
using Application.DTO;
using Application.InputParams;
using Application.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _siginManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<User> userManager,IOptions<JwtSettings> options , SignInManager<User> siginManager)
        {
            _jwtSettings = options.Value;
            _userManager = userManager;
            _siginManager = siginManager;

        }
        public   async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
           var user =await  _userManager.FindByEmailAsync( loginDto.Email );
            if ( user == null )
            {
                throw new ApplicationException();
            }
            var result =await _siginManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: true,lockoutOnFailure:false);
            var IsValidCredential = await _userManager.CheckPasswordAsync(user,loginDto.Password);

            if (result.Succeeded)
            { }


                return new AuthResponseDto
                {
                    username = loginDto.Email,
                    Token = await GenerateJwtToken(user)
                };
            
            
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName=registerDto.Email,
                Email=registerDto.Email,
            };
            var reg=await _userManager.CreateAsync(user ,registerDto.Password);
            if (!reg.Succeeded )
            {
                throw new Exception(string.Join(",",reg.Errors.Select(e=>e.Description)));
            }
            if (reg.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "ADMIN");
            }
            return new AuthResponseDto
            {
                Token = await GenerateJwtToken(user),
                username = user.UserName,
            };

        }
        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
           {
               new Claim (JwtRegisteredClaimNames.Sub,value: user.UserName),
               new Claim (JwtRegisteredClaimNames.Email,user.Email),
               new Claim(ClaimTypes.NameIdentifier,user.Id),
           };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var ceds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
             audience: _jwtSettings.Audience,
              claims: claims,
              notBefore: null,
              expires: DateTime.Now.AddDays(_jwtSettings.ExpireDays),
               signingCredentials: ceds
);
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
