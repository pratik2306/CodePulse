using AutoMapper;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(IMapper mapper, UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO request)
        {
            var user = _mapper.Map<IdentityUser>(request);

            //Create User
            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if(identityResult.Succeeded)
            {
                //Add Role for new user
                identityResult = await _userManager.AddToRoleAsync(user, "Reader");
                if(identityResult.Succeeded)
                {
                    return Ok();
                }

            }

            if (identityResult.Errors.Any())
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LogingRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if(user is not null)
            {
                var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var response = new LoginResponseDTO()
                    {
                        Email = request.Email,
                        Token = _tokenRepository.CreateJwtToken(user,roles.ToList()),
                        Roles = roles.ToList()
                    };
                    return Ok(response);
                }

            }

            ModelState.AddModelError("", "Email or Password Incorrect");
            return ValidationProblem(ModelState);
        }
    }
}
