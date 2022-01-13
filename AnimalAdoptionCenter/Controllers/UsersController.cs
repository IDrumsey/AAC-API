using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoptionCenterModels;
using AnimalAdoptionCenter.Services.Store;
using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Extensions;
using AnimalAdoptionCenter.Services.Authentication;
using AnimalAdoptionCenter.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AnimalAdoptionCenter.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IAuthenticateService _authService;
        private readonly IUserService _userService;
        private ITokenService _tokenService;

        public UsersController(IAuthenticateService authService, IUserService userService, ITokenService tokenService)
        {
            this._authService = authService;
            this._userService = userService;
            this._tokenService = tokenService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> authenticateUser([FromBody] User user)
        {
            var authenticated = await this._authService.authenticate(user);
            if (authenticated)
            {
                //calling the same repository function twice -> should rework this code.
                var res = await _userService.GetUserByCredentialsAsync(user);

                if (!res.success)
                {
                    return Unauthorized("Invalid credentials");
                }

                var token = this._tokenService.createToken(res.data);
                // generate token
                var response = new { Token = token, User = res.data };

                // set response headers
                Response.Cookies.Append("jwt", token, new CookieOptions() { HttpOnly = true, Expires = new DateTimeOffset(DateTime.Now).AddHours(1) });
                return Ok(response);
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [HttpGet("authenticate/check")]
        public IActionResult checkIfUserLoggedIn()
        {
            // checks if authenticated and returns Ok with token or Unauthorized
            var token = Request.Cookies["jwt"];
            return Ok(token);
        }

        [HttpGet("logout")]
        public IActionResult logoutUser()
        {
            // idk if the server side needs to do anything with the token. Right now it's just removing the client side authentication token
            Response.Cookies.Append("jwt", "logged out", new CookieOptions() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(-1) });

            var response = new
            {
                status = "Logged out"
            };

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> getAllUsersAsync()
        {
            var res = await this._userService.GetAllUsersAsync();

            if (!res.success)
            {
                return BadRequest(res.message);
            }

            return Ok(res.data);
        }
    }
}
