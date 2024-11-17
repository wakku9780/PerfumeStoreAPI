using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfumeStoreAPI.DTOs;
using PerfumeStoreAPI.Helpers;
using PerfumeStoreAPI.Models;
using PerfumeStoreAPI.Services;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public UserController(UserService userService, JwtTokenHelper jwtTokenHelper)
        {
            _userService = userService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        // POST: api/User/Register
        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role // assume role set during registration
            };

            await _userService.RegisterUserAsync(user, request.Password);
            return StatusCode(201); // Created
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<ActionResult> AuthenticateUser([FromBody] LoginRequest request)
        {
            var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);

            if (user == null)
            {
                return Unauthorized(); // Invalid credentials
            }

            // Token generate kar rahe hain
            var token = _jwtTokenHelper.GenerateToken(user);

            // Response me sirf token bhej rahe hain
            return Ok(new { Token = token });
        }

        // Admin-specific API - Only accessible to Admin role
        [HttpGet("AdminDashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok("Welcome to Admin Dashboard!");
        }
    }
}

    



//using Microsoft.AspNetCore.Mvc;
//using PerfumeStoreAPI.DTOs;
//using PerfumeStoreAPI.Models;
//using PerfumeStoreAPI.Services;
//using System.Threading.Tasks;

//namespace PerfumeStoreAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly UserService _userService;

//        public UserController(UserService userService)
//        {
//            _userService = userService;
//        }

//        // POST: api/User/Register
//        [HttpPost("Register")]
//        public async Task<ActionResult> RegisterUser(User user, string password)
//        {
//            await _userService.RegisterUserAsync(user, password);
//            return StatusCode(201); // Created
//        }




//        [HttpPost("Login")]
//        public async Task<ActionResult> AuthenticateUser([FromBody] LoginRequest request)
//        {
//            var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);

//            if (user == null)
//            {
//                return Unauthorized(); // Invalid credentials
//            }

//            // DTO me user ki details map karo
//            var response = new UserDto
//            {
//                Id = user.Id,
//                Name = user.Name,
//                Email = user.Email,
//                Token = user.Token
//            };

//            return Ok(response); // Sirf zaroori data bhej rahe hain
//        }



//        //[HttpPost("Login")]
//        //public async Task<ActionResult> AuthenticateUser([FromBody] LoginRequest request)
//        //{
//        //    var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);
//        //    if (user == null)
//        //    {
//        //        return Unauthorized(); // Invalid credentials
//        //    }
//        //    return Ok(user);
//        //}


//        //POST: api/User/Login
//        //[HttpPost("Login")]
//        // public async Task<ActionResult> AuthenticateUser(string email, string password)
//        // {
//        //     var user = await _userService.AuthenticateUserAsync(email, password);

//        //     if (user == null)
//        //     {
//        //         return Unauthorized(); // Invalid credentials
//        //     }

//        //     return Ok(user); // Return user or JWT token
//        // }
//    }
//}
