using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalkssAPI.Models.DTO;
using NZWalkssAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalkssAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        //register action method
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,
            };

            var identituResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identituResult.Succeeded)
            {
                //Add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identituResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identituResult.Succeeded)
                    {
                        return Ok("User was registered successfully. Please proceed to login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        //Login Action Method
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
         {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null) 
            {
               var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if(checkPasswordResult)
                {
                    //Get roles for this user
                        var roles = await userManager.GetRolesAsync(user);
                    
                        if (roles != null)
                    {
                        //Create Token
                            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                            return Ok(jwtToken);
                    }
                }
            }
            return BadRequest("Username or password is incorrect");
        }
    }
}
