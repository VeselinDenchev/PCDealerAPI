namespace PCDealerAPI.Controllers
{
    using System.Security.Claims;

    using Data.Models.Entities;
    using Data.Services.DtoModels.Jwt;
    using Data.Services.EntityServices.Interfaces;
    using Data.Services.JWT.Interfaces;
    using Data.Services.ViewModels;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService,
                                    IUserService userService)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.JwtService = jwtService;
            this.UserService = userService;
        }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        public IJwtService JwtService { get; set; }

        public IUserService UserService { get; init; }

        [HttpGet]
        [Route("getUserEmails")]
        public async Task<IActionResult> GetUserEmails()
        {
            string[] userEmails = this.UserManager.Users.Select(u => u.Email).ToArray();

            return Ok(userEmails);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel registerModel)
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

                //var token = JwtService.GenerateUserToken(new RequestTokenModel()
                //{
                //    Email = newUser.Email,
                //    UserName = newUser.UserName,
                //});

                var result = await UserManager.CreateAsync(newUser, registerModel.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest("Register attempt failed! Please, check email and password!");
            }

            return BadRequest("Such user already exists!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel)
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
                    return Ok(
                        new 
                        {
                            id = checkUser.Id,
                            firstName=checkUser.FirstName, 
                            lastName=checkUser.LastName,  
                            email = checkUser.Email,
                            mobileNumber = checkUser.PhoneNumber,
                            address = checkUser.Address,
                            accessToken = token 
                        });
                }

                return BadRequest("Wrong password or token error!");
            }

            return BadRequest("No such user!");
        }

        //[HttpPut]
        //[Route("update")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> UpdateAccount([FromForm] UpdateAccountViewModel updateAccountModel)
        //{
        //    string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    User user = this.UserService.GetUserByUserName(userName);

        //    if (user.Email != updateAccountModel.Email)
        //    {
        //        bool emailIsAlreadyRegistered = this.UserManager.Users.Select(u => u.Email).Any(e => e == updateAccountModel.Email);
        //        if (emailIsAlreadyRegistered) return BadRequest("User with this email is already registered");
        //    }

        //    User updatedUser = new User();
        //    updatedUser.Id = user.Id;
        //    updatedUser.FirstName = updateAccountModel.FirstName;
        //    updatedUser.LastName = updateAccountModel.LastName;
        //    updatedUser.Email = updateAccountModel.Email;
        //    updatedUser.UserName = updateAccountModel.Email;
        //    updatedUser.PhoneNumber = updateAccountModel.PhoneNumber;
        //    updatedUser.Address = updateAccountModel.Address;

        //    var result = await this.UserManager.UpdateAsync(updatedUser);

        //    var token = Request.Headers["Authorization"];

        //    if (result.Succeeded)
        //    {
        //        return Ok(
        //            new
        //            {
        //                firstName = updatedUser.FirstName,
        //                lastName = updatedUser.LastName,
        //                email = updatedUser.Email,
        //                mobileNumber = updatedUser.PhoneNumber,
        //                address = updatedUser.Address,
        //                accessToken = token
        //            }
        //        );
        //    }
                

        //    return BadRequest();
        //}

        //[HttpPut]
        //[Route("changePassword")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> ChangePassword([FromForm] string newPassowrd)
        //{
        //    string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    User user = this.UserService.GetUserByUserName(userName);

        //    if (user.Email != updateAccountModel.Email)
        //    {
        //        bool emailIsAlreadyRegistered = this.UserManager.Users.Select(u => u.Email).Any(e => e == updateAccountModel.Email);
        //        if (emailIsAlreadyRegistered) return BadRequest("User with this email is already registered");
        //    }

        //    User updatedUser = new User();
        //    updatedUser.Id = user.Id;
        //    updatedUser.FirstName = updateAccountModel.FirstName;
        //    updatedUser.LastName = updateAccountModel.LastName;
        //    updatedUser.Email = updateAccountModel.Email;
        //    updatedUser.UserName = updateAccountModel.Email;
        //    updatedUser.PhoneNumber = updateAccountModel.PhoneNumber;
        //    updatedUser.Address = updateAccountModel.Address;

        //    var result = await this.UserManager.UpdateAsync(updatedUser);

        //    var token = Request.Headers["Authorization"];

        //    if (result.Succeeded)
        //    {
        //        return Ok(
        //            new
        //            {
        //                firstName = updatedUser.FirstName,
        //                lastName = updatedUser.LastName,
        //                email = updatedUser.Email,
        //                mobileNumber = updatedUser.PhoneNumber,
        //                address = updatedUser.Address,
        //                accessToken = token
        //            }
        //        );
        //    }


        //    return BadRequest();
        //}
    }
}