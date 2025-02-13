using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;
using Aqarlist.Core.Services.Service_Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aqarlist.Core.Services.Service_Implemetation
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService( ApiDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Users[] GetAll()
        {
            return _db.Users.ToArray();
        }

        public Guid Create(UserDto model)
        {
            var isEmailUsed = _db.Users.Any(x=>x.EmailAddress == model.EmailAddress);
            if (isEmailUsed) throw new Exception("Email address is already used.");
            var data = new Users 
            { 
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                RoleId = model.RoleId,
                NationalId = null,
                Name = model.Name
            };
            _db.Users.Add(data);
            _db.SaveChanges();
            return data.Id;
        }

        public LoginRepsonse Login(string emailAddress , string password)
        {
            var toReturn = new LoginRepsonse();
            var user = _db.Users.FirstOrDefault(x=>x.EmailAddress == emailAddress);
            if (user == null) throw new Exception("Inavlid credentials");
            if (password != user.Password) throw new Exception("Invalid credentials");
            var token = GetJwtToken(user);
            toReturn.Token = token;
            toReturn.User = _mapper.Map<UserDto>(user);
            toReturn.User.Password = string.Empty;
            return toReturn;
        }
        private AuthTokenResponse GetJwtToken(Users user)
        {
            var accessTokenExpiry = _configuration.GetValue<int>("AccessTokenExpiryDuration");
            var jwtSettings = _configuration.GetSection("Jwt");

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, jwtSettings["Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("EmailAddress", user.EmailAddress)
            };
            var subject = new ClaimsIdentity(claims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(accessTokenExpiry),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Claims = new Dictionary<string, object>(claims.Select(x => new KeyValuePair<string, object>(x.Type, x.Value))),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = Guid.NewGuid().ToString("N");
            return new AuthTokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                Issued = DateTime.UtcNow,
                Expiry = tokenDescriptor.Expires,
                RefreshToken = refreshToken
            };
        }
    }
}
