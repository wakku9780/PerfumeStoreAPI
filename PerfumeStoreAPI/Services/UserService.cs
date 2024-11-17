using PerfumeStoreAPI.Models;
using PerfumeStoreAPI.Repositories;
using PerfumeStoreAPI.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PerfumeStoreAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, JwtTokenHelper jwtTokenHelper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
            _passwordHasher = passwordHasher;
        }


        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null; // User not found
            }

            try
            {
                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                {
                    return null; // Invalid credentials
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error verifying password hash: {ex.Message}");
                return null; // Return null or handle error as needed
            }

            // Generate JWT Token
            var token = _jwtTokenHelper.GenerateToken(user);
            user.Token = token;

            return user;
        }



        //public async Task<User> AuthenticateUserAsync(string email, string password)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return null; // User not found
        //    }

        //    // Log the PasswordHash to see if it's correct
        //    Console.WriteLine($"Stored Password Hash: {user.PasswordHash}");

        //    var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        //    Console.WriteLine($"Password Verification Result: {passwordVerificationResult}");

        //    if (passwordVerificationResult != PasswordVerificationResult.Success)
        //    {
        //        return null; // Invalid credentials
        //    }

        //    // Generate JWT Token
        //    var token = _jwtTokenHelper.GenerateToken(user);
        //    user.Token = token; // Assign the token to user if you need it

        //    return user;
        //}



        public async Task RegisterUserAsync(User user, string password)
        {
            // Hash the password
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            await _userRepository.CreateUserAsync(user);
        }
    }
}


