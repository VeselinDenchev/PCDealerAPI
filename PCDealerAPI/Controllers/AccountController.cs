namespace PCDealerAPI.Controllers
{
    using Data.Models.Entities;
    using Data.Services.DtoModels.Jwt;
    using Data.Services.JWT.Interfaces;
    using Data.Services.ViewModels;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.JwtService = jwtService;
        }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        public IJwtService JwtService { get; set; }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerModel)
        {
            var checkUser = await UserManager.FindByEmailAsync(registerModel.Email);
            if (checkUser is null)
            {
                User newUser = new User()
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Email = registerModel.Email,
                    UserName = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber,
                    Address = registerModel.Address,
                };

                var token = JwtService.GenerateUserToken(new RequestTokenModel()
                {
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                });

                var result = await UserManager.CreateAsync(newUser, registerModel.Password);

                if (result.Succeeded && token.Length > 0)
                {
                    return Ok(token);
                }
                return BadRequest("Register attempt failed! Please, check email and passowrd!");
            }

            return BadRequest("Invalid register data!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            var checkUser = await UserManager.FindByEmailAsync(loginModel.Email);

            if (checkUser is not null)
            {
                var checkPassword = 
                    await SignInManager.CheckPasswordSignInAsync(checkUser, loginModel.Password, lockoutOnFailure: false);

                var token = JwtService.GenerateUserToken(new RequestTokenModel()
                {
                    Email = checkUser.Email,
                    UserName = checkUser.Email,
                });

                if (checkPassword.Succeeded && token.Length > 0)
                {
                    return Ok(token);
                }

                return BadRequest("Wrong password or token error!");
            }

            return BadRequest("No such user!");
        }
    }
}