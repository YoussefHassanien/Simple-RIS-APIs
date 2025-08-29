using APIs.Configs;
using Core;
using Core.DTOs.Auth.Login;
using Core.DTOs.Auth.Register;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIs.Services
{
    public class AuthService(IUnitOfWork unitOfWork, JwtOptions jwtOptions) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly JwtOptions _jwtOptions = jwtOptions;
        private readonly byte[] _salt = RandomNumberGenerator.GetBytes(16);
        private readonly Int32 _iterationsNumber = 100_000;
        private readonly HashAlgorithmName _hashingAlgorithm = HashAlgorithmName.SHA256;

        private Task<bool> ComparePassowrds(string plainPassword, string hashedPassowrd)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassowrd);
            Buffer.BlockCopy(hashBytes, 0, _salt, 0, 16);

            using var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, _salt, _iterationsNumber, _hashingAlgorithm);
            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        private Task<string> HashPassword(string password)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, _iterationsNumber, _hashingAlgorithm);
            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[48];
            Buffer.BlockCopy(_salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

            return Task.FromResult(Convert.ToBase64String(hashBytes));
        }

        public async Task<AuthLoginResponse?> LoginUser(AuthLoginRequest request)
        {
            var user = await _unitOfWork.Persons.GetByEmail(request.Email);
            if (user == null) 
                return null;

            bool isCorrectPassword = await ComparePassowrds(request.Password, user.Password!);
            if (!isCorrectPassword) 
                return null;

            string token = await GenerateAccessToken(user.Email!, user.Role!);

            var response = new AuthLoginResponse
            {
                Email = request.Email,
                Name = $"{user.FirstName} {user.LastName}",
                MobileNumber = user.MobileNumber,
                Gender = user.Gender,
                SocialSecurityNumber = user.SocialSecurityNumber,
                DateOfBirth = user.DateOfBirth,
                Role = user.Role,
                AccessToken = token,
            };

            return response;
        }

        public async Task<AuthLoginResponse?> RegisterUser(AuthRegistrationRequest request)
        {
            var emailFlag = await _unitOfWork.Persons.GetByEmail(request.Email);

            if (emailFlag != null)
                return null;

            var mobileNumberFlag = await _unitOfWork.Persons.GetByMobileNumber(request.MobileNumber);

            if (mobileNumberFlag != null)
                return null;

            var socialSecurityNumberFlag = await _unitOfWork.Persons.GetBySocialSecurityNumber(request.SocialSecurityNumber);

            if (socialSecurityNumberFlag != null)
                return null;

            var hashedPassword = await HashPassword(request.Password);

            request.Password = hashedPassword;

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                SocialSecurityNumber = request.SocialSecurityNumber,
                DateOfBirth = request.DateOfBirth,
                Role = request.Role,
                MobileNumber = request.MobileNumber,
                Email = request.Email,
                Password = hashedPassword,
            };

            var newUser = await _unitOfWork.Persons.Add(person);
            _unitOfWork.Complete();

            string token = await GenerateAccessToken(newUser!.Email!, newUser.Role!);

            var response = new AuthLoginResponse
            {
                Email = newUser!.Email!,
                Name = $"{newUser.FirstName} {newUser.LastName}",
                MobileNumber = newUser.MobileNumber,
                Gender = newUser.Gender,
                SocialSecurityNumber = newUser.SocialSecurityNumber,
                DateOfBirth = newUser.DateOfBirth,
                Role = newUser.Role,
                AccessToken = token,
            };

            return response;
        }

        private Task<string> GenerateAccessToken(string email, string role) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)),
                    SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.Email, email),
                    new(ClaimTypes.Role, role)
                ])
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return Task.FromResult(accessToken);
        }
    }
}
