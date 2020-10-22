using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTApi.Helpers;
using JWTApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTApi.Data
{
    public class UserService : IUserservice
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;
        

        public UserService(DataContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<Student> Authenticate(string username, string password)
        {
            var user = await _context.Students.FirstOrDefaultAsync(x => x.Name == username);

            if(user == null)
                return null;

            // if(!verifyPasswardHash(password, user.PasswordHash, user.PasswordSalt))
            //     return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.StudentId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        private bool verifyPasswardHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i=0; i< computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Student> Register(Student user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Password = password;

            await _context.Students.AddAsync(user);//this doesnt change in our database
            await _context.SaveChangesAsync();//this saves sthe changes

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hamc.Key;
                passwordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string username)
        {
            if(await _context.Students.AnyAsync(x => x.Name == username))
                return true;
                
            return false;
        }

        // public IEnumerable<User> GetAll()
        // {
        //     return _users.WithoutPasswords();
        // }
    }
}